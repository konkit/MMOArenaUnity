using UnityEngine;
using System.Collections;

public class FireballBehaviour : MonoBehaviour {

	public float speed = 1.0f;
	public float damage = 100.0f;

	public GameObject explosion;

	public float timeToDestroy = 5.0f;

	// Use this for initialization
	void Start () {
		Destroy (gameObject, timeToDestroy);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Translate(Vector3.forward * speed);


	}

	void OnCollisionEnter(Collision coll) {
		GameObject collidedObj = coll.gameObject;
		Debug.Log("Collided : " + collidedObj);

		Instantiate(explosion, transform.position, transform.rotation);

		Destroy(this.gameObject);
	}
}
