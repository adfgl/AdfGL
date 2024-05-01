namespace AdfGLDrawingLib
{
    public static class DrawingEx_Lines
    {
        static void swap(ref int a, ref int b) { var t = a; a = b; b = t; }

        public static void DrawLine(this FrameBuffer buffer, in IntBox box, int x1, int y1, int x2, int y2, int color, bool blendColors, ELineStyle lineStyle = ELineStyle.Solid)
        {
            int a = (color >> 24) & 0xFF;
            if (a == 0) return;

            if (x2 - x1 == 0 && y2 - y1 == 0) { return; } // why would i draw 'zero' line?
            bool doBlend = blendColors && a != 255;

            uint pattern = (uint)lineStyle;

            int screenW = buffer.Width;
            int screenH = buffer.Height;
            IntBox bb = IntBox.Infinity; // new IntBox(screenW, screenH).GetIntersection(box);

            bool rol()
            {
                pattern = (pattern << 1) | (pattern >> 31);
                return (pattern & 1) != 0;
            }

            int x, y, dx1, dy1, px, py, xe, ye, i;

            int dx = x2 - x1;
            int dy = y2 - y1;
            if (dx == 0) // Line is vertical
            {
                if (y2 < y1) swap(ref y1, ref y2);

                if (bb.minX <= x1 && x1 <= bb.maxX)
                {
                    for (y = Math.Max(y1, bb.minY); y <= Math.Min(y2, bb.maxY); y++)
                    {
                        if (rol()) buffer.SetPixel(x1, y, color, doBlend);
                    }
                }
                return;
            }

            if (dy == 0) // Line is horizontal
            {
                if (x2 < x1) swap(ref x1, ref x2);

                if (bb.minY <= y1 && y1 <= bb.maxY)
                {
                    for (x = Math.Max(x1, bb.minX); x <= Math.Min(x2, bb.maxX); x++)
                    {
                        if (rol()) buffer.SetPixel(x, y1, color, doBlend);
                    }
                }
                return;
            }

            // Line is Funk-aye
            dx1 = Math.Abs(dx);
            dy1 = Math.Abs(dy);
            px = 2 * dy1 - dx1;
            py = 2 * dx1 - dy1;
            if (dy1 <= dx1)
            {
                if (dx >= 0)
                {
                    x = x1;
                    y = y1;
                    xe = x2;
                }
                else
                {
                    x = x2;
                    y = y2;
                    xe = x1;
                }
                x = Math.Max(x, bb.minX);
                y = Math.Max(y, bb.minY);

                if (rol() && bb.Contains(x, y))
                {
                    buffer.SetPixel(x, y, color, doBlend);
                }

                for (i = 0; x < Math.Min(xe, bb.maxX); i++)
                {
                    x++;
                    if (px < 0)
                    {
                        px += 2 * dy1;
                    }
                    else
                    {
                        if ((dx < 0 && dy < 0) || (dx > 0 && dy > 0)) y++; else y--;
                        px += 2 * (dy1 - dx1);
                    }
                    if (rol() && bb.Contains(x, y))
                    {
                        buffer.SetPixel(x, y, color, doBlend);
                    }
                }
            }
            else
            {
                if (dy >= 0)
                {
                    x = x1; y = y1; ye = y2;
                }
                else
                {
                    x = x2; y = y2; ye = y1;
                }
                x = Math.Max(x, bb.minX);
                y = Math.Max(y, bb.minY);

                if (rol() && bb.Contains(x, y))
                {
                    buffer.SetPixel(x, y, color, doBlend);
                }

                for (i = 0; y < Math.Min(ye, bb.maxY); i++)
                {
                    y++;
                    if (py <= 0)
                    {
                        py += 2 * dx1;
                    }
                    else
                    {
                        if ((dx < 0 && dy < 0) || (dx > 0 && dy > 0)) x++; else x--;
                        py += 2 * (dx1 - dy1);
                    }
                    if (rol() && bb.Contains(x, y))
                    {
                        buffer.SetPixel(x, y, color, doBlend);
                    }
                }
            }
        }
    }
}
