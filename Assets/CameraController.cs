using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public GameObject player;
	private Vector3 playerPosition;
	private Vector3 initialCameraPosition; 

	// Use this for initialization
	void Start () {
		initialCameraPosition = transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//the player position is the mean of positions of left and right childs

		playerPosition = new Vector3((player.transform.GetChild (0).position.x + player.transform.GetChild (1).position.x) / 2,
			0,
			0);
		transform.position = initialCameraPosition + playerPosition / 10;
	}
}
