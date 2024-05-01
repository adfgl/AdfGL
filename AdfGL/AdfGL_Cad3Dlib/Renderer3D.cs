using AdfGL_Cad3Dlib.Objects;
using AdfGL_Cad3Dlib.Utils;
using AdfGLDrawingLib;
using LinearAlgebraLib;

namespace AdfGL_Cad3Dlib
{
    public class Renderer3D
    {
        public Renderer3D(Camera camera, Scene3D scene)
        {
            Camera = camera;
            Scene = scene;
        }

        public Camera Camera { get; set; }
        public Scene3D Scene { get; set; }

        float s_theta = 0;

        public void Update(float ellapse)
        {
         
        }

        public void Render(IntBox box, FrameBuffer buffer, bool invertY)
        {
            int x = box.minX;
            int y = box.minY;
            int surfaceWidth = box.maxX - box.minX;
            int surfaceHeight = box.maxY - box.minY;

            List<Triangle3D> trianglesToRender = new List<Triangle3D>();

            if (Camera.Width != surfaceWidth || Camera.Height != surfaceHeight)
            {
                Camera.SetSize(surfaceWidth, surfaceHeight);
            }

            Mat4 mProj = Camera.Projection;
            Mat4 mCamera = Camera.View;
            Vec3 lightDirection = Camera.GetDirectionalLight();

            Trans3 trns = new Trans3();
            foreach (var obj3 in Scene)
            {
                trns.Reset();
                var (ox, oy, oz) = obj3.Position;
                trns.Translate(ox, oy, oz);
                if (obj3.RotationX != 0)
                {
                    trns.RotateX(obj3.RotationX);
                }
                if (obj3.RotationY != 0)
                {
                    trns.RotateY(obj3.RotationY);
                }
                if (obj3.RotationZ != 0)
                {
                    trns.RotateZ(obj3.RotationZ);
                }

                GLMesh mesh = obj3.Mesh;

                bool drawFill = obj3.Body.Draw;
                bool drawBorders = obj3.Fireframe.Draw;
                bool drawNodes = obj3.Nodes.Draw;

                for (int i = 0; i < mesh.TriangleCount; i++)
                {
                    mesh.GetTriangle(i, out var p1, out var p2, out var p3);
                    var triangle = new Triangle3D(obj3, mesh.GetVertex(p1), mesh.GetVertex(p2), mesh.GetVertex(p3)).Transform(trns);

                    if (invertY)
                    {
                        triangle.InvertY();
                    }

                    Vec3 triangleNormal = triangle.Normal();
                    Vec3 cameraRay = (triangle.P1 - Camera.Position).Normalize();

                    bool isTriangleVisisble = cameraRay.Dot(triangleNormal) < 0;
                    if (isTriangleVisisble == false)
                    {
                        if (false == obj3.IsTransparent && false == obj3.IsDoubleSide)
                        {
                            continue;
                        }
                    }

                    triangle.IsVisible = isTriangleVisisble;

                    triangle.DrawFill = drawFill;
                    triangle.DrawBorder = drawBorders;
                    triangle.DrawNodes = drawNodes;

                    if (obj3.IsDoubleSide && isTriangleVisisble == false)
                    {
                        triangle.FillColor = drawFill ? obj3.Body.InnerColour : Colours.Transparent;
                        triangle.BorderColor = drawBorders ? obj3.Fireframe.InnerColour : Colours.Transparent;
                        triangle.NodeColor = drawNodes ? obj3.Nodes.InnerColour : Colours.Transparent;
                    }
                    else
                    {
                        triangle.FillColor = drawFill ? obj3.Body.OuterColour : Colours.Transparent;
                        triangle.BorderColor = drawBorders ? obj3.Fireframe.OuterColour : Colours.Transparent;
                        triangle.NodeColor = drawNodes ? obj3.Nodes.OuterColour : Colours.Transparent;
                    }

                    if (obj3.IsTransparent && obj3.Opacity != 0)
                    {
                        triangle.DoBlending = true;
                    }
                    else
                    {
                        triangle.DoBlending = false;
                    }

                    // view
                    triangle.P1 *= mCamera;
                    triangle.P2 *= mCamera;
                    triangle.P3 *= mCamera;


                    var clippedZ = ClipAgainstPlane(new Vec3(0.0f, 0.0f, Camera.NearPlane), new Vec3(0.0f, 0.0f, 1f), triangle);

                    for (int j = 0; j < clippedZ.Count; j++)
                    {
                        Triangle3D? triangleClipped = clippedZ[j];

                        // project 3D >> 2D
                        triangleClipped.P1 *= mProj;
                        triangleClipped.P2 *= mProj;
                        triangleClipped.P3 *= mProj;

                        // apply shading
                        float lightIntensity = 0.6f;

                        float dp = (float)lightDirection.Dot(triangleClipped.IsVisible ? triangleNormal : triangleNormal * -1);

                        if (triangleClipped.DrawFill)
                        {
                            triangleClipped.FillColor = triangleClipped.FillColor.Eluminate(dp, lightIntensity);
                        }
                        if (triangleClipped.DrawBorder)
                        {
                            triangleClipped.BorderColor = triangleClipped.BorderColor.Eluminate(dp, lightIntensity);
                        }
                        if (triangleClipped.DrawNodes)
                        {
                            triangleClipped.NodeColor = triangleClipped.NodeColor.Eluminate(dp, lightIntensity);
                        }

                        triangleClipped.P1 = ViewportTransfrom(triangleClipped.P1, surfaceWidth, surfaceHeight);
                        triangleClipped.P2 = ViewportTransfrom(triangleClipped.P2, surfaceWidth, surfaceHeight);
                        triangleClipped.P3 = ViewportTransfrom(triangleClipped.P3, surfaceWidth, surfaceHeight);

                        trianglesToRender.Add(triangleClipped);
                    }
                }

                trianglesToRender.Sort(new PainterSort());

                foreach (var triToRender in trianglesToRender)
                {
                    Stack<Triangle3D> listTriangles = new Stack<Triangle3D>();
                    listTriangles.Push(triToRender);

                    int newTriangles = 1;
                    for (int plane = 0; plane < 4; plane++)
                    {
                        while (newTriangles > 0)
                        {
                            var test = listTriangles.Pop();
                            newTriangles--;

                            List<Triangle3D> trisToAdd = new List<Triangle3D>(2);
                            switch (plane)
                            {
                                case 0:
                                    trisToAdd = ClipAgainstPlane(new Vec3(0, 0, 0), new Vec3(0.0f, 1.0f, 0.0f), test);
                                    break;

                                case 1:
                                    trisToAdd = ClipAgainstPlane(new Vec3(0.0f, surfaceHeight - 1, 0.0f), new Vec3(0.0f, -1.0f, 0.0f), test);
                                    break;

                                case 2:
                                    trisToAdd = ClipAgainstPlane(new Vec3(0, 0, 0), new Vec3(1.0f, 0.0f, 0.0f), test);
                                    break;

                                case 3:
                                    trisToAdd = ClipAgainstPlane(new Vec3(surfaceWidth - 1, 0.0f, 0.0f), new Vec3(-1.0f, 0.0f, 0.0f), test);
                                    break;
                                default:
                                    break;
                            }

                            foreach (var item in trisToAdd)
                            {
                                listTriangles.Push(item);
                            }
                        }
                        newTriangles = listTriangles.Count;
                    }

                    // scale into view
                    foreach (Triangle3D triangle in listTriangles)
                    {
                        int x0 = (int)triangle.P1.x + x;
                        int y0 = (int)triangle.P1.y + y;

                        int x1 = (int)triangle.P2.x + x;
                        int y1 = (int)triangle.P2.y + y;

                        int x2 = (int)triangle.P3.x + x;
                        int y2 = (int)triangle.P3.y + y;

                        bool doBlend = triangle.DoBlending;
                        if (triangle.DrawFill)
                        {
                            int triangleColor = triangle.FillColor.ToInt();
                            if (doBlend)
                            {
                                triangleColor = triangle.FillColor.ApplyOpacity(triangle.Parent.Opacity).ToInt();
                            }

                            buffer.FillTriangle(box, x0, y0, x1, y1, x2, y2, triangleColor, doBlend);
                        }

                        if (triangle.DrawBorder)
                        {
                            ELineStyle lineStyle = triangle.IsVisible ? ELineStyle.Solid : ELineStyle.Dashed2by2;
                            int borderColor = triangle.BorderColor.ToInt();
                            if (doBlend)
                            {
                                borderColor = triangle.BorderColor.ApplyOpacity(triangle.Parent.Opacity).ToInt();
                            }

                            buffer.DrawLine(box, x0, y0, x1, y1, borderColor, doBlend, lineStyle);
                            buffer.DrawLine(box, x1, y1, x2, y2, borderColor, doBlend, lineStyle);
                            buffer.DrawLine(box, x2, y2, x0, y0, borderColor, doBlend, lineStyle);
                        }

                        if (triangle.DrawNodes)
                        {
                            int nodeFillColor = triangle.FillColor.ToInt();
                            if (doBlend)
                            {
                                nodeFillColor = triangle.FillColor.ApplyOpacity(triangle.Parent.Opacity).ToInt();
                            }
                            buffer.FillCircle(box, x0, y0, 2, nodeFillColor, false);
                            buffer.FillCircle(box, x1, y1, 2, nodeFillColor, false);
                            buffer.FillCircle(box, x2, y2, 2, nodeFillColor, false);

                            int nodeBorderColor = triangle.BorderColor.ToInt();
                            if (doBlend)
                            {
                                nodeBorderColor = triangle.BorderColor.ApplyOpacity(triangle.Parent.Opacity).ToInt();
                            }
                            buffer.DrawCircle(box, x0, y0, 2, nodeBorderColor, false);
                            buffer.DrawCircle(box, x1, y1, 2, nodeBorderColor, false);
                            buffer.DrawCircle(box, x2, y2, 2, nodeBorderColor, false);
                        }
                    }
                }
            }
        }

