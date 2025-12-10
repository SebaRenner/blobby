using System.Numerics;

using Blobby.Math;

using Raylib_cs;

namespace Blobby.Graphics;

public class BlobbyWindow
{
    public BlobbyWindow(int detail, int windowWidth, int windowHeight, int fps = 30)
    {
        var blobby = UvSphere.Create(detail, 1.0f);
        var noise = 0.3f;

        Raylib.InitWindow(windowWidth, windowHeight, "Blobby");
        Raylib.SetTargetFPS(fps);

        var camera = SetupCamera();

        var mesh = MeshConverter.ToRaylibMesh(blobby);
        Raylib.UploadMesh(ref mesh, false);

        var material = Raylib.LoadMaterialDefault();

        while (!Raylib.WindowShouldClose())
        {
            blobby.ApplyRandomNoise(noise);
            UpdateMeshVertices(ref mesh, blobby);

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Black);
            Raylib.BeginMode3D(camera);

            Raylib.DrawMesh(mesh, material, Raymath.MatrixIdentity());
            //DrawMeshWireframe(mesh, Color.Black);

            Raylib.EndMode3D();
            Raylib.EndDrawing();
        }

        Raylib.UnloadMesh(mesh);
        Raylib.CloseWindow();
    }

    private Camera3D SetupCamera()
    {
        var camera = new Camera3D();
        camera.Position = new Vector3(3, 2, 5);
        camera.Target = new Vector3(0, 0, 0);
        camera.Up = new Vector3(0, 1, 0);
        camera.FovY = 45.0f;
        camera.Projection = CameraProjection.Perspective;

        return camera;
    }

    private unsafe void UpdateMeshVertices(ref Mesh mesh, UvSphere sphere)
    {
        for (int i = 0; i < sphere.VertexCount; i++)
        {
            var vertex = sphere.Vertices[i];
            mesh.Vertices[i * 3 + 0] = vertex.X;
            mesh.Vertices[i * 3 + 1] = vertex.Y;
            mesh.Vertices[i * 3 + 2] = vertex.Z;
        }

        Raylib.UpdateMeshBuffer(mesh, 0, mesh.Vertices, mesh.VertexCount * 3 * sizeof(float), 0);
    }

    private unsafe void DrawMeshWireframe(Mesh mesh, Color color)
    {
        for (int i = 0; i < mesh.TriangleCount; i++)
        {
            int idx0 = mesh.Indices[i * 3 + 0];
            int idx1 = mesh.Indices[i * 3 + 1];
            int idx2 = mesh.Indices[i * 3 + 2];

            Vector3 v0 = new Vector3(
                mesh.Vertices[idx0 * 3 + 0],
                mesh.Vertices[idx0 * 3 + 1],
                mesh.Vertices[idx0 * 3 + 2]
            );
            Vector3 v1 = new Vector3(
                mesh.Vertices[idx1 * 3 + 0],
                mesh.Vertices[idx1 * 3 + 1],
                mesh.Vertices[idx1 * 3 + 2]
            );
            Vector3 v2 = new Vector3(
                mesh.Vertices[idx2 * 3 + 0],
                mesh.Vertices[idx2 * 3 + 1],
                mesh.Vertices[idx2 * 3 + 2]
            );

            Raylib.DrawLine3D(v0, v1, color);
            Raylib.DrawLine3D(v1, v2, color);
            Raylib.DrawLine3D(v2, v0, color);
        }
    }
}
