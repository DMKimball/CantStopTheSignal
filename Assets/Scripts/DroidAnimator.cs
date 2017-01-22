using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroidAnimator : MonoBehaviour {

    public Rigidbody2D localbody;
    public Animation anim;

    private bool atRest;


	// Use this for initialization
	void Start () {
        localbody = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animation>();
        atRest = false;

        foreach (AnimationState state in anim)
        {
            if (state.clip == anim.GetClip("TransformOut"))
            state.speed = 3F;
        }
    }
	
	// Update is called once per frame
	void Update () {
		if(localbody.velocity.magnitude > 0)
        {
            if (anim.IsPlaying("TransformOut"))
            {
                return; }
            atRest = false;
            if (!anim.isPlaying || anim.IsPlaying("TransformToBall"))
            {
                anim.Play("TransformOut");
                anim.PlayQueued("Walk");
            }
            else
            {
                anim.Play("Walk");
            }
        }
        else
        {
            if (!atRest)
            {
                anim.Play("TransformToBall");
                atRest = true;
            }
        }
	}
}
