using AdfGL_Cad3Dlib;
using AdfGL_Cad3Dlib.Controls;
using AdfGLCoreLib.Application;
using AdfGLDrawingLib;

namespace AdfGLConsole.Demos
{
    public class Cad3D : AppBase
    {
        Cad3DDataContext context = new Cad3DDataContext();

        public Cad3D(AppEventHandler? eventHandler) : base(eventHandler)
        {
            Background = IntColours.Transparent;
            Controls.Main = new Cad3DMainControl(context).Enable();
            Controls.AttachChild(new Cad3DCameraControl(context));
        }
    }
}
