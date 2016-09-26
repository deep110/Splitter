/**
* This script contains functions for movement of player and taking appropriate actions on collison detection.
* Will also generate player according to the theme, sets the glow etc.
*/
using UnityEngine;

namespace Splitter.Player{

	public class PlayerController : MonoBehaviour{

		public readonly float SPEED = 8;
		private Pair<Transform> player;
		private Pair<Rigidbody2D> rigidbody;
		private Pair<Vector2> velocity;

		void Start(){
			//Debug.Log(transform.FindChild("Left"));
			player = new Pair<Transform>(transform.Find("Left"), transform.Find("Right"));
			rigidbody = new Pair<Rigidbody2D>(player.Left.GetComponent<Rigidbody2D>(), player.Right.GetComponent<Rigidbody2D>());
			velocity = new Pair<Vector2>(new Vector2(0,0), new Vector2(0,0));
		}

		public void Move(InputManager.InputType input){
			
			switch(input){
				case InputManager.InputType.LEFT:
					velocity.Left = new Vector2(-1, 0);
					velocity.Right = new Vector2(-1, 0);
					break;

				case InputManager.InputType.RIGHT:
					velocity.Left = new Vector2(1, 0);
					velocity.Right = new Vector2(1, 0);
					break;

				case InputManager.InputType.BOTH:
					velocity.Left = new Vector2(-1, 0);
					velocity.Right = new Vector2(1, 0);
					break;

				case InputManager.InputType.NONE:

					break;
			}
			
			rigidbody.Left.velocity = velocity.Left * SPEED;
			rigidbody.Right.velocity = velocity.Right * SPEED;
		}

	}
}