using LinearAlgebraLib;

namespace AdfGL_Cad3Dlib.Utils
{
    public class Mesh
    {
        readonly double[] _vertices;
        readonly int _vertexCount;
        readonly int[] _triangles;
        readonly int _triangleCount;

        public Mesh(double[] vertices, int[] triangles)
        {
            _vertices = vertices.ToArray();
            _vertexCount = vertices.Length / 3;
            _triangles = triangles.ToArray();
            _triangleCount = triangles.Length / 4;
        }

        public double[] Vertices => _vertices;
        public int[] Triangles => _triangles;

        public int VertexCount => _vertexCount;
        public int TriangleCount => _triangleCount;

        public Vec3 GetVertex(int index)
        {
            double x = _vertices[index * 3];
            double y = _vertices[index * 3 + 1];
            double z = _vertices[index * 3 + 2];
            return new Vec3(x, y, z);
        }
        public void SetVertex(int index, Vec3 vertex)
        {
            int offset = index * 3;
            _vertices[offset] = vertex.x;
            _vertices[offset + 1] = vertex.y;
            _vertices[offset + 2] = vertex.z;
        }

        public void GetTriangle(int index, out int a, out int b, out int c)
        {
            int offset = index * 4;
            a = _triangles[offset];
            b = _triangles[offset + 1];
            c = _triangles[offset + 2];
        }
        public void SetTriangle(int index, int a, int b, int c)
        {
            int offset = index * 4;
            _triangles[offset] = a;
            _triangles[offset + 1] = b;
            _triangles[offset + 2] = c;
        }

        public int GetTriangleColor(int index)
        {
            return _triangles[index * 4 + 3];
        }
        public void SetTriangleColor(int index, int color)
        {
            _triangles[index * 4 + 3] = color;
        }
    }
}
