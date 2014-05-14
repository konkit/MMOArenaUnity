using UnityEngine;
using System.Collections;

public class Spellcasting : MonoBehaviour {
	public GameObject fireballPrefab;

	public float spellHeight = 1.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if( Input.GetKey(KeyCode.Mouse0) ) {
			GetComponent<MouseController>().AlignRotation();

			Debug.Log("Attacking");
			Instantiate(fireballPrefab, (transform.position + new Vector3(0.0f, spellHeight, 0.0f)) + transform.forward, transform.rotation);
		}	
	}
}
