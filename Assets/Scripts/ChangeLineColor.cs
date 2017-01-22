using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLineColor : MonoBehaviour
{
    public Material playerLine;
    public Material evilLine;
    public Material availableLine;
    public Material secureLine;

    public DeviceState debugChange;

    public const DeviceState
        Player = DeviceState.player,
        Evil = DeviceState.evil,
        Available = DeviceState.available,
        Secure = DeviceState.secure;

    private LineRenderer _lineRenderer;

    // Use this for initialization
    void Start ()
    {
        _lineRenderer = GetComponent<LineRenderer>();	
	}
	
	// Update is called once per frame
	void Update ()
    {
        ChangeToNewState(debugChange);
	}

    public void ChangeToNewState(DeviceState newState)
    {
        switch (newState)
        {
            case Player:
                _lineRenderer.material = playerLine;
                break;
            case Evil:
                _lineRenderer.material = evilLine;
                break;
            case Available:
                _lineRenderer.material = availableLine;
                break;
            case Secure:
                _lineRenderer.material = secureLine;
                break;
        }
    }
}
