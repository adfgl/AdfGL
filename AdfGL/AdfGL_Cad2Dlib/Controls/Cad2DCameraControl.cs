using AdfGLCoreLib.Enums;
using AdfGLCoreLib.Events;
using AdfGLCoreLib.Services;
using AdfGLDrawingLib;
using LinearAlgebraLib;


namespace AdfGL_Cad2Dlib.Controls
{
    public class Cad2DCameraControl : Cad2DControl<Cad2DDataContext>
    {
        const EMouseButton PAN_BUTTON = EMouseButton.Middle;
        double s_panStartX = 0;
        double s_panStartY = 0;

        public Cad2DCameraControl(Cad2DDataContext dataContext) : base(dataContext)
        {
        }

        public override void Render(IntBox box, FrameBuffer activeBuffer)
        {

        }

        public override bool OnMouseDown(MouseButtonEventArgs e)
        {
            if (e.Button == PAN_BUTTON)
            {
                AdfGLCursor.Set(EMouseCursor.SizeAll);

                s_panStartX = e.X;
                s_panStartY = e.Y;
            }
            return false;
        }

        public override bool OnMouseMove(MouseEventArgs e)
        {
            if (InputDevices.Mouse.IsPressed(PAN_BUTTON))
            {

                double scale = DataContext.Container.WorldScale;
                double offX = ((double)e.X - s_panStartX) / scale;
                double offY = ((double)e.Y - s_panStartY) / scale;

                DataContext.Container.WorldOffset -= new Vec2(offX, offY);

                s_panStartX = e.X;
                s_panStartY = e.Y;

            }
            return false;
        }

        public override bool OnMouseUp(MouseButtonEventArgs e)
        {
            AdfGLCursor.SetDefault();
            return false;
        }

        public override bool OnMouseWheel(MouseWheelEventArgs e)
        {
            double speed = 0.05f;
            DataContext.Container.ScreenToWorld(e.X, e.Y, out double xb, out double yb);
            DataContext.Container.WorldScale *= e.DeltaY > 0 ? (1f + speed) : (1f - speed);
            DataContext.Container.ScreenToWorld(e.X, e.Y, out double xa, out double ya);
            DataContext.Container.WorldOffset += new Vec2(xb - xa, -yb + ya);
            return false;
        }
    }
}
