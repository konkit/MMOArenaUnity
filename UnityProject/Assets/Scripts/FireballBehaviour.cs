using UnityEngine;
using System.Collections;

public class FireballBehaviour : MonoBehaviour {

	public float speed = 1.0f;
	public int damage = 50;

	public GameObject explosion;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void FixedUpdate () {
		transform.Translate(Vector3.forward * speed);
        

	}

	void OnCollisionEnter(Collision coll) {
		GameObject collidedObj = coll.gameObject;
        
		Instantiate(explosion, transform.position, transform.rotation);

		if( coll.gameObject.tag == "Player" || coll.gameObject.tag == "Enemy" ) {
			coll.gameObject.GetComponent<CharacterStats>().decreaseHealth(damage);
		}
        
		Destroy(gameObject);
	}
}
