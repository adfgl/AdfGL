using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AdfGLDrawingLib.FontAtlas;

namespace AdfGLDrawingLib
{
    public class FontInfo
    {
        public string Face { get; set; }
        public int Size { get; set; }
        public bool IsBold { get; set; }
        public bool IsItalic { get; set; }
        public int[] Padding { get; set; }
        public int[] Spacing { get; set; }
    }

    public class FontCommonInfo
    {
        public int LineHeight { get; set; }
        public int Base { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int NumPages { get; set; }
    }

    public class CharInfo
    {
        public int Id { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int XOffset { get; set; }
        public int YOffset { get; set; }
        public int XAdvance { get; set; }
        public int Page { get; set; }
    }

    public class Kerning
    {
        public int First { get; set; }
        public int Second { get; set; }
        public int Amount { get; set; }
    }

    public class KerningPair
    {
        public KerningPair(int first, int second)
        {
            First = first;
            Second = second;
        }

        public int First { get; set; }
        public int Second { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is KerningPair otherKey)
            {
                return First == otherKey.First && Second == otherKey.Second;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(First, Second);
        }
    }

    public class Page
    {
        public int Id { get; set; }
        public string File { get; set; }
        public TxtPixel[] Alphas { get; set; }
    }

    public class FontAtlas
    {
        const string ROOT_FOLDER = "fonts";

        public float ScaleFactor { get; set; } = 1;

        public FontInfo Info { get; set; } = null!;
        public FontCommonInfo Common { get; set; } = null!;

        public List<Page> Pages { get; set; } = null!;
        public Dictionary<int, CharInfo> Characters { get; set; } = null!;

        public bool HasKernings { get; set; } = false;
        public Dictionary<KerningPair, int> KerningPairs { get; set; } = null!;

        public CharInfo GetCharacter(char ch)
        {
            CharInfo character;
            if (!Characters.TryGetValue(ch, out character!))
            {
                character = Characters['?'];
            }
            return character;
        }

        public TxtPixel GetTxtPixel(CharInfo ch, int x, int y)
        {
            return Pages[ch.Page].Alphas[y * Common.Width + x];
        }

        public static FontAtlas Load(string fileName)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ROOT_FOLDER, fileName);
            string[] lines = File.ReadAllLines(filePath);

            FontAtlas fontAtlas = new FontAtlas();
            fontAtlas.Pages = new List<Page>();
            fontAtlas.Characters = new Dictionary<int, CharInfo>();
            fontAtlas.HasKernings = false;
            fontAtlas.KerningPairs = new Dictionary<KerningPair, int>();
            for (int i = 0; i < lines.Length; i++)
            {
                string[] tokens = lines[i].Split(' ');

                switch (tokens[0].ToLower())
                {
                    case "info":
                        fontAtlas.Info = ParseInfo(tokens);
                        break;

                    case "common":
                        fontAtlas.Common = ParseFontCommonInfo(tokens);
                        break;

                    case "page":
                        var page = ParsePage(tokens);
                        fontAtlas.Pages.Add(page);
                        break;

                    case "chars":
                        int charsCount = ParseCount(tokens);
                        break;

                    case "char":
                        var chInfo = ParseCharInfo(tokens);
                        fontAtlas.Characters[chInfo.Id] = chInfo;
                        break;

                    case "kernings":
                        int kerningCount = ParseCount(tokens);
                        fontAtlas.HasKernings = kerningCount > 0;
                        break;

                    case "kerning":
                        var kerning = ParseKerning(tokens);
                        fontAtlas.KerningPairs.Add(new KerningPair(kerning.First, kerning.Second), kerning.Amount);
                        break;

                    default:
                        throw new NotImplementedException($"Identifier '{tokens[0]}' is not recognized.");
                }
            }
            return fontAtlas;
        }

        static FontInfo ParseInfo(string[] tokens)
        {
            if (false == String.Equals("info", tokens[0]))
            {
                throw new InvalidDataException(tokens[0]);
            }

            FontInfo fontInfo = new FontInfo();
            for (int i = 1; i < tokens.Length; i++)
            {
                string[] property = tokens[i].Split('=');

                string name = property[0];
                string value = property[1];

                switch (name.ToLower())
                {
                    case "face":
                        fontInfo.Face = value.Replace("\"", "");
                        break;

                    case "size":
                        fontInfo.Size = Int32.Parse(value);
                        break;

                    case "bold":
                        fontInfo.IsBold = String.Equals("1", value);
                        break;

                    case "italic":
                        fontInfo.IsItalic = String.Equals("1", value);
                        break;

                    case "padding":
                        string[] paddingValues = value.Split(',');
                        int[] padding = new int[paddingValues.Length];
                        for (int j = 0; j < paddingValues.Length; j++)
                        {
                            padding[j] = Int32.Parse(paddingValues[j]);
                        }
                        fontInfo.Padding = padding;
                        break;

                    case "spacing":
                        string[] spacingValues = value.Split(',');
                        int[] spacing = new int[spacingValues.Length];
                        for (int j = 0; j < spacingValues.Length; j++)
                        {
                            spacing[j] = Int32.Parse(spacingValues[j]);
                        }
                        fontInfo.Spacing = spacing;
                        break;

                    default:
                        break;
                }
            }

            return fontInfo;
        }

