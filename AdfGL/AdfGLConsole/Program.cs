using AdfGL_SDL2;
using AdfGLConsole.Demos;
using AdfGLConsole.Utils;

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

            var points = PointsGenerator.Sphere(20, 100);
            points = Onion.Points.ToArray();

            app.SetShape(points);

            app.DisplayFps = true;

            host.SetApplication(app, width, height);
            win.Create("Demo", width, height);
            win.Start(host);
        }
    }
}
