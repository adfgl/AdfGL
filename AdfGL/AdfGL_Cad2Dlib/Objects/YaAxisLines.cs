using AdfGLDrawingLib;
using LinearAlgebraLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdfGL_Cad2Dlib.Objects
{
    public class YaAxisLines : Object2Dbase
    {
        int _xAxisColor;
        int _yAxisColor;

        public YaAxisLines(Container2D container, int? xAxisColor = null, int? yAxisColor = null) : base(container, 1)
        {
            float opacity = 0.5f;
            _xAxisColor = xAxisColor ?? Colours.Red.ApplyOpacity(opacity).ToInt();
            _yAxisColor = yAxisColor ?? Colours.Green.ApplyOpacity(opacity).ToInt();
            Nodes[0] = new Node(Vec2.Zero);
        }

        public Colour XAxisColor
        {
            get { return new Colour(_xAxisColor); }
            set { _xAxisColor = value.ToInt(); }
        }

        public Colour YAxisColor
        {
            get { return new Colour(_yAxisColor); }
            set { _yAxisColor = value.ToInt(); }
        }

        public override Node[] GetNodes()
        {
            return Nodes;
        }

        public override void DrawSelf(IntBox box, FrameBuffer buffer)
        {
            WorldToScreen(Nodes[0].Position, out int x, out int y);
            buffer.DrawLine(box, 0, y, buffer.Width, y, _xAxisColor, true);
            buffer.DrawLine(box, x, 0, x, buffer.Height, _yAxisColor, true);
        }
    }
}
