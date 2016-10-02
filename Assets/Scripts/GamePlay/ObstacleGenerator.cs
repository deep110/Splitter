using UnityEngine;
//using System.Collections;

public class ObstacleGenerator : MonoBehaviour {

	[System.Serializable]
	public class Obstacles{
		public GameObject spikeLeft;
		public GameObject spikeRight;
		public GameObject spikeBoth;
		public GameObject spikeCenter;
	}

	public Obstacles obstacles;

	void Start () {
		Events.GameOverEvent += GameOver;
		InvokeRepeating("GenerateObstacle", 2, 	1.7f);
	}
	
	void Update () {
		
	}

	void OnDisable(){
		Events.GameOverEvent -= GameOver;
	}

	private void GenerateObstacle(){
		switch(Random.Range(1, 5)){
			case 1:
				Instantiate(obstacles.spikeLeft);
				break;

			case 2:
				Instantiate(obstacles.spikeRight);
				break;

			case 3:
				Instantiate(obstacles.spikeBoth);
				break;

			case 4:
				Instantiate(obstacles.spikeCenter);
				break;
		}
	}

	private void GameOver(){
		//stop obstacles from moving
		//stop generating new ones
	}
}
