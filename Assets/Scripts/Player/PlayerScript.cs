using UnityEngine;

public class PlayerScript : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D col) {
        Debug.Log("player collider");
    }
}
