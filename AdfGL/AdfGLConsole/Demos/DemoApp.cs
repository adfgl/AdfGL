using AdfGL_Cad3Dlib;
using AdfGLCoreLib.Application;
using AdfGLDrawingLib;

namespace AdfGLConsole.Demos
{
    public class DemoApp : AppBase
    {
        public DemoApp(AppEventHandler? eventHandler) : base(eventHandler)
        {
            Background = IntColours.Wheat;
            Controls.Main = new DemoControl().Enable();
            Controls.AttachChild(new Cad3DMainControl(new Cad3DDataContext()));
        }
    }

    public class DemoControl : AppControlBase
    {
        public override void Render(IntBox box, FrameBuffer activeBuffer)
        {
            activeBuffer.FillCircle(box, MouseX, MouseY, 10, IntColours.Red, false);
        }
    }
}
