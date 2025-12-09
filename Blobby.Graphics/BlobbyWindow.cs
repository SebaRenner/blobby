using Raylib_cs;
using Blobby.Math;

namespace Blobby.Graphics;

public class BlobbyWindow
{
    public BlobbyWindow(int detail, int windowWidth, int windowHeight, int fps = 30)
    {
        var blobby = UvSphere.Create(detail, 2.0f);

        Raylib.InitWindow(windowWidth, windowHeight, "Blobby");

        Raylib.SetTargetFPS(fps);

        while (!Raylib.WindowShouldClose())
        {
            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.White);

            Raylib.EndDrawing();
        }

        Raylib.CloseWindow();
    }
}
