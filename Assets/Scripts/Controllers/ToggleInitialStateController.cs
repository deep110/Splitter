using UnityEngine;
using UnityEngine.UI;

public class ToggleInitialStateController : MonoBehaviour {

	public bool isMusic;

	void Start () {

		int value = isMusic ? PlayerPrefs.GetInt ("Music", 1) : PlayerPrefs.GetInt ("Sound", 1);
		
		GetComponent<Toggle> ().isOn = (value == 1);
	}

}
