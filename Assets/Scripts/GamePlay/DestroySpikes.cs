using UnityEngine;

public class DestroySpikes : MonoBehaviour {

	void OnTriggerExit2D(Collider2D other){

		if(other.gameObject.transform.parent.parent == null){
			Destroy(other.gameObject);
		}else{	
			Destroy(other.gameObject.transform.parent.gameObject);
		}
		
	}
}
