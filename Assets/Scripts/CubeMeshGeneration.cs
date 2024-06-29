using UnityEngine;

[RequireComponent (typeof(MeshFilter))]
[RequireComponent (typeof(MeshRenderer))]
public class CubeMeshGeneration : MonoBehaviour
{
    [SerializeField] float widthCube;
    [SerializeField] float lengthCube;
    [SerializeField] float heightCube;

    public float LengthCubeY
    {
        get { return lengthCube; }
        set { lengthCube = value; }
    }

    private void Start()
    {
        GetComponent<MeshFilter>().mesh = Cube(widthCube, LengthCubeY, heightCube);
    }

    public Mesh Quad(Vector3 startPoint, Vector3 width, Vector3 length)
    {
        var mesh = new Mesh
        {
            vertices = new[] { startPoint, startPoint + length, startPoint + length + width, startPoint + width },
            triangles = new[] { 0, 1, 2, 0, 2, 3 }
        };

        return mesh;
    }

    public Mesh Cube(float widthcube, float lengthCube, float heightCube)
    {
        Vector3 width = new Vector3(widthcube, 0.0f, 0.0f);
        Vector3 length = new Vector3(0.0f, lengthCube, 0.0f);
        Vector3 height = new Vector3(0.0f, 0.0f, heightCube);

        Vector3 corner0 = (width / 2 + length / 2 + height / 2);
        Vector3 corner1 = (-width / 2 - length / 2 - height / 2);

        var edge = new CombineInstance[6];
        edge[0].mesh = Quad(corner0, -length, -width);
        edge[1].mesh = Quad(corner0, -width, -height);
        edge[2].mesh = Quad(corner0, -height, -length);
        edge[3].mesh = Quad(corner1, width, length);
        edge[4].mesh = Quad(corner1, height, width);
        edge[5].mesh = Quad(corner1, length, height);

        var mesh = new Mesh();

        mesh.CombineMeshes(edge, true, false);

        mesh.RecalculateNormals();

        return mesh;
    }
}
