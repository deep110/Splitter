using UnityEngine;
using System.Collections;

public class SpikeSpeedController : MonoBehaviour {

	private float Speed = 6.5f;
	private Rigidbody2D rigidBody;
	private bool isMoving = true;

	void Start(){
		rigidBody = transform.GetComponent<Rigidbody2D>();
	}

	void FixedUpdate() {
		if (isMoving) {
			Speed = SpeedController.Speed;
			if (rigidBody == null) {
				rigidBody = transform.GetComponent<Rigidbody2D> ();
			}
			rigidBody.velocity = Vector2.down * (Speed);
		} else {
			rigidBody.velocity = Vector2.zero;
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
