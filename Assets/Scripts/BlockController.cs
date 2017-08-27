using UnityEngine;
using System.Collections;

public class BlockController : MonoBehaviour {

	private float theta;
	private Transform leftTransform;
	private Transform rightTransform;
	private float meanX;
	private float radius;

	// Use this for initialization
	void Start () { 
		leftTransform = gameObject.GetComponentsInChildren<Transform> () [0];
		rightTransform = gameObject.GetComponentsInChildren<Transform> () [1];
		meanX = (leftTransform.position.x + rightTransform.position.x ) / 2;
		radius = (rightTransform.position.x - leftTransform.position.x) / 2;
	}

	void OnEnable() {
		if (Random.Range (0, 1.0f) < 0.2f) {
			theta = 0.5f;
		}
		else {
			theta = 4.5f;
		}
	}
	
	void FixedUpdate () {
		theta += Time.deltaTime * 0.7f;
		leftTransform.position = new Vector3 (meanX - radius * Mathf.Cos(theta), leftTransform.position.y, 0);
		rightTransform.position = new Vector3 (meanX + radius * Mathf.Cos (theta), rightTransform.position.y, 0);
	}
}
