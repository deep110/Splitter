using UnityEngine;
using System.Collections;

public class FallingSpeedController : MonoBehaviour {

	public float multiplier = 1;

	private float Speed = 6.5f;
	private bool isMoving = true;
	private Vector3 tempPosition = new Vector3(0, 0);
	private Rigidbody2D rigidBody;

	void Start() {
		rigidBody = GetComponent<Rigidbody2D> ();
	}

	void Update() {
		if (isMoving) {
			Speed = SpeedController.Speed * multiplier;
			tempPosition.y = - Speed * Time.deltaTime;
			transform.position = transform.position + tempPosition;
		}
	}

	void OnEnable() {
		Events.GameOverEvent += Stop;
	}

	//stop the spikes
	public void Stop(){
		isMoving = false;
	}

	void OnDisable() {
		Events.GameOverEvent -= Stop;
	}

}
