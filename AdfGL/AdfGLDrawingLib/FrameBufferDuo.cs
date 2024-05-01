namespace AdfGLDrawingLib
{
    public class FrameBufferDuo
    {
        readonly object LOCK = new();

        int _width;
        int _height;
        FrameBuffer _frontBuffer;
        FrameBuffer _backBuffer;

        public FrameBufferDuo(int width, int height)
        {
            _width = width;
            _height = height;
            _frontBuffer = new(width, height);
            _backBuffer = new(width, height);
        }

        public FrameBuffer Front
        {
            get
            {
                lock (LOCK)
                {
                    return _frontBuffer;
                }
            }
        }

        public FrameBuffer Back
        {
            get
            {
                lock (LOCK)
                {
                    return _backBuffer;
                }
            }
        }

        public void SwapBuffers()
        {
            lock (LOCK)
            {
                var temp = _frontBuffer;
                _frontBuffer = _backBuffer;
                _backBuffer = temp;
            }
        }

        public int Width { get { return _width; } }
        public int Height { get { return _height; } }

        public bool ChangeSize(int width, int height)
        {
            if (width < 0 || height < 0) return false;
            if (_height == width && height == _height) return false;

            lock (LOCK)
            {
                _width = width;
                _height = height;

                _frontBuffer.ChangeSize(_width, _height);
                _backBuffer.ChangeSize(_width, _height);
            }
            return true;
        }

        public bool DepthBufferEnabled { get; set; }
    }
}
