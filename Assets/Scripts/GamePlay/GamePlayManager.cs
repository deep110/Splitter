using UnityEngine;

public class GamePlayManager : MonoBehaviour {

	public int Score{get {return (int)score;}}
	private float score = 0;

	public bool gameOver = false;

	void Start () {
		Events.GameOverEvent += GameOver;
	}
	
	private void GameOver(){
		gameOver = true;
	}

	void OnDisable(){
		Events.GameOverEvent -= GameOver;
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.tag.Equals("SpikeBoth")){
			score += 0.5f;
		}else{
			score++;
		}
	}

}
