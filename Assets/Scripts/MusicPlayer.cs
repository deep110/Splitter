using UnityEngine;
using System.Collections;

public class MusicPlayer : MonoBehaviour {

	//Whether it's a sound or a music
	public bool isMusic = true;
	public AudioClip clip;
	public bool loop = true;
	public float volume = 0.5f;

	private AudioSource source;

	// Use this for initialization
	void Start () {
		int value;
		if (isMusic) {
			value = PlayerPrefs.GetInt ("Music", 1);
		} else {
			value = PlayerPrefs.GetInt ("Sound", 1);
		}
		if (value == 1) {
			source = GetComponent<AudioSource> ();
			source.loop = loop;
			source.clip = clip;
			source.volume = 0.5f;
			source.Play ();
		}
	}

}
