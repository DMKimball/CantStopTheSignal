using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointJump : MonoBehaviour {

    public Transform[] waypoints = new Transform[0];
    public float jumpCooldown = 0.25f;
    public int currLocIndex = 0;
    public bool adjustingCamera = false;
    public float cameraSpeed = 50.0f;

    private bool jumpReady = true;
    private float totalDistance;

    private static Transform player;

    void Awake()
    {
        if (!player) player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    

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
            adjustingCamera = true;
            totalDistance = Vector3.Distance(transform.position, player.position);
        }
        if(jumpReady && input > 0)
        {
            currLocIndex = (currLocIndex == 0) ? waypoints.Length - 1 : currLocIndex - 1;
            transform.position = waypoints[currLocIndex].position;
            StartCoroutine("JumpCooldown");
            adjustingCamera = true;
            totalDistance = Vector3.Distance(transform.position, player.position);
        }
        if (adjustingCamera)
        {
            Debug.Log("adjustingCamera");
            float progress = 1.0f - (Vector3.Distance(transform.position,player.position) - cameraSpeed * Time.deltaTime) / totalDistance;
            player.position = Vector3.Lerp(player.position, transform.position, progress);
            if (player.position == transform.position)
            {
                adjustingCamera = false;
            }
        }
    }

    IEnumerator JumpCooldown()
    {
        jumpReady = false;
        yield return new WaitForSecondsRealtime(jumpCooldown);
        jumpReady = true;
    }
}
