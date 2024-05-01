using AdfGLDrawingLib;

namespace AdfGLCoreLib.GameLoops
{
    public class GameLoopOneBuffer : GameLoopBase
    {
        readonly FrameBuffer buffer;

        public GameLoopOneBuffer(AppHost host, int width, int height) : base(host)
        {
            buffer = new FrameBuffer(width, height);
        }

        public override bool HandleScreenResize(int width, int height)
        {
            return buffer.ChangeSize(width, height);
        }

        public override void HandleFrameBufferReset()
        {
            Host.Application.RenderToBuffer(buffer);
            Host.UpdateScreen(buffer);
        }

        public override FrameBuffer GetBuffer()
        {
            return buffer;
        }
    }
}
