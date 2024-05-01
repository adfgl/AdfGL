using LinearAlgebraLib;

namespace AdfGL_Cad3Dlib.Utils
{
    public class Mesh
    {
        readonly double[] _vertices;
        readonly int _vertexCount;
        readonly int[] _triangles;
        readonly int _triangleCount;

        Mesh(int vertexCount, int triangleCount)
        {
            _vertices = new double[vertexCount * 3];
            _vertexCount = vertexCount;
            _triangles = new int[triangleCount * 4];
            _triangleCount = triangleCount;
        }

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

        public Mesh Forward(Trans3 transformation)
        {
            Vec3 v;
            for (int i = 0; i < _vertexCount; i++)
            {
                v = GetVertex(i);
                SetVertex(i, transformation.Forward(v));
            }
            return this;
        }

        public Mesh Backward(Trans3 transformation)
        {
            Vec3 v;
            for (int i = 0; i < _vertexCount; i++)
            {
                v = GetVertex(i);
                SetVertex(i, transformation.Backward(v));
            }
            return this;
        }

        public static Mesh Load(string path)
        {
            using (var stream = File.OpenRead(path))
            {
                return Load(stream);
            }
        }

        public static Mesh Load(Stream stream)
        {
            List<Vec3> vertices = new List<Vec3>();
            List<Tuple<int, int, int>> faces = new List<Tuple<int, int, int>>();
            using (StreamReader reader = new StreamReader(stream))
            {
                string line;
                while ((line = reader.ReadLine()!) != null)
                {
                    string[] parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                    if (parts.Length == 0)
                        continue;

                    if (parts[0] == "v" && parts.Length >= 4)
                    {
                        // Parse vertex positions
                        float x = float.Parse(parts[1]);
                        float y = float.Parse(parts[2]);
                        float z = float.Parse(parts[3]);
                        vertices.Add(new Vec3(-x, -y, z));
                    }
                    else if (parts[0] == "f" && parts.Length >= 4)
                    {
                        // Parse face indices
                        int[] indices = new int[parts.Length - 1];
                        for (int i = 1; i < parts.Length; i++)
                        {
                            int index = int.Parse(parts[i].Split('/')[0]);
                            // OBJ indices are 1-based, so subtract 1 to convert to 0-based indexing
                            indices[i - 1] = index - 1;
                        }

                        for (int i = 1; i < indices.Length - 1; i++)
                        {
                            faces.Add(Tuple.Create(indices[0], indices[i], indices[i + 1]));
                        }
                    }
                }
            }

            Mesh mesh = new Mesh(vertices.Count, faces.Count);
            for (int i = 0; i < mesh.TriangleCount; i++)
            {
                Tuple<int, int, int> face = faces[i];
                mesh.SetTriangle(i, face.Item1, face.Item2, face.Item3);    
            }

            for (int i = 0; i < mesh.VertexCount; i++)
            {
                mesh.SetVertex(i, vertices[i]);
            }
            return mesh;
        }
    }
}
