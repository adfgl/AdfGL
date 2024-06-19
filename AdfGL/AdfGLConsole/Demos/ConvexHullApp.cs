using AdfGL_Cad3Dlib.Controls;
using AdfGL_Cad3Dlib;
using AdfGLCoreLib.Application;
using AdfGLDrawingLib;
using LinearAlgebraLib;
using AdfGL_Cad3Dlib.Objects;
using GeometryLib;
using AdfGL_Cad3Dlib.Utils;
using System.Diagnostics;
using static GeometryLib.ConvexHull;
using CvxLib;
using System;

namespace AdfGLConsole.Demos
{
    public class ConvexHullApp : AppBase
    {
        Cad3DDataContext context = new Cad3DDataContext();

        GLMesh ToMesh(Mesh m)
        {
            GLMesh mesh = new GLMesh(m.Points.Length, m.Faces.Count);
            for (int i = 0; i < mesh.VertexCount; i++)
            {
                mesh.SetVertex(i, m.Points[i]);
            }

            for (int i = 0; i < mesh.TriangleCount; i++)
            {
                var t = m.Faces[i];
                mesh.SetTriangle(i, t.Indices[0], t.Indices[1], t.Indices[2]);
            }
            return mesh;
        }

        GLMesh ToMesh(CVX m)
        {
            GLMesh mesh = new GLMesh(m.Vertices.Count, m.Faces.Count);
            for (int i = 0; i < mesh.VertexCount; i++)
            {
                mesh.SetVertex(i, m.Vertices[i].Pos);
            }

            for (int i = 0; i < mesh.TriangleCount; i++)
            {
                var t = m.Faces[i];
                int a = t.Edge.Origin.Index;
                int b = t.Edge.Next.Origin.Index;
                int c = t.Edge.Next.Next.Origin.Index;
                
                mesh.SetTriangle(i, a, b, c);
            }
            return mesh;
        }

        public ConvexHullApp(AppEventHandler? eventHandler) : base(eventHandler)
        {
            Background = IntColours.Transparent;
            Controls.Main = new Cad3DMainControl(context).Enable();
            Controls.AttachChild(new Cad3DCameraControl(context));
        }


        OrbitalRayCaster BuildSimpleRayCaster(Vec3 center, CVX cvx)
        {
            OrbitalRayCaster rayCaster = new OrbitalRayCaster(center, cvx.Faces.Count);
            foreach (var face in cvx.Faces)
            {
                Triangle tri = new Triangle()
                {
                    A = face.Edge.Origin.Pos,
                    B = face.Edge.Next.Origin.Pos,
                    C = face.Edge.Next.Next.Origin.Pos,
                    Plane = face.Plane
                };
                rayCaster.AddTriangle(tri);
            }
            return rayCaster;
        }

        OctOrbitalRayCaster BuildOctRayCaster(Vec3 center, CVX cvx)
        {
            OctOrbitalRayCaster rayCaster = new OctOrbitalRayCaster(center, cvx.Faces.Count);
            foreach (var face in cvx.Faces)
            {
                Triangle tri = new Triangle()
                {
                    A = face.Edge.Origin.Pos,
                    B = face.Edge.Next.Origin.Pos,
                    C = face.Edge.Next.Next.Origin.Pos,
                    Plane = face.Plane
                };
                rayCaster.AddTriangle(tri);
            }
            return rayCaster;
        }

        public void SetShape(IEnumerable<Vec3> points, double scale)
        {
            context.Container.Scene.Clear();

            Stopwatch sw = new Stopwatch();

            Console.WriteLine("Points: " + points.Count());

            Console.WriteLine("Old method");
            sw.Start();
            Mesh? hull = ConvexHull.Calculate(points.ToArray());
            sw.Stop();

            Console.WriteLine("Triangles: " + hull.Faces.Count);
            Console.WriteLine("Time: " + sw.ElapsedMilliseconds + " ms");

            Console.WriteLine();

            Console.WriteLine("New method");
            sw.Restart();
            var hull2 = new CVX(points.ToArray()).Triangulate();
            sw.Stop();
            Console.WriteLine("Points left: " + hull2.Vertices.Count);
            Console.WriteLine("Triangles: " + hull2.Faces.Count);
            Console.WriteLine("Time: " + sw.ElapsedMilliseconds + " ms");

            Console.WriteLine();

            var center = Vec3.Zero;
            var orbital = BuildSimpleRayCaster(center, hull2);
            var octOrbital = BuildOctRayCaster(center, hull2);

            int count = 10;
            double totRatio = 0;
            foreach (var item in PointsGenerator.RandomPointCloud(count, 1000))
            {
                orbital.FindIntersection(item, out int stepsSimple);
                octOrbital.FindIntersection(item, out int stepsOct);

                double ratio = Math.Round((double)stepsSimple / (double)stepsOct, 2);
                totRatio += ratio;
                //Console.WriteLine("Simple/Oct: " + stepsSimple + "/" + stepsOct + $"({ratio})");
            }
            Console.WriteLine(totRatio / count);

            GLMesh mesh = ToMesh(hull2);

            Trans3 trans = new Trans3();
            trans.Scale(scale);
            mesh.Forward(trans);

            UserObject3D cvx = new UserObject3D(mesh)
            {
                Body = new Object3D.RenderMember()
                {
                    Draw = true,
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
                IsDoubleSide = true,
                IsTransparent = true
            };

            context.Container.Scene.Add(cvx);
        }
    }
}
