using AdfGLCoreLib.Application;

namespace AdfGL_Cad3Dlib.Controls
{
    public abstract class Cad3DControl<T> : AppControlBase
    {
        public T DataContext { get; }

        public Cad3DControl(T dataContext)
        {
            DataContext = dataContext;
        }
    }
}
