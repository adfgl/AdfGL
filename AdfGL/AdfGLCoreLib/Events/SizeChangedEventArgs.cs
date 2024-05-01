namespace AdfGLCoreLib.Events
{
    public class SizeChangedEventArgs : EventArgumentsBase
    {
        public int WidthBefore { get; }
        public int WidthAfter { get; }

        public int HeightBefore { get; }
        public int HeightAfter { get; }

        public SizeChangedEventArgs(int widthBefore, int heightBefore, int widthAfter, int heightAfter) : base()
        {
            WidthBefore = widthBefore;
            WidthAfter = widthAfter;

            HeightBefore = heightBefore;
            HeightAfter = heightAfter;
        }
    }
}
