/**
* This script contains functions for movement of player and taking appropriate actions on collison detection.
* Will also generate player according to the theme, sets the glow etc.
*/
using UnityEngine;

public class PlayerController : MonoBehaviour{

	public GameObject explosionPrefab;

	private readonly float SPEED = 20;
	private Pair<Transform> player;
	private Pair<Rigidbody2D> rigidBody;
	private Pair<Vector3> initialPos;
	private Pair<float> velocity;
	private Pair<bool> posReset;

	private Transform connector;
	private Pair<float> scale;
	
	void Start(){
		player     = new Pair<Transform>(transform.GetChild(0), transform.GetChild(1));
		initialPos = new Pair<Vector3>(player.Left.position, player.Right.position);
		rigidBody  = new Pair<Rigidbody2D>(player.Left.GetComponent<Rigidbody2D>(), player.Right.GetComponent<Rigidbody2D>());
		velocity   = new Pair<float>(0,0);
		posReset   = new Pair<bool>(false,false);
		connector  = transform.GetChild(2);
		scale      = new Pair<float>(0,1);
	}

	public void Move(InputManager.InputType input){

		switch(input){
			case InputManager.InputType.Left:
				velocity.Left = -1;
				velocity.Right = -1;

				break;

			case InputManager.InputType.Right:
				velocity.Left = 1;
				velocity.Right = 1;

				break;

			case InputManager.InputType.Both:
				velocity.Left = -1;
				velocity.Right = 1;

				break;

			case InputManager.InputType.None:
				velocity.Left = GetReturnVelocity(initialPos.Left.x, player.Left.position.x);
				velocity.Right = GetReturnVelocity(initialPos.Right.x, player.Right.position.x);

				if((int)velocity.Left == 0){
					player.Left.position = initialPos.Left;
					posReset.Left = true;
				}

				if((int)velocity.Right == 0){
					player.Right.position = initialPos.Right;
					posReset.Right = true;
				}
					
				break;
		}
		
		rigidBody.Left.velocity = Vector2.right * (velocity.Left* SPEED);
		rigidBody.Right.velocity = Vector2.right * (velocity.Right* SPEED);

		AdjustConnector();
	}

	/*
	* x = mod of current Position
	*/
	private float GetVelocity(float x){
		x = Mathf.Abs(x);
		return Mathf.Pow(x+1, 4) * (-0.002233f) + 1f;
	}

	/*
	* x1 = initial Position
	* x2 = current Position
	*/
	private float GetReturnVelocity(float x1, float x2){
		float v;
		if(x2-x1 <0){
			v = 1;
		}else if(x2 - x1 > 0){
			v = -1;
		}else v = 0;

		if(Mathf.Abs(x2-x1) <= (SPEED/50)){
			v = 0;
		}

		return v;
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
