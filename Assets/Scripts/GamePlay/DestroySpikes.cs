using UnityEngine;

public class DestroySpikes : MonoBehaviour {

	void OnTriggerExit(Collider other){
		Destroy(other.gameObject);
	}
}
