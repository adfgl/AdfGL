namespace AdfGLDrawingLib
{
    public static class DrawingEx_Circle
    {
        public static void DrawCircle(this FrameBuffer buffer, in IntBox box, int centerX, int centerY, int radius, int color, bool blendColors)
        {
            int a = (color >> 24) & 0xFF;
            if (a == 0) return;
            bool doBlend = blendColors && a != 255;

            int screenW = buffer.Width;
            int screenH = buffer.Height;
            IntBox bb = new IntBox(screenW, screenH).GetIntersection(box);

            void setPixel(int x, int y, int color)
            {
                if (bb.Contains(x, y)) buffer.SetPixel(x, y, color, doBlend);
            }

            if (radius > 0)
            {
                int x0 = 0;
                int y0 = radius;
                int d = 3 - 2 * radius;

                while (y0 >= x0)
                {
                    setPixel(centerX + x0, centerY - y0, color); // Q6 - upper right 
                    setPixel(centerX + y0, centerY + x0, color); // Q4 - upper right 
                    setPixel(centerX - x0, centerY + y0, color); // Q2 - lower left
                    setPixel(centerX - y0, centerY - x0, color); // Q0 - lower left

                    if (x0 != 0 && x0 != y0)
                    {
                        setPixel(centerX + y0, centerY - x0, color); // Q7 - upper right 
                        setPixel(centerX + x0, centerY + y0, color); // Q5 - upper right 
                        setPixel(centerX - y0, centerY + x0, color); // Q3 - lower left
                        setPixel(centerX - x0, centerY - y0, color); // Q1 - lower left
                    }

                    if (d < 0)
                        d += 4 * x0++ + 6;
                    else
                        d += 4 * (x0++ - y0--) + 10;
                }
            }
            else
            {
                setPixel(centerX, centerY, color);
            }
        }

        public static void FillCircle(this FrameBuffer buffer, in IntBox box, int centerX, int centerY, int radius, int color, bool blendColors)
        {
            int a = (color >> 24) & 0xFF;
            if (a == 0) return;
            bool doBlend = blendColors && a != 255;

            int screenW = buffer.Width;
            int screenH = buffer.Height;
            IntBox bb = new IntBox(screenW, screenH).GetIntersection(box);

            if (radius > 0)
            {
                int x0 = 0;
                int y0 = radius;
                int d = 3 - 2 * radius;

                while (y0 >= x0)
                {
                    buffer.MakeScanLine(bb, centerX - y0, centerX + y0, centerY - x0, color, doBlend);
                    if (x0 > 0)
                    {
                        buffer.MakeScanLine(bb, centerX - y0, centerX + y0, centerY + x0, color, doBlend);
                    }

                    if (d < 0)
                    {
                        d += 4 * x0++ + 6;
                    }
                    else
                    {
                        if (x0 != y0)
                        {
                            buffer.MakeScanLine(bb, centerX - x0, centerX + x0, centerY - y0, color, doBlend);
                            buffer.MakeScanLine(bb, centerX - x0, centerX + x0, centerY + y0, color, doBlend);
                        }
                        d += 4 * (x0++ - y0--) + 10;
                    }
                }
            }
            else
            {
                if (bb.Contains(centerX, centerY))
                {
                    buffer.SetPixel(centerX, centerY, color, doBlend);
                }
            }
        }
    }
}
