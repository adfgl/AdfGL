namespace AdfGLCoreLib.Events
{
    public class DropFileEventArgs : EventArgumentsBase
    {
        public string Path { get; }

        public DropFileEventArgs(string path) : base()
        {
            Path = path;
        }
    }
}
