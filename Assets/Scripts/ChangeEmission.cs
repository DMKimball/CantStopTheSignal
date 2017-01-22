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

    public DeviceState state = Available;

    public Renderer rd;
    public int rdMat;

    public void ChangeToNewState(DeviceState newState)
    {
        Material[] newMats;
        switch (newState)
        {
            case Player:
                newMats = new Material[rd.materials.Length];
                for(int i = 0; i < newMats.Length; i++)
                {
                    if (i == rdMat) newMats[i] = playerEmit;
                    else newMats[i] = rd.materials[i];
                }
                rd.materials = newMats;
                state = Player;
                break;
            case Evil:
                newMats = new Material[rd.materials.Length];
                for (int i = 0; i < newMats.Length; i++)
                {
                    if (i == rdMat) newMats[i] = evilEmit;
                    else newMats[i] = rd.materials[i];
                }
                rd.materials = newMats; state = Evil;
                break;
            case Available:
                newMats = new Material[rd.materials.Length];
                for (int i = 0; i < newMats.Length; i++)
                {
                    if (i == rdMat) newMats[i] = availableEmit;
                    else newMats[i] = rd.materials[i];
                }
                rd.materials = newMats; state = Available;
                break;
            case Secure:
                newMats = new Material[rd.materials.Length];
                for (int i = 0; i < newMats.Length; i++)
                {
                    if (i == rdMat) newMats[i] = secureEmit;
                    else newMats[i] = rd.materials[i];
                }
                rd.materials = newMats; state = Secure;
                break;
        }
    }

    public DeviceState getState() { return state; }
}
