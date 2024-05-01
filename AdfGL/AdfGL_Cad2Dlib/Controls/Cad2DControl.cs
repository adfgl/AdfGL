using AdfGLCoreLib.Application;

namespace AdfGL_Cad2Dlib.Controls
{
    public abstract class Cad2DControl<T> : AppControlBase
    {
        public T DataContext { get; }

        public Cad2DControl(T dataContext)
        {
            DataContext = dataContext;
        }
    }
}
