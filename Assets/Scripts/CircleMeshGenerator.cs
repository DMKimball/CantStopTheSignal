using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class CircleMeshGenerator : MonoBehaviour
{
    public float radius = 10f;
    public int segments = 60;
    public bool CreateNewMaterial = false;
    public Material material;
}