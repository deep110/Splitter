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
	private SpecialQueue queue = new SpecialQueue();
	private int spikeNumber = 0;

	void Start () {
		Events.GameOverEvent += GameOver;
		spikeNumber = GetSpikeNumber();
		StartCoroutine(GenerateObstacle());
	}
	
	void OnDisable(){
		Events.GameOverEvent -= GameOver;
	}

	private IEnumerator GenerateObstacle(){

		yield return new WaitForSeconds(2);
		
		while(true){
			switch(spikeNumber){
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

			spikeNumber = GetSpikeNumber();
			float waitTime = 0.8f;
			if(queue.IsCenterSpike()) waitTime = 1f;
			
			yield return new WaitForSeconds(waitTime);
		}
		
	}

	private int GetSpikeNumber(){
		int x = Random.Range(1, 5);
		if(queue.IsAlert(x)){
			int y = Random.Range(1, 4);
			if(x == y) x = 4;
			else x = y;
		}
		queue.Push(x);

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

		public int[] Get(){
			return array;
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

		public bool IsCenterSpike(){
			return (array[0]==4 && array[1] <3);
		}
	}
}
