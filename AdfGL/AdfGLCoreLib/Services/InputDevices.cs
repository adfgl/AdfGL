using AdfGLCoreLib.Enums;
using System.Collections;

namespace AdfGLCoreLib.Services
{
    /// <summary>
    /// Static class providing access to singleton-instances of input device managers.
    /// </summary>
    public static class InputDevices
    {
        static KeyboardManager _keyboardManager = new KeyboardManager();
        static MouseManager _mouseManager = new MouseManager();

        /// <summary>
        /// Gets the keyboard input device manager instance.
        /// </summary>
        public static KeyboardManager Keyboard => _keyboardManager;

        /// <summary>
        /// Gets the mouse input device manager instance.
        /// </summary>
        public static MouseManager Mouse => _mouseManager;
    }

    /// <summary>
    /// Class for managing mouse input using the <see cref="InputDeviceManagerBase{T}"/> class.
    /// </summary>
    public class MouseManager : InputDeviceManagerBase<EMouseButton>
    {

    }

    /// <summary>
    /// Class for managing keyboard input using the <see cref="InputDeviceManagerBase{T}"/> class.
    /// </summary
    public class KeyboardManager : InputDeviceManagerBase<EKeyCode>
    {
        public bool IsLetter(EKeyCode key)
        {
            return EKeyCode.a <= key && key <= EKeyCode.z;
        }

        public bool IsNumber(EKeyCode key)
        {
            return EKeyCode.N_0 <= key && key <= EKeyCode.N_9;
        }

        public bool IsCtrlPressed()
        {
            return IsPressed(EKeyCode.LCTRL) || IsPressed(EKeyCode.RCTRL);
        }

        public bool IsShiftPressed()
        {
            return IsPressed(EKeyCode.LSHIFT) || IsPressed(EKeyCode.RSHIFT);
        }
    }

    /// <summary>
    /// A base class for managing input devices.
    /// </summary>
    /// <typeparam name="T">The enum type representing input device buttons.</typeparam>
    public abstract class InputDeviceManagerBase<T> where T : Enum
    {
        BitArray keys = null!;

        public InputDeviceManagerBase()
        {
            Reset();
        }

        /// <summary>
        /// Sets state of all buttons to "not pressed".
        /// </summary>
        public void Reset()
        {
            keys = new(Enum.GetValues(typeof(T)).Length);
        }

        public void Handle(T button, bool isPressed)
        {
            keys[(int)(object)button] = isPressed;
        }

        public bool IsPressed(T button)
        {
            return true == keys[(int)(object)button];
        }

        public bool IsReleased(T button)
        {
            return false == keys[(int)(object)button];
        }
    }
}
