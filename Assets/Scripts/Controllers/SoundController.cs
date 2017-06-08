using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour {

	public void OnValueChanged (bool newValue) {
		if (newValue) {
			PlayerPrefs.SetInt ("Sound", 1);
		} else {
			PlayerPrefs.SetInt ("Sound", 0);
		}
	}
}
