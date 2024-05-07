using AdfGLDrawingLib;
using LinearAlgebraLib;

namespace AdfGL_Cad2Dlib.Objects
{
    public class Circle : Object2Dbase
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Radius { get; set; }

        public Circle(Container2D container, Vec2 center, double radius) : base(container, 3)
        {
            X = center.x;
            Y = center.y;
            Radius = radius;
        }

        public override Node[] GetNodes()
        {
            Node[] nodes = [new Node(new Vec2(X - Radius, Y - Radius)), new Node(new Vec2(X + Radius, Y + Radius))];
            return nodes;
        }

        public override void DrawSelf(IntBox box, FrameBuffer buffer)
        {
            WorldToScreen(new Vec2(X, Y), out int x, out int y);
            int radius = (int)(Radius * Container.WorldScale);

            var fill = Opacity == 1f ? Fill : Fill.ApplyOpacity(Opacity);
            if (fill.a != 0)
            {
                buffer.FillCircle(in box, x, y, radius, fill.ToInt(), fill.a != 255);
            }

            var stroke = Opacity == 1f ? Stroke : Stroke.ApplyOpacity(Opacity);
            if (stroke.a != 0)
            {
                buffer.DrawCircle(in box, x, y, radius, stroke.ToInt(), stroke.a != 255);
            }
        }
    }
}
