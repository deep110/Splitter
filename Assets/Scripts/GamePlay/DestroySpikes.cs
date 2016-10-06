using UnityEngine;

public class DestroySpikes : MonoBehaviour {

	void OnTriggerExit2D(Collider2D other){

		if(other.tag.Equals("SpikeBoth")){
			other.gameObject.transform.parent.gameObject.SetActive(false);
		}else{
			other.gameObject.SetActive(false);
		}
		
	}
}
