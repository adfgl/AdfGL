namespace AdfGLCoreLib.Events
{
    public class MouseWheelEventArgs : MouseEventArgs
    {
        public int DeltaX { get; }
        public int DeltaY { get; }

        public MouseWheelEventArgs(int x, int y, int deltaX, int deltaY) : base(x, y)
        {
            DeltaX = deltaX;
            DeltaY = deltaY;
        }
    }
}
