namespace AdfGLCoreLib.Application
{
    public interface IState
    {
        int MouseX { get; }
        int MouseY { get; }
        int ScreenWidth { get; }
        int ScreenHeight { get; }
        bool HasMouseFocus { get; }
        bool HasKeyboardFocus { get; }
        bool IsMinimized { get; }
        bool IsMaximized { get; }
        bool IsShown { get; }
    }
}
