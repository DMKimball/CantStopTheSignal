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

    public const DeviceState
        Player = DeviceState.player,
        Evil = DeviceState.evil,
        Available = DeviceState.available,
        Secure = DeviceState.secure;

    private MeshRenderer _meshRenderer;
    private MeshRenderer _childMeshRenderer;

    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _childMeshRenderer = transform.GetChild(0).GetComponent<MeshRenderer>();
    }

    void Update()
    {
        ChangeToNewState(debugChange);
    }

    public void ChangeToNewState(DeviceState newState)
    {
        switch (newState)
        {
            case Player:
                _meshRenderer.material = player;
                _childMeshRenderer.material = playerPulse;
                break;
            case Evil:
                _meshRenderer.material = evil;
                _childMeshRenderer.material = evilPulse;
                break;
            case Available:
                _meshRenderer.material = available;
                _childMeshRenderer.material = availablePulse;
                break;
            case Secure:
                _meshRenderer.material = secure;
                _childMeshRenderer.material = securePulse;
                break;
        }
    }
}