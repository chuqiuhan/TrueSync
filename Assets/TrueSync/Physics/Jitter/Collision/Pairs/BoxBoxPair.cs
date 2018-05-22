using System.Collections;
using System.Collections.Generic;

namespace TrueSync.Physics3D
{
    public class BoxBoxPair : CollisionPair
    {
        public static ResourcePool<BoxBoxPair> pool = new ResourcePool<BoxBoxPair>();

        public override bool IsColliding(ref TSMatrix orientation1, ref TSMatrix orientation2, ref TSVector position1, ref TSVector position2,
            out TSVector point, out TSVector point1, out TSVector point2, out TSVector normal, out FP penetration)
        {
            // Used variables
            TSVector center1, center2;

            // Initialization of the output
            point = point1 = point2 = normal = TSVector.zero;
            penetration = FP.Zero;

            BoxShape box1 = this.Shape1 as BoxShape;
            BoxShape box2 = this.Shape2 as BoxShape;

            // Get the center of box1 in world coordinates -> center1
            box1.SupportCenter(out center1);
            TSVector.Transform(ref center1, ref orientation1, out center1);
            TSVector.Add(ref position1, ref center1, out center1);

            // Get the center of box2 in world coordinates -> center2
            box2.SupportCenter(out center2);
            TSVector.Transform(ref center2, ref orientation2, out center2);
            TSVector.Add(ref position2, ref center2, out center2);

            // TODO: box-box collision test

            return true;
        }
    }
}
