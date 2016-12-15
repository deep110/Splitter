using UnityEngine;

public class PlayerScript : MonoBehaviour {

	public ParticleSystem sparksTop;
	public ParticleSystem sparksBottom;

	private bool isPlaying = false;

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

}
