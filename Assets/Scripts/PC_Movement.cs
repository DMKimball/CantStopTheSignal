using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC_Movement : MonoBehaviour {

    public float linearSpeed = 10.0f;
    public float angularSpeed = 180.0f;
    public float accelerationTime = 0.25f;

    private Rigidbody2D localbody;
    private float acceleration;

	// Use this for initialization
	void Start () {
        localbody = GetComponent<Rigidbody2D>();
        acceleration = linearSpeed / accelerationTime;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        float hMovement = Input.GetAxis("Horizontal");
        float vMovement = Input.GetAxis("Vertical");

        Vector2 targetVelocity = new Vector2(hMovement, vMovement).normalized * linearSpeed;

        targetVelocity = Vector2.MoveTowards(localbody.velocity, targetVelocity, Time.deltaTime * acceleration);

        localbody.velocity = targetVelocity;
        
    }

    public void Halt()
    {
        localbody.velocity = Vector2.zero;
    }
}
