using AdfGLCoreLib.Enums;
using AdfGLCoreLib.Events;
using AdfGLCoreLib.Services;
using AdfGLDrawingLib;
using LinearAlgebraLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdfGL_Cad3Dlib.Controls
{
    public class Cad3DCameraControl : Cad3DControl<Cad3DDataContext>
    {
        const EMouseButton ROTATE_BUTTON = EMouseButton.Left;

        float _cameraRadius = 30f;
        float _cameraAzimuth = 0;
        float _cameraElevation = 0;

        float s_prevMouseX = 0;
        float s_prevMouseY = 0;

        Container3D axisContainer;

        public Cad3DCameraControl(Cad3DDataContext dataContext) : base(dataContext)
        {
            axisContainer = new Container3D();
            axisContainer.CameraControl.Radius = 60;
            axisContainer.CameraControl.CanChangeRadius = false;
            axisContainer.CameraControl.Origin = new Vec3(0, 0, 0);

            //axisContainer.Scene.Add(new UserObject3D(Cad3DDefaultMeshes.Axis)
            //{
            //    Position = new Vec3(0, 0, 0),
            //    IsDoubleSide = false,
            //    IsTransparent = false,
            //    Opacity = 0.8f,

            //    Body = new Object3D.RenderMember()
            //    {
            //        Draw = true,
            //        InnerColour = Colours.Transparent,
            //        OuterColour = Colours.Black,
            //    },

            //    Fireframe = new Object3D.RenderMember()
            //    {
            //        Draw = true,
            //        InnerColour = Colours.DeepSkyBlue,
            //        OuterColour = Colours.White,
            //    },

            //    Nodes = new Object3D.RenderMember()
            //    {
            //        Draw = false,
            //        InnerColour = Colours.DarkRed,
            //        OuterColour = Colours.CadetBlue,
            //    }
            //});
        }

        public override void OnEnabled()
        {
            DataContext.Container.Camera.SetSize(ScreenWidth, ScreenHeight);
            axisContainer.Camera.SetSize(ScreenWidth, ScreenHeight);

            DataContext.Container.CameraControl.UpdateCameraPosition(DataContext.Container.Camera);
            axisContainer.CameraControl.UpdateCameraPosition(axisContainer.Camera);
        }

        public override void Render(IntBox box, FrameBuffer activeBuffer)
        {
            //int width = 150;
            //int height = 150;
            //int x = 0;// Context.ScreenWidth - width;
            //int y = Context.ScreenHeight - height;

            //axisContainer.Renderer.Render(box, activeBuffer, x, y, width, height, 0);
        }

        public override bool OnSizeChanged(SizeChangedEventArgs e)
        {
            return false;
        }

        public override bool OnMouseMove(MouseEventArgs e)
        {
            float deltaX = e.X - s_prevMouseX;
            float deltaY = e.Y - s_prevMouseY;

            // handle camera rotation
            if (InputDevices.Mouse.IsPressed(ROTATE_BUTTON))
            {
                _cameraAzimuth -= deltaX * 0.15f;
                _cameraElevation -= deltaY * 0.15f;

                DataContext.Container.CameraControl.Azimuth = _cameraAzimuth;
                DataContext.Container.CameraControl.Elevation = _cameraElevation;
                DataContext.Container.CameraControl.UpdateCameraPosition(DataContext.Container.Camera);

                axisContainer.CameraControl.Azimuth = _cameraAzimuth;
                axisContainer.CameraControl.Elevation = _cameraElevation;
                axisContainer.CameraControl.UpdateCameraPosition(axisContainer.Camera);
            }

            s_prevMouseX = e.X;
            s_prevMouseY = e.Y;
            return false;
        }

        public override bool OnMouseWheel(MouseWheelEventArgs e)
        {
            int k = 100;
            if (InputDevices.Keyboard.IsCtrlPressed())
            {
                _cameraAzimuth += e.DeltaY * k * 0.15f;

                DataContext.Container.CameraControl.Azimuth = _cameraAzimuth;
                axisContainer.CameraControl.Azimuth = _cameraAzimuth;
            }
            else
            {
                _cameraRadius += -e.DeltaY * k * 0.15f;

                DataContext.Container.CameraControl.Radius = _cameraRadius;
                axisContainer.CameraControl.Radius = _cameraRadius;
            }
            DataContext.Container.CameraControl.UpdateCameraPosition(DataContext.Container.Camera);
            axisContainer.CameraControl.UpdateCameraPosition(axisContainer.Camera);
            return false;
        }

        public override bool OnMouseUp(MouseButtonEventArgs e)
        {
            //if (e.Button == AdfGL_Client.enums.EMouseButton.Middle)
            //{
            //    Ray ray = DataContext.Camera.ScreenPointToRay(e.X, e.Y);

            //    foreach (var item in DataContext.Scene)
            //    {
            //        if (RayCaster.Intersect(item.Mesh, ray, out Vec3 intersection))
            //        {
            //            _cameraOrigin = intersection;
            //            return false;
            //        }
            //    }
            //}
            return false;
        }
    }
}
