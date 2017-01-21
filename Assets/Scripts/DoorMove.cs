using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMove : MonoBehaviour {
    
    public float progress = 0.0f; // percentage towards being open
    public float speed = 1.0f;
    public Vector3 displacement;
    public int opening = 0;

    private Vector3 openLocation;
    private Vector3 closedLocation;

	// Use this for initialization
	void Start () {
        openLocation = transform.position + displacement;
        closedLocation = transform.position;
        progress = Mathf.Clamp(progress, 0.0f, 1.0f);
        transform.position = Vector3.Lerp(closedLocation, openLocation, progress);
	}
	
	// Update is called once per frame
	void Update () {
		if(opening > 0)
        {
            progress = Mathf.Clamp(progress + speed * Time.deltaTime / displacement.magnitude, 0.0f, 1.0f);
            transform.position = Vector3.Lerp(closedLocation, openLocation, progress);
            if (progress == 1.0f) Stop();
        } else if (opening < 0)
        {
            progress = Mathf.Clamp(progress - speed * Time.deltaTime / displacement.magnitude, 0.0f, 1.0f);
            transform.position = Vector3.Lerp(closedLocation, openLocation, progress);
            if (progress == 0.0f) Stop();
        }
	}

    public void Open() { opening = 1; }
    public void Close() { opening = -1; }
    public void Stop() { opening = 0; }
}
