using UnityEngine;
using System.Collections;

public class HumanPlayerController : MonoBehaviour {

	public CharacterControlInterface controlInterface;

	// Use this for initialization
	void Start () {
		controlInterface = GetComponent<CharacterControlInterface>();

		if( controlInterface == null ) {
			throw new UnityException("Character controll interface is null");
		}
	}
	
	// Update is called once per frame
	void Update () {
		controlInterface.forwardMov = Input.GetAxis("Vertical");
		controlInterface.sidewaysMov = Input.GetAxis("Horizontal");
		
		controlInterface.isJump = Input.GetButton("Jump");
		controlInterface.isPunch = Input.GetMouseButton(0);

		controlInterface.nextSpell = Input.GetKeyDown( KeyCode.E );
		controlInterface.previousSpell = Input.GetKeyDown( KeyCode.Q );
	}
}
