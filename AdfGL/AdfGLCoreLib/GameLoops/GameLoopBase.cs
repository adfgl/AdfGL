using AdfGLCoreLib.Application;
using AdfGLCoreLib.Enums;
using AdfGLCoreLib.Services;
using AdfGLDrawingLib;
using System.Collections.Concurrent;
using System.Diagnostics;

namespace AdfGLCoreLib.GameLoops
{
    public abstract class GameLoopBase
    {
        bool s_forcreRedraw = false;

        readonly AppHost _host;
        readonly Thread thread;
        readonly ConcurrentQueue<AppEvent> eventQueue;
        bool _isRunning = false;

        public GameLoopBase(AppHost host)
        {
            this._host = host;
            thread = new Thread(GameThreadFunction);
            eventQueue = new ConcurrentQueue<AppEvent>();
        }

        public int ActualFps { get; private set; }

        public bool IsRunning { get { return _isRunning; } }

        public int TargetFps { get { return _host.Application.TargetFps; } }

        public AppHost Host { get { return _host; } }

        public abstract FrameBuffer GetBuffer();

        public abstract bool HandleScreenResize(int width, int height);

        public abstract void HandleFrameBufferReset();

        public void SendEvent(AppEvent e)
        {
            eventQueue.Enqueue(e);
        }

        public void Start()
        {
            if (_isRunning) return;
            _isRunning = true;
            s_forcreRedraw = true;
            thread.Start();
        }

        public void Stop()
        {
            if (false == _isRunning) return;
            _isRunning = false;
            thread.Interrupt();
        }

        /// <summary>
        /// Forces rerender even if no actual update notification was sent.
        /// </summary>
        public void ForceRedraw()
        {
            s_forcreRedraw = true;
        }

        void GameThreadFunction()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            int previous = DateTime.Now.Millisecond;
            int current, elapsed;
            int lag = 0;

            int framesRendered = 0;
            int frameTimer = 0;

            while (_isRunning)
            {
                int targetFrameTime = 1000 / TargetFps;
                current = DateTime.Now.Millisecond;
                elapsed = Math.Abs(current - previous); // why tf i get negative values?..
                previous = current;

                if (TikTakService.TikTak.IsRunning) TikTakService.TikTak.Ms += elapsed;

                bool sizeChanged = false;
                if (eventQueue.TryDequeue(out AppEvent e))
                {
                    // it is depatable whether the size change should be done in game loop or outside of it
                    // for now let it be here
                    sizeChanged = e.type == EEventType.SizeChanged && HandleScreenResize(e.data3, e.data4);
                    _host.Application.HandleEvent(in e);
                }

                if (lag + 1 > targetFrameTime)
                {
                    _host.Application.Update(targetFrameTime);
                }

                if (lag > targetFrameTime || s_forcreRedraw || sizeChanged)
                {
                    HandleFrameBufferReset();
                    framesRendered++;
                    lag = 0;
                    s_forcreRedraw = false; // reset force redraw otherwise what fps limit was made for?!
                }
                else
                {
                    lag += elapsed;
                }

                if (frameTimer + elapsed <= 1000)
                {
                    frameTimer += elapsed;
                }
                else
                {
                    ActualFps = framesRendered;
                    frameTimer = 0;
                    framesRendered = 0;
                }

                _host.Application.ActualFps = ActualFps;
            }
        }
    }
}
