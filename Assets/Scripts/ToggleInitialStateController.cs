using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ToggleInitialStateController : MonoBehaviour {

	public bool isMusic;

	private int value;

	// Use this for initialization
	void Start () {
		if (isMusic) {
			value = PlayerPrefs.GetInt ("Music", 1);
		} else {
			value = PlayerPrefs.GetInt ("Sound", 1);
		}
		if (value == 1) {
			GetComponent<Toggle> ().isOn = true;
		} else {
			GetComponent<Toggle> ().isOn = false;
		}
	}

}
