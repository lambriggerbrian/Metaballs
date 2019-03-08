using System.Collections.Generic;
using UnityEngine;

public struct GridVertVals
{
    public float V0, V1, V2, V3, V4, V5, V6, V7;
}

public struct GridCube
{
    public Vector3 CenterPos;
    public Vector3 V0, V1, V2, V3, V4, V5, V6, V7;
}

public struct GPUBlob
{
    public float Factor;
    public Vector3 Pos;
}

public struct GPUVertices
{
    public int Index;
    public Vector3 V0, V1, V2, V3, V4, V5, V6, V7, V8, V9, V10, V11;
}

public struct GPUBuffers
{
    public ComputeBuffer CubeBuffer;
    public ComputeBuffer ValuesBuffer;
    public ComputeBuffer BlobBuffer;
    public ComputeBuffer VertMapBuffer;
    public ComputeBuffer VertBuffer;
}

public class GridUtils
{
    // Index to vert property
    public static Vector3 FindVertex(GPUVertices vert, int i)
    {
        switch (i)
        {
            case 0: return vert.V0;
            case 1: return vert.V1;
            case 2: return vert.V2;
            case 3: return vert.V3;
            case 4: return vert.V4;
            case 5: return vert.V5;
            case 6: return vert.V6;
            case 7: return vert.V7;
            case 8: return vert.V8;
            case 9: return vert.V9;
            case 10: return vert.V10;
            case 11: return vert.V11;
            default: break;
        }
        throw new System.ArgumentException("Vertex index must be 0-11", "i");
    }

    public static int[] GetTriangles(int vertCount, List<int> tris)
    {
        int num = vertCount;

        // If no Tris, create list
        if (tris == null)
        {
            tris = new List<int>();
            for (int i = 0; i < num; i++)
            {
                tris.Add(i);
            }

            return tris.ToArray();
        }
        else if (tris.Count < num)
        {
            // missing elements in buffer, add them
            for (int i = tris.Count; i < num; i++)
            {
                tris.Add(i);
            }

            return tris.ToArray();
        }
        else if (tris.Count == num)
        {
            // buffer is of perfect size, just return it

            return tris.ToArray();
        }
        else
        {
            // buffer is too long, return slice

            return tris.GetRange(0, num).ToArray();
        }
    }

    public static void DrawGridLines(List<Vector3> lines)
    {
        GL.Begin(GL.LINES);
        for (int i = 0; i < lines.Count; i += 2)
        {
            Vector3 v0 = lines[i];
            Vector3 v1 = lines[i + 1];
            GL.Vertex3(v0.x, v0.y, v0.z);
            GL.Vertex3(v1.x, v1.y, v1.z);
        }
        GL.End();
    }

    public static void DrawGridVerts(GridCube[] cubes)
    {
        GL.Begin(GL.LINES);
        foreach (GridCube cube in cubes)
        {
            DrawX(cube.V0, 0.01f);
            DrawX(cube.V1, 0.01f);
            DrawX(cube.V2, 0.01f);
            DrawX(cube.V3, 0.01f);
            DrawX(cube.V4, 0.01f);
            DrawX(cube.V5, 0.01f);
            DrawX(cube.V6, 0.01f);
            DrawX(cube.V7, 0.01f);
        }
        GL.End();
    }

    private void drawCubePos(GridCube[] cubes)
    {
        GL.Begin(GL.LINES);
        foreach (GridCube cube in cubes)
        {
            DrawX(cube.CenterPos, 0.01f);
        }
        GL.End();
    }

    public static void DrawX(Vector3 p, float size)
    {
        GL.Vertex3(p.x + size, p.y + size, p.z);
        GL.Vertex3(p.x - size, p.y - size, p.z);
        GL.Vertex3(p.x + size, p.y - size, p.z);
        GL.Vertex3(p.x - size, p.y + size, p.z);
    }
}
