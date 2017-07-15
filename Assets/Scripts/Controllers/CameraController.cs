using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject player;
	private Vector3 playerPosition;
	private Vector3 initialCameraPosition; 
	private bool isGameOver;

	// Use this for initialization
	void Start () {
		initialCameraPosition = transform.position;
		playerPosition = new Vector3();
		Events.GameOverEvent += OnGameOver;
	}

	void OnDisable() {
		Events.GameOverEvent -= OnGameOver;
	}
	
	// Update is called once per frame
	void Update () {

		if (!isGameOver) {
			//the player position is the mean of positions of left and right childs
			playerPosition.Set((player.transform.GetChild (0).position.x + player.transform.GetChild (1).position.x) / 2,
				0,
				0);
			transform.position = initialCameraPosition + playerPosition / 10;
		}
	}

	void OnGameOver() {
		isGameOver = true;
		transform.GetChild(0).GetComponent<Animator>().SetTrigger ("GameOver");
	}
}
