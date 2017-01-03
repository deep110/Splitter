using UnityEngine;

public class SpikeController : MonoBehaviour {

//	private float Speed = 6.5f;
	private Rigidbody2D rigidBody;

//	void Start(){
//		rigidBody = transform.GetComponent<Rigidbody2D>();
//	}

//	void FixedUpdate() {
//		if (rigidBody == null) {
//			rigidBody = transform.GetComponent<Rigidbody2D> ();
//		}
//		rigidBody.velocity = Vector2.down * (Speed);
//		Speed = SpeedController.Speed;
//	}

//	void OnEnable() {
//		Events.GameOverEvent += Stop;
//	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "PlayerLeft") {
			other.gameObject.transform.parent.SendMessage ("Dead", -1);
		} else if (other.gameObject.tag == "PlayerRight") {
			other.gameObject.transform.parent.SendMessage ("Dead", 1);
		}
	}

	//stop the spikes
//	public void Stop(){
//		rigidBody.velocity = Vector2.zero;
//	}

//	void OnDisable() {
//		Events.GameOverEvent -= Stop;
//	}

}
