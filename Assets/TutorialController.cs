using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour {

	public GameObject gamePlayManager;
	public GameObject scoreCounter;

	private static bool isRunning;
	private GameObject tutorialText;
	private enum Level {
		left, right, both, none
	};
	private InputManager input;
	private Level currentLevel;
	private float touchTime = 0;//Time for which screen is pressed currently in a state
	private float holdTime = 1f;//Time for which screen is to be pressed to move to next state

	void OnEnable () {
		isRunning = true;
		currentLevel = Level.left;
		tutorialText = gameObject;
		input = gamePlayManager.GetComponent<InputManager> ();

		if (PlayerPrefs.GetInt ("tutorial", 1) == 0) {
			isRunning = false;
		}
	}
	
	void Update () {
		if (isRunning) {
			if (currentLevel == Level.left) {
				scoreCounter.SetActive (false);
				tutorialText.GetComponent<Text> ().text = "Tap and hold left side of screen to move left";
				if (input.MappedInput == InputManager.InputType.Left) {
					touchTime += Time.deltaTime;
					if (touchTime > holdTime) {
						currentLevel = Level.right;
						touchTime = 0;
					}
				} else {
					touchTime = 0;
				}
			} else if (currentLevel == Level.right) {
				tutorialText.GetComponent<Text> ().text = "Tap and hold right side of screen to move right";
				if (input.MappedInput == InputManager.InputType.Right) {
					touchTime += Time.deltaTime;
					if (touchTime > holdTime) {
						currentLevel = Level.both;
						touchTime = 0;
					}
				} else {
					touchTime = 0;
				}
			} else if (currentLevel == Level.both) {
				tutorialText.GetComponent<Text> ().text = "Tap and hold both sides of screen to split";
				if (input.MappedInput == InputManager.InputType.Both) {
					touchTime += Time.deltaTime;
					if (touchTime > holdTime) {
						currentLevel = Level.none;
						touchTime = 0;
					}
				} else {
					touchTime = 0;
				}
			} else if (currentLevel == Level.none && isRunning == true) {
				tutorialText.GetComponent<Text> ().text = "";
				isRunning = false;
				scoreCounter.SetActive (true);
				PlayerPrefs.SetInt ("tutorial", 0);
			}
		}
	}

	public static bool isTutorialRunning () {
		return isRunning;
	}
}
