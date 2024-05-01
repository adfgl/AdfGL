using AdfGLCoreLib.Enums;

namespace AdfGLCoreLib.Services
{
    public class CursorManager
    {
        public virtual void SetMouseCursor(EMouseCursor cursor)
        {
            throw new NotImplementedException();
        }

        public void RemoveCursor()
        {
            SetMouseCursor(EMouseCursor.None);
        }

        public void SetDefaultCursor()
        {
            SetMouseCursor(EMouseCursor.Arrow);
        }
    }

    public static class YaCursor
    {
        static EMouseCursor _mouseCursor = EMouseCursor.None;
        internal static CursorManager s_Manager = new();

        public static void Set(EMouseCursor cursor)
        {
            _mouseCursor = cursor;
            s_Manager.SetMouseCursor(cursor);
        }

        public static EMouseCursor Get()
        {
            return _mouseCursor;
        }

        public static void SetDefault()
        {
            s_Manager.SetDefaultCursor();
        }

        public static void RemoveCursor()
        {
            s_Manager.RemoveCursor();
        }
    }
}
