using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrueSync.Physics3D
{
    public sealed class SphereSphereCollide
    {
        public static bool Detect(ISupportMappable support1, ISupportMappable support2, ref TSMatrix orientation1,
            ref TSMatrix orientation2, ref TSVector position1, ref TSVector position2,
            out TSVector point, out TSVector normal, out FP penetration)
        {
            // Used variables
            TSVector v01, v02;

            // Initialization of the output
            point = normal = TSVector.zero;
            penetration = FP.Zero;

            // Get the center of shape1 in world coordinates -> v01
            support1.SupportCenter(out v01);
            TSVector.Transform(ref v01, ref orientation1, out v01);
            TSVector.Add(ref position1, ref v01, out v01);

            // Get the center of shape2 in world coordinates -> v02
            support2.SupportCenter(out v02);
            TSVector.Transform(ref v02, ref orientation2, out v02);
            TSVector.Add(ref position2, ref v02, out v02);

            SphereShape sphere1 = support1 as SphereShape;
            SphereShape sphere2 = support2 as SphereShape;

            TSVector v01v02 = TSVector.Subtract(v01, v02);
            FP dot = TSVector.Dot(v01v02, v01v02);
            FP r = sphere1.radius + sphere2.radius;
            if (dot <= r*r)
            {
                ClosestPointSphereSphere(v01, sphere1.radius, v02, sphere2.radius, out normal, out point);
                TSVector.Negate(ref normal, out normal);
                penetration = r - TSMath.Sqrt(dot);
                return true;
            }
            return false;
        }


        /// <summary>
        /// Determines the closest point between a <see cref="SphereShape"/> and a <see cref="SphereShape"/>.
        /// </summary>
        /// <param name="center1">The first sphere center.</param>
        /// <param name="r1"> The first sphere radius.</param>
        /// <param name="center2">The second sphere center.</param>
        /// <param name="r2"> The second spshere radius</param>
        /// <param name="result">When the method completes, contains the closest point between the two objects;
        /// or, if the point is directly in the center of the sphere, contains <see cref="Vector3.Zero"/>.</param>
        /// <remarks>
        /// If the two spheres are overlapping, but not directly on top of each other, the closest point
        /// is the 'closest' point of intersection. This can also be considered is the deepest point of
        /// intersection.
        /// </remarks>
        public static void ClosestPointSphereSphere(TSVector center1, FP r1, TSVector center2, FP r2, out TSVector normal, out TSVector result)
        {
            //Source: Jorgy343
            //Reference: None

            //Get the unit direction from the first sphere's center to the second sphere's center.
            TSVector.Subtract(ref center2, ref center1, out result);
            result.Normalize();
            normal = result;

            //Multiply the unit direction by the first sphere's radius to get a vector
            //the length of the first sphere.
            result *= r1/(r1+r2);

            //Add the first sphere's center to the direction to get a point on the first sphere.
            result += center1;
        }
    }
}
