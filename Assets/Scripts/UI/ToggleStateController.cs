using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleStateController : MonoBehaviour {

	public Toggle music;
	public Toggle sound;
	public Toggle tutorial;

	void Start() {
		if (PlayerPrefs.GetInt ("Sound", 1) == 1) {
			sound.isOn = true;
		} else {
			sound.isOn = false;
		}
		if (PlayerPrefs.GetInt ("Music", 1) == 1) {
			music.isOn = true;
		} else {
			music.isOn = false;
		}
		if (PlayerPrefs.GetInt ("Tutorial", 1) == 1) {
			tutorial.isOn = true;
		} else {
			tutorial.isOn = false;
		}
	}

	public void OnToggleMusic() {
		if (music.isOn) {
			PlayerPrefs.SetInt ("Music", 1);
		} else {
			PlayerPrefs.SetInt ("Music", 0);
		}
	}

	public void OnToggleSound() {
		if (sound.isOn) {
			PlayerPrefs.SetInt ("Sound", 1);
		} else {
			PlayerPrefs.SetInt ("Sound", 0);
		}
	}

	public void OnToggleTutorial() {
		if (tutorial.isOn) {
			PlayerPrefs.SetInt ("Tutorial", 1);
		} else {
			PlayerPrefs.SetInt ("Tutorial", 0);
		}
	}

}
