using AdfGLCoreLib;

namespace AdfGL_SDL2
{
    public abstract class AppHostSDL : AppHost
    {
        public AppHostSDL(WindowSDL2 sdl) : base(initOnlyBuffer: true)
        {
            EnableScreen(new SDL_ScreenManager(sdl));
            EnableCursor(new SDL_CursorManager(sdl));
        }
    }
}
