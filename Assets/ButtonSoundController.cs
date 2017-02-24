using UnityEngine;
using System.Collections;

public class ButtonSoundController : MonoBehaviour {

	public void PlaySound() {
		if (PlayerPrefs.GetInt ("Sound", 1) == 1) {
			GetComponent<AudioSource> ().Play ();
		}
	}
}
