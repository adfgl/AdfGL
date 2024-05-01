namespace AdfGLDrawingLib
{
    public readonly struct IntSize
    {
        public readonly int width, height;

        public IntSize(int x, int y)
        {
            this.width = x; this.height = y;
        }

        public void Deconstruct(out int width, out int height)
        {
            width = this.width;
            height = this.height;
        }
    }
}
