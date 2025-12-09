using Raylib_cs;

namespace Blobby.Graphics;

public class BlobbyWindow
{
    public BlobbyWindow(int windowWidth, int windowHeight, int fps = 30)
    {
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
