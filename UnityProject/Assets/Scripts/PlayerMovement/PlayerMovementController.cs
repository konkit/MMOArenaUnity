using UnityEngine;
using System.Collections;

public class PlayerMovementController : MonoBehaviour
{
    #region zone variable xD
    public float maxSpeed;
    public bool grounded;
    public bool isJump;
    public float hight;

    
    #endregion

    void Awake()
    {        
        isJump = false;
    }
    
    void Update()
    {       
        JumpController();
    }
    void FixedUpdate()
    {
        if(grounded)
        {
            Vector3 moveTarget = new Vector3(Input.GetAxis("Horizontal") * maxSpeed, rigidbody.velocity.y, Input.GetAxis("Vertical") * maxSpeed);
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
        if(Input.GetKeyDown(KeyCode.Space) && grounded)
        {
           
            rigidbody.AddForce(new Vector3(0, hight, 0));       
        }       
    }

}
