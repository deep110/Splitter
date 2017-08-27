using UnityEngine;
using UnityEngine.UI;

public class GamePlayManager : MonoBehaviour {

	public int Score{get {return (int)score;}}

	public PlayerController playerControler;
	public Text scoreText;//score text ui while gameplay
	public Text highscoreText;
	public GameObject gameOverDialog;
	public Text gameOverScore;
	public bool gameStart = true;
	public bool gameOver = false;
	public Animator cameraShake;
	public AudioClip background; 
	public AudioSource source;
	public AudioClip splash;
	public Text coinsText;
	public GameObject PowerUp;
	public ObstacleGenerator generator;
	public ParticleSystem ffparticle;//particle system for fastforward

	private float score = 0;
	private int coins;

	void Start () {
		if (PlayerPrefs.GetInt ("Music", 1) == 1) {
			source.loop = true;
			source.clip = background;
			source.Play ();
		}
		if (PlayerPrefs.GetInt ("Sound", 1) == 1) {
			source.PlayOneShot (splash, 0.5f);
		}
		Events.GameOverEvent += GameOver;
		coins = PlayerPrefs.GetInt ("coins", 0);
		coinsText.text = coins.ToString ();
		if (coins < 10 && PlayerPrefs.GetInt("highscore", 0) >= 40) {
			PowerUp.SetActive (false);
		}
	}
	
	private void GameOver(){
		gameOver = true;
		cameraShake.SetTrigger ("GameOver");
		scoreText.gameObject.SetActive (false);
		if (score > PlayerPrefs.GetInt ("highscore")) {
			PlayerPrefs.SetInt ("highscore", (int)score);
		}
		highscoreText.text = PlayerPrefs.GetInt ("highscore") + "";

		gameOverDialog.SetActive (true);
		gameOverScore.text = score + "";
		coins += (int)(score / 3);
		PlayerPrefs.SetInt ("coins", coins);
		coinsText.text = coins.ToString();
	}

	void OnDisable(){
		Events.GameOverEvent -= GameOver;
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.tag.Equals ("SpikeBoth")) {
			IncreaseScore (0.5f);
		} else if (other.tag.Equals ("SpikeLeft") || other.tag.Equals ("SpikeRight") || other.tag.Equals("SpikeCenter")) {
			IncreaseScore (1);
		}
		//if score is an int show it
		if ((int)score == score) {
			scoreText.text = score.ToString();
		}
	}

	void IncreaseScore(float step) {
		score += step;
		SpeedController.IncreaseSpeed (step);
	}

	public void OnClickPowerUp() {
		if (UseCoins ()) {
			IncreaseScore (10);
			scoreText.text = score.ToString ();
			generator.ResetTimer ();
			ffparticle.Play();
			playerControler.PlayFastForwardAnim ();
		}
	}

	private bool UseCoins() {
		if (coins >= 10) {
			coins -= 10;
			coinsText.text = coins.ToString ();
			if (score + 10 > PlayerPrefs.GetInt ("highscore", 0) - 40 ||
				(coins < 10)) {
				PowerUp.SetActive (false);
			}
			return true;
		}
		return false;
	}

	public void DisablePowerUp() {
		PowerUp.SetActive (false);
		ffparticle.Stop ();
	}

}
