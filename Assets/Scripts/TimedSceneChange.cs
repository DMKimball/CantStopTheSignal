using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimedSceneChange : MonoBehaviour
{
    public string nextScene;

	// Use this for initialization
	void Start () {
        StartCoroutine(TimedChange());
	}

    IEnumerator TimedChange()
    {
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(nextScene);
    }
}
