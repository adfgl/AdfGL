namespace AdfGLDrawingLib
{
    public class FrameBuffer
    {
        public const int BITS_PER_PIXEL = 4;
        public const float DEEPEST_Z = float.NegativeInfinity;

        internal int[] pixels = Array.Empty<int>();
        float[] _depthBuffer = Array.Empty<float>();

        int _width;
        int _height;
        bool _depthBufferEnabled;

        public FrameBuffer(int width, int height, bool depthBufferEnabled = false)
        {
            if (width <= 0 || height <= 0)
            {
                throw new ArgumentException("invalid frame buffer dimensions");
            }

            _width = width;
            _height = height;
            pixels = new int[width * height];
            _depthBufferEnabled = depthBufferEnabled;
            if (_depthBufferEnabled)
            {
                _depthBuffer = new float[width * height];
                Array.Fill(_depthBuffer, DEEPEST_Z);
            }
        }

        public FrameBuffer(FrameBuffer buffer) : this(width: buffer._width, height: buffer._height, depthBufferEnabled: buffer._depthBufferEnabled)
        {
            Buffer.BlockCopy(buffer.pixels, 0, pixels, 0, pixels.Length * BITS_PER_PIXEL);
            if (_depthBufferEnabled)
            {
                Buffer.BlockCopy(buffer._depthBuffer, 0, _depthBuffer, 0, _depthBuffer.Length * sizeof(float));
            }
        }

        public int Width { get { return _width; } }
        public int Height { get { return _height; } }
        public bool DepthBufferEnabled { get { return _depthBufferEnabled; } }

        public IntBox GetBox()
        {
            return new IntBox(_width, _height);
        }

        public bool ChangeSize(int width, int height)
        {
            if (width < 0 || height < 0) return false;
            if (_width == width && _height == height) return false;

            _width = width;
            _height = height;
            pixels = new int[width * height];
            if (_depthBufferEnabled)
            {
                _depthBuffer = new float[width * height];
                Array.Fill(_depthBuffer, DEEPEST_Z);
            }
            return true;
        }

        public void Clear()
        {
            Array.Clear(pixels, 0, pixels.Length);
            if (_depthBufferEnabled)
            {
                Array.Fill(_depthBuffer, DEEPEST_Z);
            }
        }

        public void Clear(int x, int y, int width, int height)
        {
            if (x == 0 && y == 0 && width == _width && height == _height)
            {
                Clear();
                return;
            }

            int x0 = Math.Max(0, x);
            int y0 = Math.Max(0, y);
            int x1 = Math.Min(_width, x + width);
            int y1 = Math.Min(_height, y + height);

            int lineWidth = x1 - x0;
            for (int sy = y0; sy < y1; sy++)
            {
                Array.Clear(pixels, sy * _width + x0, lineWidth);
                if (_depthBufferEnabled)
                {
                    Array.Fill(_depthBuffer, DEEPEST_Z, sy * _width + x0, lineWidth);
                }
            }
        }

        public void Fill(int color)
        {
            Array.Fill(pixels, color);
        }

        public void Fill(int x, int y, int width, int height, int color)
        {
            if (x == 0 && y == 0 && width == _width && height == _height)
            {
                Fill(color);
                return;
            }
            else
            {
                int x0 = Math.Max(0, x);
                int y0 = Math.Max(0, y);
                int x1 = Math.Min(_width, x + width);
                int y1 = Math.Min(_height, y + height);

                int lineWidth = x1 - x0;
                for (int sy = y0; sy < y1; sy++)
                {
                    Array.Fill(pixels, color, sy * _width + x0, lineWidth);
                }
            }
        }

        public byte[] GetByteArray()
        {
            byte[] result = new byte[_width * _height * BITS_PER_PIXEL];
            Buffer.BlockCopy(pixels, 0, result, 0, result.Length);
            return result;
        }

        public ReadOnlySpan<byte> GetByteSpan()
        {
            return System.Runtime.InteropServices.MemoryMarshal.Cast<int, byte>(new ReadOnlySpan<int>(pixels));
        }

        public bool GetPixel(int x, int y, out int pixel)
        {
            if (x >= _width || x < 0 || y >= _height || y < 0)
            {
                pixel = -1;
                return false;
            }
            pixel = pixels[y * _width + x];
            return true;
        }

        public bool SetPixel(int x, int y, int color, bool doBlend = false)
        {
            if (x >= _width || x < 0 || y >= _height || y < 0) return false;
            int index = y * _width + x;
            pixels[index] = doBlend ? Colour.Blend(color, pixels[index]) : color;
            return true; ;
        }

        public void MakeScanLine(in IntBox box, int x1, int x2, int y, int color, bool doBlend = false)
        {
            if (y >= box.maxY || y < box.minY) return;

            if (x1 > x2)
            {
                int temp = x1;
                x1 = x2;
                x2 = temp;
            }

            int min = Math.Min(box.minX, box.maxX);
            int max = Math.Max(box.minX, box.maxX);

            x1 = Math.Clamp(x1, min, max);
            x2 = Math.Clamp(x2, min, max);

            int yoffset = y * _width;
            for (int x = x1; x < x2; x++)
            {
                int index = yoffset + x;
                pixels[index] = doBlend ? Colour.Blend(color, pixels[index]) : color;
            }
        }

        public void MakeScanLine(int x1, int x2, int y, int color, bool doBlend = false)
        {
            if (y >= _height || y < 0) return;

            if (x1 < 0) x1 = 0;
            if (x1 >= _width) x1 = _width - 1;

            if (x2 < 0) x2 = 0;
            if (x2 >= _width) x2 = _width - 1;

            if (x1 > x2)
            {
                int temp = x1;
                x1 = x2;
                x2 = temp;
            }

            int yoffset = y * _width;
            for (int x = x1; x < x2; x++)
            {
                int index = yoffset + x;
                pixels[index] = doBlend ? Colour.Blend(color, pixels[index]) : color;
            }
        }
    }
}
