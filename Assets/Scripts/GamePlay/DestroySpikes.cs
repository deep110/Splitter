using UnityEngine;

public class DestroySpikes : MonoBehaviour {

	void OnTriggerExit2D(Collider2D other){

		if(other.gameObject.transform.parent == null){
			other.gameObject.SetActive(false);
		}else{
			other.gameObject.transform.parent.gameObject.SetActive(false);
			//Debug.Log("In des: "+other.gameObject.transform.parent.gameObject.GetInstanceID());
		}
		
	}
}
