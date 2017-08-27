using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuButtonController : MonoBehaviour {

	public GameObject settingsPanel;
	public GameObject mainPanel;
	public GameObject settingsButton;
	public GameObject aboutMDG;

	public Animator whiteFadeIn;
	public AudioClip click;
	public AudioSource source;

	void Update() {
		//close the application when back button is pressed in android
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit ();
		}
	}

	public void OnSettingsClicked() {
		settingsPanel.SetActive (true);
		mainPanel.SetActive (false);
	}

	public void OnClickPlay() {
		whiteFadeIn.SetTrigger ("GameOver");
		if(PlayerPrefs.GetInt("Sound", 1) == 1)
			source.PlayOneShot (click);
	}

	public void OnClickBack() {
		if (settingsPanel.activeSelf) {
			settingsPanel.SetActive (false);
			mainPanel.SetActive (true);
		} else {
			aboutMDG.SetActive (false);
			mainPanel.SetActive (true);
		}
	}

	public void OnClickInfo() {
		aboutMDG.SetActive (true);
		mainPanel.SetActive (false);
	}

	public void OnClickFB() {
		Application.OpenURL ("https://www.facebook.com/mdgiitr/");
	}

	public void OnClickGithub() {
		Application.OpenURL ("https://github.com/sdsmdg");
	}

	public void OnClickPlayStore() {
		Application.OpenURL ("https://play.google.com/store/apps/developer?id=MDG,+SDS");
	}
}
