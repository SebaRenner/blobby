using System.Numerics;
using System.Runtime.InteropServices;

using Blobby.Math;

using Raylib_cs;

namespace Blobby.Graphics;

public static class MeshConverter
{
    public static unsafe Mesh ToRaylibMesh(UvSphere sphere)
    {
        var mesh = new Mesh();

        mesh.VertexCount = sphere.VertexCount;
        mesh.TriangleCount = sphere.TriangleCount;

        mesh.Vertices = (float*)Marshal.AllocHGlobal(sphere.VertexCount * 3 * sizeof(float));
        for (int i = 0; i < sphere.VertexCount; i++)
        {
            mesh.Vertices[i * 3 + 0] = sphere.Vertices[i].X;
            mesh.Vertices[i * 3 + 1] = sphere.Vertices[i].Y;
            mesh.Vertices[i * 3 + 2] = sphere.Vertices[i].Z;
        }

        mesh.Normals = (float*)Marshal.AllocHGlobal(sphere.VertexCount * 3 * sizeof(float));
        for (int i = 0; i < sphere.VertexCount; i++)
        {
            var v = sphere.Vertices[i];
            var n = Vector3.Normalize(new Vector3(v.X, v.Y, v.Z));

            mesh.Normals[i * 3 + 0] = n.X;
            mesh.Normals[i * 3 + 1] = n.Y;
            mesh.Normals[i * 3 + 2] = n.Z;
        }

        mesh.Indices = (ushort*)Marshal.AllocHGlobal(sphere.Indices.Length * sizeof(ushort));
        for (int i = 0; i < sphere.Indices.Length; i++)
        {
            mesh.Indices[i] = (ushort)sphere.Indices[i];
        }

        return mesh;
    }
}
