using UnityEngine;
using System.Collections;

public class DestroyAfterTime : MonoBehaviour {

	public float timeToDestroy;

	// Use this for initialization
	void Start () {
        if (timeToDestroy != 0.0f)
        {
            Destroy(gameObject, timeToDestroy);

        }
	}
	
	// Update is called once per frame
	void Update () {
        
	}
}
