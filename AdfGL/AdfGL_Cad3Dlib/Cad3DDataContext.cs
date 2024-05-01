using LinearAlgebraLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdfGL_Cad3Dlib
{
    public class Cad3DDataContext
    {
        public Container3D Container { get; set; } = null!;

        public Cad3DDataContext()
        {
            Container = new Container3D();
            Container.Camera.Position = new Vec3(0, 0, -60);
            Container.CameraControl.Radius = 1000;
            Container.CameraControl.Origin = new Vec3(0, 0, 0);
        }
    }
}
