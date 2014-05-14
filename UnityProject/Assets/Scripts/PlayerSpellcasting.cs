using UnityEngine;
using System.Collections;

public class PlayerSpellcasting : MonoBehaviour {
	public GameObject fireballPrefab;

	public float spellHeight = 1.4f;

	public float cooldownAmount = 1.0f, currentCooldown = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if( currentCooldown > 0.0f ) {
			currentCooldown -= Time.deltaTime;
		}

		if( Input.GetKey(KeyCode.Mouse0) ) {

			if( currentCooldown <= 0.0f ) {
				GetComponent<MouseController>().AlignRotation();

				Debug.Log("Attacking");
				Instantiate(fireballPrefab, (transform.position + new Vector3(0.0f, spellHeight, 0.0f)) + transform.forward, transform.rotation);

				currentCooldown = cooldownAmount;
			}
		}	
	}
}
