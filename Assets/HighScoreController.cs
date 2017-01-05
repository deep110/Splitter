using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HighScoreController : MonoBehaviour {

	Text highscore;

	// Use this for initialization
	void Start () {
		highscore = GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
		highscore.text = PlayerPrefs.GetInt ("highscore").ToString();
	}
}
