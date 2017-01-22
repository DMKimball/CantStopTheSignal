using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controllable : MonoBehaviour, Interactable {

	public void Interact(GameObject interactor)
    {
        interactor.GetComponentInChildren<SwitchControl>().PassControl(gameObject);
    }
}
