using LinearAlgebraLib;
using static AdfGLDrawingLib.FontAtlas;

namespace AdfGLDrawingLib
{
    // https://snowb.org/
    public static class DrawingEx_Text
    {
        const byte TAB = 9;
        const byte LINE_BREAK = 10;
        const int SHADOW_BLUR = 4;
        public static bool IgnoreKerning { get; set; } = false;

        static FontAtlas Arial = FontAtlas.Load("Arial.fnt");
        static FontAtlas ArialBold = FontAtlas.Load("ArialBold.fnt");
        static FontAtlas ArialItalic = FontAtlas.Load("ArialItalic.fnt");
        static FontAtlas ArialItalicBold = FontAtlas.Load("ArialBoldItalic.fnt");

        static FontAtlas GetAltas(bool isBold, bool isItalic, int fontSize)
        {
            FontAtlas atlas;
            if (isBold)
            {
                if (isItalic)
                {
                    atlas = ArialItalicBold;
                }
                else
                {
                    atlas = ArialBold;
                }
            }
            else if (isItalic)
            {
                if (isBold)
                {
                    atlas = ArialItalicBold;
                }
                else
                {
                    atlas = ArialItalic;
                }
            }
            else
            {
                atlas = Arial;
            }
            return atlas;
        }

        public static void DrawString(this FrameBuffer buffer, Trans2 transformation, FontAtlas atlas, IntBox box, string text, Colour fillColor, Colour borderColor, float opacity = 1.0f)
        {
            int screenH = buffer.Height;
            int screenW = buffer.Width;

            MeasureString(atlas, text, out int txtWidth, out int txtHeight);
            if (txtWidth == 0 || txtHeight == 0) return;

            Vec2[] bounds =
            [
                new Vec2(0, 0),
                new Vec2(txtWidth, 0),
                new Vec2(0 + txtWidth, 0 + txtHeight),
                new Vec2(0, 0 + txtHeight)
            ];

            double fminX = double.MaxValue;
            double fminY = double.MaxValue;
            double fmaxX = double.MinValue;
            double fmaxY = double.MinValue;

            for (int i = 0; i < 4; i++)
            {
                bounds[i] = transformation.Forward(bounds[i]);

                var (tx, ty) = bounds[i];

                if (fminX > tx) fminX = tx;
                if (fminY > ty) fminY = ty;
                if (fmaxX < tx) fmaxX = tx;
                if (fmaxY < ty) fmaxY = ty;
            }

            if (fminX < 0) { fminX = 0; }
            if (fminY < 0) { fminY = 0; }
            if (fmaxX < 0) { fmaxX = 0; }
            if (fmaxY < 0) { fmaxY = 0; }

            if (fminX >= screenW) { fminX = screenW - 1; }
            if (fminY >= screenH) { fminY = screenH - 1; }
            if (fmaxX >= screenW) { fmaxX = screenW - 1; }
            if (fmaxY >= screenH) { fmaxY = screenH - 1; }

            int minX = (int)fminX;
            int minY = (int)fminY;

            int maxX = (int)fmaxX;
            int maxY = (int)fmaxY;

            int width = maxX - minX;
            int height = maxY - minY;

            if (width == 0 && height == 0) { return; }

            // store initial byte 
            TxtPixel[] textAlphas = new TxtPixel[txtWidth * txtHeight];

            int offsetX = 0;
            int offsetY = 0;
            char previousCh = '?';
            for (int i = 0; i < text.Length; i++)
            {
                char currentCh = text[i];

                if (currentCh == LINE_BREAK)
                {
                    offsetY += atlas.Common.LineHeight;
                    offsetX = 0;
                    continue;
                }
                if (currentCh == TAB)
                {
                    offsetX += atlas.Common.Base;
                    continue;
                }

                int kerning = 0;
                if (false == IgnoreKerning && atlas.HasKernings)
                {
                    atlas.KerningPairs.TryGetValue(new KerningPair(previousCh, currentCh), out kerning);
                }

                var character = atlas.GetCharacter(currentCh);

                int ox = offsetX + character.XOffset + kerning;
                int oy = offsetY + character.YOffset;

                int chX = character.X;
                int chY = character.Y;
                for (int charX = 0; charX < character.Width; charX++)
                {
                    for (int charY = 0; charY < character.Height; charY++)
                    {
                        TxtPixel charAlpha = atlas.GetTxtPixel(character, charX + chX, charY + chY);
                        if (charAlpha.alpha == 0) continue;

                        int ax = charX + ox;
                        int ay = charY + oy;
                        if (ax < 0 || ay < 0 || ax >= txtWidth || ay >= txtHeight) continue;

                        textAlphas[ay * txtWidth + ax] = charAlpha;
                    }
                }
                offsetX += character.XAdvance + kerning;
                previousCh = currentCh;
            }

            for (int bx = minX; bx < maxX; bx++)
            {
                for (int by = minY; by < maxY; by++)
                {
                    var v = transformation.Backward(new Vec2(bx, by));
                    int ax = (int)(v.x + 0.5);
                    int ay = (int)(v.y + 0.5);
                    if (ax < 0 || ay < 0 || ax >= txtWidth || ay >= txtHeight) continue;

                    TxtPixel pixel = textAlphas[ay * txtWidth + ax];
                    if (pixel.alpha == 0) continue;

                    Colour currentColor;
                    switch (pixel.type)
                    {
                        case EPixelType.Outline:
                            if (pixel.alpha != 255)
                            {
                                currentColor = new Colour(Colour.Blend(borderColor.ToInt(), fillColor.ToInt()));
                            }
                            else
                            {
                                currentColor = borderColor;
                            }
                            break;

                        case EPixelType.Shadow:
                            currentColor = borderColor;
                            break;

                        case EPixelType.Fill:
                            currentColor = fillColor;
                            break;

                        default:
                            currentColor = fillColor;
                            break;
                    }

                    //if (currentColor.a == 0) continue;

                    Colour pxColor = new Colour(currentColor.r, currentColor.g, currentColor.b, (byte)(pixel.alpha * opacity));

                    if (box.Contains(bx, by))
                    {
                        buffer.SetPixel(bx, by, pxColor.ToInt(), true);
                    }
                }
            }
        }

