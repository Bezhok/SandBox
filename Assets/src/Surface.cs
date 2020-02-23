using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Surface : MonoBehaviour
{
    [SerializeField] private int sizeX=1, sizeY=1;
    
    private MeshFilter _meshFilter;

    void Awake()
    {
        _meshFilter = GetComponent<MeshFilter>();

        _vertices = new Vector3[(sizeX)*(sizeY)];
        int[] indices = new int[(sizeX-1)*(sizeY-1)*6];

        
        float distance = 0.1f;
        for (int j = 0; j < sizeY; j++)
        {
            for (int i = 0; i < sizeX; i++)
            {
                _vertices[i + j*sizeX] = new Vector3(i*distance, j*distance);
            }
        }
        
        int idx = 0;
        Vector2Int[] eboTemplate =
        {
            new Vector2Int(0,0),
            new Vector2Int(0,1), 
            new Vector2Int(1,0), 
            new Vector2Int(1,1), 
            new Vector2Int(1,0), 
            new Vector2Int(0,1), 
        };

        for (int j = 0; j < sizeY-1; j++)
        {
            for (int i = 0; i < sizeX-1; i++)
            {
                foreach (var shift in eboTemplate)
                {
                    indices[idx++] = shift.x + i + (j + shift.y) * sizeX;
                }
            }
        }
        Vector4[] tangents = new Vector4[_vertices.Length];
        Vector4 tangent = new Vector4(1f, 0f, 0f, -1f);
        Vector2[] uvs = new Vector2[sizeX*sizeY];
        for (int j = 0; j < sizeY; j++)
        {
            for (int i = 0; i < sizeX; i++)
            {
                uvs[i + j*sizeX] = new Vector2((float)(i)/sizeY, (float)(j)/sizeY);
                tangents[i + j * sizeX] = tangent;
            }
        }
        
        
        var mesh = new Mesh();
        _meshFilter.mesh = mesh;
        mesh.vertices = _vertices;
        mesh.triangles = indices;
        mesh.uv = uvs;
        mesh.tangents = tangents;
        mesh.RecalculateNormals();
    }

    Vector3[] _vertices;
}
