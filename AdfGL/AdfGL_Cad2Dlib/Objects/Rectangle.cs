using AdfGLDrawingLib;
using LinearAlgebraLib;

namespace AdfGL_Cad2Dlib.Objects
{
    public class Rectangle : Object2Dbase
    {
        public Rectangle(Container2D container) : base(container, 2)
        {
        }

        public float X1 { get; set; }
        public float Y1 { get; set; }

        public float X2 { get; set; }
        public float Y2 { get; set; }

        public ELineStyle LineStyle { get; set; } = ELineStyle.Solid;

        public override Node[] GetNodes()
        {
            Nodes[0] = new Node(new Vec2(X1, Y1));
            Nodes[1] = new Node(new Vec2(X2, Y2));
            return Nodes;
        }

        public override void DrawSelf(IntBox box, FrameBuffer buffer)
        {
            WorldToScreen(new Vec2(X1, Y1), out int x1, out int y1);
            WorldToScreen(new Vec2(X2, Y2), out int x2, out int y2);

            var fill = Opacity == 1f ? Fill : Fill.ApplyOpacity(Opacity);
            if (fill.a != 0)
            {
                buffer.FillRectangle(in box, x1, y1, x2, y2, fill.ToInt(), fill.a != 255);
            }

            var stroke = Opacity == 1f ? Stroke : Stroke.ApplyOpacity(Opacity);
            if (stroke.a != 0)
            {
                buffer.DrawRectangle(in box, x1, y1, x2, y2, stroke.ToInt(), stroke.a != 255, LineStyle);
            }
        }
    }
}
