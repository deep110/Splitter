using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpashScreenController : MonoBehaviour {

	public Animator fadeOut;

	bool isTimerTicking = false;
	float timer;

	void Start () {
		timer = 0;
		isTimerTicking = true;
	}
	
	void Update () {
		//close the application when back button is pressed in android
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit ();
		}
		if (isTimerTicking) {
			timer += Time.deltaTime;
			if (timer > 2f) {
				isTimerTicking = false;
				fadeOut.SetTrigger ("Home");
				timer = 0;
			}
		}
	}
}
