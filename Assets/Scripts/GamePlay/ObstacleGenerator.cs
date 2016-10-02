using UnityEngine;
//using System.Collections;

public class ObstacleGenerator : MonoBehaviour {

	[System.Serializable]
	public class Obstacles{
		public GameObject spikeBig;
		public GameObject spikeSmall;
		public GameObject spikeCenter;
	}

	public Obstacles obstacles;

	void Start () {
		Events.GameOverEvent += GameOver;
	}
	
	void Update () {
		Debug.Log(GetRandomInt());
	}

	void OnDisable(){
		Events.GameOverEvent -= GameOver;
	}

	private int GetRandomInt(){
		return Random.Range(1,5);
	}

	private void GameOver(){
		//stop obstacles from moving
		//stop generating new ones
	}
}
