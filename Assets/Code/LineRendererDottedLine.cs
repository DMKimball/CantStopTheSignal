using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof(LineRenderer))]
public class LineRendererDottedLine : MonoBehaviour
{
    public Transform startPosition;
    public Transform endPosition;
    public Material lineMaterial;

    private LineRenderer _lineRenderer;

    // Use this for initialization
    void Start()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        SetupLine();
    }

    // Update is called once per frame
    void Update()
    {
        SetupLine();
    }

    void SetupLine()
    {
        _lineRenderer.sortingLayerName = "OnTop";
        _lineRenderer.sortingOrder = 5;
        _lineRenderer.numPositions = 2;
        _lineRenderer.SetPosition(0, startPosition.position);
        _lineRenderer.SetPosition(1, endPosition.position);
        _lineRenderer.startWidth = 0.5f;
        _lineRenderer.endWidth = 0.5f;
        _lineRenderer.useWorldSpace = false;
        _lineRenderer.material = lineMaterial;
    }
}