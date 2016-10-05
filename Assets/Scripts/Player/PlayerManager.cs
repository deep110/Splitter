/*
 * Act as top level manager for player. Gets Data from various other Managers and uses to build and control player in the game.
 * Like takes input from InputManager and passes to PlayerController, etc.
 */

using UnityEngine;

[RequireComponent(typeof (InputManager))]
public class PlayerManager : MonoBehaviour {

	private InputManager input;
	private bool gameOver = false;

	public PlayerController player;

	void Start () {
	  input = GetComponent<InputManager>();
	  Events.GameOverEvent += PlayerHit;
	}
	
	void Update () {
		if(!gameOver && player!=null){
			player.Move(input.MappedInput);
		}
	}

	void OnDisable(){
		Events.GameOverEvent -= PlayerHit;
	}

	private void PlayerHit(){
		gameOver = true;
	}

	
}

