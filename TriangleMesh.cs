using UnityEngine;
using UnityEngine.Rendering;

public class TriangleMesh : MonoBehaviour
{
    public Material material = null;
    private Vector3[] vertices = new Vector3[3];
    private int[] triangles = new int[3];
    private Vector2[] uv = new Vector2[3];
    private float offset = 0.5f;

    private void Start()
    {
        DefineMeshData();
        Mesh mesh = GetMesh();
        SetMesh(mesh);
    }
    private void DefineMeshData()
    {
        vertices[0] = new Vector3(-offset, offset, 0) + this.transform.position;
        vertices[1] = new Vector3(offset, -offset, 0) + this.transform.position;
        vertices[2] = new Vector3(-offset, -offset, 0) + this.transform.position;

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        uv[0] = new Vector2(0, 1);
        uv[1] = new Vector2(1, 0);
        uv[2] = new Vector2(0, 0);
    }
    private Mesh GetMesh()
    {
        Mesh m = null;
        MeshFilter mf = this.gameObject.AddComponent<MeshFilter>();
        MeshRenderer mr = this.gameObject.AddComponent<MeshRenderer>();
        mr.material = material;
        if (Application.isEditor == true)
        {
            m = mf.sharedMesh;
            if (m == null)
            {
                mf.sharedMesh = new Mesh();
                m = mf.sharedMesh;
            }
        }
        else
        {
            m = mf.mesh;
            if (m == null)
            {
                mf.mesh = new Mesh();
                m = mf.mesh;
            }
        }

        return m;
    }
    private void SetMesh(Mesh m)
    {
        m.Clear();

        m.vertices = vertices;
        m.triangles = triangles;
        m.uv = uv;

        m.RecalculateNormals();
        m.RecalculateBounds();
        m.RecalculateTangents();
    }
}
