namespace AdfGLCoreLib.Application
{
    internal static class ApplicationState
    {
        public static int MouseX { get; set; }
        public static int MouseY { get; set; }
        public static int ScreenWidth { get; set; }
        public static int ScreenHeight { get; set; }
        public static bool HasMouseFocus { get; set; }
        public static bool HasKeyboardFocus { get; set; }
        public static bool IsMinimized { get; set; }
        public static bool IsMaximized { get; set; }
        public static bool IsShown { get; set; }
    }
}
