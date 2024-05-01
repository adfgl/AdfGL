namespace AdfGLDrawingLib
{
    /// <summary>
    /// uses BGRA encoding
    /// </summary>
    public readonly struct Colour
    {
        public readonly byte a, r, g, b;

        public Colour(byte r, byte g, byte b, byte a = 255)
        {
            this.a = a;
            this.r = r;
            this.g = g;
            this.b = b;
        }

        public Colour(int argb)
        {
            this.b = (byte)(argb & 0xff);
            this.g = (byte)(argb >> 8);
            this.r = (byte)(argb >> 16);
            this.a = (byte)(argb >> 24);
        }

        public Colour(int rgb, byte a = 255)
        {
            this.b = (byte)(rgb & 0xff);
            this.g = (byte)(rgb >> 8);
            this.r = (byte)(rgb >> 16);
            this.a = a;
        }

        public void Deconstruct(out byte r, out byte g, out byte b, out byte a)
        {
            r = this.r;
            g = this.g;
            b = this.b;
            a = this.a;
        }

        public Colour ApplyOpacity(float opacity)
        {
            return new Colour(r, g, b, (byte)(a * opacity));
        }

        public Colour DisableOpacity()
        {
            return new Colour(r, g, b, 255);
        }

        public Colour Darken(float factor)
        {
            return new Colour(
                (byte)(r * (1.0f - factor)),
                (byte)(g * (1.0f - factor)),
                (byte)(b * (1.0f - factor)), a);
        }

        public Colour Brighten(float factor)
        {
            var nr = r + (255 - r) * factor;
            var ng = g + (255 - g) * factor;
            var nb = b + (255 - b) * factor;

            return new Colour(
                 (byte)((nr > 255) ? 255 : nr),
                 (byte)((ng > 255) ? 255 : ng),
                 (byte)((nb > 255) ? 255 : nb), a);
        }

        public int ToInt()
        {
            if (a == 0) return 0;
            if (a == 255) return b | (g << 8) | (r << 16) | (a << 24);
            int k = (a << 8) / 255;
            return (b * k) >> 8 | ((g * k) >> 8 << 8) | ((r * k) >> 8 << 16) | (a << 24);
        }

        public static int Multiply(int color)
        {
            int a = (color >> 24) & 0xFF;
            if (a == 0) return 0;
            if (a == 255) return color;

            int b = color & 0xFF;
            int g = (color >> 8) & 0xFF;
            int r = (color >> 16) & 0xFF;

            int k = (a << 8) / 255;
            return (b * k) >> 8 | ((g * k) >> 8 << 8) | ((r * k) >> 8 << 16) | (a << 24);
        }

        public static int Blend(int front, int back)
        {
            int fb = front & 0xFF;
            int fg = (front >> 8) & 0xFF;
            int fr = (front >> 16) & 0xFF;
            int fa = (front >> 24) & 0xFF;

            int bb = back & 0xFF;
            int bg = (back >> 8) & 0xFF;
            int br = (back >> 16) & 0xFF;
            int ba = (back >> 24) & 0xFF;

            int k = (255 - fa) * 0x8081;
            int b = (fb + ((bb * k) >> 23)) & 0xFF;
            int g = (fg + ((bg * k) >> 23)) & 0xFF;
            int r = (fr + ((br * k) >> 23)) & 0xFF;
            int a = (fa + ((ba * k) >> 23)) & 0xFF;

            return b | (g << 8) | (r << 16) | (a << 24);
        }

        public static int Brighten(int color, float factor)
        {
            int b = color & 0xFF;
            int g = (color >> 8) & 0xFF;
            int r = (color >> 16) & 0xFF;
            int a = (color >> 24) & 0xFF;

            r = (int)(r + (255 - r) * factor);
            g = (int)(g + (255 - g) * factor);
            b = (int)(b + (255 - b) * factor);

            r = (r > 255) ? 255 : r;
            g = (g > 255) ? 255 : g;
            b = (b > 255) ? 255 : b;

            return b | (g << 8) | (r << 16) | (a << 24);
        }

        public static int Darken(int color, float factor)
        {
            int b = color & 0xFF;
            int g = (color >> 8) & 0xFF;
            int r = (color >> 16) & 0xFF;
            int a = (color >> 24) & 0xFF;

            r = (int)(r * (1.0f - factor));
            g = (int)(g * (1.0f - factor));
            b = (int)(b * (1.0f - factor));

            return b | (g << 8) | (r << 16) | (a << 24);
        }

        public Colour Eluminate(float lumen, float intensity = 0.3f)
        {
            float r = this.r;
            float g = this.g;
            float b = this.b;

            lumen *= intensity;

            if (lumen < 0f)
            {
                lumen = 1 + lumen;
                r *= lumen;
                g *= lumen;
                b *= lumen;
            }
            else
            {
                r = (255 - r) * lumen + r;
                g = (255 - g) * lumen + g;
                b = (255 - b) * lumen + b;
            }
            return new Colour((byte)r, (byte)g, (byte)b, this.a);
        }

        public static Colour Random(Random? random = null)
        {
            random ??= new Random();
            return new Colour(
                (byte)random.Next(256),
                (byte)random.Next(256),
                (byte)random.Next(256));
        }
    }
}
