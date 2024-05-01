namespace AdfGLCoreLib.Events
{
    public class TextInputEventArgs : EventArgumentsBase
    {
        public string Text { get; }

        public TextInputEventArgs(string text) : base()
        {
            Text = text;
        }
    }
}
