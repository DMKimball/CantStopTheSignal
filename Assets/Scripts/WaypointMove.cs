using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMove : MonoBehaviour {

    public Transform[] waypoints = new Transform[0];
    public float speed = 10.0f;
    public float arriveDist = 0.1f;

    private int currentDest = 0;
    private Rigidbody2D localbody;

	// Use this for initialization
	void Start () {

        localbody = GetComponent<Rigidbody2D>();
        

    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (waypoints.Length > 0)
        {
            if (Vector3.Distance(transform.position, waypoints[currentDest].position) <= arriveDist)
            {
                currentDest = (currentDest + 1) % waypoints.Length;
            }
            Vector3 targetVelocity = (waypoints[currentDest].position - transform.position).normalized * speed;
            localbody.velocity = targetVelocity;
        }
    }
}
