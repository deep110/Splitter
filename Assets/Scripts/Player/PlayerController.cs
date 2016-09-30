/**
* This script contains functions for movement of player and taking appropriate actions on collison detection.
* Will also generate player according to the theme, sets the glow etc.
*/
using UnityEngine;

namespace Splitter.Player{

	public class PlayerController : MonoBehaviour{

		public readonly float SPEED = 22;
		private Pair<Transform> player;
		private Pair<Rigidbody2D> rigidBody;
		private Pair<Vector3> initialPos;

		private Pair<float> velocity;
		private Pair<bool> posReset;
		

		void Start(){
			player     = new Pair<Transform>(transform.Find("Left"), transform.Find("Right"));
			initialPos = new Pair<Vector3>(player.Left.position, player.Right.position);
			rigidBody  = new Pair<Rigidbody2D>(player.Left.GetComponent<Rigidbody2D>(), player.Right.GetComponent<Rigidbody2D>());
			velocity   = new Pair<float>(0,0);
			posReset   = new Pair<bool>(false,false);
		}

		public void Move(InputManager.InputType input){

			switch(input){
				case InputManager.InputType.LEFT:
					velocity.Left = -1* getVelocity(player.Left.position.x - initialPos.Left.x);
					velocity.Right = -1*getVelocity(player.Right.position.x);
					posReset.Set(false, false);

					break;

				case InputManager.InputType.RIGHT:
					velocity.Left = getVelocity(player.Left.position.x - initialPos.Left.x);
					velocity.Right = getVelocity(player.Right.position.x);
					posReset.Set(false, false);

					break;

				case InputManager.InputType.BOTH:
					velocity.Left = -1* getVelocity(player.Left.position.x - initialPos.Left.x);
					velocity.Right = getVelocity(player.Right.position.x);
					posReset.Set(false, false);

					break;

				case InputManager.InputType.NONE:
					if(!posReset.Left){
						velocity.Left = getReturnVelocity(initialPos.Left.x, player.Left.position.x);

						if((int)velocity.Left == -10) {
							player.Left.position = initialPos.Left;
							velocity.Left = 0;
							posReset.Left = true;
						}
					}

					if(!posReset.Right){
						velocity.Right = getReturnVelocity(initialPos.Right.x, player.Right.position.x);

						if((int)velocity.Right == -10){
							player.Right.position = initialPos.Right;
							velocity.Right = 0;
							posReset.Right = true;
						}
					}
						
					break;
			}
			
			rigidBody.Left.velocity = new Vector2(velocity.Left, 0) * SPEED;
			rigidBody.Right.velocity = new Vector2(velocity.Right, 0) * SPEED;
		}

		/*
		* x = mod of current Position
		*/
		private float getVelocity(float x){
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
				v = getVelocity(x2-x1);
			}else if(x2 - x1 > 0){
				v = -1* getVelocity(x2-x1);
			}else v = 0;

			if(Mathf.Abs(x2-x1) <= (SPEED/50)){
				v = -10;
			}

			return v;
		}

	}
}