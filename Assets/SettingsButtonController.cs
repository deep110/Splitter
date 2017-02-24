using UnityEngine;
using System.Collections;

public class SettingsButtonController : MonoBehaviour {

	public GameObject settingsPanel;
	public GameObject scorePanel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OpenSettings() {
		settingsPanel.SetActive (true);
		scorePanel.SetActive (false);
	}
}
