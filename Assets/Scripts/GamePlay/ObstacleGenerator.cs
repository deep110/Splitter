using UnityEngine;
using System.Collections;

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
		StartCoroutine(GenerateObstacle());
	}
	
	void OnDisable(){
		Events.GameOverEvent -= GameOver;
	}

	private IEnumerator GenerateObstacle(){

		yield return new WaitForSeconds(2);
		SpecialQueue lastSpikes = new SpecialQueue();

		while(true){
			switch(GetObstacleNumber(lastSpikes)){
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
			
			yield return new WaitForSeconds(1);
		}
		
	}

	private int GetObstacleNumber(SpecialQueue q){
		int x = Random.Range(1, 5);
		if(q.IsAlert(x)){
			int y = Random.Range(1, 4);
			if(x == y) x = 4;
			else x = y;
		}

		q.Push(x);
		return x;	
	}

	private void GameOver(){
		//stop obstacles from moving
		//stop generating new ones
	}


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

		public bool IsAlert(int num){
			if(array[0] == 4 && num == 4){
				return true;
			}

			return (array[0]== array[1] && array[0] == num);
		}
	}
}
