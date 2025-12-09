using System.Numerics;

using Blobby.Math;

using Raylib_cs;

namespace Blobby.Graphics;

public class BlobbyWindow
{
    public BlobbyWindow(int detail, int windowWidth, int windowHeight, int fps = 30)
    {
        var blobby = UvSphere.Create(detail, 1.0f);

        Raylib.InitWindow(windowWidth, windowHeight, "Blobby");
        Raylib.SetTargetFPS(fps);

        var camera = SetupCamera();

        var mesh = MeshConverter.ToRaylibMesh(blobby);
        Raylib.UploadMesh(ref mesh, false);

        var material = Raylib.LoadMaterialDefault();

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.Green);

            Raylib.BeginMode3D(camera);

            Raylib.DrawMesh(mesh, material, Raymath.MatrixIdentity());
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
}
