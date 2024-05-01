using AdfGLCoreLib.Events;
using AdfGLCoreLib.Services;
using AdfGLDrawingLib;

namespace AdfGLCoreLib.Application
{
    public class AppHost : IAppEventListener, IControlEventListener, IState
    {
        readonly AppEventHandler _eventHandler = null!;

        public AppHost(AppEventHandler? eventHandler)
        {
            if (eventHandler is not null)
            {
                eventHandler.EventListener = this;
                _eventHandler = eventHandler;
            }
            else
            {
                _eventHandler = new AppEventHandler(this);
            }
        }

        public int Background { get; set; }
        public int TargetFps { get; set; } = 60;
        public int ActualFps { get; internal set; } = 0;
        public bool UseDoubleBuffer { get; set; } = false;

        public void HandleEvent(in AppEvent e)
        {
            _eventHandler.HandleEvent(in e);
        }

        public bool Update(float elapsed)
        {
            return true;
        }

        public void RenderToBuffer(FrameBuffer activeBuffer)
        {
            if (Background == 0)
            {
                activeBuffer.Clear();
            }
            else
            {
                activeBuffer.Fill(Background);
            }

            IntBox box = activeBuffer.GetBox();

            activeBuffer.FillCircle(box, MouseX, MouseY, 10, IntColours.Gray, false);
        }


        #region IState
        public int MouseX { get { return ApplicationState.MouseX; } }
        public int MouseY { get { return ApplicationState.MouseY; } }
        public int ScreenWidth { get { return ApplicationState.ScreenWidth; } }
        public int ScreenHeight { get { return ApplicationState.ScreenHeight; } }
        public bool HasMouseFocus { get { return ApplicationState.HasMouseFocus; } }
        public bool HasKeyboardFocus { get { return ApplicationState.HasKeyboardFocus; } }
        public bool IsMinimized { get { return ApplicationState.IsMinimized; } }
        public bool IsMaximized { get { return ApplicationState.IsMaximized; } }
        public bool IsShown { get { return ApplicationState.IsShown; } }
        #endregion IState

        #region IAppEventListener
        public bool OnQuit()
        {
            return true;
        }

        public bool OnShown()
        {
            ApplicationState.IsShown = true;
            return true;
        }

        public bool OnHidden()
        {
            ApplicationState.IsShown = false;
            return true;
        }

        public bool OnMinimized()
        {
            ApplicationState.IsMinimized = true;
            ApplicationState.IsMaximized = false;
            return true;
        }

        public bool OnMaximized()
        {
            ApplicationState.IsMinimized = false;
            ApplicationState.IsMaximized = true;
            return true;
        }

        public bool OnRestored()
        {
            ApplicationState.IsMaximized = true;
            ApplicationState.IsMinimized = false;
            return true;
        }

        public bool OnGotMouseFocus()
        {
            ApplicationState.HasMouseFocus = true;
            return true;
        }

        public bool OnLostMouseFocus()
        {
            ApplicationState.HasMouseFocus = false;
            return true;
        }

        public bool OnGotKeyboardFocus()
        {
            ApplicationState.HasKeyboardFocus = true;
            return true;
        }

        public bool OnLostKeyboardFocus()
        {
            ApplicationState.HasKeyboardFocus = false;
            return true;
        }
        #endregion IAppEventListener

        #region IControlEventListener
        public bool OnSizeChanged(SizeChangedEventArgs e)
        {
            ApplicationState.ScreenWidth = e.WidthAfter;
            ApplicationState.ScreenHeight = e.HeightAfter;

            return false;
        }

        public bool OnCustomEvent(in AppEvent e)
        {

            return false;
        }

        public bool OnMouseDown(MouseButtonEventArgs e)
        {
            InputDevices.Mouse.Handle(e.Button, true);

            return false;
        }

        public bool OnMouseUp(MouseButtonEventArgs e)
        {
            InputDevices.Mouse.Handle(e.Button, false);

            return false;
        }

        public bool OnMouseMove(MouseEventArgs e)
        {
            ApplicationState.MouseX = e.X;
            ApplicationState.MouseY = e.Y;

            return false;
        }

        public bool OnMouseWheel(MouseWheelEventArgs e)
        {
            return false;
        }

        public bool OnKeyDown(KeyEventArgs e)
        {
            InputDevices.Keyboard.Handle(e.Key, true);
            return false;
        }

        public bool OnKeyUp(KeyEventArgs e)
        {
            InputDevices.Keyboard.Handle(e.Key, false);
            return false;
        }

        public bool OnTextInput(TextInputEventArgs e)
        {
            return false;
        }

        public bool OnDropFile(DropFileEventArgs e)
        {
            return false;
        }
        #endregion IControlEventListener
    }
}
