using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdfGLCoreLib.Enums
{
    public enum EEventType : byte
    {
        Quit,
        SizeChanged,

        MouseMove,
        MouseWheel,
        MouseButtonDown,
        MouseButtonUp,

        KeyDown,
        KeyUp,
        TextInput,

        DropFile,

        Shown,
        Hidden,

        Minimized,
        Maximized,
        Restored,

        MouseEnter,
        MouseLeave,

        GotKeyboardFocus,
        LostKeyboardFocus,

        GotMouseFocus,
        LostMouseFocus,

        Custom
    }
}
