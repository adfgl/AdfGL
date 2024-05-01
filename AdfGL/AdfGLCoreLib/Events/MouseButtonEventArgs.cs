using AdfGLCoreLib.Enums;

namespace AdfGLCoreLib.Events
{
    public class MouseButtonEventArgs : MouseEventArgs
    {
        public EMouseButton Button { get; }
        public int ClickCount { get; }

        public MouseButtonEventArgs(int x, int y, EMouseButton button, int clickCount = 1) : base(x, y)
        {
            Button = button;
            ClickCount = clickCount;
        }
    }
}
