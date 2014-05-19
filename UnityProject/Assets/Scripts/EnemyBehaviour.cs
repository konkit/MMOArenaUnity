using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

	public GameObject playerObject;

	public float rotationSpeed = 100.0f;

	public float cooldownAmount = 3.0f, currentCooldown = 3.0f;
	public GameObject fireballPrefab;

	Quaternion lookRotation;

	// minimal angle (in deg) which allows enemy to start shooting fireballs;
	public float shootAngleThreshold = 5.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		RotateAtPlayer();
		ShootFireball();
	}

	void RotateAtPlayer ()
	{
		//find the vector pointing from our position to the target
		Vector3 direction = (playerObject.transform.position - transform.position).normalized;
		
		//create the rotation we need to be in to look at the target
		lookRotation = Quaternion.LookRotation(direction);
		
		//rotate us over time according to speed until we are in the required rotation
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
	}

	void ShootFireball ()
	{
		if( Quaternion.Angle(lookRotation, transform.rotation) > shootAngleThreshold ) {
			return;
		}

		if( currentCooldown <= 0) {
			Instantiate(fireballPrefab, (transform.position + new Vector3(0.0f, 1.4f, 0.0f)) + transform.forward, transform.rotation);
			currentCooldown = cooldownAmount;
		} else {
			currentCooldown -= Time.deltaTime;
		}
	}
}
