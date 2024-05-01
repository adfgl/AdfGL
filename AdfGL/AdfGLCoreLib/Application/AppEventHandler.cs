using AdfGLCoreLib.Enums;
using AdfGLCoreLib.Events;

namespace AdfGLCoreLib.Application
{
    public class AppEventHandler
    {
        public IAppEventListener EventListener;

        public AppEventHandler(IAppEventListener eventListener)
        {
            this.EventListener = eventListener;
        }

        public virtual void HandleEvent(in AppEvent e)
        {
            switch (e.type)
            {
                #region main
                case EEventType.Quit:
                    EventListener.OnQuit();
                    break;

                case EEventType.SizeChanged:
                    EventListener.OnSizeChanged(new SizeChangedEventArgs(
                        widthBefore: e.data1,
                        heightBefore: e.data2,
                        widthAfter: e.data3,
                        heightAfter: e.data4));
                    break;
                #endregion main

                #region mouse
                case EEventType.MouseMove:
                    EventListener.OnMouseMove(new MouseEventArgs(x: e.data1, y: e.data2));
                    break;

                case EEventType.MouseWheel:
                    EventListener.OnMouseWheel(new MouseWheelEventArgs(
                        x: e.data1,
                        y: e.data2,
                        deltaX: e.data3,
                        deltaY: e.data4));
                    break;

                case EEventType.MouseButtonDown:
                    EventListener.OnMouseDown(new MouseButtonEventArgs(
                        x: e.data1,
                        y: e.data2,
                        button: (EMouseButton)e.data3,
                        clickCount: e.data4));
                    break;
                case EEventType.MouseButtonUp:
                    EventListener.OnMouseUp(new MouseButtonEventArgs(
                        x: e.data1,
                        y: e.data2,
                        button: (EMouseButton)e.data3,
                        clickCount: e.data4));
                    break;

                case EEventType.GotMouseFocus:
                    EventListener.OnGotMouseFocus();
                    break;
                case EEventType.LostMouseFocus:
                    EventListener.OnLostMouseFocus();
                    break;
                #endregion mouse

                #region keyboard
                case EEventType.GotKeyboardFocus:
                    EventListener.OnGotKeyboardFocus();
                    break;
                case EEventType.LostKeyboardFocus:
                    EventListener.OnLostKeyboardFocus();
                    break;

                case EEventType.KeyDown:
                    EventListener.OnKeyDown(new KeyEventArgs(key: (EKeyCode)e.data3));
                    break;
                case EEventType.KeyUp:
                    EventListener.OnKeyUp(new KeyEventArgs(key: (EKeyCode)e.data3));
                    break;
                case EEventType.TextInput:
                    EventListener.OnTextInput(new TextInputEventArgs(e.stringdata));
                    break;
                #endregion keyboard

                #region other
                case EEventType.DropFile:
                    EventListener.OnDropFile(new DropFileEventArgs(path: e.stringdata));
                    break;

                case EEventType.Custom:
                    EventListener.OnCustomEvent(e);
                    break;
                #endregion other

                #region window
                case EEventType.Shown:
                    EventListener.OnShown();
                    break;
                case EEventType.Hidden:
                    EventListener.OnHidden();
                    break;
                case EEventType.Minimized:
                    EventListener.OnMinimized();
                    break;
                case EEventType.Maximized:
                    EventListener.OnMaximized();
                    break;
                case EEventType.Restored:
                    EventListener.OnRestored();
                    break;
                #endregion window

                default:
                    break;
            }
        }
    }
}
