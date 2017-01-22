using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controllable : MonoBehaviour, Interactable {

    public bool secure = false;

	public void Interact(GameObject interactor)
    {
        if (secure) return;
        SwitchControl controller = interactor.GetComponentInChildren<SwitchControl>();
        controller.PassControl(gameObject);
    }
}
