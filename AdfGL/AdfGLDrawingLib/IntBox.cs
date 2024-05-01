namespace AdfGLDrawingLib
{
    public readonly struct IntBox
    {
        public readonly int minX, minY;
        public readonly int maxX, maxY;

        public static IntBox Infinity
        {
            get
            {
                return new IntBox(int.MinValue, int.MinValue, int.MaxValue, int.MaxValue);
            }
        }

        public IntBox(int width, int height)
        {
            this.minX = 0;
            this.minY = 0;
            this.maxX = width;
            this.maxY = height;
        }

        public IntBox(int minX, int minY, int maxX, int maxY)
        {
            this.minX = minX;
            this.minY = minY;
            this.maxX = maxX;
            this.maxY = maxY;
        }

        public void Deconstruct(out int minX, out int minY, out int maxX, out int maxY)
        {
            minX = this.minX;
            minY = this.minY;
            maxX = this.maxX;
            maxY = this.maxY;
        }

        public bool Contains(int x, int y)
        {
            return
             minX <= x && x <= maxX
             &&
             minY <= y && y <= maxY;
        }

        public bool Intersects(in IntBox other)
        {
            return
            minX <= other.maxX &&
            minY <= other.maxY &&
            maxX >= other.minX &&
            maxY >= other.minY;
        }

        public bool Contains(in IntBox other)
        {
            return
            minX <= other.minX &&
            minY <= other.minY &&
            maxX >= other.maxX &&
            maxY >= other.maxY;
        }

        /// <summary>
        /// Extends given <see cref="IntBox"/> to fully contain <paramref name="other"/> <see cref="IntBox"/>.
        /// </summary>
        public IntBox Extend(in IntBox other)
        {
            return
            new IntBox(
            other.minX < minX ? other.minX : minX,
            other.minY < minY ? other.minY : minY,
            other.maxX > maxX ? other.maxX : maxX,
            other.maxY > maxY ? other.maxY : maxY);
        }

        /// <summary>
        /// Get portion of <paramref name="other"/> <see cref="IntBox"/> contained in this <see cref="IntBox"/>.
        /// </summary>
        /// <remarks>
        /// Returns <paramref name="other"/> if it is fully contained in this.
        /// </remarks>
        public IntBox GetIntersection(in IntBox other)
        {
            return
            new IntBox(
            other.minX > minX ? other.minX : minX,
            other.minY > minY ? other.minY : minY,
            other.maxX < maxX ? other.maxX : maxX,
            other.maxY < maxY ? other.maxY : maxY);
        }
    }
}
