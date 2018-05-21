using System.Collections;
using System.Collections.Generic;

namespace TrueSync.Physics3D
{
    public class SphereTrianglePair : CollisionPair
    {
        public static ResourcePool<SphereTrianglePair> pool = new ResourcePool<SphereTrianglePair>();

        public override bool IsColliding(ref TSMatrix orientation1, ref TSMatrix orientation2, ref TSVector position1, ref TSVector position2,
            out TSVector point, out TSVector point1, out TSVector point2, out TSVector normal, out FP penetration)
        {
            // Used variables
            TSVector center1, center2;

            // Initialization of the output
            point = point1 = point2 = normal = TSVector.zero;
            penetration = FP.Zero;

            TriangleMeshShape triangle = this.Shape1 as TriangleMeshShape;
            SphereShape sphere = this.Shape2 as SphereShape;

            // Get the center of sphere in world coordinates -> center1
            triangle.SupportCenter(out center1);
            TSVector.Transform(ref center1, ref orientation1, out center1);
            TSVector.Add(ref position1, ref center1, out center1);

            // Get the center of triangle in world coordinates -> center2
            sphere.SupportCenter(out center2);
            TSVector.Transform(ref center2, ref orientation2, out center2);
            TSVector.Add(ref position2, ref center2, out center2);


            TSVector[] vertices = triangle.Vertices;
            TSVector vertex0;
            TSVector.Transform(ref vertices[0], ref orientation1, out vertex0);
            TSVector.Add(ref position1, ref vertex0, out vertex0);
            TSVector vertex1;
            TSVector.Transform(ref vertices[1], ref orientation1, out vertex1);
            TSVector.Add(ref position1, ref vertex1, out vertex1);
            TSVector vertex2;
            TSVector.Transform(ref vertices[2], ref orientation1, out vertex2);
            TSVector.Add(ref position1, ref vertex2, out vertex2);


            ClosestPointPointTriangle(ref center2, ref vertex0, ref vertex1, ref vertex2, out point);
            TSVector v = point - center2;

            FP dot = TSVector.Dot(ref v, ref v);

            if (dot <= sphere.Radius * sphere.Radius)
            {
                normal = TSVector.Cross(TSVector.Subtract(vertex0, vertex1), TSVector.Subtract(vertex0, vertex2)).normalized;
                point1 = point;
                point2 = center2 + TSVector.Negate(normal) * sphere.radius;
                penetration = sphere.Radius - TSMath.Sqrt(dot);
                return true;
            }
            return false;
        }


        /// <summary>
        /// Determines the closest point between a point and a triangle.
        /// </summary>
        /// <param name="point">The point to test.</param>
        /// <param name="vertex1">The first vertex to test.</param>
        /// <param name="vertex2">The second vertex to test.</param>
        /// <param name="vertex3">The third vertex to test.</param>
        /// <param name="result">When the method completes, contains the closest point between the two objects.</param>
        public static void ClosestPointPointTriangle(ref TSVector point, ref TSVector vertex1, ref TSVector vertex2, ref TSVector vertex3, out TSVector result)
        {
            //Source: Real-Time Collision Detection by Christer Ericson
            //Reference: Page 136

            //Check if P in vertex region outside A
            TSVector ab = vertex2 - vertex1;
            TSVector ac = vertex3 - vertex1;
            TSVector ap = point - vertex1;

            FP d1 = TSVector.Dot(ab, ap);
            FP d2 = TSVector.Dot(ac, ap);
            if (d1 <= FP.Zero && d2 <= FP.Zero)
            {
                result = vertex1; //Barycentric coordinates (1,0,0)
                return;
            }

            //Check if P in vertex region outside B
            TSVector bp = point - vertex2;
            FP d3 = TSVector.Dot(ab, bp);
            FP d4 = TSVector.Dot(ac, bp);
            if (d3 >= FP.Zero && d4 <= d3)
            {
                result = vertex2; // Barycentric coordinates (0,1,0)
                return;
            }

            //Check if P in edge region of AB, if so return projection of P onto AB
            FP vc = d1 * d4 - d3 * d2;
            if (vc <= FP.Zero && d1 >= FP.Zero && d3 <= FP.Zero)
            {
                FP v = d1 / (d1 - d3);
                result = vertex1 + v * ab; //Barycentric coordinates (1-v,v,0)
                return;
            }

            //Check if P in vertex region outside C
            TSVector cp = point - vertex3;
            FP d5 = TSVector.Dot(ab, cp);
            FP d6 = TSVector.Dot(ac, cp);
            if (d6 >= FP.Zero && d5 <= d6)
            {
                result = vertex3; //Barycentric coordinates (0,0,1)
                return;
            }

            //Check if P in edge region of AC, if so return projection of P onto AC
            FP vb = d5 * d2 - d1 * d6;
            if (vb <= FP.Zero && d2 >= FP.Zero && d6 <= FP.Zero)
            {
                FP w = d2 / (d2 - d6);
                result = vertex1 + w * ac; //Barycentric coordinates (1-w,0,w)
                return;
            }

            //Check if P in edge region of BC, if so return projection of P onto BC
            FP va = d3 * d6 - d5 * d4;
            if (va <= FP.Zero && (d4 - d3) >= FP.Zero && (d5 - d6) >= FP.Zero)
            {
                FP w = (d4 - d3) / ((d4 - d3) + (d5 - d6));
                result = vertex2 + w * (vertex3 - vertex2); //Barycentric coordinates (0,1-w,w)
                return;
            }

            //P inside face region. Compute Q through its Barycentric coordinates (u,v,w)
            FP denom = FP.One / (va + vb + vc);
            FP v2 = vb * denom;
            FP w2 = vc * denom;
            result = vertex1 + ab * v2 + ac * w2; //= u*vertex1 + v*vertex2 + w*vertex3, u = va * denom = 1.0f - v - w
        }
    }
}
