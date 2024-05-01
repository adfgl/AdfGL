using AdfGLDrawingLib;
using LinearAlgebraLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdfGL_Cad3Dlib.Objects
{
    public class Triangle3D
    {
        public Triangle3D(Triangle3D triangle)
        {
            P1 = triangle.P1;
            P2 = triangle.P2;
            P3 = triangle.P3;

            DrawFill = triangle.DrawFill;
            FillColor = triangle.FillColor;

            DrawBorder = triangle.DrawBorder;
            BorderColor = triangle.BorderColor;

            DrawNodes = triangle.DrawNodes;
            NodeColor = triangle.NodeColor;

            Parent = triangle.Parent;
            IsVisible = triangle.IsVisible;
            DoBlending = triangle.DoBlending;
        }

        public Triangle3D(Object3D parent, Vec3 p1, Vec3 p2, Vec3 p3)
        {
            P1 = p1;
            P2 = p2;
            P3 = p3;
            Parent = parent;
        }

        public Vec3 P1 { get; set; }
        public Vec3 P2 { get; set; }
        public Vec3 P3 { get; set; }

        public bool DrawFill { get; set; }
        public Colour FillColor { get; set; }

        public bool DrawBorder { get; set; }
        public Colour BorderColor { get; set; }

        public bool DrawNodes { get; set; }
        public Colour NodeColor { get; set; }

        public Object3D Parent { get; private set; }
        public bool IsVisible { get; set; }
        public bool DoBlending { get; set; } = false;

        public void InvertY()
        {
            P1 = new Vec3(P1.x, -P1.y, P1.z, P1.w);
            P2 = new Vec3(P2.x, -P2.y, P2.z, P2.w);
            P3 = new Vec3(P3.x, -P3.y, P3.z, P3.w);
        }

        public void Invert()
        {
            var temp = P1;
            P1 = P2;
            P2 = temp;
        }

        public Vec3 Normal()
        {
            var v12 = P2 - P1;
            var v13 = P3 - P1;
            return v12.Cross(v13).Normalize();
        }

        public Triangle3D Transform(Trans3 mat)
        {
            P1 = mat.Forward(P1);
            P2 = mat.Forward(P2);
            P3 = mat.Forward(P3);
            return this;
        }

        public double AverageZ()
        {
            return (P1.z + P2.z + P3.z) / 3.0f;
        }
    }
}
