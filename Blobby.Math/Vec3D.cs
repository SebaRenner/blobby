namespace Blobby.Math;

public readonly struct Vec3D
{
    public float X { get; }

    public float Y { get; }

    public float Z { get; }

    public Vec3D(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static Vec3D operator +(Vec3D a, Vec3D b)
        => new(a.X + b.X, a.Y + b.Y, a.Z + b.Z);

    public static Vec3D operator -(Vec3D a, Vec3D b)
        => new(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

    public static Vec3D operator -(Vec3D v)
        => new(-v.X, -v.Y, -v.Z);

    public static Vec3D operator *(Vec3D v, float scalar)
        => new(v.X * scalar, v.Y * scalar, v.Z * scalar);

    public static Vec3D operator /(Vec3D v, float scalar)
        => new(v.X / scalar, v.Y / scalar, v.Z / scalar);
}