        static FontCommonInfo ParseFontCommonInfo(string[] tokens)
        {
            if (false == String.Equals("common", tokens[0]))
            {
                throw new InvalidDataException(tokens[0]);
            }

            FontCommonInfo fontCommonInfo = new FontCommonInfo();
            for (int i = 1; i < tokens.Length; i++)
            {
                string[] property = tokens[i].Split('=');
                string name = property[0];
                string value = property[1];

                switch (name.ToLower())
                {
                    case "lineheight":
                        fontCommonInfo.LineHeight = Int32.Parse(value);
                        break;

                    case "base":
                        fontCommonInfo.Base = Int32.Parse(value);
                        break;

                    case "scalew":
                        fontCommonInfo.Width = Int32.Parse(value);
                        break;

                    case "scaleh":
                        fontCommonInfo.Height = Int32.Parse(value);
                        break;

                    case "pages":
                        fontCommonInfo.NumPages = Int32.Parse(value);
                        break;

                    default:
                        break;
                }
            }

            return fontCommonInfo;
        }

        public enum EPixelType : byte
        {
            Undefined,
            Outline,
            Fill,
            Shadow
        }

        public readonly struct TxtPixel
        {
            public readonly byte alpha;
            public readonly EPixelType type;

            public TxtPixel(byte alpha, EPixelType type)
            {
                this.alpha = alpha;
                this.type = type;
            }
        }

        static Page ParsePage(string[] tokens)
        {
            if (false == String.Equals("page", tokens[0]))
            {
                throw new InvalidDataException(tokens[0]);
            }

            Page page = new Page();
            for (int i = 1; i < tokens.Length; i++)
            {
                string[] property = tokens[i].Split('=');
                string name = property[0];
                string value = property[1];

                switch (name.ToLower())
                {
                    case "id":
                        page.Id = Int32.Parse(value);
                        break;

                    case "file":
                        page.File = value.Replace("\"", "");
                        break;

                    default:
                        break;
                }
            }

            string imgPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, ROOT_FOLDER, page.File);
            using FileStream stream = File.OpenRead(imgPath);
            var image = BigGustave.Png.Open(stream);

            int imgWidth = image.Width;
            int imgHeight = image.Height;
            TxtPixel[] alphas = new TxtPixel[imgWidth * imgHeight];

            for (int x = 0; x < imgWidth; x++)
            {
                for (int y = 0; y < imgHeight; y++)
                {
                    var pixel = image.GetPixel(x, y);
                    if (pixel.A != 0)
                    {
                        EPixelType type = EPixelType.Undefined;
                        if (pixel.R > pixel.G && pixel.R > pixel.B)
                        {
                            type = EPixelType.Outline;
                        }
                        else if (pixel.G > pixel.R && pixel.G > pixel.B)
                        {
                            type = EPixelType.Fill;
                        }
                        else if (pixel.B > pixel.R && pixel.B > pixel.G)
                        {
                            type = EPixelType.Shadow;
                        }
                        alphas[y * imgWidth + x] = new TxtPixel(pixel.A, type);
                    }
                }
            }

            page.Alphas = alphas;

            return page;
        }

        static int ParseCount(string[] tokens)
        {
            string[] property = tokens[1].Split('=');
            string name = property[0];
            string value = property[1];

            return Int32.Parse(value);
        }

        static Kerning ParseKerning(string[] tokens)
        {
            if (false == String.Equals("kerning", tokens[0]))
            {
                throw new InvalidDataException(tokens[0]);
            }

            Kerning kerning = new Kerning();
            for (int i = 1; i < tokens.Length; i++)
            {
                string[] property = tokens[i].Split('=');
                string name = property[0];
                string value = property[1];

                switch (name.ToLower())
                {
                    case "first":
                        kerning.First = Int32.Parse(value);
                        break;

                    case "second":
                        kerning.Second = Int32.Parse(value);
                        break;

                    case "amount":
                        kerning.Amount = Int32.Parse(value);
                        break;

                    default:
                        break;
                }
            }

            return kerning;
        }

        static CharInfo ParseCharInfo(string[] tokens)
        {
            if (false == String.Equals("char", tokens[0]))
            {
                throw new InvalidDataException(tokens[0]);
            }

            CharInfo charInfo = new CharInfo();
            for (int i = 1; i < tokens.Length; i++)
            {
                string[] property = tokens[i].Split('=');
                string name = property[0];
                string value = property[1];

                switch (name.ToLower())
                {
                    case "id":
                        charInfo.Id = Int32.Parse(value);
                        break;

                    case "x":
                        charInfo.X = Int32.Parse(value);
                        break;

                    case "y":
                        charInfo.Y = Int32.Parse(value);
                        break;

                    case "width":
                        charInfo.Width = Int32.Parse(value);
                        break;

                    case "height":
                        charInfo.Height = Int32.Parse(value);
                        break;

                    case "xoffset":
                        charInfo.XOffset = Int32.Parse(value);
                        break;

                    case "yoffset":
                        charInfo.YOffset = Int32.Parse(value);
                        break;

                    case "xadvance":
                        charInfo.XAdvance = Int32.Parse(value);
                        break;

                    case "page":
                        charInfo.Page = Int32.Parse(value);
                        break;

                    default:
                        break;
                }
            }

            return charInfo;
        }
    }
}
