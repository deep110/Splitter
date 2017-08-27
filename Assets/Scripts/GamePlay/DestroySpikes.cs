using UnityEngine;

public class DestroySpikes : MonoBehaviour {

	void OnTriggerExit2D(Collider2D other){
		
		if (string.Compare(other.gameObject.name, "Left") != 0 && string.Compare(other.gameObject.name, "Right") != 0) {
			if(other.tag.Equals("SpikeBoth")){
				other.gameObject.transform.parent.gameObject.SetActive(false);
			}else{
				other.gameObject.SetActive(false);
			}
		}

	}
}
