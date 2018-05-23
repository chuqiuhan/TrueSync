using System.Collections;
using System.Collections.Generic;

namespace TrueSync.Physics3D
{
    public class CapsuleCapsulePair : CollisionPair
    {
        public static ResourcePool<CapsuleCapsulePair> pool = new ResourcePool<CapsuleCapsulePair>();

        private static SegmentShape capa = new SegmentShape();
        private static SegmentShape capb = new SegmentShape();

        public override bool IsColliding(ref TSMatrix orientation1, ref TSMatrix orientation2, ref TSVector position1, ref TSVector position2,
            out TSVector point, out TSVector point1, out TSVector point2, out TSVector normal, out FP penetration)
        {
            // Used variables
            TSVector center1, center2;

            // Initialization of the output
            point = point1 = point2 = normal = TSVector.zero;
            penetration = FP.Zero;

            CapsuleShape capsule1 = this.Shape1 as CapsuleShape;
            CapsuleShape capsule2 = this.Shape2 as CapsuleShape;

            // Get the center of box1 in world coordinates -> center1
            capsule1.SupportCenter(out center1);
            TSVector.Transform(ref center1, ref orientation1, out center1);
            TSVector.Add(ref position1, ref center1, out center1);

            // Get the center of box2 in world coordinates -> center2
            capsule2.SupportCenter(out center2);
            TSVector.Transform(ref center2, ref orientation2, out center2);
            TSVector.Add(ref position2, ref center2, out center2);

            TSVector aP1, aP2, axisA;
            TSVector halfAxisVecA = TSVector.up * FP.Half * capsule1.length;
            TSVector.Transform(ref halfAxisVecA, ref orientation1, out axisA);
            TSVector.Add(ref center1, ref axisA, out aP1);
            TSVector.Negate(ref axisA, out axisA);
            TSVector.Add(ref center1, ref axisA, out aP2);

            //UnityEngine.Debug.DrawLine(aP1.ToVector(), aP2.ToVector(), UnityEngine.Color.red, 1);

            TSVector bP1, bP2, axisB;
            TSVector halfAxisVecB = TSVector.up * FP.Half * capsule2.length;
            TSVector.Transform(ref halfAxisVecB, ref orientation2, out axisB);
            TSVector.Add(ref center2, ref axisB, out bP1);
            TSVector.Negate(ref axisB, out axisB);
            TSVector.Add(ref center2, ref axisB, out bP2);

            //UnityEngine.Debug.DrawLine(bP1.ToVector(), bP2.ToVector(), UnityEngine.Color.blue, 1);

            return DoOverlapTest(ref capsule1, ref aP1, ref aP2,
                ref capsule2, ref bP1, ref bP2, TSVector.zero, ref axisA, ref axisB,
                ref point1, ref point2, ref normal, ref penetration);
        }

        private bool DoOverlapTest(ref CapsuleShape a, ref TSVector aP1, ref TSVector aP2, 
            ref CapsuleShape b, ref TSVector bP1, ref TSVector bP2, TSVector offset, ref TSVector axisA, ref TSVector axisB,
            ref TSVector pa, ref TSVector pb, ref TSVector normal, ref FP penetration)
        {
            capa.P1 = aP1;
            capa.P2 = aP2;
            TSVector.Add(ref aP1, ref offset, out capa.P1);
            TSVector.Add(ref aP2, ref offset, out capa.P2);

            capb.P1 = bP1;
            capb.P2 = bP2;

            TSVector v;
            FP sa, sb, r2 = a.Radius + b.Radius;
            r2 *= r2;

            // find the closest point between the two capsules
            SegmentShape.ClosestPoints(ref capa, ref capb, out sa, out pa, out sb, out pb);
            TSVector.Subtract(ref pa, ref pb, out normal);
            FP sqrPaPb = normal.sqrMagnitude;
            if (sqrPaPb - r2 >= TSMath.Epsilon)
                return false;
            else if (sqrPaPb < TSMath.Epsilon)
                normal = TSVector.forward;
            else
                normal.Normalize();

            TSVector.Negate(ref normal, out normal);
            penetration = TSVector.Subtract(pa, pb).magnitude - (a.radius + b.radius);

            TSVector.Multiply(ref normal, -a.Radius, out v);
            TSVector.Add(ref pa, ref v, out pa);
            TSVector.Multiply(ref normal, b.Radius, out v);
            TSVector.Add(ref pb, ref v, out pb);
            TSVector.Subtract(ref pa, ref offset, out pa);

            // if the two capsules are nearly parallel, an additional support point provides stability
            if (sa == FP.Zero || sa == FP.One)
            {
                pa = sa == FP.Zero ? capa.P2 : capa.P1;
                capb.ClosestPointTo(ref pa, out sa, out pb);
            }
            else if (sb == FP.Zero || sb == FP.One)
            {
                pb = sb == FP.Zero ? capb.P2 : capb.P1;
                capa.ClosestPointTo(ref pb, out sb, out pa);
            }
            else
                return true;

            FP dist = TSVector.Subtract(pa, pb).sqrMagnitude - r2;
            penetration = TSVector.Subtract(pa, pb).magnitude - (a.radius + b.radius);
            if (dist < TSMath.Epsilon)
            {
                TSVector.Multiply(ref normal, -a.Radius, out v);
                TSVector.Add(ref pa, ref v, out pa);
                TSVector.Multiply(ref normal, b.Radius, out v);
                TSVector.Add(ref pb, ref v, out pb);
                TSVector.Subtract(ref pa, ref offset, out pa);
            }
            TSVector.Negate(ref normal, out normal);
            return true;
        }
    }
}
