using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshGen
{
    public MeshData GenTerrainMesh(float[,] heighMap, float heightMult, AnimationCurve hCurve) 
    {
        int width = heighMap.GetLength(0);
        int height = heighMap.GetLength(1);

        float TLX = (width - 1) / -2f; // TLX - top left x
        float TLZ = (height - 1) / 2f;

        MeshData meshData = new MeshData(width, height);
        int vertexIndex = 0;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                meshData.meshVertices[vertexIndex] = new Vector3(TLX + x, hCurve.Evaluate(heighMap[x, y]) * heightMult, TLZ - y);
                meshData.UVs[vertexIndex] = new Vector2(x / (float)width, y / (float)height);
                if (x < (width -1) && y < (height -1)) 
                {
                    meshData.AddTriangles(vertexIndex, vertexIndex + width + 1, vertexIndex + width );
                    meshData.AddTriangles(vertexIndex + width + 1, vertexIndex, vertexIndex + 1);

                }
                vertexIndex++;
            }
        }

        return meshData;
    }
}

public class MeshData
{
    public Vector3[] meshVertices;
    public int[] meshTriangles;
    public Vector2[] UVs;


    int trianleIndex;

    public MeshData(int width, int height) 
    {
        meshVertices = new Vector3[width * height];
        UVs = new Vector2[width * height]; 
        meshTriangles = new int[((width - 1) * (height - 1)) * 6];
    }

    public void AddTriangles(int a, int b, int c )  //3 vertices
    {
        meshTriangles[trianleIndex] = a;
        meshTriangles[trianleIndex + 1] = b;
        meshTriangles[trianleIndex + 2] = c;

        trianleIndex += 3;
    }

    public Mesh CreateMesh() 
    {
        Mesh mesh = new Mesh();
        mesh.vertices = meshVertices;
        mesh.triangles = meshTriangles;
        mesh.uv = UVs;
        mesh.RecalculateNormals();

        return mesh;
    }
}
