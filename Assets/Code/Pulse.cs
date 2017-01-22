using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulse : MonoBehaviour
{
    public GameObject[] pulses;

    [SerializeField]
    private Vector3 _startSize = new Vector3(0.1f, 0.1f, 0.1f);
    [SerializeField]
    [Range(0,1)]
    private float _scaleRate = 0.1f;
    [SerializeField]
    private float currTime = 0;

    //void Start()
    //{
    //    float _currStartSize = 0;
    //    foreach (var pulse in Pulses)
    //    {
    //        pulse.transform.localScale = new Vector3(_startSize.x + _currStartSize, _startSize.y + _currStartSize, _startSize.z);
    //        _currStartSize +=_startSizeDiff;
    //    }
    //}

	void Update ()
    {
        foreach (var pulse in pulses)
        {
            pulse.transform.localScale = Vector3.Lerp(_startSize, transform.localScale, currTime);
            currTime += _scaleRate;
            if (pulse.transform.localScale.sqrMagnitude >= transform.localScale.sqrMagnitude)
            {
                pulse.transform.localScale = _startSize;
                currTime = 0;
            }
        }
	}
}