        public static void DrawString(this FrameBuffer buffer, IntBox size, int x, int y, string text, int fontSize, float rotAngle, Colour fillColor, Colour borderColor, bool isBold = false, bool isItalic = false, float opacity = 1.0f)
        {
            FontAtlas atlas = GetAltas(isBold, isItalic, fontSize);

            Trans2 trns = new Trans2();
            float scale = (float)fontSize / (float)atlas.Info.Size;
            trns.Scale(scale);
            trns.Rotate(rotAngle);
            trns.Translate(x, y);

            DrawString(buffer, trns, atlas, size, text, fillColor, borderColor, opacity);
        }

        public static IntSize MeasureString(this string text, int fontSize, bool isBold = false, bool isItalic = false)
        {
            FontAtlas atlas = GetAltas(isBold, isItalic, fontSize);
            float scale = (float)fontSize / (float)atlas.Info.Size;

            MeasureString(atlas, text, out int width, out int height);
            return new IntSize((int)(width * scale), (int)(height * scale));
        }

        static void MeasureString(FontAtlas atlas, string text, out int width, out int height)
        {
            width = 0; height = 0;
            if (String.IsNullOrEmpty(text)) { return; }

            int offsetX = 0;
            int lineBreakCount = 1;
            byte previousCh = 0;

            for (int i = 0; i < text.Length; i++)
            {
                char currentCh = text[i];
                if (currentCh == LINE_BREAK)
                {
                    if (width < offsetX) width = offsetX;
                    offsetX = 0;
                    lineBreakCount++;
                }
                else if (currentCh == TAB)
                {
                    offsetX += atlas.Common.Base;
                }
                else
                {
                    int kerning = 0;
                    if (false == IgnoreKerning || atlas.HasKernings)
                    {
                        atlas.KerningPairs.TryGetValue(new KerningPair(previousCh, currentCh), out kerning);
                    }
                    var chInfo = atlas.GetCharacter(currentCh);
                    offsetX += chInfo.XAdvance + kerning;
                }
            }

            if (width < offsetX) width = offsetX;
            height = (atlas.Common.LineHeight + SHADOW_BLUR * 2) * lineBreakCount;
        }
    }
}
