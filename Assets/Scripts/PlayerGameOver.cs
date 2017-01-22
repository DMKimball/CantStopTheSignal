using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGameOver : MonoBehaviour
{
	void Update ()
    {
        if (Input.GetButtonDown("Interact"))
        {
            GameManager.instance.InitLevel();
        }
	}
}