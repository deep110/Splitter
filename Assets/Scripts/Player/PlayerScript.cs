using UnityEngine;

public class PlayerScript : MonoBehaviour {

	public ParticleSystem sparksTop;
	public ParticleSystem sparksBottom;
	public AudioClip sawClip;

	private bool isPlaying = false;
	private AudioSource source;

	void OnEnable() {
		Events.GameOverEvent += OnGameOver;
		source = GetComponent<AudioSource> ();
		source.loop = true;
	}

	void OnDisable() {
		Events.GameOverEvent -= OnGameOver;
	}

	void OnCollisionEnter2D(Collision2D other) {
        if(other.transform.tag.Equals("Wall")){
      		sparksTop.Play();
      		sparksBottom.Play();
      		isPlaying = true;

			//Play the saw clip
			if (PlayerPrefs.GetInt ("Sound", 1) == 1) {
				source.PlayOneShot (sawClip, 0.3f);
			}
        }
    }

    void OnCollisionExit2D(Collision2D other){
    	if(isPlaying && other.transform.tag.Equals("Wall")){
    		sparksTop.Stop();
    		sparksBottom.Stop();
    		isPlaying = false;

			source.Stop ();
    	}
    }

	void OnGameOver() {
		if (isPlaying) {
			sparksTop.Stop ();
			sparksBottom.Stop ();
			isPlaying = false;
		}
	}

}
