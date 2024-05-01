using AdfGLCoreLib.Events;
using AdfGLDrawingLib;

namespace AdfGLCoreLib.Application
{
    public abstract class AppControlBase : IControlEventListener, IState
    {
        public bool IsEnabled { get; private set; }

        public virtual bool Update(float ellapse) { return false; }
        public abstract void Render(IntBox box, FrameBuffer activeBuffer);

        public virtual void OnEnabled() { }
        public virtual void OnDisabled() { }

        public AppControlBase Enable()
        {
            if (IsEnabled) return this;
            IsEnabled = true;
            OnEnabled();
            return this;
        }

        public AppControlBase Disable()
        {
            if (false == IsEnabled) return this;
            IsEnabled = false;
            OnDisabled();
            return this;
        }

        #region IControlEventListener
        public virtual bool OnCustomEvent(in AppEvent e)
        {
            return false;
        }

        public virtual bool OnSizeChanged(SizeChangedEventArgs e)
        {
            return false;
        }

        public virtual bool OnMouseDown(MouseButtonEventArgs e)
        {
            return false;
        }

        public virtual bool OnMouseUp(MouseButtonEventArgs e)
        {
            return false;
        }

        public virtual bool OnMouseMove(MouseEventArgs e)
        {
            return false;
        }

        public virtual bool OnMouseWheel(MouseWheelEventArgs e)
        {
            return false;
        }

        public virtual bool OnKeyDown(KeyEventArgs e)
        {
            return false;
        }

        public virtual bool OnKeyUp(KeyEventArgs e)
        {
            return false;
        }

        public virtual bool OnTextInput(TextInputEventArgs e)
        {
            return false;
        }

        public virtual bool OnDropFile(DropFileEventArgs e)
        {
            return false;
        }
        #endregion IControlEventListener

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
    }
}
