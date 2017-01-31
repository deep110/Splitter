using UnityEngine;

public class SpikeController : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "PlayerLeft") {
			other.gameObject.transform.parent.SendMessage ("Dead", -1);
		} else if (other.gameObject.tag == "PlayerRight") {
			other.gameObject.transform.parent.SendMessage ("Dead", 1);
		}
	}

}
