using UnityEngine;
using System.Collections;


[RequireComponent (typeof (Rigidbody))]
public class PlayerMovementRigidbodyController : MonoBehaviour {

	// rigidbody controller stuff
	public float forwardSpeed = 5.0f;
	public float sideSpeed = 3.0f; 
		
	public float gravity = 5.0f;
	public float maxVelocityChange = 5.0f;
	
	public bool canJump = true;
	public float jumpHeight = 2.5f;
	private bool grounded = false;
	
	public bool isMoving = false;
	
	// Use this for initialization
	void Start () {
	
	}
	
	void Awake() {
			rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
	    	rigidbody.useGravity = false;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(grounded) {
	        // Calculate how fast we should be moving
	        Vector3 targetVelocity = new Vector3( Input.GetAxis("Horizontal") * sideSpeed, 
												  0, 
												  Input.GetAxis("Vertical") * forwardSpeed );
			
			isMoving = 	targetVelocity.sqrMagnitude > 0.0001;
			
	        targetVelocity = transform.TransformDirection(targetVelocity);
 
	        // Apply a force that attempts to reach our target velocity
	        Vector3 velocity = rigidbody.velocity;
	        Vector3 velocityChange = (targetVelocity - velocity);			
	        	velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
	        	velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
	        	velocityChange.y = 0;
	        rigidbody.AddForce(velocityChange, ForceMode.VelocityChange);
			
	        // Jump
	        if (canJump && Input.GetButton("Jump")) {
	            rigidbody.velocity = new Vector3(velocity.x, /*velocity.y*/CalculateJumpVerticalSpeed(), velocity.z);
	        }
	    }
 
	    // We apply gravity manually for more tuning control
	    rigidbody.AddForce(new Vector3 (0, -gravity * rigidbody.mass, 0));
 
	    grounded = false;
	}
	
	void OnCollisionStay () {
	    grounded = true;    
	}
 
	float CalculateJumpVerticalSpeed () {
	    // From the jump height and gravity we deduce the upwards speed 
	    // for the character to reach at the apex.
	    return Mathf.Sqrt(2 * jumpHeight * gravity);
	}
}
