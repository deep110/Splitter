/**
* This script contains functions for movement of player and taking appropriate actions on collison detection.
* Will also generate player according to the theme, sets the glow etc.
*/
using UnityEngine;

public class PlayerController : MonoBehaviour{

	public GameObject explosionPrefab;

	private float Speed = 0;
	private float Speed1 = 0;
	private Pair<Transform> player;
	private Pair<Rigidbody2D> rigidBody;
	private Pair<Vector3> initialPos;
	private Pair<float> velocity;
	private Pair<bool> posReset;

	private Transform connector;
	private Pair<float> scale;

	private Pair<float> currentX;
	private Pair<float> targetX;
	
	void Start(){
		player     = new Pair<Transform>(transform.GetChild(0), transform.GetChild(1));
		initialPos = new Pair<Vector3>(player.Left.position, player.Right.position);
		rigidBody  = new Pair<Rigidbody2D>(player.Left.GetComponent<Rigidbody2D>(), player.Right.GetComponent<Rigidbody2D>());
		velocity   = new Pair<float>(0,0);
		posReset   = new Pair<bool>(false,false);
		connector  = transform.GetChild(2);
		scale      = new Pair<float>(0,1);
		targetX = new Pair<float> (0, 0);
		currentX = new Pair<float> (0, 0);
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

		currentX.Left = Mathf.SmoothDamp (currentX.Left, targetX.Left, ref Speed, 0.1f);
		currentX.Right = Mathf.SmoothDamp (currentX.Right, targetX.Right, ref Speed1, 0.1f);

	}

	void FixedUpdate() {

		if (player.Left != null) {
			player.Left.position = new Vector3 (currentX.Left, player.Left.position.y);
		} 
		if (player.Right != null) {
			player.Right.position = new Vector3 (currentX.Right, player.Right.position.y);
		}

	}

	private void AdjustConnector(){
		float distance = player.Right.position.x - player.Left.position.x;
		scale.Left = 1.13f * (distance - 0.9f);
		scale.Right = 1f - (0.081f * distance);
		connector.localScale = new Vector2(scale.Left, scale.Right);
		connector.position = new Vector2((player.Right.position.x + player.Left.position.x)/2, initialPos.Left.y);
	}

	public void Dead(int index){
        Events.CallGameOver();
        Destroy(player.Left.gameObject);
		Destroy (player.Right.gameObject);
        Instantiate(explosionPrefab, player.Left.position, Quaternion.identity); 

        if(connector!=null){
        	Destroy(connector.gameObject);
        	connector = null;
        }
	}

}
