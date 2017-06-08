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
		theta = Random.Range (0, 3.14f);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		theta += Time.deltaTime * 0.7f;
		leftTransform.position = new Vector3 (meanX - radius * Mathf.Cos(theta), leftTransform.position.y, 0);
		rightTransform.position = new Vector3 (meanX + radius * Mathf.Cos (theta), rightTransform.position.y, 0);
	}
}
