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
		public GameObject spikeCenterBig;
		public GameObject blocks;
	}

	private enum State {
		pause, start
	};
	private State state;

	public GamePlayManager manager;
	public Obstacles obstacles;
	
	private Dictionary<int,ObjectPooler> objectPoolers;
	private int spikeId = 0;
	private bool isGameOver = false;
	private bool tutorialEnded = false;

	private bool isTimerRunning = false;
	private float timer = 0;
	/*
	 * This condition is applied so that the moving obstacles appear
	 * after certain score, not initially
	 */
	private int maxObstacleId = 6;
	//This is the no. of spikes after which the moving obstacles start appearing
	public int threshold = 0;

	void Start () {
		Events.GameOverEvent += GameOver;

		if (PlayerPrefs.GetInt ("Tutorial", 1) == 0) {
			state = State.start;
			isTimerRunning = true;
		} else {
			state = State.pause;
		}
	}

	void Update() {
		if(isTimerRunning) {
			timer += Time.deltaTime;
			if (timer > 2.0f) {
				isTimerRunning = false;
				StartCoroutine (GenerateObstacle ());
				timer = 0;
				manager.DisablePowerUp ();
			}
		}
	}

	public void ResetTimer() {
		if (isTimerRunning) {
			timer = 0;
		}
	}
	
	void OnDisable (){
		Events.GameOverEvent -= GameOver;
	}

	//Called by tutorial controller when tutorial is over
	public void StartObstacleGeneration() {
		StartCoroutine (GenerateObstacle ());
	}

	private IEnumerator GenerateObstacle (){

		objectPoolers = new Dictionary<int, ObjectPooler> (5);
		InitObjectPools ();

		//make queue for storing previous values which will help in generating next spike.
		SpecialQueue queue = new SpecialQueue ();
		spikeId = GetspikeId (queue);

		//Initial wait before the objects start falling
		//yield return new WaitForSeconds (1.8f);

		float waitTime = 1.8f / SpeedController.Speed * 6.5f;

		while (!isGameOver) {
			Spawn (objectPoolers [spikeId].GetPooledObject ());

			//Generate the next spike id and decide its generation rate.
			spikeId = GetspikeId (queue);

			if (queue.IsCenterSpike ()) {
				waitTime = 1.4f / SpeedController.Speed * 6.5f;
			} else if (queue.IsBigCenterSpike ()) {
				waitTime = 1.6f / SpeedController.Speed * 6.5f;
			} else if(queue.WasBigCenterSpike()) {
				waitTime = 1.3f / SpeedController.Speed * 6.5f;
			} else {
				waitTime = 1.1f / SpeedController.Speed * 6.5f;
			}
		
			yield return new WaitForSeconds (waitTime);

			//This increases the maxObstacleId to allow moving obstacles to fall
			if (manager.Score > threshold) {
				if (maxObstacleId == 6) {
					maxObstacleId = 7;
				}
			}
		}

	}

	private void InitObjectPools(){
		objectPoolers[1] = new ObjectPooler(obstacles.spikeLeft, 3);
		objectPoolers[2] = new ObjectPooler(obstacles.spikeRight, 3);
		objectPoolers[3] = new ObjectPooler(obstacles.spikeBoth, 3);
		objectPoolers[4] = new ObjectPooler(obstacles.spikeCenter, 2);
		objectPoolers [5] = new ObjectPooler (obstacles.spikeCenterBig, 2);
		objectPoolers[6] = new ObjectPooler (obstacles.blocks, 3);
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
			return (array[0] == array[1] && array[0] == num);
		}

		// If the coming spike is the center one
		public bool IsCenterSpike(){
			return (array[0] == 4);
		}

		public bool IsBigCenterSpike() {
			return (array [0] == 5);
		}

		public bool WasBigCenterSpike() {
			return (array [1] == 5);
		}

		// If the last spike was the center one
		public bool WasCenterSpike() {
			return (array [1] == 4 || array[1] == 5);
		}
	}
}
