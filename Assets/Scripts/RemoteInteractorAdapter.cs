using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteInteractorAdapter : MonoBehaviour, Interactor {

    public RemoteInteractor remote;

    public void DisableInteraction()
    {
        remote.DisableInteraction();
    }

    public void EnableInteraction()
    {
        remote.EnableInteraction();
    }
}
