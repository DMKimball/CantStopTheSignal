using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteInteractor : MonoBehaviour
{
    public GameObject master;

    private Interactable interactionTarget;
    private MeshRenderer meshRender;
    private PC_Movement pc_move;

    void Awake()
    {
        pc_move = GetComponent<PC_Movement>();
        meshRender = GetComponentInChildren<MeshRenderer>();
    }

    // Use this for initialization
    void Start()
    {
        RemoteInteractorAdapter adapter = master.GetComponentInChildren<RemoteInteractorAdapter>();
        if (adapter) adapter.remote = this;
        else
        {
            adapter = master.AddComponent<RemoteInteractorAdapter>();
            adapter.remote = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (interactionTarget != null && Input.GetAxis("Interact") != 0)
        {
            interactionTarget.Interact(master);
        }
    }

    public void OnTriggerEnter2D(Collider2D collider)
    {
        Interactable target = collider.gameObject.GetComponent<Interactable>();
        if (target != null) interactionTarget = target;
    }

    public void OnTriggerExit2D(Collider2D collider)
    {
        Interactable target = collider.gameObject.GetComponent<Interactable>();
        if (target == interactionTarget) interactionTarget = null;
    }

    public void EnableInteraction()
    {
        enabled = true;
        pc_move.enabled = true;
        meshRender.enabled = true;
    }

    public void DisableInteraction()
    {
        enabled = false;
        pc_move.enabled = false;
        meshRender.enabled = false;
        transform.localPosition = Vector3.zero;
    }
}
