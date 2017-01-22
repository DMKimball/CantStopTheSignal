using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMove : MonoBehaviour {

    public Transform[] waypoints = new Transform[0];
    public float linearSpeed = 10.0f;
    public float angularSpeed = 480.0f;
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
            Vector3 targetVelocity = (waypoints[currentDest].position - transform.position).normalized * linearSpeed;
            localbody.velocity = targetVelocity;

            float direction = Mathf.Atan2(targetVelocity.y, targetVelocity.x) * Mathf.Rad2Deg;

            //convert to desired Z rotation
            direction -= 90;
            if (direction < 0) direction += 360;

            float rotation = direction - transform.eulerAngles.z;
            if (rotation > 180) rotation -= 360;
            if (rotation < -180) rotation += 360;

            if (rotation > 0)
            {
                transform.Rotate(new Vector3(0, 0, Mathf.Min(angularSpeed * Time.deltaTime, rotation)), Space.Self);
            }
            else
            {
                transform.Rotate(new Vector3(0, 0, Mathf.Max(-angularSpeed * Time.deltaTime, rotation)), Space.Self);
            }

        }
    }
}
