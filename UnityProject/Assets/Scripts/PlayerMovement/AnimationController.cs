using UnityEngine;
using System.Collections;

public class AnimationController : MonoBehaviour 
{
    Animator anim;
    PlayerMovementController playerController;
    HealthOfPlayer death;
    CapsuleCollider colliderr;

	CharacterControlInterface characterControlInterface;


    void Start()
    {
        anim = GetComponent<Animator>();
        playerController = GetComponent<PlayerMovementController>();
        death = GetComponent<HealthOfPlayer>();
        colliderr = GetComponent<CapsuleCollider>();

		characterControlInterface = GetComponent<CharacterControlInterface>();
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
    	anim.SetFloat("forward", Mathf.Abs( characterControlInterface.forwardMov) );
		anim.SetFloat("aside", Mathf.Abs(characterControlInterface.sidewaysMov));
    }

    void JumpAnimation()
    {
		anim.SetBool("jumpTr", characterControlInterface.isJump);
	}
	
    void PunchAnimation()
    {
		if ( characterControlInterface.isPunch )
		{
            anim.SetTrigger("punch");
        }
    }
    void DeathAnimation()
    {
		if( death.isDeath )
		{
            anim.SetBool("isDeath", death.isDeath);
            colliderr.enabled=false;
        }
    }
}
