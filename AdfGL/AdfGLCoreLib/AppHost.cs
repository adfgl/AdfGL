using AdfGLCoreLib.Application;
using AdfGLCoreLib.GameLoops;
using AdfGLCoreLib.Services;
using AdfGLDrawingLib;

namespace AdfGLCoreLib
{
    public abstract class AppHost
    {
        GameLoopBase _gameLoop = null!;
        AppBase _application = null!;
        bool _initOnlyBuffer;

        public AppHost(bool initOnlyBuffer)
        {
            _initOnlyBuffer = initOnlyBuffer;
        }

        public AppBase Application { get { return _application; } }
        public GameLoopBase GameLoop { get { return _gameLoop; } }

        public virtual bool IsRunning { get; private set; }

        public virtual void SendEvent(in AppEvent e)
        {
            _application.HandleEvent(e);
        }

        public void RenderToBuffer()
        {
            _gameLoop.HandleFrameBufferReset();
        }

        public bool HandleResize(int width, int height)
        {
            return _gameLoop.HandleScreenResize(width, height);
        }

        public void UpdateScreen(FrameBuffer buffer)
        {
            YaScreen.SetByteSpan(buffer.Width, buffer.Height, buffer);
        }

        public void SetApplication(AppBase application, int width, int height)
        {
            bool wasRunning;
            if (_initOnlyBuffer)
            {
                wasRunning = false;
            }
            else
            {
                wasRunning = _gameLoop is not null && _gameLoop.IsRunning;
                if (wasRunning) _gameLoop!.Stop();
            }

            ApplicationState.ScreenWidth = width;
            ApplicationState.ScreenHeight = height;

            _application = application;
            if (_application.UseDoubleBuffer)
            {
                _gameLoop = new GameLoopDuoBuffer(this, width, height);
            }
            else
            {
                _gameLoop = new GameLoopOneBuffer(this, width, height);
            }
            if (wasRunning)
            {
                _gameLoop.Start();
            }
        }

        public void Start()
        {
            IsRunning = true;
            if (_gameLoop is null) { throw new Exception(); }

            if (false == _initOnlyBuffer)
            {
                _gameLoop.Start();
            }
        }

        public void Stop()
        {
            IsRunning = false;
            if (_gameLoop is null) { throw new Exception(); }

            if (false == _initOnlyBuffer)
            {
                _gameLoop.Stop();
            }
        }

        public void EnableScreen(ScreenManager manager)
        {
            YaScreen.s_Manager = manager;
        }

        public void EnableCursor(CursorManager manager)
        {
            AdfGLCursor.s_Manager = manager;
        }
    }
}
