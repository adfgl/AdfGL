using AdfGLCoreLib.Enums;

namespace AdfGLCoreLib
{
    public struct AppEvent
    {
        public readonly EEventType type;

        public AppEvent(EEventType type)
        {
            this.type = type;
            chardata = 'n';
            stringdata = string.Empty;
            data1 = data2 = data3 = data4 = -1;
        }

        /// <summary>
        /// Constructor for <see cref="EEventType.Custom"/> event
        /// </summary>
        /// <param name="e"></param>
        public AppEvent(AppEvent e)
        {
            type = EEventType.Custom;
            chardata = e.chardata;
            stringdata = e.stringdata;
            data1 = e.data1;
            data2 = e.data2;
            data3 = e.data3;
            data4 = e.data4;
        }

        /// <summary>
        /// Stores one of the following in standard events:
        /// <para>-<see cref="TextInputEventArgs.Character"/></para>
        /// </summary>
        public char chardata;

        /// <summary>
        /// Stores one of the following in standard events:
        /// <para>-<see cref="DropFileEventArgs.Path"/></para>
        /// </summary>
        public string stringdata;

        /// <summary>
        /// Stores one of the following in standard events:
        /// <para>-<see cref="MouseEventArgs.X"/></para>
        /// <para>-<see cref="SizeChangedEventArgs.WidthBefore"/></para>
        /// </summary>
        public int data1;

        /// <summary>
        /// Stores one of the following in standard events:
        /// <para>-<see cref="MouseEventArgs.Y"/></para>
        /// <para>-<see cref="SizeChangedEventArgs.HeightBefore"/></para>
        /// </summary>
        public int data2;

        /// <summary>
        /// Stores one of the following in standard events:
        /// <para>-<see cref="EMouseButton"/></para>
        /// <para>-<see cref="EKeyCode"/></para>
        /// <para>-<see cref="MouseWheelEventArgs.DeltaX"/></para>
        /// <para>-<see cref="SizeChangedEventArgs.WidthAfter"/></para>
        /// </summary>
        public int data3;

        /// <summary>
        /// Stores one of the following in standard events:
        /// <para>-<see cref="MouseButtonEventArgs.ClickCount"/></para>
        /// <para>-<see cref="MouseWheelEventArgs.DeltaY"/></para>
        /// <para>-<see cref="SizeChangedEventArgs.HeightAfter"/></para>
        /// </summary>
        public int data4;
    }
}
