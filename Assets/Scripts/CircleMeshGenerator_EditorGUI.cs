using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(CircleMeshGenerator))]
public class CircleMeshGenerator_EditorGUI : Editor
{
    [MenuItem("GameObject/Create Other/2D Circle")]
    static void Create(MenuCommand command)
    {
        GameObject gameObject = new GameObject("Circle");
        CircleMeshGenerator s = gameObject.AddComponent<CircleMeshGenerator>();
        MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();
        meshFilter.mesh = new Mesh();

        GameObjectUtility.SetParentAndAlign(gameObject, command.context as GameObject);
        Undo.RegisterCreatedObjectUndo(gameObject, "Create " + gameObject.name);
        Selection.activeObject = gameObject;
    }

    public override void OnInspectorGUI()
    {
        CircleMeshGenerator obj;
        obj = target as CircleMeshGenerator;
        if(obj == null) return;
        base.DrawDefaultInspector();
        if(obj.material == null) EditorGUILayout.HelpBox("Material required.", MessageType.Error);
        if(GUILayout.Button("Rebuild", GUILayout.Width(150)))
        {
            if(obj.material != null) Rebuild(obj);
        }
    }

    private void Rebuild(CircleMeshGenerator c)
    {
        GameObject go = c.gameObject;
        if(c.material == null) return;
        MeshFilter meshFilter = go.GetComponent<MeshFilter>();
        if(meshFilter == null) return;
        Mesh mesh = meshFilter.sharedMesh;
        if(mesh == null) mesh = new Mesh();
        mesh.Clear();

        if(c.segments < 3) c.segments = 3;

        float step = (2 * Mathf.PI) / c.segments; // forward angle
        float tanStep = Mathf.Tan(step);
        float radStep = Mathf.Cos(step);

        float x = c.radius;
        float y = 0;

        Vector3[] verts = new Vector3[c.segments + 1];
        Vector2[] uvs = new Vector2[c.segments + 1];

        verts[0] = new Vector3(0, 0, 0); // center of circle
        uvs[0] = new Vector2(0.5f, 0.5f);
        for(int i = 1; i < (c.segments + 1); i++)
        {
            float tx = -y;
            float ty = x;
            x += tx * tanStep;
            y += ty * tanStep;
            x *= radStep;
            y *= radStep;
            verts[i] = new Vector3(x, y, 0);
            uvs[i] = new Vector2(0.5f + x / (2 * c.radius), 0.5f + y / (2 * c.radius));
        }

        int idx = 1;
        int indices = (c.segments) * 3;

        int[] tris = new int[indices]; // one triagle for each section (3 verts)
        for(int i = 0; i < (indices); i += 3)
        {
            tris[i + 1] = 0;         //center of circle
            tris[i] = idx;           //next vertex
            if(i >= (indices - 3))
            {
                tris[i + 2] = 1;     // loop on last
            }
            else
            {
                tris[i + 2] = idx + 1; // next vertex	
            }
            idx++;
        }

        mesh.vertices = verts;
        mesh.triangles = tris;
        mesh.uv = uvs;

        if(c.CreateNewMaterial)
        {
            Material newMat = new Material(c.material);
            newMat.name = string.Format("{0} (copy)", newMat.name);
            go.GetComponent<Renderer>().material = newMat;
        }
        else
        {
            go.GetComponent<Renderer>().material = c.material;
        }

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }
}