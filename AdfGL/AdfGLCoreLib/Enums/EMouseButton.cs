namespace AdfGLCoreLib.Enums
{
    // NOTE: do not change enum type from 'int' to anything else, othewise casting in the manager won't work
    public enum EMouseButton : int
    {
        Undefined,

        Left,
        Middle,
        Right,

        /// <summary>
        /// 'XButton1' mouse button.
        /// </summary>
        Previous,

        /// <summary>
        /// 'XButton2' mouse button.
        /// </summary>
        Next,
    }
}
