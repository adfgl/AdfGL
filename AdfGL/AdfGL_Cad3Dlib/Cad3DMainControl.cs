using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdfGL_Cad3Dlib
{
    public class Cad3DMainControl : Cad3DControl<Cad3DDataContext>
    {
        public Cad3DMainControl(Cad3DDataContext dataContext) : base(dataContext)
        {

        }

        public override void Render(IntBox box, FrameBuffer activeBuffer)
        {
            DataContext.Container.Renderer.Render(box, activeBuffer, false);
        }

        public override bool Update(float ellapse)
        {
            DataContext.Container.Renderer.Update(ellapse);
            return false;
        }
    }
}
