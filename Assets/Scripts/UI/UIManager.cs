using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

	public GamePlayManager gamePlayManager;

	private Text scoreText;
	private int oldScore, newScore;
	private bool isDialogShown = false;

	void Start () {
		scoreText = transform.GetChild(0).GetComponent<Text>();
		oldScore = 0;
	}
	
	// Update is called once per frame
	void Update () {
		newScore = gamePlayManager.Score;
		if(oldScore!= newScore){
			scoreText.text = newScore.ToString();
			oldScore = newScore;
		}

		if(gamePlayManager.gameOver && !isDialogShown){
			// hide current scoreboard
			scoreText.gameObject.SetActive(false);

			//activate gameOver Dialog
			GameObject gameOverDialog = transform.GetChild(1).gameObject;
			gameOverDialog.SetActive(true);
			gameOverDialog.GetComponentInChildren<Text>().text = string.Format("Score: {0}", gamePlayManager.Score);
			isDialogShown = true;
		}
	}

	public void OnClickRestart(){
		 SceneManager.LoadScene(0);
	}
}
