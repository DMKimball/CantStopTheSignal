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
        float xMovement = Input.GetAxis("Horizontal");
        float yMovement = Input.GetAxis("Vertical");

        Vector2 targetVelocity = new Vector2(xMovement, yMovement).normalized * linearSpeed;

        targetVelocity = Vector2.MoveTowards(localbody.velocity, targetVelocity, Time.deltaTime * acceleration);

        localbody.velocity = targetVelocity;

        if ((xMovement != 0 || yMovement != 0))
        {
            float direction = Mathf.Atan2(yMovement, xMovement) * Mathf.Rad2Deg; //obtain direction of movement
            float facing = transform.eulerAngles.z; //obtain the direction the object is facing

            //convert to desired Z rotation
            direction -= 90;
            if (direction < 0) direction += 360;

            float rotation = direction - facing;
            if (rotation > 180) rotation -= 360;
            if (rotation < -180) rotation += 360;

            Vector3 camRotation = Vector3.zero;
            Transform child = null;
            for (int i = 0; i < transform.childCount; i++)
            {
                child = transform.GetChild(i);
                if (child.tag == "Player")
                {
                    camRotation = child.eulerAngles;
                    break;
                }
            }

            if (rotation > 0)
            {
                transform.Rotate(new Vector3(0, 0, Mathf.Min(angularSpeed * Time.deltaTime, rotation)), Space.Self);
            }
            else
            {
                transform.Rotate(new Vector3(0, 0, Mathf.Max(-angularSpeed * Time.deltaTime, rotation)), Space.Self);
            }

            if(child) child.eulerAngles = camRotation;


        }

    }

    public void Halt()
    {
        localbody.velocity = Vector2.zero;
    }
}
