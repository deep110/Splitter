using UnityEngine;
using System.Collections;

public class TutorialToggleController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnValueChanged (bool newValue) {
		if (newValue) {
			PlayerPrefs.SetInt ("tutorial", 1);
		} else {
			PlayerPrefs.SetInt ("tutorial", 0);
		}
	}
}
