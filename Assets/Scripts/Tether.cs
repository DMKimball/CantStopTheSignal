using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tether : MonoBehaviour {

    public Transform anchor;
    public float maxDistance = 10.0f;
	
	// Update is called once per frame
	void Update () {
		if(anchor != null && Vector3.Distance(transform.position, anchor.position) > maxDistance)
        {
            transform.position = anchor.position + (transform.position - anchor.position).normalized * maxDistance;
        }
	}
}
