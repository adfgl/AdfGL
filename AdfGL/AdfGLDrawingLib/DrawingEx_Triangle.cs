namespace AdfGLDrawingLib
{
    public static class DrawingEx_Triangle
    {
        static void swap(ref int a, ref int b) { var t = a; a = b; b = t; }

        public static void DrawTriangle(this FrameBuffer buffer, in IntBox box, int ax, int ay, int bx, int by, int cx, int cy, int color, bool doBlend, ELineStyle lineStyle = ELineStyle.Solid)
        {
            buffer.DrawLine(box, ax, ay, bx, by, color, doBlend, lineStyle);
            buffer.DrawLine(box, bx, by, cx, cy, color, doBlend, lineStyle);
            buffer.DrawLine(box, cx, cy, ax, ay, color, doBlend, lineStyle);
        }

        public static void FillTriangle(this FrameBuffer buffer, in IntBox box, int x1, int y1, int x2, int y2, int x3, int y3, int color, bool doBlend)
        {
            if (color == 0) return;

            int t1x, t2x, y, minx, maxx, t1xp, t2xp;
            bool changed1 = false;
            bool changed2 = false;
            int signx1, signx2, dx1, dy1, dx2, dy2;
            int e1, e2;
            // Sort vertices
            void Swap(ref int a, ref int b) { int t = a; a = b; b = t; }
            if (y1 > y2) { Swap(ref y1, ref y2); Swap(ref x1, ref x2); }
            if (y1 > y3) { Swap(ref y1, ref y3); Swap(ref x1, ref x3); }
            if (y2 > y3) { Swap(ref y2, ref y3); Swap(ref x2, ref x3); }

            t1x = t2x = x1; y = y1;   // Starting points
            dx1 = x2 - x1; if (dx1 < 0) { dx1 = -dx1; signx1 = -1; }
            else signx1 = 1;
            dy1 = y2 - y1;

            dx2 = x3 - x1; if (dx2 < 0) { dx2 = -dx2; signx2 = -1; }
            else signx2 = 1;
            dy2 = y3 - y1;

            if (dy1 > dx1)
            {   // swap values
                Swap(ref dx1, ref dy1);
                changed1 = true;
            }
            if (dy2 > dx2)
            {   // swap values
                Swap(ref dy2, ref dx2);
                changed2 = true;
            }

            e2 = dx2 >> 1;
            // Flat top, just process the second half
            if (y1 == y2) goto next;
            e1 = dx1 >> 1;

            for (int i = 0; i < dx1;)
            {
                t1xp = 0; t2xp = 0;
                if (t1x < t2x) { minx = t1x; maxx = t2x; }
                else { minx = t2x; maxx = t1x; }
                // process first line until y value is about to change
                while (i < dx1)
                {
                    i++;
                    e1 += dy1;
                    while (e1 >= dx1)
                    {
                        e1 -= dx1;
                        if (changed1) t1xp = signx1;//t1x += signx1;
                        else goto next1;
                    }
                    if (changed1) break;
                    else t1x += signx1;
                }
            // Move line
            next1:
                // process second line until y value is about to change
                while (true)
                {
                    e2 += dy2;
                    while (e2 >= dx2)
                    {
                        e2 -= dx2;
                        if (changed2) t2xp = signx2;//t2x += signx2;
                        else goto next2;
                    }
                    if (changed2) break;
                    else t2x += signx2;
                }
            next2:
                if (minx > t1x) minx = t1x; if (minx > t2x) minx = t2x;
                if (maxx < t1x) maxx = t1x; if (maxx < t2x) maxx = t2x;
                buffer.MakeScanLine(minx, maxx, y, color, doBlend);    // Draw line from min to max points found on the y
                                                                       // Now increase y
                if (!changed1) t1x += signx1;
                t1x += t1xp;
                if (!changed2) t2x += signx2;
                t2x += t2xp;
                y += 1;
                if (y == y2) break;

            }
        next:
            // Second half
            dx1 = x3 - x2; if (dx1 < 0) { dx1 = -dx1; signx1 = -1; }
            else signx1 = 1;
            dy1 = y3 - y2;
            t1x = x2;

            if (dy1 > dx1)
            {   // swap values
                Swap(ref dy1, ref dx1);
                changed1 = true;
            }
            else changed1 = false;

            e1 = dx1 >> 1;

            for (int i = 0; i <= dx1; i++)
            {
                t1xp = 0; t2xp = 0;
                if (t1x < t2x) { minx = t1x; maxx = t2x; }
                else { minx = t2x; maxx = t1x; }
                // process first line until y value is about to change
                while (i < dx1)
                {
                    e1 += dy1;
                    while (e1 >= dx1)
                    {
                        e1 -= dx1;
                        if (changed1) { t1xp = signx1; break; }//t1x += signx1;
                        else goto next3;
                    }
                    if (changed1) break;
                    else t1x += signx1;
                    if (i < dx1) i++;
                }
            next3:
                // process second line until y value is about to change
                while (t2x != x3)
                {
                    e2 += dy2;
                    while (e2 >= dx2)
                    {
                        e2 -= dx2;
                        if (changed2) t2xp = signx2;
                        else goto next4;
                    }
                    if (changed2) break;
                    else t2x += signx2;
                }
            next4:

                if (minx > t1x) minx = t1x; if (minx > t2x) minx = t2x;
                if (maxx < t1x) maxx = t1x; if (maxx < t2x) maxx = t2x;
                buffer.MakeScanLine(minx, maxx, y, color, doBlend);
                if (!changed1) t1x += signx1;
                t1x += t1xp;
                if (!changed2) t2x += signx2;
                t2x += t2xp;
                y += 1;
                if (y > y3) return;
            }

        }

        public static void FillTriangleNaive(this FrameBuffer buffer, in IntBox box, int x1, int y1, int x2, int y2, int x3, int y3, int color, bool blendColors)
        {
            int a = (color >> 24) & 0xFF;
            if (a == 0) return;
            bool doBlend = blendColors && a != 255;

            int screenW = buffer.Width;
            int screenH = buffer.Height;
            IntBox bb = new IntBox(screenW, screenH).GetIntersection(box);

            if (y1 > y2) { swap(ref y1, ref y2); swap(ref x1, ref x2); }
            if (y1 > y3) { swap(ref y1, ref y3); swap(ref x1, ref x3); }
            if (y2 > y3) { swap(ref y2, ref y3); swap(ref x2, ref x3); }

            if (y2 == y3) // flat bottom
            {
                FillFlatBottomTriangle(buffer, bb, x1, y1, x2, y2, x3, y3, color, doBlend);
            }
            else if (y1 == y2) // flat top
            {
                FillFlatTopTriangle(buffer, bb, x1, y1, x2, y2, x3, y3, color, doBlend);
            }
            else
            {
                int x4 = (int)(x1 + ((float)(y2 - y1) / (float)(y3 - y1)) * (x3 - x1));
                int y4 = y2;
                FillFlatBottomTriangle(buffer, bb, x1, y1, x2, y2, x4, y4, color, doBlend);
                FillFlatTopTriangle(buffer, bb, x2, y2, x4, y4, x3, y3, color, doBlend);
            }
        }
        static void FillFlatBottomTriangle(this FrameBuffer buffer, in IntBox box, int x1, int y1, int x2, int y2, int x3, int y3, int color, bool doBlend)
        {
            float invslope1 = (float)(x2 - x1) / (y2 - y1);
            float invslope2 = (float)(x3 - x1) / (y3 - y1);

            float curx1 = x1;
            float curx2 = x1;

            for (int scanlineY = y1; scanlineY <= y2; scanlineY++)
            {
                buffer.MakeScanLine(box, (int)curx1, (int)curx2, scanlineY, color, doBlend);
                curx1 += invslope1;
                curx2 += invslope2;
            }
        }

        static void FillFlatTopTriangle(this FrameBuffer buffer, in IntBox box, int x1, int y1, int x2, int y2, int x3, int y3, int color, bool doBlend)
        {
            float invslope1 = (float)(x3 - x1) / (y3 - y1);
            float invslope2 = (float)(x3 - x2) / (y3 - y2);

            float curx1 = x3;
            float curx2 = x3;

            for (int scanlineY = y3; scanlineY > y1; scanlineY--)
            {
                buffer.MakeScanLine(box, (int)curx1, (int)curx2, scanlineY, color, doBlend);
                curx1 -= invslope1;
                curx2 -= invslope2;
            }
        }
    }
}
