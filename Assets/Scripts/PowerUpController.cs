using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpController : MonoBehaviour {

	public GameObject powerup;

	private float timeInterval = 12f;
	private float stepChange = 2f;
	private float lowerLimit = 5f;

	void Start () {
		timeInterval = 12f;
		powerup.SetActive (false);
		Invoke ("spawnPowerUp", timeInterval);
	}
	
	void spawnPowerUp() {
		powerup.SetActive(true);
		powerup.transform.position = new Vector3 (
				powerup.transform.position.x,
				12f,
				powerup.transform.position.z
			);

		if (timeInterval > lowerLimit) {
			Invoke ("spawnPowerUp", timeInterval);
			timeInterval -= stepChange;
		} else {
			Invoke ("spawnPowerUp", lowerLimit);
		}
	}
}
