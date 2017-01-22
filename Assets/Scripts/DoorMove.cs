using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMove : MonoBehaviour, Interactable
{

    public float progress = 0.0f; // percentage towards being open
    public float speed = 1.0f;
    public Vector3 displacement;
    public int opening = 0;
    public bool automatic = true;
    public bool playerAutoOnly = true;

    private Vector3 openLocation;
    private Vector3 closedLocation;
    private int detectionCapacity = 0;

    // Use this for initialization
    void Start()
    {
        openLocation = transform.position + displacement;
        closedLocation = transform.position;
        progress = Mathf.Clamp(progress, 0.0f, 1.0f);
        transform.position = Vector3.Lerp(closedLocation, openLocation, progress);
    }

    // Update is called once per frame
    void Update()
    {
        if (opening > 0)
        {
            progress = Mathf.Clamp(progress + speed * Time.deltaTime / displacement.magnitude, 0.0f, 1.0f);
            transform.position = Vector3.Lerp(closedLocation, openLocation, progress);
            if (progress == 1.0f) Stop();
        }
        else if (opening < 0)
        {
            progress = Mathf.Clamp(progress - speed * Time.deltaTime / displacement.magnitude, 0.0f, 1.0f);
            transform.position = Vector3.Lerp(closedLocation, openLocation, progress);
            if (progress == 0.0f) Stop();
        }
    }

    public void Open() { opening = 1; }
    public void Close() { opening = -1; }
    public void Stop() { opening = 0; }

    public void Interact(GameObject interactor)
    {
        Open();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(playerAutoOnly) {
            if (collision.tag == "Player")
            {
                detectionCapacity++;
                Open();
            }
            else foreach (Transform child in collision.transform) {
                    if (child.tag == "Player")
                    {
                        Open();
                        detectionCapacity++;
                        break;
                    }
                }
        }
        else
        {
            Open();
            detectionCapacity++;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (playerAutoOnly)
        {
            if (collision.tag == "Player")
            {
                detectionCapacity--;
            }
            else foreach (Transform child in collision.transform)
                {
                    if (child.tag == "Player")
                    {
                        detectionCapacity--;
                        break;
                    }
                }
        }
        else
        {
            detectionCapacity--;
        }

        if(detectionCapacity <= 0) Close();
    }
}
