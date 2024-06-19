using AdfGL_SDL2;
using AdfGLConsole.Demos;
using GeometryLib;
using LinearAlgebraLib;

namespace AdfGLConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int width = 1000;
            int height = 600;
            var win = new WindowSDL2();
            var host = new DemoHost(win);
            var app = new ConvexHullApp(null);

            var points = PointsGenerator.Sphere(100, 500);
            //points = PointsGenerator.RandomPointCloud(1000, 500);
            points = PreprocessOnion(Onion.Points, out Vec3 norm);
            points = PointsGenerator.RandomPointCloud(1000, 100);

            app.SetShape(points, 0.5);

            app.DisplayFps = true;

            host.SetApplication(app, width, height);
            win.Create("Demo", width, height);
            win.Start(host);
        }

        public static Vec3[] PreprocessOnion(Vec3[] points, out Vec3 norm)
        {
            double minX, minY, minZ, maxX, maxY, maxZ;
            minX = minY = minZ = double.MaxValue;
            maxX = maxY = maxZ = double.MinValue;
            for (int i = 0; i < points.Length; i++)
            {
                var (x, y, z) = points[i];

                if (x < minX) minX = x;
                if (y < minY) minY = y;
                if (z < minZ) minZ = z;

                if (x > maxX) maxX = x;
                if (y > maxY) maxY = y;
                if (z > maxZ) maxZ = z;
            }

            double dx = maxX - minX;
            double dy = maxY - minY;
            double dz = maxZ - minZ;

            Vec3[] normalized = new Vec3[points.Length];
            for (int i = 0; i < points.Length; i++)
            {
                var (x, y, z) = points[i];
                normalized[i] = new Vec3(x / dx, y / dy, z / dz);
            }

            norm = new Vec3(dx, dy, dz);    
            return normalized;
        }
    }
}
