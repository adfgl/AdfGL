using AdfGLCoreLib.Services;

namespace AdfGL_SDL2
{
    public class SDL_ScreenManager : ScreenManager
    {
        readonly WindowSDL2 sdl;

        public SDL_ScreenManager(WindowSDL2 sdl)
        {
            this.sdl = sdl;
        }

        public override void SetByteSpan(int width, int height, ReadOnlySpan<byte> pixels)
        {
            sdl.SetPixelsToScreen(width, height, pixels);
        }

        public override void SetByteArray(int width, int height, byte[] pixels)
        {
            sdl.SetPixelsToScreen(width, height, pixels);
        }

        public override void GetMousePosition(out int x, out int y)
        {
            sdl.GetMouse(out x, out y);
        }
    }
}
