using UnityEngine;
using System.Collections;

public class PlayerMovementController : MonoBehaviour {
    #region zone variable xD
    public float maxSpeed;
    public bool grounded;
    public bool isJump;
    public float hight;

	public CharacterControlInterface characterControlInterface;
    
    #endregion

    void Awake()
    {        
        isJump = false;
		characterControlInterface = GetComponent<CharacterControlInterface>();
    }
    
    void Update()
    {       
        JumpController();
    }

    void FixedUpdate()
    {
        if(grounded)
        {
            Vector3 moveTarget = new Vector3(
				characterControlInterface.sidewaysMov * maxSpeed, 
				rigidbody.velocity.y, 
				characterControlInterface.forwardMov * maxSpeed 
				);
            moveTarget = transform.TransformDirection(moveTarget);
            rigidbody.velocity = moveTarget;
        }
        else
        {
            return;
        }
    }

    void OnTriggerStay(Collider other)
    {
        grounded = true;
        isJump = false;
    }
    void OnTriggerExit(Collider other)
    {
        grounded = false;
        isJump = true;
    }
    void JumpController()
    {
        if( characterControlInterface.isJump && grounded)
        {           
            rigidbody.AddForce(new Vector3(0, hight, 0));       
        }       
    }

}
