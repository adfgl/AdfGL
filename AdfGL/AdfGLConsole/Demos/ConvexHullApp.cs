using AdfGL_Cad3Dlib.Controls;
using AdfGL_Cad3Dlib;
using AdfGLCoreLib.Application;
using AdfGLDrawingLib;
using LinearAlgebraLib;
using AdfGL_Cad3Dlib.Objects;
using GeometryLib;
using AdfGL_Cad3Dlib.Utils;
using System.Diagnostics;

namespace AdfGLConsole.Demos
{
    public class ConvexHullApp : AppBase
    {
        Cad3DDataContext context = new Cad3DDataContext();

        public ConvexHullApp(AppEventHandler? eventHandler) : base(eventHandler)
        {
            Background = IntColours.Transparent;
            Controls.Main = new Cad3DMainControl(context).Enable();
            Controls.AttachChild(new Cad3DCameraControl(context));
        }

        public void SetShape(IEnumerable<Vec3> points)
        {
            context.Container.Scene.Clear();

            Console.WriteLine("Points: " + points.Count());
            Stopwatch sw = new Stopwatch();
            sw.Start();

            var hull = ConvexHull.Calculate(points.ToArray());

            sw.Stop();

            Console.WriteLine("Triangles: " + hull.Faces.Count);
            Console.WriteLine("Time: " + sw.ElapsedMilliseconds + " ms");

            GLMesh mesh = new GLMesh(hull.Points.Length, hull.Faces.Count);
            for (int i = 0; i < mesh.VertexCount; i++)
            {
                mesh.SetVertex(i, hull.Points[i]);
            }

            for (int i = 0; i < mesh.TriangleCount; i++)
            {
                var t = hull.Faces[i];
                mesh.SetTriangle(i, t.Indices[0], t.Indices[1], t.Indices[2]);
            }

            Trans3 trans = new Trans3();
            trans.Scale(0.5);
            mesh.Forward(trans);

            UserObject3D cvx = new UserObject3D(mesh)
            {
                Body = new Object3D.RenderMember()
                {
                    Draw = false,
                    InnerColour = Colours.Wheat,
                    OuterColour = Colours.Gray,
                },

                Nodes = new Object3D.RenderMember()
                {
                    Draw = false,
                    InnerColour = Colours.DarkRed,
                    OuterColour = Colours.DarkBlue,
                },

                Fireframe = new Object3D.RenderMember()
                {
                    Draw = true,
                    InnerColour = Colours.Purple,
                    OuterColour = Colours.DeepSkyBlue,
                },

                Opacity = 0.25f,
                IsDoubleSide = true
            };

            context.Container.Scene.Add(cvx);
        }
    }
}
