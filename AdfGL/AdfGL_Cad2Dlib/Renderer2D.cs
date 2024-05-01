using AdfGLDrawingLib;

namespace AdfGL_Cad2Dlib
{
    public class Renderer2D
    {
        public Renderer2D(Scene2D scene)
        {
            Scene = scene;
        }

        public Scene2D Scene { get; set; }

        public void Update(float ellapse)
        {

        }

        public void Render(IntBox box, FrameBuffer buffer)
        {
            foreach (var item in Scene)
            {
                IntBox ebox = item.GetBox();
                if (false == box.Intersects(ebox)) continue;
                item.DrawSelf(box, buffer);
            }
        }
    }
}
