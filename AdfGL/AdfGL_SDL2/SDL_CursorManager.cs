using AdfGLCoreLib.Enums;
using AdfGLCoreLib.Services;

namespace AdfGL_SDL2
{
    public class SDL_CursorManager : CursorManager
    {
        readonly WindowSDL2 sdl;

        public SDL_CursorManager(WindowSDL2 sdl)
        {
            this.sdl = sdl;
        }

        public override void SetMouseCursor(EMouseCursor cursor)
        {
            sdl.SetMouseCursor(cursor);
        }
    }
}
