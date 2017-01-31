using UnityEngine;

public class PlayerScript : MonoBehaviour {

	public ParticleSystem sparksTop;
	public ParticleSystem sparksBottom;

	private bool isPlaying = false;

	void OnEnable() {
		Events.GameOverEvent += OnGameOver;
	}

	void OnDisable() {
		Events.GameOverEvent -= OnGameOver;
	}

	void OnCollisionEnter2D(Collision2D other) {
        if(other.transform.tag.Equals("Wall")){
      		sparksTop.Play();
      		sparksBottom.Play();
      		isPlaying = true;
        }
    }

    void OnCollisionExit2D(Collision2D other){
    	if(isPlaying && other.transform.tag.Equals("Wall")){
    		sparksTop.Stop();
    		sparksBottom.Stop();
    		isPlaying = false;
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
