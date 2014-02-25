using UnityEngine;
using System.Collections;

public class playerAnimation : MonoBehaviour {
	
	//main animation object
	public Animation mAnimation;
	
	//animation clips - already sliced, ready to play
	public AnimationClip idleAnimation;
	public AnimationClip walkAnimation;
	
	//Player state
	private enum State { Idle, Walking};
	private State mState;
	
	void Awake() {	
		mState = State.Idle;
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetAxis ("Vertical") != 0.0 )
		{
			mState = State.Walking;	
		}
		else
		{
			mState = State.Idle;	
		}		
		
		if(mState == State.Walking)
		{
			mAnimation.CrossFade(walkAnimation.name);
		}
		
		if(mState == State.Idle)
		{
			mAnimation.CrossFade(idleAnimation.name);
		}			
	}
}
