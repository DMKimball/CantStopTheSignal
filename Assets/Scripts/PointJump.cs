using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointJump : MonoBehaviour {

    public Transform[] waypoints = new Transform[0];
    public float jumpCooldown = 0.25f;
    public int currLocIndex = 0;

    private bool jumpReady = true;

    // Use this for initialization
    void Start () {
        if (waypoints.Length == 0) jumpReady = false;
	}
	
	// Update is called once per frame
	void Update () {
        float input = Input.GetAxis("Horizontal");

        if (waypoints.Length > currLocIndex)
        {
            transform.position = waypoints[currLocIndex].position;
        }

        if (jumpReady && input < 0)
        {
            currLocIndex = (currLocIndex + 1) % waypoints.Length;
            transform.position = waypoints[currLocIndex].position;
            StartCoroutine("JumpCooldown");
        }
        if(jumpReady && input > 0)
        {
            currLocIndex = (currLocIndex == 0) ? waypoints.Length - 1 : currLocIndex - 1;
            transform.position = waypoints[currLocIndex].position;
            StartCoroutine("JumpCooldown");
        }
	}

    IEnumerator JumpCooldown()
    {
        jumpReady = false;
        yield return new WaitForSecondsRealtime(jumpCooldown);
        jumpReady = true;
    }
}
