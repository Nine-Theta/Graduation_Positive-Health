using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Rendering;

[RequireComponent(typeof(MeshRenderer), typeof(MeshFilter))]
public class SpiderGraphScript : MonoBehaviour
{
    public Mesh SpiderMesh;

    [Range(1, 10)]
    public int[] Stats = new int[6] { 10, 10, 10, 10, 10, 10 };

    private Vector3[] _graphDirs = new Vector3[6]
    {
        new Vector3(0, 1, 0).normalized * 0.1f,
        new Vector3(Mathf.Sqrt(0.75f), 0.5f, 0).normalized *0.1f,
        new Vector3(Mathf.Sqrt(0.75f), -0.5f, 0).normalized *0.1f,
        new Vector3(0, -1, 0).normalized *0.1f,
        new Vector3(-Mathf.Sqrt(0.75f), -0.5f, 0).normalized *0.1f,
        new Vector3(-Mathf.Sqrt(0.75f), 0.5f, 0).normalized *0.1f
    };

    [Button]
    public void CreateMesh()
    {
        #region meshdef 

        Vector3[] verts = new Vector3[]
        {
            new Vector3(0,0,0),
            _graphDirs[0]*Stats[0], _graphDirs[1]*Stats[1], _graphDirs[2]*Stats[2],
            _graphDirs[3]*Stats[3], _graphDirs[4]*Stats[4], _graphDirs[5]*Stats[5]
        };

        int[] triangles = new int[]
        {
            0,1,2,
            0,2,3,
            0,3,4,
            0,4,5,
            0,5,6,
            0,6,1
        };

        Vector3 normal = new Vector3(0, 0, -1);

        Vector3[] normals = new Vector3[]
        {
            normal,
            normal, normal, normal,
            normal, normal, normal
        };

        #endregion

        SpiderMesh = new Mesh();
        SpiderMesh.name = "SpiderwebGraph";
        SpiderMesh.vertices = verts;
        SpiderMesh.triangles = triangles;
        SpiderMesh.normals = normals;
        //SpiderMesh.Optimize();

        GetComponent<MeshFilter>().mesh = SpiderMesh;
        GetComponent<MeshRenderer>().material = new Material(GraphicsSettings.defaultRenderPipeline.defaultShader);
    }

    [Button]
    public void UpdateSpiderWeb()
    {
        Vector3[] verts = new Vector3[]
        {
            new Vector3(0,0,0),
            _graphDirs[0]*Stats[0], _graphDirs[1]*Stats[1], _graphDirs[2]*Stats[2],
            _graphDirs[3]*Stats[3], _graphDirs[4]*Stats[4], _graphDirs[5]*Stats[5]
        };

        GetComponent<MeshFilter>().mesh.vertices = verts;
    }
}
