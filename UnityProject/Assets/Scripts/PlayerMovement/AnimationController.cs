using UnityEngine;
using System.Collections;

public class AnimationController : MonoBehaviour 
{
    Animator anim;
    PlayerMovementController playerController;
    HealthOfPlayer death;
    CapsuleCollider colliderr;


    void Start()
    {
        anim = GetComponent<Animator>();
        playerController = GetComponent<PlayerMovementController>();
        death = GetComponent<HealthOfPlayer>();
        colliderr = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        ForwardAnimation();
        JumpAnimation();
        PunchAnimation();
        DeathAnimation();
    }

    void ForwardAnimation()
    {
        
        float moveForward = Input.GetAxis("Vertical");
        anim.SetFloat("forward", moveForward);
        float moveAside = Input.GetAxis("Horizontal");
        anim.SetFloat("aside", moveAside);
        
    }

    void JumpAnimation()
    {
        if (playerController.isJump)
        {
            anim.SetBool("jumpTr", true);
        }
        else
        {
            anim.SetBool("jumpTr", false);
        }
    }

    void PunchAnimation()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("punch");
        }
    }
    void DeathAnimation()
    {
        if(death.isDeath)
        {
            anim.SetBool("isDeath", death.isDeath);
            colliderr.enabled=false;
        }
    }
}
