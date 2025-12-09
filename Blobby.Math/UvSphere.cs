namespace Blobby.Math;

public class UvSphere
{
    private const double PI = System.Math.PI;

    public IEnumerable<Vec3D> Vertices { get; }

    private UvSphere(IEnumerable<Vec3D> vertices)
    {
        Vertices = vertices;
    }

    public static UvSphere Create(int detail, float radius = 1f)
    {
        int numLatitudes = detail;
        int numLongitudes = detail * 2;

        List<Vec3D> vertices = new();

        for (int lat = 0; lat <= numLatitudes; lat++)
        {
            float theta = (float)(lat * PI / numLatitudes);

            for (int lon = 0; lon <= numLongitudes; lon++)
            {
                float phi = (float)(lon * 2 * PI / numLongitudes);

                float sinTheta = MathF.Sin(theta);
                float cosTheta = MathF.Cos(theta);
                float sinPhi = MathF.Sin(phi);
                float cosPhi = MathF.Cos(phi);

                float x = radius * sinTheta * cosPhi;
                float y = radius * cosTheta;
                float z = radius * sinTheta * sinPhi;

                vertices.Add(new Vec3D(x, y, z));
            }
        }

        return new UvSphere(vertices);
    }
}
