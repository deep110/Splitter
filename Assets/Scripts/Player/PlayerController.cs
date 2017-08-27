/**
* This script contains functions for movement of playerTransform and taking appropriate actions on collison detection.
* Will also generate playerTransform according to the theme, sets the glow etc.
*/
using UnityEngine;

public class PlayerController : MonoBehaviour{

	public GameObject explosionPrefab;
	public AudioClip explosionClip;
	public AudioClip swoosh;

	private Animator animator;
	//x component of speed of left part of rocket
	private float SpeedXL = 0;
	private float SpeedXR = 0;
	private Pair<Transform> playerTransform;

	private Transform connector;
	private Pair<float> scale;
	private bool isGameOver = false;
	private Vector2 leftUp = new Vector2 (), rightUp = new Vector2 ();//indicates the direction in which the two parts point
	private Vector3 posLeft = new Vector3(), posRight = new Vector3();//temp pos of rocket left and right

	private Pair<float> prevX;
	private Pair<float> currentX;
	private Pair<float> targetX;
	private float timeToMove;
	private AudioSource source;
	
	void Start(){
		playerTransform = new Pair<Transform>(transform.GetChild(0), transform.GetChild(1));
		connector = transform.GetChild(2);
		scale = new Pair<float>(0,1);
		prevX = new Pair<float> (-0.6f, 0.6f);
		targetX = new Pair<float> (-0.6f, 0.6f);
		currentX = new Pair<float> (-0.6f, 0.6f);
		timeToMove = 0.1f * SpeedController.Speed / 6.5f;
		source = GetComponent<AudioSource> ();
		animator = GetComponent<Animator> ();
	}

	public void Move(InputManager.InputType input){

		switch(input){
		case InputManager.InputType.Left:

			targetX.Left = -4.1f;
			targetX.Right = -2.9f;

			break;

		case InputManager.InputType.Right:

			targetX.Left = 2.9f;
			targetX.Right = 4.1f;

			break;

		case InputManager.InputType.Both:

			targetX.Left = -4.1f;
			targetX.Right = 4.1f;

			break;

		case InputManager.InputType.None:

			targetX.Left = -0.6f;
			targetX.Right = 0.6f;
					
			break;
		}

		AdjustConnector();

		currentX.Left = Mathf.SmoothDamp (currentX.Left, targetX.Left, ref SpeedXL, timeToMove);
		currentX.Right = Mathf.SmoothDamp (currentX.Right, targetX.Right, ref SpeedXR, timeToMove);

	}

	void FixedUpdate() {
		if (!isGameOver) {
			leftUp.x = currentX.Left - prevX.Left;
			leftUp.y = 5;

			rightUp.x = currentX.Right - prevX.Right;
			rightUp.y = 5;

			playerTransform.Left.up = leftUp;
			playerTransform.Right.up = rightUp;

			if (playerTransform.Left != null) {
				posLeft.x = currentX.Left;
				posLeft.y = playerTransform.Left.position.y;
				playerTransform.Left.position = posLeft;
			}
			if (playerTransform.Right != null) {
				posRight.x = currentX.Right;
				posRight.y = playerTransform.Right.position.y;
				playerTransform.Right.position = posRight;
			}

			prevX.Left = currentX.Left;
			prevX.Right = currentX.Right;
		}
	}

	private void AdjustConnector(){
		float distance = playerTransform.Right.position.x - playerTransform.Left.position.x;
		scale.Left = 1.1f * (distance - 1.1f);
		scale.Right = 1f - (0.081f * distance);
		connector.localScale = new Vector2(scale.Left, scale.Right);
		connector.position = new Vector2((playerTransform.Right.position.x + playerTransform.Left.position.x)/2, playerTransform.Left.position.y);
	}

	public void Dead(){
		if (!isGameOver) {
			Events.CallGameOver ();
			Destroy (playerTransform.Left.gameObject);
			isGameOver = true;
			Destroy (playerTransform.Right.gameObject);
			Instantiate (explosionPrefab, playerTransform.Left.position, Quaternion.identity); 

			if (PlayerPrefs.GetInt ("Sound", 1) == 1) {
				source.PlayOneShot (explosionClip, 1f);
			}

			if (connector != null) {
				Destroy (connector.gameObject);
				connector = null;
			}
		}
	}

	public void PlayFastForwardAnim() {
		animator.SetTrigger ("FastForward");
		source.PlayOneShot (swoosh);
	}

}
