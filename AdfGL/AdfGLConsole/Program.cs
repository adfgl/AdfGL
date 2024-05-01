using AdfGL_SDL2;

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
            var app = new DemoApp(null);

            app.DisplayFps = true;

            host.SetApplication(app, width, height);
            win.Create("Demo", width, height);
            win.Start(host);
        }
    }
}
