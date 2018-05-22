using System.Collections;
using System.Collections.Generic;

namespace TrueSync.Physics3D
{
    public class BoxSpherePair : CollisionPair
    {
        public static ResourcePool<BoxSpherePair> pool = new ResourcePool<BoxSpherePair>();

        protected TSVector[] n = new TSVector[]{
        new TSVector(-FP.One, FP.Zero, FP.Zero),
        new TSVector(FP.Zero, -FP.One, FP.Zero),
        new TSVector(FP.Zero, FP.Zero, -FP.One),
        new TSVector(FP.One, FP.Zero, FP.Zero),
        new TSVector(FP.Zero, FP.One, FP.Zero),
        new TSVector(FP.Zero, FP.Zero, FP.One)
        };

        private TSVector[] bounds = new TSVector[2];
        private TSVector[] boundsVec = new TSVector[2];

        public override bool IsColliding(ref TSMatrix orientation1, ref TSMatrix orientation2, ref TSVector position1, ref TSVector position2,
            out TSVector point, out TSVector point1, out TSVector point2, out TSVector normal, out FP penetration)
        {
            // Used variables
            TSVector center1, center2;

            // Initialization of the output
            point = point1 = point2 = normal = TSVector.zero;
            penetration = FP.Zero;

            BoxShape box = this.Shape1 as BoxShape;
            SphereShape sphere = this.Shape2 as SphereShape;

            // Get the center of box in world coordinates -> center1
            box.SupportCenter(out center1);
            TSVector.Transform(ref center1, ref orientation1, out center1);
            TSVector.Add(ref position1, ref center1, out center1);

            // Get the center of sphere in world coordinates -> center2
            sphere.SupportCenter(out center2);
            TSVector.Transform(ref center2, ref orientation2, out center2);
            TSVector.Add(ref position2, ref center2, out center2);

            FP dist = GetSphereDistance(ref box, ref center1, ref orientation1, ref center2, sphere.radius,
                ref point1, ref point2, ref normal);

            if (dist < TSMath.Epsilon)
            {
                penetration = -dist;
                return true;
            }
            return false;
        }

        public FP GetSphereDistance(ref BoxShape box, ref TSVector boxPosition, ref TSMatrix boxOrientation, ref TSVector sphereCenter, FP radius,
            ref TSVector pointOnBox, ref TSVector pointOnSphere, ref TSVector normal)
        {
            FP margins;

            bounds[0] = TSVector.Negate(box.halfSize);
            bounds[1] = box.halfSize;

            margins = FP.Zero; //TODO: box.Margin; //also add sphereShape margin?

            boundsVec[0] = bounds[0];
            boundsVec[1] = bounds[1];

            TSVector marginsVec = TSVector.one * margins;

            // add margins
            bounds[0] += marginsVec;
            bounds[1] -= marginsVec;

            TSVector prel, v3P;
            FP sep = FP.MaxValue; 
            FP sepThis;

            // convert  point in local space
            TSVector.Subtract(ref sphereCenter, ref boxPosition, out sphereCenter);
            TSMatrix invBoxOrientation;
            TSMatrix.Inverse(ref boxOrientation, out invBoxOrientation);
            TSVector.Transform(ref sphereCenter, ref invBoxOrientation, out prel);

            bool found = false;

            v3P = prel;

            for (int i = 0; i < 6; i++)
            {
                int j = i < 3 ? 0 : 1;
                if ((sepThis = (TSVector.Dot(v3P - bounds[j], n[i]))) > FP.Zero)
                {
                    v3P = v3P - n[i] * sepThis;
                    found = true;
                }
            }

            if (found)
            {
                bounds[0] = boundsVec[0];
                bounds[1] = boundsVec[1];

                normal = TSVector.Normalize(prel - v3P);
                pointOnBox = v3P + normal * margins;
                pointOnSphere = prel - normal * radius;

                if ((TSVector.Dot(pointOnSphere - pointOnBox, normal)) > FP.Zero)
                {
                    return FP.One;
                }

                FP seps2 = (pointOnBox - pointOnSphere).sqrMagnitude;

                //if this fails, fallback into deeper penetration case, below
                if (seps2 > TSMath.Epsilon)
                {
                    // transform back in world space
                    TSVector.Transform(ref pointOnBox, ref boxOrientation, out pointOnBox);
                    TSVector.Add(ref pointOnBox, ref boxPosition, out pointOnBox);
                    TSVector.Transform(ref pointOnSphere, ref boxOrientation, out pointOnSphere);
                    TSVector.Add(ref pointOnSphere, ref boxPosition, out pointOnSphere);

                    sep = -TSMath.Sqrt(seps2);
                    normal = (pointOnBox - pointOnSphere);
                    normal *= FP.One / sep;
                }
                return sep;
            }
            return FP.One;
        }
    }
}
