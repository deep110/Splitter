using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObstacleGenerator : MonoBehaviour {

	[System.Serializable]
	public class Obstacles{
		public GameObject spikeLeft;
		public GameObject spikeRight;
		public GameObject spikeBoth;
		public GameObject spikeCenter;
		public GameObject blocks;
	}

	private enum State {
		tutorial, playing
	};
	private State state;
	private int isTutShown;//This stores whether tutorial has been shown once or not

	public Obstacles obstacles;
	
	private Dictionary<int,ObjectPooler> objectPoolers;
	private int spikeId = 0;
	private bool isGameOver = false;
	private bool tutorialEnded = false;
	/*
	 * This condition is applied so that the moving obstacles appear
	 * after certain score, not initially
	 */
	private int maxObstacleId = 5;
	//Counts the no. of spikes generated from start
	private int spikesGenerated = 0;
	//This is the no. of spikes after which the moving obstacles start appearing
	public int threshold = 40;

	void Start () {
		Events.GameOverEvent += GameOver;
		isTutShown = PlayerPrefs.GetInt ("tutorial", 1);
		if (isTutShown == 0) {
			state = State.playing;
		} else {
			state = State.tutorial;
		}
	}
	
	void OnDisable (){
		Events.GameOverEvent -= GameOver;
	}

	void Update () {
		if (state == State.tutorial) {
			if (!TutorialController.isTutorialRunning ()) {
				state = State.playing;
			}
		} else if (state == State.playing && !tutorialEnded) {
			StartCoroutine (GenerateObstacle ());
			tutorialEnded = true;
		}
	}

	private IEnumerator GenerateObstacle (){

		objectPoolers = new Dictionary<int, ObjectPooler> (5);
		InitObjectPools ();

		//make queue for storing previous values which will help in generating next spike.
		SpecialQueue queue = new SpecialQueue ();
		spikeId = GetspikeId (queue);

		yield return new WaitForSeconds (1.8f);
		spikesGenerated = 1;

		float waitTime = 1.8f / SpeedController.Speed * 6.5f;

		while (!isGameOver) {
			Spawn (objectPoolers [spikeId].GetPooledObject ());

			//Generate the next spike id and decide its generation rate.
			spikeId = GetspikeId (queue);

			if (queue.IsCenterSpike ())
				waitTime = 1.3f / SpeedController.Speed * 6.5f;
			else
				waitTime = 1.1f / SpeedController.Speed * 6.5f;
		
			yield return new WaitForSeconds (waitTime);
			spikesGenerated++; 

			//This increases the maxObstacleId to allow moving obstacles to fall
			if (spikesGenerated > threshold) {
				if (maxObstacleId == 5) {
					maxObstacleId = 6;
				}
			}
		}

	}

	private void InitObjectPools(){
		objectPoolers[1] = new ObjectPooler(obstacles.spikeLeft, 3);
		objectPoolers[2] = new ObjectPooler(obstacles.spikeRight, 3);
		objectPoolers[3] = new ObjectPooler(obstacles.spikeBoth, 3);
		objectPoolers[4] = new ObjectPooler(obstacles.spikeCenter, 2);
		objectPoolers[5] = new ObjectPooler (obstacles.blocks, 3);
	}

	// Generates the new spike id that is to be instatiated.
	private int GetspikeId(SpecialQueue queue){
		int x = Random.Range(1, maxObstacleId);
		while(queue.IsAlert(x)){
			x = Random.Range (1, maxObstacleId);
		}
		queue.Push(x);

		return x;
	}

	private GameObject Spawn(GameObject obj){
		Transform transForm = obj.transform;
		transForm.position = new Vector2(transForm.position.x, 12f);
		transForm.rotation = Quaternion.identity;

		//reset Y position of its each children
		for(int i=0; i<transForm.childCount; i++){
			Transform childTransform = transForm.GetChild(i);
			childTransform.localPosition = new Vector2(childTransform.localPosition.x, 0f);
		}
		transForm.parent = transform;
		obj.SetActive(true);

		return obj;
	}

	private void GameOver(){
		//stopping all the spikes.
		if(!isGameOver){
			isGameOver = true;
		}
	}


	// Data Struct for holding previous generated values and provide useful to generate next spike.
	private class SpecialQueue{

		private readonly int [] array;

		public SpecialQueue(){
			array = new int[2];
			array[0] = array[1] = -1;
		}

		public void Push(int num){
			array[1] = array[0];
			array[0] = num;
		}

		// returns true is any spike (i.e 1,2,3) is going to be instatiated 3 times or 4th one 2 times in a row. 
		public bool IsAlert(int num){
			if(array[0] == 4 && num == 4){
				return true;
			}
			return (array[0]== array[1] && array[0] == num);
		}

		// returns true if next spike coming is 4th one after 1st or 2nd one.
		public bool IsCenterSpike(){
			return (array[0]==4 && array[1] <3);
		}
	}
}
