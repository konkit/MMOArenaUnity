using UnityEngine;
using System.Collections;

public class DestroyAfterTime : MonoBehaviour {

	public float timeToDestroy;

	// Use this for initialization
	void Start () {
		if( timeToDestroy != 0.0f ) {
			Destroy (this, timeToDestroy);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
