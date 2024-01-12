using UnityEngine;

public class WaterTerrain : MonoBehaviour
{
    Mesh mesh;

    Vector3[] vertices;
    Vector2[] theUV;
    Vector3[] normals;
    Vector4[] tangents;
    int[] triangles;

    public int xSize = 20;
    public int zSize = 20;

    public float waveSpeed = 1f;
    public float waveHeight = 1f;

    public float offsetX = 100, offsetY = 100;

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        offsetX = Random.Range(0f, 9999f);
        offsetY = Random.Range(0f, 9999f);
        CreateMeshShape();
        UpdateMesh();
    }

    /// <summary>
    /// Updates the water to move like waves.
    /// Now called from inside the water culling script on camera instead of its own update.
    /// </summary>
    public void UpdateWater()
    {
        CreateMeshShape();
        offsetX += Time.deltaTime * waveSpeed;
        offsetY += Time.deltaTime * waveSpeed;
        UpdateMesh();
    }

    void CreateMeshShape()
    {
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        DrawVertices();

        triangles = new int[xSize * zSize * 6];

        int verticeNumber = 0;
        int triangleNumber = 0;

        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[triangleNumber + 0] = verticeNumber + 0;
                triangles[triangleNumber + 1] = verticeNumber + xSize + 1;
                triangles[triangleNumber + 2] = verticeNumber + 1;
                triangles[triangleNumber + 3] = verticeNumber + 1;
                triangles[triangleNumber + 4] = verticeNumber + xSize + 1;
                triangles[triangleNumber + 5] = verticeNumber + xSize + 2;

                verticeNumber++;
                triangleNumber += 6;
            }

            verticeNumber++;
        }
    }
    /// <summary>
    /// Draws new vertices based on Perlin Noise value
    /// </summary>
    void DrawVertices()
    {
        theUV = new Vector2[vertices.Length];
        tangents = new Vector4[vertices.Length];
        Vector4 tangent = new Vector4(0, 0, 1, -1);
        for (int iVertices = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                float y = Mathf.PerlinNoise(x * .3f + offsetX, z * .3f + offsetY) * waveHeight;
                vertices[iVertices] = new Vector3(x - xSize / 2, y, z - zSize / 2);
                theUV[iVertices] = new Vector2((float)x / xSize, (float)z / zSize);
                tangents[iVertices] = tangent;
                iVertices++;
            }
        }
    }
    /// <summary>
    /// Updates the mesh by clearing it and updating it with new values for vertices
    /// </summary>
    void UpdateMesh()
    {
        mesh.Clear();

        mesh.vertices = vertices;
        mesh.uv = theUV;
        mesh.tangents = tangents; 
        mesh.triangles = triangles;

        mesh.normals = normals;

        mesh.RecalculateNormals();
    }

    private void OnDrawGizmos()
    {
        if (vertices == null)
            return;

        Gizmos.color = Color.yellow;
        for (int iVertices = 0; iVertices < vertices.Length; iVertices++)
        {
            Gizmos.DrawSphere(vertices[iVertices], .1f);
        }
    }
}
