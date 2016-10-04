/**
* This script contains functions for movement of player and taking appropriate actions on collison detection.
* Will also generate player according to the theme, sets the glow etc.
*/
using UnityEngine;

public class PlayerController : MonoBehaviour{

	private readonly float SPEED = 22;
	private Pair<Transform> player;
	private Pair<Rigidbody2D> rigidBody;
	private Pair<Vector3> initialPos;

	private Pair<float> velocity;
	private Pair<bool> posReset;
	public GameObject explosionPrefab;
	

	void Start(){
		player     = new Pair<Transform>(transform.Find("Left"), transform.Find("Right"));
		initialPos = new Pair<Vector3>(player.Left.position, player.Right.position);
		rigidBody  = new Pair<Rigidbody2D>(player.Left.GetComponent<Rigidbody2D>(), player.Right.GetComponent<Rigidbody2D>());
		velocity   = new Pair<float>(0,0);
		posReset   = new Pair<bool>(false,false);
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
				velocity.Left = -GetVelocity(player.Left.position.x);
				velocity.Right = GetVelocity(player.Right.position.x);

				break;

			case InputManager.InputType.None:
				if((int)velocity.Left == 0){
					player.Left.position = initialPos.Left;
				}

				if((int)velocity.Right == 0){
					player.Right.position = initialPos.Right;
				}
					
				break;
		}
		
		rigidBody.Left.velocity = new Vector2(velocity.Left, 0) * SPEED;
		rigidBody.Right.velocity = new Vector2(velocity.Right, 0) * SPEED;
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


	public void Dead(int index){
        Events.CallGameOver();

        if(index == -1){
        	Destroy(player.Left.gameObject);
        	Instantiate(explosionPrefab, player.Left.position, Quaternion.identity);
        	player.Left = null;
        	if(player.Right!= null){
        		rigidBody.Right.velocity = Vector2.zero;
        	}
        }else{
        	Destroy(player.Right.gameObject);
        	Instantiate(explosionPrefab, player.Right.position, Quaternion.identity);
        	player.Right = null;
        	if(player.Left!= null){
        		rigidBody.Left.velocity = Vector2.zero;
        	}
        }
	}

}
