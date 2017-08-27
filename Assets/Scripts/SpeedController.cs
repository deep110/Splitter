using UnityEngine;
using System.Collections;

public class SpeedController : MonoBehaviour {

	public static float Speed = 6.5f;

	void Start() {
		Speed = 6.5f;
	}

	public static void IncreaseSpeed(float step) {
		Speed += step / 6;
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

	public static void Reset() {
		Speed = 6.5f;
	}

}
