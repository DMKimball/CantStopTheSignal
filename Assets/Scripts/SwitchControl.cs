using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchControl : MonoBehaviour {

    public float speed = 50.0f;
    public float interactorDowntime = 1.0f;

    public AudioSource audioSource;
    public AudioClip[] transferSFX;
    
    private bool adjustingCamera = false;
    private Vector3 startPos;
    private PC_Movement pc_move;
    private WaypointMove wp_move;
    private Interactor interactor;

	// Use this for initialization
	void Start () {
        pc_move = GetComponentInParent<PC_Movement>();
        wp_move = GetComponentInParent<WaypointMove>();
        interactor = GetComponentInParent<Interactor>();
        if (interactor != null) interactor.EnableInteraction();
    }
	
	// Update is called once per frame
	void Update () {
		if(adjustingCamera)
        {
            float progress = 1.0f - (transform.localPosition.magnitude - speed * Time.deltaTime) / startPos.magnitude;
            Vector3 newPos = Vector3.Lerp(startPos, Vector3.zero, progress);
            transform.localPosition = newPos;
            if(newPos == Vector3.zero)
            {
                adjustingCamera = false;
                if(pc_move) pc_move.enabled = true;
                if (wp_move) wp_move.enabled = true;
                wp_move = GetComponentInParent<WaypointMove>();
            }
        }
	}
        
    public void PassControl(GameObject newController)
    {
        if (pc_move)
        {
            pc_move.Halt();
            pc_move.enabled = false;
        }
        if (interactor != null) interactor.DisableInteraction();
        WaypointMove targetMovement = newController.GetComponent<WaypointMove>();
        if (targetMovement) targetMovement.enabled = false;
        transform.parent = newController.gameObject.transform;
        adjustingCamera = true;
        startPos = transform.localPosition;
        pc_move = GetComponentInParent<PC_Movement>();
        interactor = GetComponentInParent<Interactor>();

        if (audioSource.isPlaying) return; // don't play a new sound while the last hasn't finished
        audioSource.clip = transferSFX[Random.Range(0, 1)];
        audioSource.Play();

        StartCoroutine("DelayInteraction");
    }

    IEnumerator DelayInteraction()
    {
        yield return new WaitForSeconds(interactorDowntime);
        if(interactor != null) interactor.EnableInteraction();
    }
}
