using UnityEngine;

public class SpikeController : MonoBehaviour {

	private readonly float Speed = 4;
	private bool move = true;
	private Rigidbody2D rigidBody;
	

	void Start(){
		rigidBody = transform.GetComponent<Rigidbody2D>();
	}

	void Update () {
		if(move){
			rigidBody.velocity = Vector2.down * (Speed);
		}else{
			rigidBody.velocity = Vector2.zero;
		}
	}

	public void Invert(){
		float scaleX = transform.localScale.x;
		transform.localScale = new Vector3(-scaleX, transform.localScale.y);
	}

	public void Stop(){
		move = false;
	}

}
