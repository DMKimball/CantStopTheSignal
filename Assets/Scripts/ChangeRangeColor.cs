using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DeviceState
{
    player,
    evil,
    available,
    secure
}


public class ChangeRangeColor : MonoBehaviour
{
    public Material player;
    public Material playerPulse;
    public Material evil;
    public Material evilPulse;
    public Material available;
    public Material availablePulse;
    public Material secure;
    public Material securePulse;

    public DeviceState debugChange;

    public bool useDebug = false;

    public const DeviceState
        Player = DeviceState.player,
        Evil = DeviceState.evil,
        Available = DeviceState.available,
        Secure = DeviceState.secure;

    private MeshRenderer _meshRenderer;
    private MeshRenderer _childMeshRenderer;

    private bool hasChildren = true;

    void Start()
    {
        hasChildren = (transform.childCount != 0);
        _meshRenderer = GetComponent<MeshRenderer>();
        if(hasChildren) _childMeshRenderer = transform.GetChild(0).GetComponent<MeshRenderer>();
    }

    void Update()
    {
        if(useDebug) ChangeToNewState(debugChange);
    }

    public void ChangeToNewState(DeviceState newState)
    {
        switch (newState)
        {
            case Player:
                debugChange = Player;
                _meshRenderer.material = player;
                if (hasChildren) _childMeshRenderer.material = playerPulse;
                break;
            case Evil:
                debugChange = Evil;
                _meshRenderer.material = evil;
                if (hasChildren) _childMeshRenderer.material = evilPulse;
                break;
            case Available:
                debugChange = Available;
                _meshRenderer.material = available;
                if (hasChildren) _childMeshRenderer.material = availablePulse;
                break;
            case Secure:
                debugChange = Secure;
                _meshRenderer.material = secure;
                if (hasChildren) _childMeshRenderer.material = securePulse;
                break;
        }
    }

    public DeviceState GetState()
    {
        return debugChange;
    }
}