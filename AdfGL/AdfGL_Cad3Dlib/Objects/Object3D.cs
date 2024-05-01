using AdfGL_Cad3Dlib.Utils;
using AdfGLDrawingLib;
using LinearAlgebraLib;

namespace AdfGL_Cad3Dlib.Objects
{
    public class Object3D
    {
        float _opacity = 1.0f;

        public Object3D()
        {
            Body = new RenderMember();
            Nodes = new RenderMember();
            Fireframe = new RenderMember();
        }

        public Object3D(Mesh mesh) : this()
        {
            Mesh = mesh;
            UsesSharedMesh = true;
        }

        public Mesh Mesh { get; set; } = null!;
        public bool UsesSharedMesh { get; } = false;

        public RenderMember Body { get; set; }
        public RenderMember Nodes { get; set; }
        public RenderMember Fireframe { get; set; }

        public Vec3 Position { get; set; } = Vec3.Zero;
        public float RotationX { get; set; } = 0.0f;
        public float RotationY { get; set; } = 0.0f;
        public float RotationZ { get; set; } = 0.0f;

        public bool ClipIgnore { get; set; } = false;
        public bool IsTransparent { get; set; } = false;
        public bool IsDoubleSide { get; set; } = false;

        public float Opacity
        {
            get { return _opacity; }
            set
            {
                if (value < 0 || value > 1.0f)
                {
                    throw new ArgumentException($"Invalid opacity value '{value}' Opacity must be positive number less or equal to zero.");
                }
                _opacity = value;
            }
        }

        public virtual void Update(float ellapse)
        {

        }

        public class RenderMember
        {
            public bool Draw { get; set; } = true;
            public Colour InnerColour { get; set; } = Colours.Maroon;
            public Colour OuterColour { get; set; } = Colours.DeepSkyBlue;
        }

    }
}
