namespace AdfGLDrawingLib
{
    public static class DrawingEx_Rectangle
    {
        public static void DrawRectangle(this FrameBuffer buffer, in IntBox box, int x1, int y1, int x2, int y2, int color, bool blendColors, ELineStyle lineStyle = ELineStyle.Solid)
        {
            int a = (color >> 24) & 0xFF;
            if (a == 0) return;
            bool doBlend = blendColors && a != 255;

            buffer.DrawLine(box, x1, y1, x1, y2, color, doBlend, lineStyle); // left 
            buffer.DrawLine(box, x2, y1, x2, y2, color, doBlend, lineStyle); // right
            buffer.DrawLine(box, x1, y1, x2, y1, color, doBlend, lineStyle); // top 
            buffer.DrawLine(box, x1, y2, x2, y2, color, doBlend, lineStyle); // bottom
        }

        public static void FillRectangle(this FrameBuffer buffer, in IntBox box, int x1, int y1, int x2, int y2, int color, bool blendColors)
        {
            int a = (color >> 24) & 0xFF;
            if (a == 0) return;
            bool doBlend = blendColors && a != 255;

            if (x1 > x2)
            {
                var t = x1;
                x1 = x2;
                x2 = t;
            }

            if (y1 > y2)
            {
                var t = y1;
                y1 = y2;
                y2 = t;
            }

            int screenW = buffer.Width;
            int screenH = buffer.Height;
            IntBox bb = new IntBox(screenW, screenH).GetIntersection(box);

            // Clamp boundaries
            x1 = Math.Max(bb.minX, x1);
            y1 = Math.Max(bb.minY, y1);
            x2 = Math.Min(bb.maxX, x2);
            y2 = Math.Min(bb.maxY, y2);

            if (x2 - x1 == 0)
            {
                return;
            }

            for (int y = y1; y < y2; y++)
            {
                int index = screenW * y;
                for (int x = x1; x < x2; x++)
                {
                    buffer.pixels[index + x] = doBlend ? Colour.Blend(color, buffer.pixels[index + x]) : color;
                }
            }
        }

        public static void DrawRounderCornersRectangle(this FrameBuffer buffer, in IntBox box, int r, int x1, int y1, int x2, int y2, int color, bool blendColors)
        {
            int a = (color >> 24) & 0xFF;
            if (a == 0) return;
            bool doBlend = blendColors && a != 255;


            r = Math.Min(r, Math.Min(Math.Abs(x2 - x1) / 2, Math.Abs(y2 - y1) / 2));
            if (r <= 0)
            {
                buffer.DrawRectangle(box, x1, y1, x2, y2, color, doBlend);
                return;
            }

            int screenW = buffer.Width;
            int screenH = buffer.Height;
            IntBox bb = new IntBox(screenW, screenH).GetIntersection(box);

            if (x1 > x2)
            {
                var t = x1;
                x1 = x2;
                x2 = t;
            }

            if (y1 > y2)
            {
                var t = y1;
                y1 = y2;
                y2 = t;
            }

            // Clamp boundaries
            x1 = Math.Max(bb.minX, x1);
            y1 = Math.Max(bb.minY, y1);
            x2 = Math.Min(bb.maxX, x2);
            y2 = Math.Min(bb.maxY, y2);

            buffer.DrawLine(bb, x1, y1 + r, x1, y2 - r, color, doBlend); // left 
            buffer.DrawLine(bb, x2, y1 + r, x2, y2 - r, color, doBlend); // right
            buffer.DrawLine(bb, x1 + r, y1, x2 - r, y1, color, doBlend); // top 
            buffer.DrawLine(bb, x1 + r, y2, x2 - r, y2, color, doBlend); // bottom

            void setPixel(int x, int y, int color)
            {
                if (bb.Contains(x, y)) buffer.SetPixel(x, y, color, doBlend);
            }

            if (r > 0)
            {
                int x0 = 0;
                int y0 = r;
                int d = 3 - 2 * r;

                int cx1 = x1 + r;
                int cy1 = y1 + r;

                int cx2 = x2 - r;
                int cy2 = y1 + r;

                int cx3 = x2 - r;
                int cy3 = y2 - r;

                int cx4 = x1 + r;
                int cy4 = y2 - r;

                while (y0 >= x0)
                {
                    setPixel(cx1 - y0, cy1 - x0, color);// Q0 - upper left
                    setPixel(cx2 + x0, cy2 - y0, color);// Q6 - upper right
                    setPixel(cx3 + y0, cy3 + x0, color);// Q4 - lower right
                    setPixel(cx4 - x0, cy4 + y0, color);// Q2 - lower left

                    if (x0 != 0 && x0 != y0)
                    {
                        setPixel(cx1 - x0, cy1 - y0, color);// Q1 - upper left
                        setPixel(cx2 + y0, cy2 - x0, color);// Q7 - upper right
                        setPixel(cx3 + x0, cy3 + y0, color);// Q5 - lower right
                        setPixel(cx4 - y0, cy4 + x0, color);// Q3 - lower left
                    }

                    if (d < 0)
                        d += 4 * x0++ + 6;
                    else
                        d += 4 * (x0++ - y0--) + 10;
                }
            }
        }

        public static void FillRectangleRoundedCorners(this FrameBuffer buffer, in IntBox box, int r, int x1, int y1, int x2, int y2, int color, bool blendColors)
        {
            int a = (color >> 24) & 0xFF;
            if (a == 0) return;
            bool doBlend = blendColors && a != 255;

            r = Math.Min(r, Math.Min(Math.Abs(x2 - x1) / 2, Math.Abs(y2 - y1) / 2));
            if (r <= 0)
            {
                buffer.FillRectangle(box, x1, y1, x2, y2, color, doBlend);
                return;
            }

            int screenW = buffer.Width;
            int screenH = buffer.Height;
            IntBox bb = new IntBox(screenW, screenH).GetIntersection(box);
            if (x1 > x2)
            {
                var t = x1;
                x1 = x2;
                x2 = t;
            }

            if (y1 > y2)
            {
                var t = y1;
                y1 = y2;
                y2 = t;
            }

            // Clamp boundaries
            x1 = Math.Max(bb.minX, x1);
            y1 = Math.Max(bb.minY, y1);
            x2 = Math.Min(bb.maxX, x2);
            y2 = Math.Min(bb.maxY, y2);

            void setPixel(int x, int y, int color)
            {
                if (bb.Contains(x, y)) buffer.SetPixel(x, y, color, doBlend);
            }

            for (int y = y1; y <= y2; y++)
            {
                for (int x = x1; x <= x2; x++)
                {
                    bool isRounded = false;

                    if (y <= y1 + r && x <= x1 + r && IsInCircle(x, y, x1 + r, y1 + r, r))
                    {
                        isRounded = true;
                    }
                    else if (y <= y1 + r && x >= x2 - r && IsInCircle(x, y, x2 - r, y1 + r, r))
                    {
                        isRounded = true;
                    }
                    else if (y >= y2 - r && x <= x1 + r && IsInCircle(x, y, x1 + r, y2 - r, r))
                    {
                        isRounded = true;
                    }
                    else if (y >= y2 - r && x >= x2 - r && IsInCircle(x, y, x2 - r, y2 - r, r))
                    {
                        isRounded = true;
                    }

                    if (!isRounded)
                    {
                        setPixel(x, y, color);
                    }
                }
            }
        }

        private static bool IsInCircle(int x, int y, int cx, int cy, int radius)
        {
            int dx = x - cx;
            int dy = y - cy;
            return (dx * dx + dy * dy) > (radius * radius);
        }
    }
}
