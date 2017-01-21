using UnityEngine;
using System.Collections;

public class Range : MonoBehaviour
{
    public float borderWidth = 5;

    [SerializeField]
    [Range(0,1)]
    private float _alpha = 0.5f;

    private Material _rendererMaterial;

    // Use this for initialization
    void Start()
    {
        _rendererMaterial = GetComponent<MeshRenderer>().material;
        Color c = Color.blue;
        c.a = _alpha;
        _rendererMaterial.color = c;
    }

    // Update is called once per frame
    void Update()
    {
    }
}