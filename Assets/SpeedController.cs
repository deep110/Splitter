using UnityEngine;
using System.Collections;

public class SpeedController : MonoBehaviour {

	public static float Speed = 6.5f;
		
	// Update is called once per frame
	void FixedUpdate () {
		Speed += Time.deltaTime / 7;
	}

	void OnEnable() {
		Events.GameOverEvent += OnGameOver;
	}

	public void OnGameOver(){
		Speed = 6.5f;
	}

	void OnDisable() {
		Events.GameOverEvent -= OnGameOver;
	}

}
