using UnityEngine;
using System.Collections;

public class AnimationController : MonoBehaviour 
{
    Animator anim;
    PlayerMovementController playerController;


    void Start()
    {
        anim = GetComponent<Animator>();
        playerController = GetComponent<PlayerMovementController>();
    }

    void Update()
    {
        ForwardAnimation();
        JumpAnimation();
        PunchAnimation();
    }

    void ForwardAnimation()
    {
        float move = Input.GetAxis("Vertical");
        anim.SetFloat("speed", move);
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
}
