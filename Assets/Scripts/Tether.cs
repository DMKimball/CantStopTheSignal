using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tether : MonoBehaviour {

    public Transform anchor;
    public float maxDistance = 10.0f;

    void Start()
    {
        maxDistance = transform.parent.FindChild("WirelessCircle").transform.localScale.x/2.0f;

    }
	
	// Update is called once per frame
	void Update () {
		if(anchor != null && Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.y), new Vector3(anchor.position.x, 0, anchor.position.y)) > maxDistance)
        {
            float yPos = transform.position.z;
            transform.position = anchor.position + (transform.position - anchor.position).normalized * maxDistance;
            transform.position = new Vector3(transform.position.x, transform.position.y, yPos);
        }
	}
}
