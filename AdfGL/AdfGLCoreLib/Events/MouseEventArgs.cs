namespace AdfGLCoreLib.Events
{
    public class MouseEventArgs : EventArgumentsBase
    {
        public int X { get; }
        public int Y { get; }

        public MouseEventArgs(int x, int y) : base()
        {
            X = x;
            Y = y;
        }
    }
}
