using AdfGLDrawingLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdfGLCoreLib.Services
{
    public class ScreenManager
    {
        public virtual void SetByteArray(int width, int height, byte[] pixels) { throw new NotImplementedException(); }
        public virtual void SetByteSpan(int width, int height, ReadOnlySpan<byte> pixels) { throw new NotImplementedException(); }

        public virtual void GetMousePosition(out int x, out int y) { throw new NotImplementedException(); }
    }

    public static class YaScreen
    {
        internal static ScreenManager s_Manager = new();

        public static void SetByteArray(int width, int height, FrameBuffer pixels)
        {
            s_Manager.SetByteArray(width, height, pixels.GetByteArray());
        }

        public static void SetByteSpan(int width, int height, FrameBuffer pixels)
        {
            s_Manager.SetByteSpan(width, height, pixels.GetByteSpan());
        }

        public static void GetMousePosition(out int x, out int y)
        {
            s_Manager.GetMousePosition(out x, out y);
        }
    }
}
