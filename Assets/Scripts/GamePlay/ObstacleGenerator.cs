using UnityEngine;
//using System.Collections;

public class ObstacleGenerator : MonoBehaviour {

	void Start () {
		Events.GameOverEvent += GameOver;
	}
	
	void Update () {
	
	}

	void OnDisable(){
		Events.GameOverEvent -= GameOver;
	}

	private void GameOver(){
	}
}
