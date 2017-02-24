/**
* This script contains functions for movement of playerTransform and taking appropriate actions on collison detection.
* Will also generate playerTransform according to the theme, sets the glow etc.
*/
using UnityEngine;

public class PlayerController : MonoBehaviour{

	public GameObject explosionPrefab;
	public AudioClip explosionClip;

	private float Speed = 0;
	private float Speed1 = 0;
	private Pair<Transform> playerTransform;

	private Transform connector;
	private Pair<float> scale;

	private Pair<float> currentX;
	private Pair<float> targetX;
	private float timeToMove;
	private AudioSource source;
	
	void Start(){
		playerTransform     = new Pair<Transform>(transform.GetChild(0), transform.GetChild(1));
		connector  = transform.GetChild(2);
		scale      = new Pair<float>(0,1);
		targetX = new Pair<float> (0, 0);
		currentX = new Pair<float> (0, 0);
		timeToMove = 0.1f * SpeedController.Speed / 6.5f;
		source = GetComponent<AudioSource> ();
	}

	public void Move(InputManager.InputType input){

		switch(input){
		case InputManager.InputType.Left:

			targetX.Left = -3.2f;
			targetX.Right = -2.18f;

			break;

		case InputManager.InputType.Right:

			targetX.Left = 2.18f;
			targetX.Right = 3.2f;

			break;

		case InputManager.InputType.Both:

			targetX.Left = -3.2f;
			targetX.Right = 3.2f;

			break;

		case InputManager.InputType.None:

			targetX.Left = -0.48f;
			targetX.Right = 0.48f;
					
			break;
		}

		AdjustConnector();

		currentX.Left = Mathf.SmoothDamp (currentX.Left, targetX.Left, ref Speed, timeToMove);
		currentX.Right = Mathf.SmoothDamp (currentX.Right, targetX.Right, ref Speed1, timeToMove);

	}

	void FixedUpdate() {

		if (playerTransform.Left != null) {
			playerTransform.Left.position = new Vector3 (currentX.Left, playerTransform.Left.position.y);
		} 
		if (playerTransform.Right != null) {
			playerTransform.Right.position = new Vector3 (currentX.Right, playerTransform.Right.position.y);
		}

	}

	private void AdjustConnector(){
		float distance = playerTransform.Right.position.x - playerTransform.Left.position.x;
		scale.Left = 1.13f * (distance - 0.9f);
		scale.Right = 1f - (0.081f * distance);
		connector.localScale = new Vector2(scale.Left, scale.Right);
		connector.position = new Vector2((playerTransform.Right.position.x + playerTransform.Left.position.x)/2, playerTransform.Left.position.y);
	}

	public void Dead(int index){
        Events.CallGameOver();
        Destroy(playerTransform.Left.gameObject);
		Destroy (playerTransform.Right.gameObject);
        Instantiate(explosionPrefab, playerTransform.Left.position, Quaternion.identity); 

		if (PlayerPrefs.GetInt ("Sound", 1) == 1) {
			source.PlayOneShot (explosionClip, 1f);
		}

        if(connector!=null){
        	Destroy(connector.gameObject);
        	connector = null;
        }
	}

}
