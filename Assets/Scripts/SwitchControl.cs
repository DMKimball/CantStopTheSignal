using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchControl : MonoBehaviour {

    private const string camName = "CameraHolder";
    private static SwitchControl currentController;
    private static float speed = 50.0f;

    private GameObject cam;
    private bool adjustingCamera = false;
    private Vector3 camStartPos;
    public PC_Movement pc_move;

	// Use this for initialization
	void Start () {
        if (cam == null) cam = GameObject.Find(camName);
		if (transform.FindChild(camName)) currentController = this;
        
        pc_move = GetComponent<PC_Movement>();
	}
	
	// Update is called once per frame
	void Update () {
		if(adjustingCamera)
        {
            float progress = 1.0f - (cam.transform.localPosition.magnitude - speed * Time.deltaTime) / camStartPos.magnitude;
            Vector3 newPos = Vector3.Lerp(camStartPos, Vector3.zero, progress);
            cam.transform.localPosition = newPos;
            if(newPos == Vector3.zero)
            {
                adjustingCamera = false;
                pc_move.enabled = true;
            }
        }
	}

    public void PassControl(SwitchControl newController)
    {
        if (currentController == this)
        {
            currentController = newController;
            cam.transform.parent = newController.gameObject.transform;
            newController.adjustingCamera = true;
            newController.cam = cam;
            newController.camStartPos = cam.transform.localPosition;
            pc_move.Halt();
            pc_move.enabled = false;
            cam = null;
        }
    }
}
