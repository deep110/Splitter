/*
* Act as top level manager for player. Gets Data from various other Managers and uses to build and control player in the game.
* Like takes input from InputManager and passes to PlayerController, etc.
*/

using UnityEngine;
//using System.Collections;

namespace Splitter.Player{

	[RequireComponent(typeof (InputManager))]
	public class PlayerManager : MonoBehaviour {

		private InputManager input;
		private PlayerController mPlayer;

		void Start () {
		  input = GetComponent<InputManager>();
		  mPlayer = GetComponent<PlayerController>();
		}
		
		void Update () {
			mPlayer.Move(input.MappedInput);
		}
		
	}

}

