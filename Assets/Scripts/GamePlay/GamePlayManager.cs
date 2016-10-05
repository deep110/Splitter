using UnityEngine;

public class GamePlayManager : MonoBehaviour {

	public int Score{get; private set;}

	void Start () {
		Events.GameOverEvent += GameOver;
		Score = 0;
	}
	
	private void GameOver(){
		//Debug.Log("over in manager");
		// show gameover dialog
	}

	void OnDisable(){
		Events.GameOverEvent -= GameOver;
	}

	void OnTriggerExit2D(Collider2D other){
		Score++;
	}

}
