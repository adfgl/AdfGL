using LinearAlgebraLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdfGL_Cad3Dlib
{
    public interface ICameraControl
    {
        Camera UpdateCameraPosition(Camera camera);
    }

    public class Container3D
    {


        public Container3D()
        {
            Scene = new Scene3D();
            Camera = new Camera()
            {
                Position = new Vec3(0, 0, 30),
                NearPlane = 0.1f,
                FarPlane = 6000.0f,
                FieldOfView = 90
            };
            Renderer = new Renderer3D(Camera, Scene);
            CameraControl = new OrbitalCameraControl();
        }

        public Camera Camera { get; set; } = null!;
        public OrbitalCameraControl CameraControl { get; set; } = null!;
        public Scene3D Scene { get; set; } = null!;
        public Renderer3D Renderer { get; set; } = null!;
    }

    public class Camera
    {
        public const double DEG2RAD = Math.PI / 180;
        public const double RAD2DEG = 180 / Math.PI;

        public Camera()
        {
            AxiiIdentity();
        }

        public Vec3 Right { get; set; } = Vec3.UnitX;
        public Vec3 Up { get; set; } = Vec3.UnitY;
        public Vec3 Forward { get; set; } = Vec3.UnitZ;

        public Vec3 Position { get; set; }

        public double NearPlane { get; set; }
        public double FarPlane { get; set; }
        public double FieldOfView { get; set; }

        public double MovingSpeed { get; set; } = 0.25f;
        public double RotationSpeed { get; set; } = 0.015f;

        public double AnglePitch { get; set; }
        public double AngleYaw { get; set; }

        public double Width { get; private set; }
        public double Height { get; private set; }

        public Mat4 Projection { get; private set; }
        public Mat4 View
        {
            get
            {
                (double x, double y, double z) = Position;
                var trans = new Mat4(
                     1, 0, 0, 0,
                     0, 1, 0, 0,
                     0, 0, 1, 0,
                     -x, -y, -z, 1);

                (double rx, double ry, double rz) = Right;
                (double fx, double fy, double fz) = Forward;
                (double ux, double uy, double uz) = Up;

                var rot = new Mat4(
                    rx, ux, fx, 0,
                    ry, uy, fy, 0,
                    rz, uz, fz, 0,
                    0, 0, 0, 1);

                return Mat4.Transpose(trans * rot);
            }
        }

        public void SetSize(double width, double height)
        {
            Width = width;
            Height = height;
            UpdatePerspectiveMatrix(width, height);
        }

        public void AxiiIdentity()
        {
            Right = Vec3.UnitX;
            Up = Vec3.UnitY;
            Forward = Vec3.UnitZ;
        }

        public void LookAt(Vec3 cameraTarget)
        {
            AxiiIdentity();
            Forward = (cameraTarget - Position).Normalize();
            AnglePitch = Math.Asin(-Forward.y);
            AngleYaw = Math.Atan2(Forward.x, Forward.z);
            Right = Forward.Cross(Vec3.UnitY).Normalize();
            Up = Right.Cross(Forward).Normalize();
        }

        public Vec3 GetDirectionalLight()
        {
            return Forward * -1;
        }

        void UpdatePerspectiveMatrix(double width, double height)
        {
            double fAspectRatio = width / height;
            double fFovVertical = FieldOfView * DEG2RAD;
            double fFovHorizontal = 2.0f * Math.Atan(Math.Tan(fFovVertical / 2.0f) * fAspectRatio);

            double NEAR = NearPlane;
            double FAR = FarPlane;
            double RIGHT = Math.Tan(fFovHorizontal / 2.0f) * NEAR;
            double LEFT = -RIGHT;
            double TOP = Math.Tan(fFovVertical / 2.0f) * NEAR;
            double BOTTOM = -TOP;

            double m00 = 2.0f * NEAR / (RIGHT - LEFT);
            double m11 = 2.0f * NEAR / (TOP - BOTTOM);
            double m22 = -(FAR + NEAR) / (FAR - NEAR);
            double m32 = -(2.0f * FAR * NEAR) / (FAR - NEAR);

            Projection = new Mat4(
               m00, 0, 0, 0,
               0, m11, 0, 0,
               0, 0, m22, -1,
               0, 0, m32, 0);
        }

        void CreateOrthographicProjectionMatrix(double left, double right, double bottom, double top)
        {
            double near = NearPlane;
            double far = FarPlane;

            double m00 = 2.0f / (right - left);
            double m11 = 2.0f / (top - bottom);
            double m22 = -2.0f / (far - near);
            double m30 = -(right + left) / (right - left);
            double m31 = -(top + bottom) / (top - bottom);
            double m32 = -(far + near) / (far - near);

            Projection = new Mat4(
               m00, 0, 0, m30,
               0, m11, 0, m31,
               0, 0, m22, m32,
               0, 0, 0, 1
            );
        }
    }

    public class OrbitalCameraControl : ICameraControl
    {
        public const double DEG2RAD = Math.PI / 180;
        public const double RAD2DEG = 180 / Math.PI;

        Vec3 _origin = Vec3.Zero;
        double _radius = 100f;
        double _elevation = 0.01f;
        double _azimuth = 0.01f;

        public Vec3 Origin
        {
            get { return _origin; }
            set { _origin = value; }
        }

        public bool CanChangeAzimuth { get; set; } = true;
        public double Azimuth
        {
            get { return _azimuth; }
            set
            {
                if (false == CanChangeAzimuth) return;
                _azimuth = value;
            }
        }

        public bool CanChangeRadius { get; set; } = true;
        public double Radius
        {
            get { return _radius; }
            set
            {
                if (false == CanChangeRadius) return;
                _radius = Math.Clamp(value, MinRadius, MaxRadius);
            }
        }
        public double MinRadius { get; set; } = 0.001f;
        public double MaxRadius { get; set; } = 10000f;

        public bool CanChangeElevation { get; set; } = true;
        public double Elevation
        {
            get { return _elevation; }
            set
            {
                if (false == CanChangeElevation) return;
                _elevation = Math.Clamp(value, MinElevation, MaxElevation);
            }
        }
        public double MinElevation { get; set; } = -89.5f;
        public double MaxElevation { get; set; } = 89.5f;

        public Camera UpdateCameraPosition(Camera camera)
        {
            double x = _radius * Math.Sin(_azimuth * DEG2RAD) * Math.Cos(_elevation * DEG2RAD);
            double y = _radius * Math.Sin(_elevation * DEG2RAD);    
            double z = _radius * Math.Cos(_azimuth * DEG2RAD) * Math.Cos(_elevation * DEG2RAD);
                
            camera.Position = new Vec3(x, y, z) + _origin;
            camera.LookAt(_origin);
            return camera;
        }
    }
}
