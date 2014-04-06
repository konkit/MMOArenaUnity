using UnityEngine;
using System.Collections;

public class PlayerMovementController : MonoBehaviour
{
    #region zone variable xD
    public float maxSpeed;
    public bool grounded;
    public bool isJump;
    public float hight;

    Animator anim;
    #endregion

    void Awake()
    {
        grounded = true;
        isJump = false;
    }
    void Start()
    {
        anim = GetComponent<Animator>();
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
            float move = Input.GetAxis("Vertical");
            anim.SetFloat("speed", move);
        }        
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag=="Player")
        {
            grounded = false;
        }
    }

    void JumpController()
    {
        if(Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            isJump = true;
            rigidbody.AddForce(new Vector3(0, hight, 0));       
        }
        else
        {
            isJump = false;         
        }
        if(isJump)
        {
            anim.SetBool("jumpTr", true);
        }
        else
        {
            anim.SetBool("jumpTr", false);
        }
    }

}
