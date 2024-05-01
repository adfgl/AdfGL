using AdfGLCoreLib.Events;

namespace AdfGLCoreLib.Application
{
    public interface IControlEventListener
    {
        bool OnCustomEvent(in AppEvent e);

        bool OnSizeChanged(SizeChangedEventArgs e);

        bool OnMouseDown(MouseButtonEventArgs e);
        bool OnMouseUp(MouseButtonEventArgs e);

        bool OnMouseMove(MouseEventArgs e);
        bool OnMouseWheel(MouseWheelEventArgs e);

        bool OnKeyDown(KeyEventArgs e);
        bool OnKeyUp(KeyEventArgs e);
        bool OnTextInput(TextInputEventArgs e);

        bool OnDropFile(DropFileEventArgs e);
    }

    public interface IAppEventListener : IControlEventListener
    {
        bool OnQuit();

        bool OnShown();
        bool OnHidden();

        bool OnMinimized();
        bool OnMaximized();
        bool OnRestored();

        bool OnGotMouseFocus();
        bool OnLostMouseFocus();

        bool OnGotKeyboardFocus();
        bool OnLostKeyboardFocus();
    }

    public interface IHoverEventListener
    {
        bool OnMouseEnter();
        bool OnMouseLeave();

        bool OnGotFocus();
        bool OnLostFocus();
    }
}
