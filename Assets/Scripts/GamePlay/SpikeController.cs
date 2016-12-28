using UnityEngine;

public class SpikeController : MonoBehaviour {

	private float Speed = 6.5f;
	private Rigidbody2D rigidBody;
	private bool move = true;

	void Start(){
		rigidBody = transform.GetComponent<Rigidbody2D>();
	}

	void FixedUpdate() {
		if (move) {
			rigidBody.velocity = Vector2.down * (Speed);
		} else {
			rigidBody.velocity = Vector2.zero;
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "PlayerLeft") {
			other.gameObject.transform.parent.SendMessage ("Dead", -1);
		} else if (other.gameObject.tag == "PlayerRight") {
			other.gameObject.transform.parent.SendMessage ("Dead", 1);
		}
	}

	//stop the spikes
	public void Stop(){
		move = false;
	}

}
