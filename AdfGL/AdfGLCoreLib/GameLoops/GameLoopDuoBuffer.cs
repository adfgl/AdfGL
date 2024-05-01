using AdfGLCoreLib.Application;
using AdfGLDrawingLib;

namespace AdfGLCoreLib.GameLoops
{
    public class GameLoopDuoBuffer : GameLoopBase
    {
        readonly FrameBufferDuo buffer;

        public GameLoopDuoBuffer(AppHost host, int width, int height) : base(host)
        {
            buffer = new FrameBufferDuo(width, height);
        }

        public override bool HandleScreenResize(int width, int height)
        {
            return buffer.ChangeSize(width, height);
        }

        public override void HandleFrameBufferReset()
        {
            Host.Application.RenderToBuffer(buffer.Back);
            buffer.SwapBuffers();
            Host.UpdateScreen(buffer.Front);
        }

        public override FrameBuffer GetBuffer()
        {
            return buffer.Back;
        }
    }
}
