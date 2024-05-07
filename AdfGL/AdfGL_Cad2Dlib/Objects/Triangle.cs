using AdfGLDrawingLib;
using LinearAlgebraLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdfGL_Cad2Dlib.Objects
{
    public class Triangle : Object2Dbase
    {
        public double X1 { get; set; }
        public double Y1 { get; set; }

        public double X2 { get; set; }
        public double Y2 { get; set; }

        public double X3 { get; set; }
        public double Y3 { get; set; }

        public ELineStyle LineStyle { get; set; } = ELineStyle.Solid;

        public Triangle(Container2D container, Vec2 a, Vec2 b, Vec2 c) : base(container, 3)
        {
            X1 = a.x;
            Y1 = a.y;

            X2 = b.x;
            Y2 = b.y;

            X3 = c.x;
            Y3 = c.y;
        }

        public override Node[] GetNodes()
        {
            Nodes[0] = new Node(new Vec2(X1, Y1));
            Nodes[1] = new Node(new Vec2(X2, Y2));
            Nodes[2] = new Node(new Vec2(X3, Y3));
            return Nodes;
        }

        public override void DrawSelf(IntBox box, FrameBuffer buffer)
        {
            WorldToScreen(new Vec2(X1, Y1), out int x1, out int y1);
            WorldToScreen(new Vec2(X2, Y2), out int x2, out int y2);
            WorldToScreen(new Vec2(X3, Y3), out int x3, out int y3);

            var fill = Opacity == 1f ? Fill : Fill.ApplyOpacity(Opacity);
            if (fill.a != 0)
            {
                buffer.FillTriangleNaive(in box, x1, y1, x2, y2, x3, y3, fill.ToInt(), fill.a != 255);
            }

            var stroke = Opacity == 1f ? Stroke : Stroke.ApplyOpacity(Opacity);
            if (stroke.a != 0)
            {
                buffer.DrawTriangle(in box, x1, y1, x2, y2, x3, y3, stroke.ToInt(), stroke.a != 255, LineStyle);
            }
        }
    }
}
