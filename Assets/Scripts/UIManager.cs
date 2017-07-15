using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public GameManager gameManager;

	private Text scoreText;
	private int oldScore, newScore;
	private bool isDialogShown;

	void Start () {
		scoreText = transform.GetChild(0).GetComponent<Text>();
		oldScore = 0;
	}
	
	// Update is called once per frame
	void Update () {
		newScore = gameManager.Score;
		if(oldScore!= newScore){
			scoreText.text = newScore.ToString();
			oldScore = newScore;
		}

		if(gameManager.gameOver && !isDialogShown){
			// hide current scoreboard
			scoreText.gameObject.SetActive(false);

			//activate gameOver Dialog
			GameObject gameOverDialog = transform.GetChild(1).gameObject;
			gameOverDialog.SetActive(true);
			gameOverDialog.GetComponentInChildren<Text>().text = string.Format("{0}", gameManager.Score);
			isDialogShown = true;
		}
	}

}
