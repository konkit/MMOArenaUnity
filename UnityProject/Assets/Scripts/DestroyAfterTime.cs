using UnityEngine;
using System.Collections;

public class DestroyAfterTime : MonoBehaviour {

	public float timeToDestroy;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (timeToDestroy != 0.0f) {
            Destroy(gameObject, timeToDestroy);
            
        }
	}
}
