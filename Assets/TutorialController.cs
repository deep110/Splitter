using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour {

	private static bool isRunning;
	private GameObject tutorialText;

	private enum Level {
		left, right, both, none
	};

	public GameObject gamePlayManager;
	private InputManager input;
	private Level currentLevel;
	private float touchTime = 0;

	void OnEnable () {
		isRunning = true;
		currentLevel = Level.left;
		tutorialText = gameObject;
		input = gamePlayManager.GetComponent<InputManager> ();
	}
	
	void Update () {
		if (currentLevel == Level.left) {
			tutorialText.GetComponent<Text> ().text = "Tap and hold left side of screen to move left";
			if (input.MappedInput == InputManager.InputType.Left) {
				touchTime += Time.deltaTime;
				if (touchTime > 0.5f) {
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
				if (touchTime > 0.5f) {
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
				if (touchTime > 0.5f) {
					currentLevel = Level.none;
					touchTime = 0;
				}
			} else {
				touchTime = 0;
			}
		} else if (currentLevel == Level.none && isRunning == true) {
			tutorialText.GetComponent<Text> ().text = "";
			isRunning = false;
		}

	}

	public static bool isTutorialRunning () {
		return isRunning;
	}
}
