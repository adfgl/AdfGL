namespace AdfGL_Cad2Dlib
{
    public class Cad2DDataContext
    {
        public Container2D Container { get; set; }

        public Cad2DDataContext()
        {
            Container = new Container2D();
            Container.Scene = new Scene2D();
        }
    }
}
