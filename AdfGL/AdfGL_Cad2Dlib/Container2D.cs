using AdfGLDrawingLib;
using LinearAlgebraLib;

namespace AdfGL_Cad2Dlib
{
    public class Container2D
    {
        public Vec2 WorldOffset { get; set; } = Vec2.Zero;
        public double WorldScale { get; set; } = 1.0f;
        public Scene2D Scene { get; set; } = new Scene2D();

        public void Update(double ellapse)
        {

        }

        public void Render(IntBox box, FrameBuffer buffer)
        {
            var axis = new YaAxisLines(this);
            axis.DrawSelf(box, buffer);

            foreach (var item in Scene)
            {
                IntBox ebox = item.GetBox();
                if (false == box.Intersects(ebox)) continue;
                item.DrawSelf(box, buffer);
            }
        }

        static void WorldToScreen(Vec2 worldOffset, double worldScale, double worldX, double worldY, out int screenX, out int screenY)
        {
            screenX = (int)((worldX - worldOffset.x) * worldScale);
            screenY = (int)((-worldY - worldOffset.y) * worldScale);
        }

        static void ScreenToWorld(Vec2 worldOffset, double worldScale, int screenX, int screenY, out double worldX, out double worldY)
        {
            worldX = screenX / worldScale + worldOffset.x;
            worldY = -(screenY / worldScale + worldOffset.y); // invert y
        }

        public void WorldToScreen(double worldX, double worldY, out int screenX, out int screenY)
        {
            WorldToScreen(WorldOffset, WorldScale, worldX, worldY, out screenX, out screenY);
        }

        public void ScreenToWorld(int screenX, int screenY, out double worldX, out double worldY)
        {
            ScreenToWorld(WorldOffset, WorldScale, screenX, screenY, out worldX, out worldY);
        }
    }
}
