using UnityEngine;
using System.Collections;

public class InputMapper : MonoBehaviour {

	
	void Update () {
		
		for(int i=0;i< Input.touchCount && i<2;i++){
			Debug.Log(""+Input.GetTouch(i).position);
		}
	}
}
