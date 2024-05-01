using AdfGLCoreLib.Enums;

namespace AdfGLCoreLib.Services
{
    public static class AppEventBuilder
    {
        public static AppEvent CustomEvent(AppEvent e)
        {
            return new AppEvent(e);
        }

        public static AppEvent Quit()
        {
            return new(EEventType.Quit);
        }

        public static AppEvent SizeChanged(int widthBefore, int heightBefore, int widthAfter, int heightAfter)
        {
            return new(EEventType.SizeChanged)
            {
                data1 = widthBefore,
                data2 = heightBefore,
                data3 = widthAfter,
                data4 = heightAfter,
            };
        }

        public static AppEvent MouseDown(EMouseButton button, int x, int y, int clickCount = 1)
        {
            return new(EEventType.MouseButtonDown)
            {
                data1 = x,
                data2 = y,
                data3 = (int)button,
                data4 = clickCount
            };
        }

        public static AppEvent MouseUp(EMouseButton button, int x, int y)
        {
            return new(EEventType.MouseButtonUp)
            {
                data1 = x,
                data2 = y,
                data3 = (int)button
            };
        }

        public static AppEvent MouseMove(int x, int y)
        {
            return new(EEventType.MouseMove)
            {
                data1 = x,
                data2 = y
            };
        }

        public static AppEvent MouseWheel(int deltaX, int deltaY, int x, int y)
        {
            return new(EEventType.MouseWheel)
            {
                data1 = x,
                data2 = y,
                data3 = deltaX,
                data4 = deltaY
            };
        }

        public static AppEvent KeyDown(EKeyCode key)
        {
            return new(EEventType.KeyDown) { data3 = (int)key };
        }

        public static AppEvent KeyUp(EKeyCode key)
        {
            return new(EEventType.KeyUp) { data3 = (int)key };
        }

        public static AppEvent TextInput(string text)
        {
            return new(EEventType.TextInput) { stringdata = text };
        }

        public static AppEvent DropFile(string file)
        {
            return new(EEventType.DropFile) { stringdata = file };
        }

        public static AppEvent GainMouseFocus()
        {
            return new(EEventType.GotMouseFocus);
        }

        public static AppEvent LoseMouseFocus()
        {
            return new(EEventType.LostMouseFocus);
        }

        public static AppEvent GainKeyboardFocus()
        {
            return new(EEventType.GotKeyboardFocus);
        }

        public static AppEvent LoseKeyboardFocus()
        {
            return new(EEventType.LostMouseFocus);
        }

        public static AppEvent MouseEnter()
        {
            return new(EEventType.MouseEnter);
        }

        public static AppEvent MouseLeave()
        {
            return new(EEventType.MouseLeave);
        }

        public static AppEvent Restore()
        {
            return new(EEventType.Restored);
        }

        public static AppEvent Hide()
        {
            return new(EEventType.Hidden);
        }

        public static AppEvent Show()
        {
            return new(EEventType.Shown);
        }

        public static AppEvent Maximize()
        {
            return new(EEventType.Maximized);
        }

        public static AppEvent Minimize()
        {
            return new(EEventType.Minimized);
        }
    }
}
