using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PC_Movement2 : MonoBehaviour {

    public float linearSpeed = 10.0f;
    public float angularSpeed = 180.0f;

    private Rigidbody localbody;

    // Use this for initialization
    void Start () {
        localbody = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        float hMovement = Input.GetAxis("Horizontal");
        float vMovement = Input.GetAxis("Vertical");

        Vector3 newPos = new Vector3(localbody.position.x, localbody.position.y, localbody.position.z);

        if(hMovement != 0 && vMovement != 0) //this case prevents player from moving faster by going diagonally
        {
            newPos.x += hMovement * linearSpeed * Time.deltaTime / Mathf.Sqrt(2);
            newPos.z += vMovement * linearSpeed * Time.deltaTime / Mathf.Sqrt(2);
            localbody.MovePosition(newPos);
        } else if(hMovement != 0 || vMovement != 0)
        {
            newPos.x += hMovement * linearSpeed * Time.deltaTime;
            newPos.z += vMovement * linearSpeed * Time.deltaTime;
            localbody.MovePosition(newPos);
        }
    }
}
