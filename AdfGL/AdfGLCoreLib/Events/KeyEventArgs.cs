using AdfGLCoreLib.Enums;

namespace AdfGLCoreLib.Events
{
    public class KeyEventArgs : EventArgumentsBase
    {
        public EKeyCode Key { get; }

        public KeyEventArgs(EKeyCode key) : base()
        {
            Key = key;
        }
    }
}
