using AdfGL_Cad2Dlib.Controls;
using AdfGLDrawingLib;

namespace AdfGL_Cad2Dlib
{
    public class Cad2DMainControl : Cad2DControl<Cad2DDataContext>
    {
        public Cad2DMainControl(Cad2DDataContext dataContext) : base(dataContext)
        {
        }

        public override void Render(IntBox box, FrameBuffer activeBuffer)
        {
            DataContext.Container.Render(box, activeBuffer);
        }

        public override bool Update(float ellapse)
        {
            DataContext.Container.Update(ellapse);
            return false;
        }
    }
}
