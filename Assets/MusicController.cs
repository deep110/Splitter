using UnityEngine;
using System.Collections;

public class MusicController : MonoBehaviour {

	public void OnValueChanged(bool newValue) {
		if (newValue) {
			PlayerPrefs.SetInt ("Music", 1);
		} else {
			PlayerPrefs.SetInt ("Music", 0);
		}
	}

}
