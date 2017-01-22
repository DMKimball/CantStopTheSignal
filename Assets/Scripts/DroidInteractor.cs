using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroidInteractor : MonoBehaviour, Interactor
{

    private Interactable interactionTarget;
    private bool activelyInteract;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (activelyInteract && interactionTarget != null && Input.GetAxis("Interact") != 0)
        {
            interactionTarget.Interact(gameObject);
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
        activelyInteract = true;
    }

    public void DisableInteraction()
    {
        activelyInteract = false;
    }
}
