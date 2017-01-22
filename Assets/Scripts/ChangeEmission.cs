using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEmission : MonoBehaviour {

    public const DeviceState
        Player = DeviceState.player,
        Evil = DeviceState.evil,
        Available = DeviceState.available,
        Secure = DeviceState.secure;

    public Material playerEmit;
    public Material availableEmit;
    public Material secureEmit;
    public Material evilEmit;

    public Renderer rd;
    public int rdMat;

    public void ChangeToNewState(DeviceState newState)
    {
        switch (newState)
        {
            case Player:
                rd.materials[rdMat] = playerEmit;
                break;
            case Evil:
                rd.materials[rdMat] = evilEmit;
                break;
            case Available:
                rd.materials[rdMat] = availableEmit;
                break;
            case Secure:
                rd.materials[rdMat] = secureEmit;
                break;
        }
    }
}