        static Vec3 ViewportTransfrom(Vec3 v, float w, float h)
        {
            // X/Y are inverted so put them back
            double ndcX = -v.x;
            double ndcY = -v.y;

            if (v.w != 0)
            {
                ndcX /= v.w;
                ndcY /= v.w;
            }

            double screenX = (ndcX + 1.0f) * (w / 2.0f);
            double screenY = (ndcY + 1.0f) * (h / 2.0f);
            return new Vec3(screenX, screenY, v.z);
        }

        public static List<Triangle3D> ClipAgainstPlane(Vec3 pointOnPlane, Vec3 planeNormal, Triangle3D triangle)
        {
            List<Triangle3D> newTriangles = new List<Triangle3D>(2);

            planeNormal = planeNormal.Normalize();

            Vec3[] inside = new Vec3[3];
            int insideCount = 0;

            Vec3[] outside = new Vec3[3];
            int outsideCount = 0;

            void checkPoint(Vec3 p)
            {
                var vectorToPoint = p - pointOnPlane;
                var dotProduct = planeNormal.Dot(vectorToPoint);
                if (dotProduct >= 0)
                {
                    inside[insideCount++] = p;
                }
                else
                {
                    outside[outsideCount++] = p;
                }
            }

            checkPoint(triangle.P1);
            checkPoint(triangle.P2);
            checkPoint(triangle.P3);

            if (insideCount == 0)
            {
                return newTriangles;
            }
            else if (insideCount == 3)
            {
                newTriangles.Add(new Triangle3D(triangle));
            }
            else if (insideCount == 1 && outsideCount == 2)
            {
                //       /\
                //   1  /  \  2
                //  ------------
                //    /      \
                var tri = new Triangle3D(triangle)
                {
                    P1 = LinePlaneIntersection(pointOnPlane, planeNormal, inside[0], outside[0]),
                    P2 = inside[0],
                    P3 = LinePlaneIntersection(pointOnPlane, planeNormal, inside[0], outside[1]),
                };
                newTriangles.Add(tri);
            }
            else if (insideCount == 2 && outsideCount == 1)
            {
                //    2
                //  +--+
                // 1 \ | 3
                //    \|
                //  ---+---
                var np1 = LinePlaneIntersection(pointOnPlane, planeNormal, inside[0], outside[0]);
                var tri1 = new Triangle3D(triangle)
                {
                    P1 = np1,
                    P2 = inside[0],
                    P3 = inside[1],
                };
                newTriangles.Add(tri1);

                //      2
                //    +---+
                //  1 |  / 3
                //    | /  
                // -- + ----
                var tri2 = new Triangle3D(triangle)
                {
                    P1 = np1,
                    P2 = inside[1],
                    P3 = LinePlaneIntersection(pointOnPlane, planeNormal, inside[1], outside[0]),
                };
                newTriangles.Add(tri2);
            }
            return newTriangles;
        }

        class PainterSort : IComparer<Triangle3D>
        {
            public int Compare(Triangle3D t1, Triangle3D t2)
            {
                var avgZ1 = t1.AverageZ();
                var avgZ2 = t2.AverageZ();

                if (avgZ1 < avgZ2) return -1;
                if (avgZ1 > avgZ2) return 1;
                return 0;
            }
        }

        public static Vec3 LinePlaneIntersection(Vec3 pointOnPlane, Vec3 planeNormal, Vec3 lineStart, Vec3 lineEnd)
        {
            planeNormal = planeNormal.Normalize();
            double planeDot = -planeNormal.Dot(pointOnPlane);

            double ad = lineStart.Dot(planeNormal);
            double bd = lineEnd.Dot(planeNormal);
            double t = (-planeDot - ad) / (bd - ad);
            Vec3 lineStartToEnd = lineEnd - lineStart;
            Vec3 lineToIntersect = lineStartToEnd * t;
            return lineStart + lineToIntersect;
        }
    }
}
