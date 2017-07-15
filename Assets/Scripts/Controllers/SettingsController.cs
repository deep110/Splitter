using UnityEngine;

public class SettingsController : MonoBehaviour {

	public void OnMusicToggled(bool newValue) {
		if (newValue) {
			PlayerPrefs.SetInt ("Music", 1);
		} else {
			PlayerPrefs.SetInt ("Music", 0);
		}
	}

	public void OnTutorialToggled (bool newValue) {
		if (newValue) {
			PlayerPrefs.SetInt ("tutorial", 1);
		} else {
			PlayerPrefs.SetInt ("tutorial", 0);
		}
	}

	public void OnSoundToggled (bool newValue) {
		if (newValue) {
			PlayerPrefs.SetInt ("Sound", 1);
		} else {
			PlayerPrefs.SetInt ("Sound", 0);
		}
	}
}
