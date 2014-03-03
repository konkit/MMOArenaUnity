using UnityEngine;
using System.Collections;

public class playerAnimation : MonoBehaviour {
	
	Animator animator;

	int punchHash = Animator.StringToHash("Base Layer.punch");

	void Start() {
		animator = GetComponent<Animator>();
	}

	void Update() {
		float move = Input.GetAxis("Vertical");
		animator.SetFloat("speed", move);

		AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
		if( stateInfo.nameHash != punchHash && Input.GetKeyDown(KeyCode.Space) )
		{
			animator.SetTrigger("punchTr");
		}
	}
}
