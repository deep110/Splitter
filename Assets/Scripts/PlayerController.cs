/**
* This script contains functions for movement of player and taking appropriate actions on collison detection.
* Will also generate player according to the theme, sets the glow etc.
*/
using UnityEngine;

namespace Splitter.Player{

	public class PlayerController : MonoBehaviour{

		public readonly float SPEED = 20;
		private Pair<Transform> player;
		private Pair<Rigidbody2D> rigidBody;
		private Pair<Vector3> initialPos;

		private Pair<float> velocity;
		

		void Start(){
			player = new Pair<Transform>(transform.Find("Left"), transform.Find("Right"));
			initialPos = new Pair<Vector3>(player.Left.position, player.Right.position);
			rigidBody = new Pair<Rigidbody2D>(player.Left.GetComponent<Rigidbody2D>(), player.Right.GetComponent<Rigidbody2D>());
			velocity = new Pair<float>(0,0);
		}

		public void Move(InputManager.InputType input){
			//Debug.Log(input);

			switch(input){
				case InputManager.InputType.LEFT:
					velocity.Left = -1;
					velocity.Right = -1;
					break;

				case InputManager.InputType.RIGHT:
					velocity.Left = 1;
					velocity.Right = 1;
					break;

				case InputManager.InputType.BOTH:
					velocity.Left = -1;
					velocity.Right = 1;
					break;

				case InputManager.InputType.NONE:
					velocity.Left = getReturnVelocity(initialPos.Left.x, player.Left.position.x);
					velocity.Right = getReturnVelocity(initialPos.Right.x, player.Right.position.x);

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

		/**
		* x1 = initial Position
		* x2 = current Position
		**/
		private float getReturnVelocity(float x1, float x2){
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

	}
}