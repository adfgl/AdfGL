namespace AdfGLDrawingLib
{
    public enum ELineStyle : uint
    {
        /// <summary>
        /// '________'
        /// </summary>
        Solid = 0xFFFFFFFF,

        /// <summary>
        /// '__##__##'
        /// </summary>
        Dashed2by2 = 0xFF00FF00,

        /// <summary>
        /// '__####__'
        /// </summary>
        Dashed4by4 = 0xFF0000FF,

        /// <summary>
        /// '__######'
        /// </summary>
        Dashed2by6 = 0xFF000000,

        /// <summary>
        /// '__#.#__'
        /// </summary>
        DashDot = 0xFF0C0FF,

        /// <summary>
        /// ........
        /// </summary>
        Dotted = 0xCCCCCCCC,

        /// <summary>
        /// __#__#__#
        /// </summary>
        LongDash = 0xFF7F7F7F
    }
}
