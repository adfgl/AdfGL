using AdfGLDrawingLib;
using LinearAlgebraLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdfGL_Cad2Dlib.Objects
{
    public abstract class Object2Dbase
    {
        float _opacity = 1f;

        public Container2D Container { get; set; }

        public Object2Dbase(Container2D container, int maxNodeNumber)
        {
            Container = container;
            MaxNodeNumber = maxNodeNumber;
            Nodes = new Node[MaxNodeNumber];
        }

        public int MaxNodeNumber { get; protected set; } = -1;
        public Node[] Nodes { get; protected set; } = null!;
        public Colour Stroke { get; set; } = Colours.Black;
        public Colour Fill { get; set; } = Colours.Red;

        public float Opacity
        {
            get { return _opacity; }
            set
            {
                if (value < 0.0f || value > 1.0f)
                { throw new ArgumentOutOfRangeException(nameof(Opacity), value, "Invalid opacity. Opacity must be between 0.0 and 1.0"); }
                _opacity = value;
            }
        }

        public abstract Node[] GetNodes();
        public abstract void DrawSelf(IntBox box, FrameBuffer buffer);

        public virtual IntBox GetBox()
        {
            Node[] nodes = GetNodes();

            WorldToScreen(nodes[0].Position, out int testX, out int testY);

            int minx = testX; int maxx = testX;
            int miny = testY; int maxy = testY;

            for (int i = 1; i < nodes.Length; i++)
            {
                WorldToScreen(nodes[i].Position, out int x, out int y);

                if (minx > x) { minx = x; }
                if (maxx < x) { maxx = x; }
                if (miny > y) { miny = y; }
                if (maxy < y) { maxy = y; }
            }

            return new IntBox(minx, miny, maxx, maxy);
        }

        protected void WorldToScreen(Vec2 v, out int x, out int y)
        {
            Container.WorldToScreen(v.x, v.y, out x, out y);
        }

        protected void ScreenToWorld(int x, int y, out double wx, out double wy)
        {
            Container.ScreenToWorld(x, y, out wx, out wy);
        }

        public class Node
        {
            public Node(Vec2 pos)
            {
                Position = pos;
            }

            public Vec2 Position { get; private set; }
        }
    }
}
