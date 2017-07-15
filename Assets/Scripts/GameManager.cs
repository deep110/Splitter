using UnityEngine;

public class GameManager : MonoBehaviour {

	public int Score{get {return (int)score;}}
	public bool gameStart = true;

	private float score;
	public bool gameOver;

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
		if(other.tag.Equals("SpikeBoth")) {
			score += 0.5f;
		} else if(other.tag.Equals("SpikeOther")){
			score++;
		}
		if (score > PlayerPrefs.GetInt ("highscore")) {
			PlayerPrefs.SetInt ("highscore", (int)score);
		}
	}

}
