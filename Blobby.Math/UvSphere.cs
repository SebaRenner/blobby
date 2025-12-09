namespace Blobby.Math;

public class UvSphere
{
    private const double PI = System.Math.PI;

    public Vec3D[] Vertices { get; }

    public int[] Indices { get; }

    public int VertexCount => Vertices.Length;

    public int TriangleCount => Indices.Length / 3;

    private UvSphere(Vec3D[] vertices, int[] indices)
    {
        Vertices = vertices;
        Indices = indices;
    }

    public static UvSphere Create(int detail, float radius = 1f)
    {
        var numLatitudes = detail;
        var numLongitudes = detail * 2;

        var vertices = CreateVertices(numLatitudes, numLongitudes, radius);
        var indices = CreateIndices(numLatitudes, numLongitudes);

        return new UvSphere(vertices.ToArray(), indices);
    }

    private static IEnumerable<Vec3D> CreateVertices(int numLatitudes, int numLongitudes, float radius)
    {
        for (int lat = 0; lat <= numLatitudes; lat++)
        {
            var theta = (float)(lat * PI / numLatitudes);
            var sinTheta = MathF.Sin(theta);
            var cosTheta = MathF.Cos(theta);

            for (int lon = 0; lon <= numLongitudes; lon++)
            {
                var phi = (float)(lon * 2 * PI / numLongitudes);
                var sinPhi = MathF.Sin(phi);
                var cosPhi = MathF.Cos(phi);

                var x = radius * sinTheta * cosPhi;
                var y = radius * cosTheta;
                var z = radius * sinTheta * sinPhi;

                yield return new Vec3D(x, y, z);
            }
        }
    }

    private static int[] CreateIndices(int numLatitudes, int numLongitudes)
    {
        var numQuads = numLatitudes * numLongitudes;
        var indices = new int[numQuads * 6]; // 1 quad = 2 triangles = 6 indices

        var indexPtr = 0;
        for (var lat = 0; lat < numLatitudes; lat++)
        {
            for (var lon = 0; lon < numLongitudes; lon++)
            {
                var current = lat * (numLongitudes + 1) + lon;
                var next = current + numLongitudes + 1;

                // First triangle (top-left, bottom-left, bottom-right)
                indices[indexPtr++] = current;
                indices[indexPtr++] = next;
                indices[indexPtr++] = next + 1;

                // Second triangle (top-left, bottom-right, top-right)
                indices[indexPtr++] = current;
                indices[indexPtr++] = next + 1;
                indices[indexPtr++] = current + 1;
            }
        }

        return indices;
    }
}
