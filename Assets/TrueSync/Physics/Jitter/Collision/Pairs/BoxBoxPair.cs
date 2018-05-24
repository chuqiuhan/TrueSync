using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.InteropServices;

namespace TrueSync.Physics3D
{
    [StructLayout(LayoutKind.Sequential)]
    public struct BoxContactData : IEquatable<BoxContactData>
    {
        /// <summary>
        /// Position of the candidate contact.
        /// </summary>
        public TSVector Position;

        /// <summary>
        /// Depth of the candidate contact.
        /// </summary>
        public FP Depth;

        /// <summary>
        /// Id of the candidate contact.
        /// </summary>
        public int Id;

        #region IEquatable<BoxContactData> Members

        /// <summary>
        /// Returns true if the other data has the same id.
        /// </summary>
        /// <param name="other">Data to compare.</param>
        /// <returns>True if the other data has the same id, false otherwise.</returns>
        public bool Equals(BoxContactData other)
        {
            return Id == other.Id;
        }

        #endregion
    }

    /// <summary>
    /// Special datatype used for heapless lists without unsafe/stackalloc.
    /// Since reference types would require heap-side allocation and
    /// do not match well with this structure's ref-parameter based access,
    /// only structs are allowed.
    /// Stores a maximum of 8 entries.
    /// </summary>
    /// <typeparam name="T">Struct type to use.</typeparam>
    public struct TinyStructList<T> where T : struct, IEquatable<T>
    {
        private T entry1;
        private T entry2;
        private T entry3;
        private T entry4;

        private T entry5;
        private T entry6;
        private T entry7;
        private T entry8;

        internal int count;

        /// <summary>
        /// Gets the current number of elements in the list.
        /// </summary>
        public int Count
        {
            get { return count; }
        }

        /// <summary>
        /// Creates a string representation of the list.
        /// </summary>
        /// <returns>String representation of the list.</returns>
        public override string ToString()
        {
            return "TinyStructList<" + typeof(T) + ">, Count: " + count;
        }

        /// <summary>
        /// Tries to add an element to the list.
        /// </summary>
        /// <param name="item">Item to add.</param>
        /// <returns>Whether or not the item could be added.
        /// Will return false when the list is full.</returns>
        public bool Add(ref T item)
        {
            switch (count)
            {
                case 0:
                    entry1 = item;
                    break;
                case 1:
                    entry2 = item;
                    break;
                case 2:
                    entry3 = item;
                    break;
                case 3:
                    entry4 = item;
                    break;
                case 4:
                    entry5 = item;
                    break;
                case 5:
                    entry6 = item;
                    break;
                case 6:
                    entry7 = item;
                    break;
                case 7:
                    entry8 = item;
                    break;
                default:
                    return false;
            }
            count++;
            return true;
        }

        /// <summary>
        /// Clears the list.
        /// </summary>
        public void Clear()
        {
            //Everything is a struct in this kind of list, so not much work to do!
            count = 0;
        }

        /// <summary>
        /// Gets the item at the specified index.
        /// </summary>
        /// <param name="index">Index to retrieve.</param>
        /// <param name="item">Retrieved item.</param>
        /// <returns>Whether or not the index was valid.</returns>
        public bool Get(int index, out T item)
        {
            if (index > count - 1 || index < 0)
            {
                item = default(T);
                return false;
            }
            switch (index)
            {
                case 0:
                    item = entry1;
                    return true;
                case 1:
                    item = entry2;
                    return true;
                case 2:
                    item = entry3;
                    return true;
                case 3:
                    item = entry4;
                    return true;
                case 4:
                    item = entry5;
                    return true;
                case 5:
                    item = entry6;
                    return true;
                case 6:
                    item = entry7;
                    return true;
                case 7:
                    item = entry8;
                    return true;
                default:
                    //Curious!
                    item = default(T);
                    return false;
            }
        }

        /// <summary>
        /// Gets the index of the item in the list, if it is present.
        /// </summary>
        /// <param name="item">Item to look for.</param>
        /// <returns>Index of the item, if present.  -1 otherwise.</returns>
        public int IndexOf(ref T item)
        {
            //This isn't a super fast operation.
            if (entry1.Equals(item))
                return 0;
            if (entry2.Equals(item))
                return 1;
            if (entry3.Equals(item))
                return 2;
            if (entry4.Equals(item))
                return 3;
            if (entry5.Equals(item))
                return 4;
            if (entry6.Equals(item))
                return 5;
            if (entry7.Equals(item))
                return 6;
            if (entry8.Equals(item))
                return 7;
            return -1;
        }

        /// <summary>
        /// Tries to remove an element from the list.
        /// </summary>
        /// <param name="item">Item to remove.</param>
        /// <returns>Whether or not the item existed in the list.</returns>
        public bool Remove(ref T item)
        {
            //Identity-based removes aren't a super high priority feature, so can be a little slower.
            int index = IndexOf(ref item);
            if (index != -1)
            {
                RemoveAt(index);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Removes the item at the specified index.
        /// </summary>
        /// <param name="index">Index of the element to remove.</param>
        /// <returns>Whether or not the item could be removed.
        /// Returns false if the index is out of bounds.</returns>
        public bool RemoveAt(int index)
        {
            if (index > count - 1 || index < 0)
                return false;
            switch (index)
            {
                case 0:
                    entry1 = entry2;
                    entry2 = entry3;
                    entry3 = entry4;
                    entry4 = entry5;
                    entry5 = entry6;
                    entry6 = entry7;
                    entry7 = entry8;
                    break;
                case 1:
                    entry2 = entry3;
                    entry3 = entry4;
                    entry4 = entry5;
                    entry5 = entry6;
                    entry6 = entry7;
                    entry7 = entry8;
                    break;
                case 2:
                    entry3 = entry4;
                    entry4 = entry5;
                    entry5 = entry6;
                    entry6 = entry7;
                    entry7 = entry8;
                    break;
                case 3:
                    entry4 = entry5;
                    entry5 = entry6;
                    entry6 = entry7;
                    entry7 = entry8;
                    break;
                case 4:
                    entry5 = entry6;
                    entry6 = entry7;
                    entry7 = entry8;
                    break;
                case 5:
                    entry6 = entry7;
                    entry7 = entry8;
                    break;
                case 6:
                    entry7 = entry8;
                    break;
            }
            count--;
            return true;
        }

        /// <summary>
        /// Tries to add an element to the list.
        /// </summary>
        /// <param name="index">Index to replace.</param>
        /// <param name="item">Item to add.</param>
        /// <returns>Whether or not the item could be replaced.
        /// Returns false if the index is invalid.</returns>
        public bool Replace(int index, ref T item)
        {
            if (index > count - 1 || index < 0)
            {
                return false;
            }
            switch (index)
            {
                case 0:
                    entry1 = item;
                    break;
                case 1:
                    entry2 = item;
                    break;
                case 2:
                    entry3 = item;
                    break;
                case 3:
                    entry4 = item;
                    break;
                case 4:
                    entry5 = item;
                    break;
                case 5:
                    entry6 = item;
                    break;
                case 6:
                    entry7 = item;
                    break;
                case 7:
                    entry8 = item;
                    break;
                default:
                    return false;
            }
            return true;
        }
    }

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


            FP distance;
            TSVector axis;
            TinyStructList<BoxContactData> contactData;
            if (AreBoxesColliding(ref box1, ref box2, ref center1, ref center2, ref orientation1, ref orientation2, out distance, out axis, out contactData))
            {
                int length = contactData.count;
                if (length == 0)
                    return false;

                BoxContactData contact;
                contactData.Get(0, out contact);
                for (int i = 1; i < length; i++)
                {
                    BoxContactData tmpContact;
                    contactData.Get(i, out tmpContact);
                    if (tmpContact.Depth < contact.Depth)
                        contact = tmpContact;
                }

                normal = axis.normalized;
                penetration = contact.Depth;
                point = contact.Position;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Determines if the two boxes are colliding.
        /// </summary>
        /// <param name="a">First box to collide.</param>
        /// <param name="b">Second box to collide.</param>
        /// <param name="positionA"> World position of first box.</param>
        /// <param name="positionB"> World position of second box.</param>
        /// <param name="orientationA"> Orientation of first box.</param>
        /// <param name="orientationB"> Orientation of second box.</param>
        /// <returns>Whether or not the boxes collide.</returns>
        public static bool AreBoxesColliding(ref BoxShape a, ref BoxShape b, ref TSVector positionA, ref TSVector positionB, ref TSMatrix orientationA, ref TSMatrix orientationB)
        {
            TSVector halfSizeA = a.halfSize;
            FP aX = halfSizeA.z;
            FP aY = halfSizeA.y;
            FP aZ = halfSizeA.x;

            TSVector halfSizeB = b.halfSize;
            FP bX = halfSizeB.z;
            FP bY = halfSizeB.y;
            FP bZ = halfSizeB.x;

            //Relative rotation from A to B.
            TSMatrix bR;

            TSMatrix aO = orientationA;
            TSMatrix bO = orientationB;

            //Relative translation rotated into A's configuration space.
            TSVector t;
            TSVector.Subtract(ref positionB, ref positionB, out t);

            bR.M11 = aO.M11 * bO.M11 + aO.M12 * bO.M12 + aO.M13 * bO.M13;
            bR.M12 = aO.M11 * bO.M21 + aO.M12 * bO.M22 + aO.M13 * bO.M23;
            bR.M13 = aO.M11 * bO.M31 + aO.M12 * bO.M32 + aO.M13 * bO.M33;
            TSMatrix absBR;
            //Epsilons are added to deal with near-parallel edges.
            absBR.M11 = FP.Abs(bR.M11) + TSMath.Epsilon;
            absBR.M12 = FP.Abs(bR.M12) + TSMath.Epsilon;
            absBR.M13 = FP.Abs(bR.M13) + TSMath.Epsilon;
            FP tX = t.x;
            t.x = t.x * aO.M11 + t.y * aO.M12 + t.z * aO.M13;

            //Test the axes defines by entity A's rotation matrix.
            //A.x
            FP rb = bX * absBR.M11 + bY * absBR.M12 + bZ * absBR.M13;
            if (FP.Abs(t.x) > aX + rb)
                return false;
            bR.M21 = aO.M21 * bO.M11 + aO.M22 * bO.M12 + aO.M23 * bO.M13;
            bR.M22 = aO.M21 * bO.M21 + aO.M22 * bO.M22 + aO.M23 * bO.M23;
            bR.M23 = aO.M21 * bO.M31 + aO.M22 * bO.M32 + aO.M23 * bO.M33;
            absBR.M21 = FP.Abs(bR.M21) + TSMath.Epsilon;
            absBR.M22 = FP.Abs(bR.M22) + TSMath.Epsilon;
            absBR.M23 = FP.Abs(bR.M23) + TSMath.Epsilon;
            FP tY = t.y;
            t.y = tX * aO.M21 + t.y * aO.M22 + t.z * aO.M23;

            //A.y
            rb = bX * absBR.M21 + bY * absBR.M22 + bZ * absBR.M23;
            if (FP.Abs(t.y) > aY + rb)
                return false;

            bR.M31 = aO.M31 * bO.M11 + aO.M32 * bO.M12 + aO.M33 * bO.M13;
            bR.M32 = aO.M31 * bO.M21 + aO.M32 * bO.M22 + aO.M33 * bO.M23;
            bR.M33 = aO.M31 * bO.M31 + aO.M32 * bO.M32 + aO.M33 * bO.M33;
            absBR.M31 = FP.Abs(bR.M31) + TSMath.Epsilon;
            absBR.M32 = FP.Abs(bR.M32) + TSMath.Epsilon;
            absBR.M33 = FP.Abs(bR.M33) + TSMath.Epsilon;
            t.z = tX * aO.M31 + tY * aO.M32 + t.z * aO.M33;

            //A.z
            rb = bX * absBR.M31 + bY * absBR.M32 + bZ * absBR.M33;
            if (FP.Abs(t.z) > aZ + rb)
                return false;

            //Test the axes defines by entity B's rotation matrix.
            //B.x
            FP ra = aX * absBR.M11 + aY * absBR.M21 + aZ * absBR.M31;
            if (FP.Abs(t.x * bR.M11 + t.y * bR.M21 + t.z * bR.M31) > ra + bX)
                return false;

            //B.y
            ra = aX * absBR.M12 + aY * absBR.M22 + aZ * absBR.M32;
            if (FP.Abs(t.x * bR.M12 + t.y * bR.M22 + t.z * bR.M32) > ra + bY)
                return false;

            //B.z
            ra = aX * absBR.M13 + aY * absBR.M23 + aZ * absBR.M33;
            if (FP.Abs(t.x * bR.M13 + t.y * bR.M23 + t.z * bR.M33) > ra + bZ)
                return false;

            //Now for the edge-edge cases.
            //A.x x B.x
            ra = aY * absBR.M31 + aZ * absBR.M21;
            rb = bY * absBR.M13 + bZ * absBR.M12;
            if (FP.Abs(t.z * bR.M21 - t.y * bR.M31) > ra + rb)
                return false;

            //A.x x B.y
            ra = aY * absBR.M32 + aZ * absBR.M22;
            rb = bX * absBR.M13 + bZ * absBR.M11;
            if (FP.Abs(t.z * bR.M22 - t.y * bR.M32) > ra + rb)
                return false;

            //A.x x B.z
            ra = aY * absBR.M33 + aZ * absBR.M23;
            rb = bX * absBR.M12 + bY * absBR.M11;
            if (FP.Abs(t.z * bR.M23 - t.y * bR.M33) > ra + rb)
                return false;


            //A.y x B.x
            ra = aX * absBR.M31 + aZ * absBR.M11;
            rb = bY * absBR.M23 + bZ * absBR.M22;
            if (FP.Abs(t.x * bR.M31 - t.z * bR.M11) > ra + rb)
                return false;

            //A.y x B.y
            ra = aX * absBR.M32 + aZ * absBR.M12;
            rb = bX * absBR.M23 + bZ * absBR.M21;
            if (FP.Abs(t.x * bR.M32 - t.z * bR.M12) > ra + rb)
                return false;

            //A.y x B.z
            ra = aX * absBR.M33 + aZ * absBR.M13;
            rb = bX * absBR.M22 + bY * absBR.M21;
            if (FP.Abs(t.x * bR.M33 - t.z * bR.M13) > ra + rb)
                return false;

            //A.z x B.x
            ra = aX * absBR.M21 + aY * absBR.M11;
            rb = bY * absBR.M33 + bZ * absBR.M32;
            if (FP.Abs(t.y * bR.M11 - t.x * bR.M21) > ra + rb)
                return false;

            //A.z x B.y
            ra = aX * absBR.M22 + aY * absBR.M12;
            rb = bX * absBR.M33 + bZ * absBR.M31;
            if (FP.Abs(t.y * bR.M12 - t.x * bR.M22) > ra + rb)
                return false;

            //A.z x B.z
            ra = aX * absBR.M23 + aY * absBR.M13;
            rb = bX * absBR.M32 + bY * absBR.M31;
            if (FP.Abs(t.y * bR.M13 - t.x * bR.M23) > ra + rb)
                return false;

            return true;
        }

        /// <summary>
        /// Determines if the two boxes are colliding.
        /// </summary>
        /// <param name="a">First box to collide.</param>
        /// <param name="b">Second box to collide.</param>
        /// <param name="positionA"> World position of first box.</param>
        /// <param name="positionB"> World position of second box.</param>
        /// <param name="orientationA"> Orientation of first box.</param>
        /// <param name="orientationB"> Orientation of second box.</param>
        /// <param name="separationDistance">Distance of separation.</param>
        /// <param name="separatingAxis">Axis of separation.</param>
        /// <returns>Whether or not the boxes collide.</returns>
        public static bool AreBoxesColliding(ref BoxShape a, ref BoxShape b, ref TSVector positionA, ref TSVector positionB, ref TSMatrix orientationA, ref TSMatrix orientationB,
            out FP separationDistance, out TSVector separatingAxis)
        {
            TSVector halfSizeA = a.halfSize;
            FP aX = halfSizeA.z;
            FP aY = halfSizeA.y;
            FP aZ = halfSizeA.x;

            TSVector halfSizeB = b.halfSize;
            FP bX = halfSizeB.z;
            FP bY = halfSizeB.y;
            FP bZ = halfSizeB.x;

            //Relative rotation from A to B.
            TSMatrix bR;

            TSMatrix aO = orientationA;
            TSMatrix bO = orientationB;

            //Relative translation rotated into A's configuration space.
            TSVector t;
            TSVector.Subtract(ref positionB, ref positionA, out t);

            #region A Face Normals

            bR.M11 = aO.M11 * bO.M11 + aO.M12 * bO.M12 + aO.M13 * bO.M13;
            bR.M12 = aO.M11 * bO.M21 + aO.M12 * bO.M22 + aO.M13 * bO.M23;
            bR.M13 = aO.M11 * bO.M31 + aO.M12 * bO.M32 + aO.M13 * bO.M33;
            TSMatrix absBR;
            //Epsilons are added to deal with near-parallel edges.
            absBR.M11 = FP.Abs(bR.M11) + TSMath.Epsilon;
            absBR.M12 = FP.Abs(bR.M12) + TSMath.Epsilon;
            absBR.M13 = FP.Abs(bR.M13) + TSMath.Epsilon;
            FP tX = t.x;
            t.x = t.x * aO.M11 + t.y * aO.M12 + t.z * aO.M13;

            //Test the axes defines by entity A's rotation matrix.
            //A.x
            FP rarb = aX + bX * absBR.M11 + bY * absBR.M12 + bZ * absBR.M13;
            if (t.x > rarb)
            {
                separationDistance = t.x - rarb;
                separatingAxis = new TSVector(aO.M11, aO.M12, aO.M13);
                return false;
            }
            if (t.x < -rarb)
            {
                separationDistance = -t.x - rarb;
                separatingAxis = new TSVector(-aO.M11, -aO.M12, -aO.M13);
                return false;
            }


            bR.M21 = aO.M21 * bO.M11 + aO.M22 * bO.M12 + aO.M23 * bO.M13;
            bR.M22 = aO.M21 * bO.M21 + aO.M22 * bO.M22 + aO.M23 * bO.M23;
            bR.M23 = aO.M21 * bO.M31 + aO.M22 * bO.M32 + aO.M23 * bO.M33;
            absBR.M21 = FP.Abs(bR.M21) + TSMath.Epsilon;
            absBR.M22 = FP.Abs(bR.M22) + TSMath.Epsilon;
            absBR.M23 = FP.Abs(bR.M23) + TSMath.Epsilon;
            FP tY = t.y;
            t.y = tX * aO.M21 + t.y * aO.M22 + t.z * aO.M23;

            //A.y
            rarb = aY + bX * absBR.M21 + bY * absBR.M22 + bZ * absBR.M23;
            if (t.y > rarb)
            {
                separationDistance = t.y - rarb;
                separatingAxis = new TSVector(aO.M21, aO.M22, aO.M23);
                return false;
            }
            if (t.y < -rarb)
            {
                separationDistance = -t.y - rarb;
                separatingAxis = new TSVector(-aO.M21, -aO.M22, -aO.M23);
                return false;
            }

            bR.M31 = aO.M31 * bO.M11 + aO.M32 * bO.M12 + aO.M33 * bO.M13;
            bR.M32 = aO.M31 * bO.M21 + aO.M32 * bO.M22 + aO.M33 * bO.M23;
            bR.M33 = aO.M31 * bO.M31 + aO.M32 * bO.M32 + aO.M33 * bO.M33;
            absBR.M31 = FP.Abs(bR.M31) + TSMath.Epsilon;
            absBR.M32 = FP.Abs(bR.M32) + TSMath.Epsilon;
            absBR.M33 = FP.Abs(bR.M33) + TSMath.Epsilon;
            t.z = tX * aO.M31 + tY * aO.M32 + t.z * aO.M33;

            //A.z
            rarb = aZ + bX * absBR.M31 + bY * absBR.M32 + bZ * absBR.M33;
            if (t.z > rarb)
            {
                separationDistance = t.z - rarb;
                separatingAxis = new TSVector(aO.M31, aO.M32, aO.M33);
                return false;
            }
            if (t.z < -rarb)
            {
                separationDistance = -t.z - rarb;
                separatingAxis = new TSVector(-aO.M31, -aO.M32, -aO.M33);
                return false;
            }

            #endregion

            #region B Face Normals

            //Test the axes defines by entity B's rotation matrix.
            //B.x
            rarb = bX + aX * absBR.M11 + aY * absBR.M21 + aZ * absBR.M31;
            FP tl = t.x * bR.M11 + t.y * bR.M21 + t.z * bR.M31;
            if (tl > rarb)
            {
                separationDistance = tl - rarb;
                separatingAxis = new TSVector(bO.M11, bO.M12, bO.M13);
                return false;
            }
            if (tl < -rarb)
            {
                separationDistance = -tl - rarb;
                separatingAxis = new TSVector(-bO.M11, -bO.M12, -bO.M13);
                return false;
            }

            //B.y
            rarb = bY + aX * absBR.M12 + aY * absBR.M22 + aZ * absBR.M32;
            tl = t.x * bR.M12 + t.y * bR.M22 + t.z * bR.M32;
            if (tl > rarb)
            {
                separationDistance = tl - rarb;
                separatingAxis = new TSVector(bO.M21, bO.M22, bO.M23);
                return false;
            }
            if (tl < -rarb)
            {
                separationDistance = -tl - rarb;
                separatingAxis = new TSVector(-bO.M21, -bO.M22, -bO.M23);
                return false;
            }


            //B.z
            rarb = bZ + aX * absBR.M13 + aY * absBR.M23 + aZ * absBR.M33;
            tl = t.x * bR.M13 + t.y * bR.M23 + t.z * bR.M33;
            if (tl > rarb)
            {
                separationDistance = tl - rarb;
                separatingAxis = new TSVector(bO.M31, bO.M32, bO.M33);
                return false;
            }
            if (tl < -rarb)
            {
                separationDistance = -tl - rarb;
                separatingAxis = new TSVector(-bO.M31, -bO.M32, -bO.M33);
                return false;
            }

            #endregion

            #region A.x x B.()

            //Now for the edge-edge cases.
            //A.x x B.x
            rarb = aY * absBR.M31 + aZ * absBR.M21 +
                   bY * absBR.M13 + bZ * absBR.M12;
            tl = t.z * bR.M21 - t.y * bR.M31;
            if (tl > rarb)
            {
                separationDistance = tl - rarb;
                separatingAxis = new TSVector(aO.M12 * bO.M13 - aO.M13 * bO.M12,
                                             aO.M13 * bO.M11 - aO.M11 * bO.M13,
                                             aO.M11 * bO.M12 - aO.M12 * bO.M11);
                return false;
            }
            if (tl < -rarb)
            {
                separationDistance = -tl - rarb;
                separatingAxis = new TSVector(bO.M12 * aO.M13 - bO.M13 * aO.M12,
                                             bO.M13 * aO.M11 - bO.M11 * aO.M13,
                                             bO.M11 * aO.M12 - bO.M12 * aO.M11);
                return false;
            }

            //A.x x B.y
            rarb = aY * absBR.M32 + aZ * absBR.M22 +
                   bX * absBR.M13 + bZ * absBR.M11;
            tl = t.z * bR.M22 - t.y * bR.M32;
            if (tl > rarb)
            {
                separationDistance = tl - rarb;
                separatingAxis = new TSVector(aO.M12 * bO.M23 - aO.M13 * bO.M22,
                                             aO.M13 * bO.M21 - aO.M11 * bO.M23,
                                             aO.M11 * bO.M22 - aO.M12 * bO.M21);
                return false;
            }
            if (tl < -rarb)
            {
                separationDistance = -tl - rarb;
                separatingAxis = new TSVector(bO.M22 * aO.M13 - bO.M23 * aO.M12,
                                             bO.M23 * aO.M11 - bO.M21 * aO.M13,
                                             bO.M21 * aO.M12 - bO.M22 * aO.M11);
                return false;
            }

            //A.x x B.z
            rarb = aY * absBR.M33 + aZ * absBR.M23 +
                   bX * absBR.M12 + bY * absBR.M11;
            tl = t.z * bR.M23 - t.y * bR.M33;
            if (tl > rarb)
            {
                separationDistance = tl - rarb;
                separatingAxis = new TSVector(aO.M12 * bO.M33 - aO.M13 * bO.M32,
                                             aO.M13 * bO.M31 - aO.M11 * bO.M33,
                                             aO.M11 * bO.M32 - aO.M12 * bO.M31);
                return false;
            }
            if (tl < -rarb)
            {
                separationDistance = -tl - rarb;
                separatingAxis = new TSVector(bO.M32 * aO.M13 - bO.M33 * aO.M12,
                                             bO.M33 * aO.M11 - bO.M31 * aO.M13,
                                             bO.M31 * aO.M12 - bO.M32 * aO.M11);
                return false;
            }

            #endregion

            #region A.y x B.()

            //A.y x B.x
            rarb = aX * absBR.M31 + aZ * absBR.M11 +
                   bY * absBR.M23 + bZ * absBR.M22;
            tl = t.x * bR.M31 - t.z * bR.M11;
            if (tl > rarb)
            {
                separationDistance = tl - rarb;
                separatingAxis = new TSVector(aO.M22 * bO.M13 - aO.M23 * bO.M12,
                                             aO.M23 * bO.M11 - aO.M21 * bO.M13,
                                             aO.M21 * bO.M12 - aO.M22 * bO.M11);
                return false;
            }
            if (tl < -rarb)
            {
                separationDistance = -tl - rarb;
                separatingAxis = new TSVector(bO.M12 * aO.M23 - bO.M13 * aO.M22,
                                             bO.M13 * aO.M21 - bO.M11 * aO.M23,
                                             bO.M11 * aO.M22 - bO.M12 * aO.M21);
                return false;
            }

            //A.y x B.y
            rarb = aX * absBR.M32 + aZ * absBR.M12 +
                   bX * absBR.M23 + bZ * absBR.M21;
            tl = t.x * bR.M32 - t.z * bR.M12;
            if (tl > rarb)
            {
                separationDistance = tl - rarb;
                separatingAxis = new TSVector(aO.M22 * bO.M23 - aO.M23 * bO.M22,
                                             aO.M23 * bO.M21 - aO.M21 * bO.M23,
                                             aO.M21 * bO.M22 - aO.M22 * bO.M21);
                return false;
            }
            if (tl < -rarb)
            {
                separationDistance = -tl - rarb;
                separatingAxis = new TSVector(bO.M22 * aO.M23 - bO.M23 * aO.M22,
                                             bO.M23 * aO.M21 - bO.M21 * aO.M23,
                                             bO.M21 * aO.M22 - bO.M22 * aO.M21);
                return false;
            }

            //A.y x B.z
            rarb = aX * absBR.M33 + aZ * absBR.M13 +
                   bX * absBR.M22 + bY * absBR.M21;
            tl = t.x * bR.M33 - t.z * bR.M13;
            if (tl > rarb)
            {
                separationDistance = tl - rarb;
                separatingAxis = new TSVector(aO.M22 * bO.M33 - aO.M23 * bO.M32,
                                             aO.M23 * bO.M31 - aO.M21 * bO.M33,
                                             aO.M21 * bO.M32 - aO.M22 * bO.M31);
                return false;
            }
            if (tl < -rarb)
            {
                separationDistance = -tl - rarb;
                separatingAxis = new TSVector(bO.M32 * aO.M23 - bO.M33 * aO.M22,
                                             bO.M33 * aO.M21 - bO.M31 * aO.M23,
                                             bO.M31 * aO.M22 - bO.M32 * aO.M21);
                return false;
            }

            #endregion

            #region A.z x B.()

            //A.z x B.x
            rarb = aX * absBR.M21 + aY * absBR.M11 +
                   bY * absBR.M33 + bZ * absBR.M32;
            tl = t.y * bR.M11 - t.x * bR.M21;
            if (tl > rarb)
            {
                separationDistance = tl - rarb;
                separatingAxis = new TSVector(aO.M32 * bO.M13 - aO.M33 * bO.M12,
                                             aO.M33 * bO.M11 - aO.M31 * bO.M13,
                                             aO.M31 * bO.M12 - aO.M32 * bO.M11);
                return false;
            }
            if (tl < -rarb)
            {
                separationDistance = -tl - rarb;
                separatingAxis = new TSVector(bO.M12 * aO.M33 - bO.M13 * aO.M32,
                                             bO.M13 * aO.M31 - bO.M11 * aO.M33,
                                             bO.M11 * aO.M32 - bO.M12 * aO.M31);
                return false;
            }

            //A.z x B.y
            rarb = aX * absBR.M22 + aY * absBR.M12 +
                   bX * absBR.M33 + bZ * absBR.M31;
            tl = t.y * bR.M12 - t.x * bR.M22;
            if (tl > rarb)
            {
                separationDistance = tl - rarb;
                separatingAxis = new TSVector(aO.M32 * bO.M23 - aO.M33 * bO.M22,
                                             aO.M33 * bO.M21 - aO.M31 * bO.M23,
                                             aO.M31 * bO.M22 - aO.M32 * bO.M21);
                return false;
            }
            if (tl < -rarb)
            {
                separationDistance = -tl - rarb;
                separatingAxis = new TSVector(bO.M22 * aO.M33 - bO.M23 * aO.M32,
                                             bO.M23 * aO.M31 - bO.M21 * aO.M33,
                                             bO.M21 * aO.M32 - bO.M22 * aO.M31);
                return false;
            }

            //A.z x B.z
            rarb = aX * absBR.M23 + aY * absBR.M13 +
                   bX * absBR.M32 + bY * absBR.M31;
            tl = t.y * bR.M13 - t.x * bR.M23;
            if (tl > rarb)
            {
                separationDistance = tl - rarb;
                separatingAxis = new TSVector(aO.M32 * bO.M33 - aO.M33 * bO.M32,
                                             aO.M33 * bO.M31 - aO.M31 * bO.M33,
                                             aO.M31 * bO.M32 - aO.M32 * bO.M31);
                return false;
            }
            if (tl < -rarb)
            {
                separationDistance = -tl - rarb;
                separatingAxis = new TSVector(bO.M32 * aO.M33 - bO.M33 * aO.M32,
                                             bO.M33 * aO.M31 - bO.M31 * aO.M33,
                                             bO.M31 * aO.M32 - bO.M32 * aO.M31);
                return false;
            }

            #endregion

            separationDistance = FP.Zero;
            separatingAxis = TSVector.zero;
            return true;
        }

        /// <summary>
        /// Determines if the two boxes are colliding, including penetration depth data.
        /// </summary>
        /// <param name="a">First box to collide.</param>
        /// <param name="b">Second box to collide.</param>
        /// <param name="positionA"> World position of first box.</param>
        /// <param name="positionB"> World position of second box.</param>
        /// <param name="orientationA"> Orientation of first box.</param>
        /// <param name="orientationB"> Orientation of second box.</param>
        /// <param name="distance">Distance of separation or penetration.</param>
        /// <param name="axis">Axis of separation or penetration.</param>
        /// <returns>Whether or not the boxes collide.</returns>
        public static bool AreBoxesCollidingWithPenetration(ref BoxShape a, ref BoxShape b, ref TSVector positionA, ref TSVector positionB, ref TSMatrix orientationA, ref TSMatrix orientationB,
            out FP distance, out TSVector axis)
        {
            TSVector halfSizeA = a.halfSize;
            FP aX = halfSizeA.z;
            FP aY = halfSizeA.y;
            FP aZ = halfSizeA.x;

            TSVector halfSizeB = b.halfSize;
            FP bX = halfSizeB.z;
            FP bY = halfSizeB.y;
            FP bZ = halfSizeB.x;

            //Relative rotation from A to B.
            TSMatrix bR;

            TSMatrix aO = orientationA;
            TSMatrix bO = orientationB;

            //Relative translation rotated into A's configuration space.
            TSVector t;
            TSVector.Subtract(ref positionB, ref positionA, out t);

            FP tempDistance;
            FP minimumDistance = -FP.MaxValue;
            var minimumAxis = TSVector.zero;

            #region A Face Normals

            bR.M11 = aO.M11 * bO.M11 + aO.M12 * bO.M12 + aO.M13 * bO.M13;
            bR.M12 = aO.M11 * bO.M21 + aO.M12 * bO.M22 + aO.M13 * bO.M23;
            bR.M13 = aO.M11 * bO.M31 + aO.M12 * bO.M32 + aO.M13 * bO.M33;
            TSMatrix absBR;
            //Epsilons are added to deal with near-parallel edges.
            absBR.M11 = FP.Abs(bR.M11) + TSMath.Epsilon;
            absBR.M12 = FP.Abs(bR.M12) + TSMath.Epsilon;
            absBR.M13 = FP.Abs(bR.M13) + TSMath.Epsilon;
            FP tX = t.x;
            t.x = t.x * aO.M11 + t.y * aO.M12 + t.z * aO.M13;

            //Test the axes defines by entity A's rotation matrix.
            //A.x
            FP rarb = aX + bX * absBR.M11 + bY * absBR.M12 + bZ * absBR.M13;
            if (t.x > rarb)
            {
                distance = t.x - rarb;
                axis = new TSVector(aO.M11, aO.M12, aO.M13);
                return false;
            }
            if (t.x < -rarb)
            {
                distance = -t.x - rarb;
                axis = new TSVector(-aO.M11, -aO.M12, -aO.M13);
                return false;
            }
            //Inside
            if (t.x > FP.Zero)
            {
                tempDistance = t.x - rarb;
                if (tempDistance > minimumDistance)
                {
                    minimumDistance = tempDistance;
                    minimumAxis = new TSVector(aO.M11, aO.M12, aO.M13);
                }
            }
            else
            {
                tempDistance = -t.x - rarb;
                if (tempDistance > minimumDistance)
                {
                    minimumDistance = tempDistance;
                    minimumAxis = new TSVector(-aO.M11, -aO.M12, -aO.M13);
                }
            }


            bR.M21 = aO.M21 * bO.M11 + aO.M22 * bO.M12 + aO.M23 * bO.M13;
            bR.M22 = aO.M21 * bO.M21 + aO.M22 * bO.M22 + aO.M23 * bO.M23;
            bR.M23 = aO.M21 * bO.M31 + aO.M22 * bO.M32 + aO.M23 * bO.M33;
            absBR.M21 = FP.Abs(bR.M21) + TSMath.Epsilon;
            absBR.M22 = FP.Abs(bR.M22) + TSMath.Epsilon;
            absBR.M23 = FP.Abs(bR.M23) + TSMath.Epsilon;
            FP tY = t.y;
            t.y = tX * aO.M21 + t.y * aO.M22 + t.z * aO.M23;

            //A.y
            rarb = aY + bX * absBR.M21 + bY * absBR.M22 + bZ * absBR.M23;
            if (t.y > rarb)
            {
                distance = t.y - rarb;
                axis = new TSVector(aO.M21, aO.M22, aO.M23);
                return false;
            }
            if (t.y < -rarb)
            {
                distance = -t.y - rarb;
                axis = new TSVector(-aO.M21, -aO.M22, -aO.M23);
                return false;
            }
            //Inside
            if (t.y > FP.Zero)
            {
                tempDistance = t.y - rarb;
                if (tempDistance > minimumDistance)
                {
                    minimumDistance = tempDistance;
                    minimumAxis = new TSVector(aO.M21, aO.M22, aO.M23);
                }
            }
            else
            {
                tempDistance = -t.y - rarb;
                if (tempDistance > minimumDistance)
                {
                    minimumDistance = tempDistance;
                    minimumAxis = new TSVector(-aO.M21, -aO.M22, -aO.M23);
                }
            }

            bR.M31 = aO.M31 * bO.M11 + aO.M32 * bO.M12 + aO.M33 * bO.M13;
            bR.M32 = aO.M31 * bO.M21 + aO.M32 * bO.M22 + aO.M33 * bO.M23;
            bR.M33 = aO.M31 * bO.M31 + aO.M32 * bO.M32 + aO.M33 * bO.M33;
            absBR.M31 = FP.Abs(bR.M31) + TSMath.Epsilon;
            absBR.M32 = FP.Abs(bR.M32) + TSMath.Epsilon;
            absBR.M33 = FP.Abs(bR.M33) + TSMath.Epsilon;
            t.z = tX * aO.M31 + tY * aO.M32 + t.z * aO.M33;

            //A.z
            rarb = aZ + bX * absBR.M31 + bY * absBR.M32 + bZ * absBR.M33;
            if (t.z > rarb)
            {
                distance = t.z - rarb;
                axis = new TSVector(aO.M31, aO.M32, aO.M33);
                return false;
            }
            if (t.z < -rarb)
            {
                distance = -t.z - rarb;
                axis = new TSVector(-aO.M31, -aO.M32, -aO.M33);
                return false;
            }
            //Inside
            if (t.z > FP.Zero)
            {
                tempDistance = t.z - rarb;
                if (tempDistance > minimumDistance)
                {
                    minimumDistance = tempDistance;
                    minimumAxis = new TSVector(aO.M31, aO.M32, aO.M33);
                }
            }
            else
            {
                tempDistance = -t.z - rarb;
                if (tempDistance > minimumDistance)
                {
                    minimumDistance = tempDistance;
                    minimumAxis = new TSVector(-aO.M31, -aO.M32, -aO.M33);
                }
            }

            #endregion

            #region B Face Normals

            //Test the axes defines by entity B's rotation matrix.
            //B.x
            rarb = bX + aX * absBR.M11 + aY * absBR.M21 + aZ * absBR.M31;
            FP tl = t.x * bR.M11 + t.y * bR.M21 + t.z * bR.M31;
            if (tl > rarb)
            {
                distance = tl - rarb;
                axis = new TSVector(bO.M11, bO.M12, bO.M13);
                return false;
            }
            if (tl < -rarb)
            {
                distance = -tl - rarb;
                axis = new TSVector(-bO.M11, -bO.M12, -bO.M13);
                return false;
            }
            //Inside
            if (tl > FP.Zero)
            {
                tempDistance = tl - rarb;
                if (tempDistance > minimumDistance)
                {
                    minimumDistance = tempDistance;
                    minimumAxis = new TSVector(bO.M11, bO.M12, bO.M13);
                }
            }
            else
            {
                tempDistance = -tl - rarb;
                if (tempDistance > minimumDistance)
                {
                    minimumDistance = tempDistance;
                    minimumAxis = new TSVector(-bO.M11, -bO.M12, -bO.M13);
                }
            }

            //B.y
            rarb = bY + aX * absBR.M12 + aY * absBR.M22 + aZ * absBR.M32;
            tl = t.x * bR.M12 + t.y * bR.M22 + t.z * bR.M32;
            if (tl > rarb)
            {
                distance = tl - rarb;
                axis = new TSVector(bO.M21, bO.M22, bO.M23);
                return false;
            }
            if (tl < -rarb)
            {
                distance = -tl - rarb;
                axis = new TSVector(-bO.M21, -bO.M22, -bO.M23);
                return false;
            }
            //Inside
            if (tl > FP.Zero)
            {
                tempDistance = tl - rarb;
                if (tempDistance > minimumDistance)
                {
                    minimumDistance = tempDistance;
                    minimumAxis = new TSVector(bO.M21, bO.M22, bO.M23);
                }
            }
            else
            {
                tempDistance = -tl - rarb;
                if (tempDistance > minimumDistance)
                {
                    minimumDistance = tempDistance;
                    minimumAxis = new TSVector(-bO.M21, -bO.M22, -bO.M23);
                }
            }

            //B.z
            rarb = bZ + aX * absBR.M13 + aY * absBR.M23 + aZ * absBR.M33;
            tl = t.x * bR.M13 + t.y * bR.M23 + t.z * bR.M33;
            if (tl > rarb)
            {
                distance = tl - rarb;
                axis = new TSVector(bO.M31, bO.M32, bO.M33);
                return false;
            }
            if (tl < -rarb)
            {
                distance = -tl - rarb;
                axis = new TSVector(-bO.M31, -bO.M32, -bO.M33);
                return false;
            }
            //Inside
            if (tl > FP.Zero)
            {
                tempDistance = tl - rarb;
                if (tempDistance > minimumDistance)
                {
                    minimumDistance = tempDistance;
                    minimumAxis = new TSVector(bO.M31, bO.M32, bO.M33);
                }
            }
            else
            {
                tempDistance = -tl - rarb;
                if (tempDistance > minimumDistance)
                {
                    minimumDistance = tempDistance;
                    minimumAxis = new TSVector(-bO.M31, -bO.M32, -bO.M33);
                }
            }

            #endregion

            FP axisLengthInverse;
            TSVector tempAxis;

            #region A.x x B.()

            //Now for the edge-edge cases.
            //A.x x B.x
            rarb = aY * absBR.M31 + aZ * absBR.M21 +
                   bY * absBR.M13 + bZ * absBR.M12;
            tl = t.z * bR.M21 - t.y * bR.M31;
            if (tl > rarb)
            {
                distance = tl - rarb;
                axis = new TSVector(aO.M12 * bO.M13 - aO.M13 * bO.M12,
                                   aO.M13 * bO.M11 - aO.M11 * bO.M13,
                                   aO.M11 * bO.M12 - aO.M12 * bO.M11);
                return false;
            }
            if (tl < -rarb)
            {
                distance = -tl - rarb;
                axis = new TSVector(bO.M12 * aO.M13 - bO.M13 * aO.M12,
                                   bO.M13 * aO.M11 - bO.M11 * aO.M13,
                                   bO.M11 * aO.M12 - bO.M12 * aO.M11);
                return false;
            }
            //Inside
            if (tl > FP.Zero)
            {
                tempAxis = new TSVector(aO.M12 * bO.M13 - aO.M13 * bO.M12,
                                       aO.M13 * bO.M11 - aO.M11 * bO.M13,
                                       aO.M11 * bO.M12 - aO.M12 * bO.M11);
                axisLengthInverse = FP.One / tempAxis.magnitude;
                tempDistance = (tl - rarb) * axisLengthInverse;
                if (tempDistance > minimumDistance)
                {
                    minimumDistance = tempDistance;
                    tempAxis.x *= axisLengthInverse;
                    tempAxis.y *= axisLengthInverse;
                    tempAxis.z *= axisLengthInverse;
                    minimumAxis = tempAxis;
                }
            }
            else
            {
                tempAxis = new TSVector(bO.M12 * aO.M13 - bO.M13 * aO.M12,
                                       bO.M13 * aO.M11 - bO.M11 * aO.M13,
                                       bO.M11 * aO.M12 - bO.M12 * aO.M11);
                axisLengthInverse = FP.One / tempAxis.magnitude;
                tempDistance = (-tl - rarb) * axisLengthInverse;
                if (tempDistance > minimumDistance)
                {
                    minimumDistance = tempDistance;
                    tempAxis.x *= axisLengthInverse;
                    tempAxis.y *= axisLengthInverse;
                    tempAxis.z *= axisLengthInverse;
                    minimumAxis = tempAxis;
                }
            }

            //A.x x B.y
            rarb = aY * absBR.M32 + aZ * absBR.M22 +
                   bX * absBR.M13 + bZ * absBR.M11;
            tl = t.z * bR.M22 - t.y * bR.M32;
            if (tl > rarb)
            {
                distance = tl - rarb;
                axis = new TSVector(aO.M12 * bO.M23 - aO.M13 * bO.M22,
                                   aO.M13 * bO.M21 - aO.M11 * bO.M23,
                                   aO.M11 * bO.M22 - aO.M12 * bO.M21);
                return false;
            }
            if (tl < -rarb)
            {
                distance = -tl - rarb;
                axis = new TSVector(bO.M22 * aO.M13 - bO.M23 * aO.M12,
                                   bO.M23 * aO.M11 - bO.M21 * aO.M13,
                                   bO.M21 * aO.M12 - bO.M22 * aO.M11);
                return false;
            }
            //Inside
            if (tl > FP.Zero)
            {
                tempAxis = new TSVector(aO.M12 * bO.M23 - aO.M13 * bO.M22,
                                       aO.M13 * bO.M21 - aO.M11 * bO.M23,
                                       aO.M11 * bO.M22 - aO.M12 * bO.M21);
                axisLengthInverse = FP.One / tempAxis.magnitude;
                tempDistance = (tl - rarb) * axisLengthInverse;
                if (tempDistance > minimumDistance)
                {
                    minimumDistance = tempDistance;
                    tempAxis.x *= axisLengthInverse;
                    tempAxis.y *= axisLengthInverse;
                    tempAxis.z *= axisLengthInverse;
                    minimumAxis = tempAxis;
                }
            }
            else
            {
                tempAxis = new TSVector(bO.M22 * aO.M13 - bO.M23 * aO.M12,
                                       bO.M23 * aO.M11 - bO.M21 * aO.M13,
                                       bO.M21 * aO.M12 - bO.M22 * aO.M11);
                axisLengthInverse = FP.One / tempAxis.magnitude;
                tempDistance = (-tl - rarb) * axisLengthInverse;
                if (tempDistance > minimumDistance)
                {
                    minimumDistance = tempDistance;
                    tempAxis.x *= axisLengthInverse;
                    tempAxis.y *= axisLengthInverse;
                    tempAxis.z *= axisLengthInverse;
                    minimumAxis = tempAxis;
                }
            }

            //A.x x B.z
            rarb = aY * absBR.M33 + aZ * absBR.M23 +
                   bX * absBR.M12 + bY * absBR.M11;
            tl = t.z * bR.M23 - t.y * bR.M33;
            if (tl > rarb)
            {
                distance = tl - rarb;
                axis = new TSVector(aO.M12 * bO.M33 - aO.M13 * bO.M32,
                                   aO.M13 * bO.M31 - aO.M11 * bO.M33,
                                   aO.M11 * bO.M32 - aO.M12 * bO.M31);
                return false;
            }
            if (tl < -rarb)
            {
                distance = -tl - rarb;
                axis = new TSVector(bO.M32 * aO.M13 - bO.M33 * aO.M12,
                                   bO.M33 * aO.M11 - bO.M31 * aO.M13,
                                   bO.M31 * aO.M12 - bO.M32 * aO.M11);
                return false;
            }
            //Inside
            if (tl > FP.Zero)
            {
                tempAxis = new TSVector(aO.M12 * bO.M33 - aO.M13 * bO.M32,
                                       aO.M13 * bO.M31 - aO.M11 * bO.M33,
                                       aO.M11 * bO.M32 - aO.M12 * bO.M31);
                axisLengthInverse = FP.One / tempAxis.magnitude;
                tempDistance = (tl - rarb) * axisLengthInverse;
                if (tempDistance > minimumDistance)
                {
                    minimumDistance = tempDistance;
                    tempAxis.x *= axisLengthInverse;
                    tempAxis.y *= axisLengthInverse;
                    tempAxis.z *= axisLengthInverse;
                    minimumAxis = tempAxis;
                }
            }
            else
            {
                tempAxis = new TSVector(bO.M32 * aO.M13 - bO.M33 * aO.M12,
                                       bO.M33 * aO.M11 - bO.M31 * aO.M13,
                                       bO.M31 * aO.M12 - bO.M32 * aO.M11);
                axisLengthInverse = FP.One / tempAxis.magnitude;
                tempDistance = (-tl - rarb) * axisLengthInverse;
                if (tempDistance > minimumDistance)
                {
                    minimumDistance = tempDistance;
                    tempAxis.x *= axisLengthInverse;
                    tempAxis.y *= axisLengthInverse;
                    tempAxis.z *= axisLengthInverse;
                    minimumAxis = tempAxis;
                }
            }

            #endregion

            #region A.y x B.()

            //A.y x B.x
            rarb = aX * absBR.M31 + aZ * absBR.M11 +
                   bY * absBR.M23 + bZ * absBR.M22;
            tl = t.x * bR.M31 - t.z * bR.M11;
            if (tl > rarb)
            {
                distance = tl - rarb;
                axis = new TSVector(aO.M22 * bO.M13 - aO.M23 * bO.M12,
                                   aO.M23 * bO.M11 - aO.M21 * bO.M13,
                                   aO.M21 * bO.M12 - aO.M22 * bO.M11);
                return false;
            }
            if (tl < -rarb)
            {
                distance = -tl - rarb;
                axis = new TSVector(bO.M12 * aO.M23 - bO.M13 * aO.M22,
                                   bO.M13 * aO.M21 - bO.M11 * aO.M23,
                                   bO.M11 * aO.M22 - bO.M12 * aO.M21);
                return false;
            }
            //Inside
            if (tl > FP.Zero)
            {
                tempAxis = new TSVector(aO.M22 * bO.M13 - aO.M23 * bO.M12,
                                       aO.M23 * bO.M11 - aO.M21 * bO.M13,
                                       aO.M21 * bO.M12 - aO.M22 * bO.M11);
                axisLengthInverse = FP.One / tempAxis.magnitude;
                tempDistance = (tl - rarb) * axisLengthInverse;
                if (tempDistance > minimumDistance)
                {
                    minimumDistance = tempDistance;
                    tempAxis.x *= axisLengthInverse;
                    tempAxis.y *= axisLengthInverse;
                    tempAxis.z *= axisLengthInverse;
                    minimumAxis = tempAxis;
                }
            }
            else
            {
                tempAxis = new TSVector(bO.M12 * aO.M23 - bO.M13 * aO.M22,
                                       bO.M13 * aO.M21 - bO.M11 * aO.M23,
                                       bO.M11 * aO.M22 - bO.M12 * aO.M21);
                axisLengthInverse = FP.One / tempAxis.magnitude;
                tempDistance = (-tl - rarb) * axisLengthInverse;
                if (tempDistance > minimumDistance)
                {
                    minimumDistance = tempDistance;
                    tempAxis.x *= axisLengthInverse;
                    tempAxis.y *= axisLengthInverse;
                    tempAxis.z *= axisLengthInverse;
                    minimumAxis = tempAxis;
                }
            }

            //A.y x B.y
            rarb = aX * absBR.M32 + aZ * absBR.M12 +
                   bX * absBR.M23 + bZ * absBR.M21;
            tl = t.x * bR.M32 - t.z * bR.M12;
            if (tl > rarb)
            {
                distance = tl - rarb;
                axis = new TSVector(aO.M22 * bO.M23 - aO.M23 * bO.M22,
                                   aO.M23 * bO.M21 - aO.M21 * bO.M23,
                                   aO.M21 * bO.M22 - aO.M22 * bO.M21);
                return false;
            }
            if (tl < -rarb)
            {
                distance = -tl - rarb;
                axis = new TSVector(bO.M22 * aO.M23 - bO.M23 * aO.M22,
                                   bO.M23 * aO.M21 - bO.M21 * aO.M23,
                                   bO.M21 * aO.M22 - bO.M22 * aO.M21);
                return false;
            }
            //Inside
            if (tl > FP.Zero)
            {
                tempAxis = new TSVector(aO.M22 * bO.M23 - aO.M23 * bO.M22,
                                       aO.M23 * bO.M21 - aO.M21 * bO.M23,
                                       aO.M21 * bO.M22 - aO.M22 * bO.M21);
                axisLengthInverse = FP.One / tempAxis.magnitude;
                tempDistance = (tl - rarb) * axisLengthInverse;
                if (tempDistance > minimumDistance)
                {
                    minimumDistance = tempDistance;
                    tempAxis.x *= axisLengthInverse;
                    tempAxis.y *= axisLengthInverse;
                    tempAxis.z *= axisLengthInverse;
                    minimumAxis = tempAxis;
                }
            }
            else
            {
                tempAxis = new TSVector(bO.M22 * aO.M23 - bO.M23 * aO.M22,
                                       bO.M23 * aO.M21 - bO.M21 * aO.M23,
                                       bO.M21 * aO.M22 - bO.M22 * aO.M21);
                axisLengthInverse = FP.One / tempAxis.magnitude;
                tempDistance = (-tl - rarb) * axisLengthInverse;
                if (tempDistance > minimumDistance)
                {
                    minimumDistance = tempDistance;
                    tempAxis.x *= axisLengthInverse;
                    tempAxis.y *= axisLengthInverse;
                    tempAxis.z *= axisLengthInverse;
                    minimumAxis = tempAxis;
                }
            }

            //A.y x B.z
            rarb = aX * absBR.M33 + aZ * absBR.M13 +
                   bX * absBR.M22 + bY * absBR.M21;
            tl = t.x * bR.M33 - t.z * bR.M13;
            if (tl > rarb)
            {
                distance = tl - rarb;
                axis = new TSVector(aO.M22 * bO.M33 - aO.M23 * bO.M32,
                                   aO.M23 * bO.M31 - aO.M21 * bO.M33,
                                   aO.M21 * bO.M32 - aO.M22 * bO.M31);
                return false;
            }
            if (tl < -rarb)
            {
                distance = -tl - rarb;
                axis = new TSVector(bO.M32 * aO.M23 - bO.M33 * aO.M22,
                                   bO.M33 * aO.M21 - bO.M31 * aO.M23,
                                   bO.M31 * aO.M22 - bO.M32 * aO.M21);
                return false;
            }
            //Inside
            if (tl > FP.Zero)
            {
                tempAxis = new TSVector(aO.M22 * bO.M33 - aO.M23 * bO.M32,
                                       aO.M23 * bO.M31 - aO.M21 * bO.M33,
                                       aO.M21 * bO.M32 - aO.M22 * bO.M31);
                axisLengthInverse = FP.One / tempAxis.magnitude;
                tempDistance = (tl - rarb) * axisLengthInverse;
                if (tempDistance > minimumDistance)
                {
                    minimumDistance = tempDistance;
                    tempAxis.x *= axisLengthInverse;
                    tempAxis.y *= axisLengthInverse;
                    tempAxis.z *= axisLengthInverse;
                    minimumAxis = tempAxis;
                }
            }
            else
            {
                tempAxis = new TSVector(bO.M32 * aO.M23 - bO.M33 * aO.M22,
                                       bO.M33 * aO.M21 - bO.M31 * aO.M23,
                                       bO.M31 * aO.M22 - bO.M32 * aO.M21);
                axisLengthInverse = FP.One / tempAxis.magnitude;
                tempDistance = (-tl - rarb) * axisLengthInverse;
                if (tempDistance > minimumDistance)
                {
                    minimumDistance = tempDistance;
                    tempAxis.x *= axisLengthInverse;
                    tempAxis.y *= axisLengthInverse;
                    tempAxis.z *= axisLengthInverse;
                    minimumAxis = tempAxis;
                }
            }

            #endregion

            #region A.z x B.()

            //A.z x B.x
            rarb = aX * absBR.M21 + aY * absBR.M11 +
                   bY * absBR.M33 + bZ * absBR.M32;
            tl = t.y * bR.M11 - t.x * bR.M21;
            if (tl > rarb)
            {
                distance = tl - rarb;
                axis = new TSVector(aO.M32 * bO.M13 - aO.M33 * bO.M12,
                                   aO.M33 * bO.M11 - aO.M31 * bO.M13,
                                   aO.M31 * bO.M12 - aO.M32 * bO.M11);
                return false;
            }
            if (tl < -rarb)
            {
                distance = -tl - rarb;
                axis = new TSVector(bO.M12 * aO.M33 - bO.M13 * aO.M32,
                                   bO.M13 * aO.M31 - bO.M11 * aO.M33,
                                   bO.M11 * aO.M32 - bO.M12 * aO.M31);
                return false;
            }
            //Inside
            if (tl > FP.Zero)
            {
                tempAxis = new TSVector(aO.M32 * bO.M13 - aO.M33 * bO.M12,
                                       aO.M33 * bO.M11 - aO.M31 * bO.M13,
                                       aO.M31 * bO.M12 - aO.M32 * bO.M11);
                axisLengthInverse = FP.One / tempAxis.magnitude;
                tempDistance = (tl - rarb) * axisLengthInverse;
                if (tempDistance > minimumDistance)
                {
                    minimumDistance = tempDistance;
                    tempAxis.x *= axisLengthInverse;
                    tempAxis.y *= axisLengthInverse;
                    tempAxis.z *= axisLengthInverse;
                    minimumAxis = tempAxis;
                }
            }
            else
            {
                tempAxis = new TSVector(bO.M12 * aO.M33 - bO.M13 * aO.M32,
                                       bO.M13 * aO.M31 - bO.M11 * aO.M33,
                                       bO.M11 * aO.M32 - bO.M12 * aO.M31);
                axisLengthInverse = FP.One / tempAxis.magnitude;
                tempDistance = (-tl - rarb) * axisLengthInverse;
                if (tempDistance > minimumDistance)
                {
                    minimumDistance = tempDistance;
                    tempAxis.x *= axisLengthInverse;
                    tempAxis.y *= axisLengthInverse;
                    tempAxis.z *= axisLengthInverse;
                    minimumAxis = tempAxis;
                }
            }

            //A.z x B.y
            rarb = aX * absBR.M22 + aY * absBR.M12 +
                   bX * absBR.M33 + bZ * absBR.M31;
            tl = t.y * bR.M12 - t.x * bR.M22;
            if (tl > rarb)
            {
                distance = tl - rarb;
                axis = new TSVector(aO.M32 * bO.M23 - aO.M33 * bO.M22,
                                   aO.M33 * bO.M21 - aO.M31 * bO.M23,
                                   aO.M31 * bO.M22 - aO.M32 * bO.M21);
                return false;
            }
            if (tl < -rarb)
            {
                distance = -tl - rarb;
                axis = new TSVector(bO.M22 * aO.M33 - bO.M23 * aO.M32,
                                   bO.M23 * aO.M31 - bO.M21 * aO.M33,
                                   bO.M21 * aO.M32 - bO.M22 * aO.M31);
                return false;
            }
            //Inside
            if (tl > FP.Zero)
            {
                tempAxis = new TSVector(aO.M32 * bO.M23 - aO.M33 * bO.M22,
                                       aO.M33 * bO.M21 - aO.M31 * bO.M23,
                                       aO.M31 * bO.M22 - aO.M32 * bO.M21);
                axisLengthInverse = FP.One / tempAxis.magnitude;
                tempDistance = (tl - rarb) * axisLengthInverse;
                if (tempDistance > minimumDistance)
                {
                    minimumDistance = tempDistance;
                    tempAxis.x *= axisLengthInverse;
                    tempAxis.y *= axisLengthInverse;
                    tempAxis.z *= axisLengthInverse;
                    minimumAxis = tempAxis;
                }
            }
            else
            {
                tempAxis = new TSVector(bO.M22 * aO.M33 - bO.M23 * aO.M32,
                                       bO.M23 * aO.M31 - bO.M21 * aO.M33,
                                       bO.M21 * aO.M32 - bO.M22 * aO.M31);
                axisLengthInverse = FP.One / tempAxis.magnitude;
                tempDistance = (-tl - rarb) * axisLengthInverse;
                if (tempDistance > minimumDistance)
                {
                    minimumDistance = tempDistance;
                    tempAxis.x *= axisLengthInverse;
                    tempAxis.y *= axisLengthInverse;
                    tempAxis.z *= axisLengthInverse;
                    minimumAxis = tempAxis;
                }
            }

            //A.z x B.z
            rarb = aX * absBR.M23 + aY * absBR.M13 +
                   bX * absBR.M32 + bY * absBR.M31;
            tl = t.y * bR.M13 - t.x * bR.M23;
            if (tl > rarb)
            {
                distance = tl - rarb;
                axis = new TSVector(aO.M32 * bO.M33 - aO.M33 * bO.M32,
                                   aO.M33 * bO.M31 - aO.M31 * bO.M33,
                                   aO.M31 * bO.M32 - aO.M32 * bO.M31);
                return false;
            }
            if (tl < -rarb)
            {
                distance = -tl - rarb;
                axis = new TSVector(bO.M32 * aO.M33 - bO.M33 * aO.M32,
                                   bO.M33 * aO.M31 - bO.M31 * aO.M33,
                                   bO.M31 * aO.M32 - bO.M32 * aO.M31);
                return false;
            }
            //Inside
            if (tl > FP.Zero)
            {
                tempAxis = new TSVector(aO.M32 * bO.M33 - aO.M33 * bO.M32,
                                       aO.M33 * bO.M31 - aO.M31 * bO.M33,
                                       aO.M31 * bO.M32 - aO.M32 * bO.M31);
                axisLengthInverse = FP.One / tempAxis.magnitude;
                tempDistance = (tl - rarb) * axisLengthInverse;
                if (tempDistance > minimumDistance)
                {
                    minimumDistance = tempDistance;
                    tempAxis.x *= axisLengthInverse;
                    tempAxis.y *= axisLengthInverse;
                    tempAxis.z *= axisLengthInverse;
                    minimumAxis = tempAxis;
                }
            }
            else
            {
                tempAxis = new TSVector(bO.M32 * aO.M33 - bO.M33 * aO.M32,
                                       bO.M33 * aO.M31 - bO.M31 * aO.M33,
                                       bO.M31 * aO.M32 - bO.M32 * aO.M31);
                axisLengthInverse = FP.One / tempAxis.magnitude;
                tempDistance = (-tl - rarb) * axisLengthInverse;
                if (tempDistance > minimumDistance)
                {
                    minimumDistance = tempDistance;
                    tempAxis.x *= axisLengthInverse;
                    tempAxis.y *= axisLengthInverse;
                    tempAxis.z *= axisLengthInverse;
                    minimumAxis = tempAxis;
                }
            }

            #endregion

            distance = minimumDistance;
            axis = minimumAxis;
            return true;
        }

#if ALLOWUNSAFE
        /// <summary>
        /// Determines if the two boxes are colliding and computes contact data.
        /// </summary>
        /// <param name="a">First box to collide.</param>
        /// <param name="b">Second box to collide.</param>
        /// <param name="distance">Distance of separation or penetration.</param>
        /// <param name="axis">Axis of separation or penetration.</param>
        /// <param name="contactData">Computed contact data.</param>
        /// <param name="transformA">Transform to apply to shape A.</param>
        /// <param name="transformB">Transform to apply to shape B.</param>
        /// <returns>Whether or not the boxes collide.</returns>
        public static unsafe bool AreBoxesColliding(ref BoxShape a, ref BoxShape b, ref RigidTransform transformA, ref RigidTransform transformB, out FP distance, out TSVector axis, out TinyStructList<BoxContactData> contactData)
        {
            BoxContactDataCache tempData;
            bool toReturn = AreBoxesColliding(a, b, ref transformA, ref transformB, out distance, out axis, out tempData);
            BoxContactData* dataPointer = &tempData.D1;
            contactData = new TinyStructList<BoxContactData>();
            for (int i = 0; i < tempData.Count; i++)
            {
                contactData.Add(ref dataPointer[i]);
            }
            return toReturn;
        }
#endif

        /// <summary>
        /// Determines if the two boxes are colliding and computes contact data.
        /// </summary>
        /// <param name="a">First box to collide.</param>
        /// <param name="b">Second box to collide.</param>
        /// <param name="distance">Distance of separation or penetration.</param>
        /// <param name="axis">Axis of separation or penetration.</param>
        /// <param name="contactData">Contact positions, depths, and ids.</param>
        /// <param name="transformA">Transform to apply to shape A.</param>
        /// <param name="transformB">Transform to apply to shape B.</param>
        /// <returns>Whether or not the boxes collide.</returns>
#if ALLOWUNSAFE
        public static bool AreBoxesColliding(ref BoxShape a, ref BoxShape b, ref TSVector positionA, ref TSVector positionB, ref TSMatrix orientationA, ref TSMatrix orientationB
            , out FP distance, out TSVector axis, out BoxContactDataCache contactData)
#else
        public static bool AreBoxesColliding(ref BoxShape a, ref BoxShape b, ref TSVector positionA, ref TSVector positionB, ref TSMatrix orientationA, ref TSMatrix orientationB,
            out FP distance, out TSVector axis, out TinyStructList<BoxContactData> contactData)
#endif
        {
            TSVector halfSizeA = a.halfSize;
            FP aX = halfSizeA.z;
            FP aY = halfSizeA.y;
            FP aZ = halfSizeA.x;

            TSVector halfSizeB = b.halfSize;
            FP bX = halfSizeB.z;
            FP bY = halfSizeB.y;
            FP bZ = halfSizeB.x;

#if ALLOWUNSAFE
            contactData = new BoxContactDataCache();
#else
            contactData = new TinyStructList<BoxContactData>();
#endif
            //Relative rotation from A to B.
            TSMatrix bR;

            TSMatrix aO = orientationA;
            TSMatrix bO = orientationB;

            //Relative translation rotated into A's configuration space.
            TSVector t;
            TSVector.Subtract(ref positionB, ref positionA, out t);

            FP tempDistance;
            FP minimumDistance = -FP.MaxValue;
            var minimumAxis = TSVector.zero;
            byte minimumFeature = 2; //2 means edge.  0-> A face, 1 -> B face.

            #region A Face Normals

            bR.M11 = aO.M11 * bO.M11 + aO.M12 * bO.M12 + aO.M13 * bO.M13;
            bR.M12 = aO.M11 * bO.M21 + aO.M12 * bO.M22 + aO.M13 * bO.M23;
            bR.M13 = aO.M11 * bO.M31 + aO.M12 * bO.M32 + aO.M13 * bO.M33;
            TSMatrix absBR;
            //Epsilons are added to deal with near-parallel edges.
            absBR.M11 = FP.Abs(bR.M11) + TSMath.Epsilon;
            absBR.M12 = FP.Abs(bR.M12) + TSMath.Epsilon;
            absBR.M13 = FP.Abs(bR.M13) + TSMath.Epsilon;
            FP tX = t.x;
            t.x = t.x * aO.M11 + t.y * aO.M12 + t.z * aO.M13;

            //Test the axes defines by entity A's rotation matrix.
            //A.x
            FP rarb = aX + bX * absBR.M11 + bY * absBR.M12 + bZ * absBR.M13;
            if (t.x > rarb)
            {
                distance = t.x - rarb;
                axis = new TSVector(-aO.M11, -aO.M12, -aO.M13);
                return false;
            }
            if (t.x < -rarb)
            {
                distance = -t.x - rarb;
                axis = new TSVector(aO.M11, aO.M12, aO.M13);
                return false;
            }
            //Inside
            if (t.x > FP.Zero)
            {
                tempDistance = t.x - rarb;
                if (tempDistance > minimumDistance)
                {
                    minimumDistance = tempDistance;
                    minimumAxis = new TSVector(-aO.M11, -aO.M12, -aO.M13);
                    minimumFeature = 0;
                }
            }
            else
            {
                tempDistance = -t.x - rarb;
                if (tempDistance > minimumDistance)
                {
                    minimumDistance = tempDistance;
                    minimumAxis = new TSVector(aO.M11, aO.M12, aO.M13);
                    minimumFeature = 0;
                }
            }


            bR.M21 = aO.M21 * bO.M11 + aO.M22 * bO.M12 + aO.M23 * bO.M13;
            bR.M22 = aO.M21 * bO.M21 + aO.M22 * bO.M22 + aO.M23 * bO.M23;
            bR.M23 = aO.M21 * bO.M31 + aO.M22 * bO.M32 + aO.M23 * bO.M33;
            absBR.M21 = FP.Abs(bR.M21) + TSMath.Epsilon;
            absBR.M22 = FP.Abs(bR.M22) + TSMath.Epsilon;
            absBR.M23 = FP.Abs(bR.M23) + TSMath.Epsilon;
            FP tY = t.y;
            t.y = tX * aO.M21 + t.y * aO.M22 + t.z * aO.M23;

            //A.y
            rarb = aY + bX * absBR.M21 + bY * absBR.M22 + bZ * absBR.M23;
            if (t.y > rarb)
            {
                distance = t.y - rarb;
                axis = new TSVector(-aO.M21, -aO.M22, -aO.M23);
                return false;
            }
            if (t.y < -rarb)
            {
                distance = -t.y - rarb;
                axis = new TSVector(aO.M21, aO.M22, aO.M23);
                return false;
            }
            //Inside
            if (t.y > FP.Zero)
            {
                tempDistance = t.y - rarb;
                if (tempDistance > minimumDistance)
                {
                    minimumDistance = tempDistance;
                    minimumAxis = new TSVector(-aO.M21, -aO.M22, -aO.M23);
                    minimumFeature = 0;
                }
            }
            else
            {
                tempDistance = -t.y - rarb;
                if (tempDistance > minimumDistance)
                {
                    minimumDistance = tempDistance;
                    minimumAxis = new TSVector(aO.M21, aO.M22, aO.M23);
                    minimumFeature = 0;
                }
            }

            bR.M31 = aO.M31 * bO.M11 + aO.M32 * bO.M12 + aO.M33 * bO.M13;
            bR.M32 = aO.M31 * bO.M21 + aO.M32 * bO.M22 + aO.M33 * bO.M23;
            bR.M33 = aO.M31 * bO.M31 + aO.M32 * bO.M32 + aO.M33 * bO.M33;
            absBR.M31 = FP.Abs(bR.M31) + TSMath.Epsilon;
            absBR.M32 = FP.Abs(bR.M32) + TSMath.Epsilon;
            absBR.M33 = FP.Abs(bR.M33) + TSMath.Epsilon;
            t.z = tX * aO.M31 + tY * aO.M32 + t.z * aO.M33;

            //A.z
            rarb = aZ + bX * absBR.M31 + bY * absBR.M32 + bZ * absBR.M33;
            if (t.z > rarb)
            {
                distance = t.z - rarb;
                axis = new TSVector(-aO.M31, -aO.M32, -aO.M33);
                return false;
            }
            if (t.z < -rarb)
            {
                distance = -t.z - rarb;
                axis = new TSVector(aO.M31, aO.M32, aO.M33);
                return false;
            }
            //Inside
            if (t.z > FP.Zero)
            {
                tempDistance = t.z - rarb;
                if (tempDistance > minimumDistance)
                {
                    minimumDistance = tempDistance;
                    minimumAxis = new TSVector(-aO.M31, -aO.M32, -aO.M33);
                    minimumFeature = 0;
                }
            }
            else
            {
                tempDistance = -t.z - rarb;
                if (tempDistance > minimumDistance)
                {
                    minimumDistance = tempDistance;
                    minimumAxis = new TSVector(aO.M31, aO.M32, aO.M33);
                    minimumFeature = 0;
                }
            }

            #endregion

            FP antiBBias = FP.EN2;
            minimumDistance += antiBBias;

            #region B Face Normals

            //Test the axes defines by entity B's rotation matrix.
            //B.x
            rarb = bX + aX * absBR.M11 + aY * absBR.M21 + aZ * absBR.M31;
            FP tl = t.x * bR.M11 + t.y * bR.M21 + t.z * bR.M31;
            if (tl > rarb)
            {
                distance = tl - rarb;
                axis = new TSVector(-bO.M11, -bO.M12, -bO.M13);
                return false;
            }
            if (tl < -rarb)
            {
                distance = -tl - rarb;
                axis = new TSVector(bO.M11, bO.M12, bO.M13);
                return false;
            }
            //Inside
            if (tl > FP.Zero)
            {
                tempDistance = tl - rarb;
                if (tempDistance > minimumDistance)
                {
                    minimumDistance = tempDistance;
                    minimumAxis = new TSVector(-bO.M11, -bO.M12, -bO.M13);
                    minimumFeature = 1;
                }
            }
            else
            {
                tempDistance = -tl - rarb;
                if (tempDistance > minimumDistance)
                {
                    minimumDistance = tempDistance;
                    minimumAxis = new TSVector(bO.M11, bO.M12, bO.M13);
                    minimumFeature = 1;
                }
            }

            //B.y
            rarb = bY + aX * absBR.M12 + aY * absBR.M22 + aZ * absBR.M32;
            tl = t.x * bR.M12 + t.y * bR.M22 + t.z * bR.M32;
            if (tl > rarb)
            {
                distance = tl - rarb;
                axis = new TSVector(-bO.M21, -bO.M22, -bO.M23);
                return false;
            }
            if (tl < -rarb)
            {
                distance = -tl - rarb;
                axis = new TSVector(bO.M21, bO.M22, bO.M23);
                return false;
            }
            //Inside
            if (tl > FP.Zero)
            {
                tempDistance = tl - rarb;
                if (tempDistance > minimumDistance)
                {
                    minimumDistance = tempDistance;
                    minimumAxis = new TSVector(-bO.M21, -bO.M22, -bO.M23);
                    minimumFeature = 1;
                }
            }
            else
            {
                tempDistance = -tl - rarb;
                if (tempDistance > minimumDistance)
                {
                    minimumDistance = tempDistance;
                    minimumAxis = new TSVector(bO.M21, bO.M22, bO.M23);
                    minimumFeature = 1;
                }
            }

            //B.z
            rarb = bZ + aX * absBR.M13 + aY * absBR.M23 + aZ * absBR.M33;
            tl = t.x * bR.M13 + t.y * bR.M23 + t.z * bR.M33;
            if (tl > rarb)
            {
                distance = tl - rarb;
                axis = new TSVector(-bO.M31, -bO.M32, -bO.M33);
                return false;
            }
            if (tl < -rarb)
            {
                distance = -tl - rarb;
                axis = new TSVector(bO.M31, bO.M32, bO.M33);
                return false;
            }
            //Inside
            if (tl > FP.Zero)
            {
                tempDistance = tl - rarb;
                if (tempDistance > minimumDistance)
                {
                    minimumDistance = tempDistance;
                    minimumAxis = new TSVector(-bO.M31, -bO.M32, -bO.M33);
                    minimumFeature = 1;
                }
            }
            else
            {
                tempDistance = -tl - rarb;
                if (tempDistance > minimumDistance)
                {
                    minimumDistance = tempDistance;
                    minimumAxis = new TSVector(bO.M31, bO.M32, bO.M33);
                    minimumFeature = 1;
                }
            }

            #endregion

            if (minimumFeature != 1)
                minimumDistance -= antiBBias;

            FP antiEdgeBias = FP.EN2;
            minimumDistance += antiEdgeBias;
            FP axisLengthInverse;
            TSVector tempAxis;

            #region A.x x B.()

            //Now for the edge-edge cases.
            //A.x x B.x
            rarb = aY * absBR.M31 + aZ * absBR.M21 +
                   bY * absBR.M13 + bZ * absBR.M12;
            tl = t.z * bR.M21 - t.y * bR.M31;
            if (tl > rarb)
            {
                distance = tl - rarb;
                axis = new TSVector(bO.M12 * aO.M13 - bO.M13 * aO.M12,
                                   bO.M13 * aO.M11 - bO.M11 * aO.M13,
                                   bO.M11 * aO.M12 - bO.M12 * aO.M11);
                return false;
            }
            if (tl < -rarb)
            {
                distance = -tl - rarb;
                axis = new TSVector(aO.M12 * bO.M13 - aO.M13 * bO.M12,
                                   aO.M13 * bO.M11 - aO.M11 * bO.M13,
                                   aO.M11 * bO.M12 - aO.M12 * bO.M11);
                return false;
            }
            //Inside
            if (tl > FP.Zero)
            {
                tempAxis = new TSVector(bO.M12 * aO.M13 - bO.M13 * aO.M12,
                                       bO.M13 * aO.M11 - bO.M11 * aO.M13,
                                       bO.M11 * aO.M12 - bO.M12 * aO.M11);

                axisLengthInverse = FP.One / tempAxis.magnitude;
                tempDistance = (tl - rarb) * axisLengthInverse;
                if (tempDistance > minimumDistance)
                {
                    minimumFeature = 2;
                    minimumDistance = tempDistance;
                    tempAxis.x *= axisLengthInverse;
                    tempAxis.y *= axisLengthInverse;
                    tempAxis.z *= axisLengthInverse;
                    minimumAxis = tempAxis;
                }
            }
            else
            {
                tempAxis = new TSVector(aO.M12 * bO.M13 - aO.M13 * bO.M12,
                                       aO.M13 * bO.M11 - aO.M11 * bO.M13,
                                       aO.M11 * bO.M12 - aO.M12 * bO.M11);
                axisLengthInverse = FP.One / tempAxis.magnitude;
                tempDistance = (-tl - rarb) * axisLengthInverse;
                if (tempDistance > minimumDistance)
                {
                    minimumFeature = 2;
                    minimumDistance = tempDistance;
                    tempAxis.x *= axisLengthInverse;
                    tempAxis.y *= axisLengthInverse;
                    tempAxis.z *= axisLengthInverse;
                    minimumAxis = tempAxis;
                }
            }

            //A.x x B.y
            rarb = aY * absBR.M32 + aZ * absBR.M22 +
                   bX * absBR.M13 + bZ * absBR.M11;
            tl = t.z * bR.M22 - t.y * bR.M32;
            if (tl > rarb)
            {
                distance = tl - rarb;
                axis = new TSVector(bO.M22 * aO.M13 - bO.M23 * aO.M12,
                                   bO.M23 * aO.M11 - bO.M21 * aO.M13,
                                   bO.M21 * aO.M12 - bO.M22 * aO.M11);
                return false;
            }
            if (tl < -rarb)
            {
                distance = -tl - rarb;
                axis = new TSVector(aO.M12 * bO.M23 - aO.M13 * bO.M22,
                                   aO.M13 * bO.M21 - aO.M11 * bO.M23,
                                   aO.M11 * bO.M22 - aO.M12 * bO.M21);

                return false;
            }
            //Inside
            if (tl > FP.Zero)
            {
                tempAxis = new TSVector(bO.M22 * aO.M13 - bO.M23 * aO.M12,
                                       bO.M23 * aO.M11 - bO.M21 * aO.M13,
                                       bO.M21 * aO.M12 - bO.M22 * aO.M11);

                axisLengthInverse = FP.One / tempAxis.magnitude;
                tempDistance = (tl - rarb) * axisLengthInverse;
                if (tempDistance > minimumDistance)
                {
                    minimumFeature = 2;
                    minimumDistance = tempDistance;
                    tempAxis.x *= axisLengthInverse;
                    tempAxis.y *= axisLengthInverse;
                    tempAxis.z *= axisLengthInverse;
                    minimumAxis = tempAxis;
                }
            }
            else
            {
                tempAxis = new TSVector(aO.M12 * bO.M23 - aO.M13 * bO.M22,
                                       aO.M13 * bO.M21 - aO.M11 * bO.M23,
                                       aO.M11 * bO.M22 - aO.M12 * bO.M21);
                axisLengthInverse = FP.One / tempAxis.magnitude;
                tempDistance = (-tl - rarb) * axisLengthInverse;
                if (tempDistance > minimumDistance)
                {
                    minimumFeature = 2;
                    minimumDistance = tempDistance;
                    tempAxis.x *= axisLengthInverse;
                    tempAxis.y *= axisLengthInverse;
                    tempAxis.z *= axisLengthInverse;
                    minimumAxis = tempAxis;
                }
            }

            //A.x x B.z
            rarb = aY * absBR.M33 + aZ * absBR.M23 +
                   bX * absBR.M12 + bY * absBR.M11;
            tl = t.z * bR.M23 - t.y * bR.M33;
            if (tl > rarb)
            {
                distance = tl - rarb;
                axis = new TSVector(bO.M32 * aO.M13 - bO.M33 * aO.M12,
                                   bO.M33 * aO.M11 - bO.M31 * aO.M13,
                                   bO.M31 * aO.M12 - bO.M32 * aO.M11);
                return false;
            }
            if (tl < -rarb)
            {
                distance = -tl - rarb;

                axis = new TSVector(aO.M12 * bO.M33 - aO.M13 * bO.M32,
                                   aO.M13 * bO.M31 - aO.M11 * bO.M33,
                                   aO.M11 * bO.M32 - aO.M12 * bO.M31);
                return false;
            }
            //Inside
            if (tl > FP.Zero)
            {
                tempAxis = new TSVector(bO.M32 * aO.M13 - bO.M33 * aO.M12,
                                       bO.M33 * aO.M11 - bO.M31 * aO.M13,
                                       bO.M31 * aO.M12 - bO.M32 * aO.M11);
                axisLengthInverse = FP.One / tempAxis.magnitude;
                tempDistance = (tl - rarb) * axisLengthInverse;
                if (tempDistance > minimumDistance)
                {
                    minimumFeature = 2;
                    minimumDistance = tempDistance;
                    tempAxis.x *= axisLengthInverse;
                    tempAxis.y *= axisLengthInverse;
                    tempAxis.z *= axisLengthInverse;
                    minimumAxis = tempAxis;
                }
            }
            else
            {
                tempAxis = new TSVector(aO.M12 * bO.M33 - aO.M13 * bO.M32,
                                       aO.M13 * bO.M31 - aO.M11 * bO.M33,
                                       aO.M11 * bO.M32 - aO.M12 * bO.M31);

                axisLengthInverse = FP.One / tempAxis.magnitude;
                tempDistance = (-tl - rarb) * axisLengthInverse;
                if (tempDistance > minimumDistance)
                {
                    minimumFeature = 2;
                    minimumDistance = tempDistance;
                    tempAxis.x *= axisLengthInverse;
                    tempAxis.y *= axisLengthInverse;
                    tempAxis.z *= axisLengthInverse;
                    minimumAxis = tempAxis;
                }
            }

            #endregion

            #region A.y x B.()

            //A.y x B.x
            rarb = aX * absBR.M31 + aZ * absBR.M11 +
                   bY * absBR.M23 + bZ * absBR.M22;
            tl = t.x * bR.M31 - t.z * bR.M11;
            if (tl > rarb)
            {
                distance = tl - rarb;
                axis = new TSVector(bO.M12 * aO.M23 - bO.M13 * aO.M22,
                                   bO.M13 * aO.M21 - bO.M11 * aO.M23,
                                   bO.M11 * aO.M22 - bO.M12 * aO.M21);
                return false;
            }
            if (tl < -rarb)
            {
                distance = -tl - rarb;
                axis = new TSVector(aO.M22 * bO.M13 - aO.M23 * bO.M12,
                                   aO.M23 * bO.M11 - aO.M21 * bO.M13,
                                   aO.M21 * bO.M12 - aO.M22 * bO.M11);

                return false;
            }
            //Inside
            if (tl > FP.Zero)
            {
                tempAxis = new TSVector(bO.M12 * aO.M23 - bO.M13 * aO.M22,
                                       bO.M13 * aO.M21 - bO.M11 * aO.M23,
                                       bO.M11 * aO.M22 - bO.M12 * aO.M21);
                axisLengthInverse = FP.One / tempAxis.magnitude;
                tempDistance = (tl - rarb) * axisLengthInverse;
                if (tempDistance > minimumDistance)
                {
                    minimumFeature = 2;
                    minimumDistance = tempDistance;
                    tempAxis.x *= axisLengthInverse;
                    tempAxis.y *= axisLengthInverse;
                    tempAxis.z *= axisLengthInverse;
                    minimumAxis = tempAxis;
                }
            }
            else
            {
                tempAxis = new TSVector(aO.M22 * bO.M13 - aO.M23 * bO.M12,
                                       aO.M23 * bO.M11 - aO.M21 * bO.M13,
                                       aO.M21 * bO.M12 - aO.M22 * bO.M11);

                axisLengthInverse = FP.One / tempAxis.magnitude;
                tempDistance = (-tl - rarb) * axisLengthInverse;
                if (tempDistance > minimumDistance)
                {
                    minimumFeature = 2;
                    minimumDistance = tempDistance;
                    tempAxis.x *= axisLengthInverse;
                    tempAxis.y *= axisLengthInverse;
                    tempAxis.z *= axisLengthInverse;
                    minimumAxis = tempAxis;
                }
            }

            //A.y x B.y
            rarb = aX * absBR.M32 + aZ * absBR.M12 +
                   bX * absBR.M23 + bZ * absBR.M21;
            tl = t.x * bR.M32 - t.z * bR.M12;
            if (tl > rarb)
            {
                distance = tl - rarb;
                axis = new TSVector(bO.M22 * aO.M23 - bO.M23 * aO.M22,
                                   bO.M23 * aO.M21 - bO.M21 * aO.M23,
                                   bO.M21 * aO.M22 - bO.M22 * aO.M21);
                return false;
            }
            if (tl < -rarb)
            {
                distance = -tl - rarb;

                axis = new TSVector(aO.M22 * bO.M23 - aO.M23 * bO.M22,
                                   aO.M23 * bO.M21 - aO.M21 * bO.M23,
                                   aO.M21 * bO.M22 - aO.M22 * bO.M21);
                return false;
            }
            //Inside
            if (tl > FP.Zero)
            {
                tempAxis = new TSVector(bO.M22 * aO.M23 - bO.M23 * aO.M22,
                                       bO.M23 * aO.M21 - bO.M21 * aO.M23,
                                       bO.M21 * aO.M22 - bO.M22 * aO.M21);
                axisLengthInverse = FP.One / tempAxis.magnitude;
                tempDistance = (tl - rarb) * axisLengthInverse;
                if (tempDistance > minimumDistance)
                {
                    minimumFeature = 2;
                    minimumDistance = tempDistance;
                    tempAxis.x *= axisLengthInverse;
                    tempAxis.y *= axisLengthInverse;
                    tempAxis.z *= axisLengthInverse;
                    minimumAxis = tempAxis;
                }
            }
            else
            {
                tempAxis = new TSVector(aO.M22 * bO.M23 - aO.M23 * bO.M22,
                                       aO.M23 * bO.M21 - aO.M21 * bO.M23,
                                       aO.M21 * bO.M22 - aO.M22 * bO.M21);

                axisLengthInverse = FP.One / tempAxis.magnitude;
                tempDistance = (-tl - rarb) * axisLengthInverse;
                if (tempDistance > minimumDistance)
                {
                    minimumFeature = 2;
                    minimumDistance = tempDistance;
                    tempAxis.x *= axisLengthInverse;
                    tempAxis.y *= axisLengthInverse;
                    tempAxis.z *= axisLengthInverse;
                    minimumAxis = tempAxis;
                }
            }

            //A.y x B.z
            rarb = aX * absBR.M33 + aZ * absBR.M13 +
                   bX * absBR.M22 + bY * absBR.M21;
            tl = t.x * bR.M33 - t.z * bR.M13;
            if (tl > rarb)
            {
                distance = tl - rarb;
                axis = new TSVector(bO.M32 * aO.M23 - bO.M33 * aO.M22,
                                   bO.M33 * aO.M21 - bO.M31 * aO.M23,
                                   bO.M31 * aO.M22 - bO.M32 * aO.M21);
                return false;
            }
            if (tl < -rarb)
            {
                distance = -tl - rarb;

                axis = new TSVector(aO.M22 * bO.M33 - aO.M23 * bO.M32,
                                   aO.M23 * bO.M31 - aO.M21 * bO.M33,
                                   aO.M21 * bO.M32 - aO.M22 * bO.M31);
                return false;
            }
            //Inside
            if (tl > FP.Zero)
            {
                tempAxis = new TSVector(bO.M32 * aO.M23 - bO.M33 * aO.M22,
                                       bO.M33 * aO.M21 - bO.M31 * aO.M23,
                                       bO.M31 * aO.M22 - bO.M32 * aO.M21);
                axisLengthInverse = FP.One / tempAxis.magnitude;
                tempDistance = (tl - rarb) * axisLengthInverse;
                if (tempDistance > minimumDistance)
                {
                    minimumFeature = 2;
                    minimumDistance = tempDistance;
                    tempAxis.x *= axisLengthInverse;
                    tempAxis.y *= axisLengthInverse;
                    tempAxis.z *= axisLengthInverse;
                    minimumAxis = tempAxis;
                }
            }
            else
            {
                tempAxis = new TSVector(aO.M22 * bO.M33 - aO.M23 * bO.M32,
                                       aO.M23 * bO.M31 - aO.M21 * bO.M33,
                                       aO.M21 * bO.M32 - aO.M22 * bO.M31);

                axisLengthInverse = FP.One / tempAxis.magnitude;
                tempDistance = (-tl - rarb) * axisLengthInverse;
                if (tempDistance > minimumDistance)
                {
                    minimumFeature = 2;
                    minimumDistance = tempDistance;
                    tempAxis.x *= axisLengthInverse;
                    tempAxis.y *= axisLengthInverse;
                    tempAxis.z *= axisLengthInverse;
                    minimumAxis = tempAxis;
                }
            }

            #endregion

            #region A.z x B.()

            //A.z x B.x
            rarb = aX * absBR.M21 + aY * absBR.M11 +
                   bY * absBR.M33 + bZ * absBR.M32;
            tl = t.y * bR.M11 - t.x * bR.M21;
            if (tl > rarb)
            {
                distance = tl - rarb;
                axis = new TSVector(bO.M12 * aO.M33 - bO.M13 * aO.M32,
                                   bO.M13 * aO.M31 - bO.M11 * aO.M33,
                                   bO.M11 * aO.M32 - bO.M12 * aO.M31);
                return false;
            }
            if (tl < -rarb)
            {
                distance = -tl - rarb;

                axis = new TSVector(aO.M32 * bO.M13 - aO.M33 * bO.M12,
                                   aO.M33 * bO.M11 - aO.M31 * bO.M13,
                                   aO.M31 * bO.M12 - aO.M32 * bO.M11);
                return false;
            }
            //Inside
            if (tl > FP.Zero)
            {
                tempAxis = new TSVector(bO.M12 * aO.M33 - bO.M13 * aO.M32,
                                       bO.M13 * aO.M31 - bO.M11 * aO.M33,
                                       bO.M11 * aO.M32 - bO.M12 * aO.M31);
                axisLengthInverse = FP.One / tempAxis.magnitude;
                tempDistance = (tl - rarb) * axisLengthInverse;
                if (tempDistance > minimumDistance)
                {
                    minimumFeature = 2;
                    minimumDistance = tempDistance;
                    tempAxis.x *= axisLengthInverse;
                    tempAxis.y *= axisLengthInverse;
                    tempAxis.z *= axisLengthInverse;
                    minimumAxis = tempAxis;
                }
            }
            else
            {
                tempAxis = new TSVector(aO.M32 * bO.M13 - aO.M33 * bO.M12,
                                       aO.M33 * bO.M11 - aO.M31 * bO.M13,
                                       aO.M31 * bO.M12 - aO.M32 * bO.M11);
                axisLengthInverse = FP.One / tempAxis.magnitude;
                tempDistance = (-tl - rarb) * axisLengthInverse;
                if (tempDistance > minimumDistance)
                {
                    minimumFeature = 2;
                    minimumDistance = tempDistance;
                    tempAxis.x *= axisLengthInverse;
                    tempAxis.y *= axisLengthInverse;
                    tempAxis.z *= axisLengthInverse;
                    minimumAxis = tempAxis;
                }
            }

            //A.z x B.y
            rarb = aX * absBR.M22 + aY * absBR.M12 +
                   bX * absBR.M33 + bZ * absBR.M31;
            tl = t.y * bR.M12 - t.x * bR.M22;
            if (tl > rarb)
            {
                distance = tl - rarb;
                axis = new TSVector(bO.M22 * aO.M33 - bO.M23 * aO.M32,
                                   bO.M23 * aO.M31 - bO.M21 * aO.M33,
                                   bO.M21 * aO.M32 - bO.M22 * aO.M31);
                return false;
            }
            if (tl < -rarb)
            {
                distance = -tl - rarb;

                axis = new TSVector(aO.M32 * bO.M23 - aO.M33 * bO.M22,
                                   aO.M33 * bO.M21 - aO.M31 * bO.M23,
                                   aO.M31 * bO.M22 - aO.M32 * bO.M21);
                return false;
            }
            //Inside
            if (tl > FP.Zero)
            {
                tempAxis = new TSVector(bO.M22 * aO.M33 - bO.M23 * aO.M32,
                                       bO.M23 * aO.M31 - bO.M21 * aO.M33,
                                       bO.M21 * aO.M32 - bO.M22 * aO.M31);
                axisLengthInverse = FP.One / tempAxis.magnitude;
                tempDistance = (tl - rarb) * axisLengthInverse;
                if (tempDistance > minimumDistance)
                {
                    minimumFeature = 2;
                    minimumDistance = tempDistance;
                    tempAxis.x *= axisLengthInverse;
                    tempAxis.y *= axisLengthInverse;
                    tempAxis.z *= axisLengthInverse;
                    minimumAxis = tempAxis;
                }
            }
            else
            {
                tempAxis = new TSVector(aO.M32 * bO.M23 - aO.M33 * bO.M22,
                                       aO.M33 * bO.M21 - aO.M31 * bO.M23,
                                       aO.M31 * bO.M22 - aO.M32 * bO.M21);
                axisLengthInverse = FP.One / tempAxis.magnitude;
                tempDistance = (-tl - rarb) * axisLengthInverse;
                if (tempDistance > minimumDistance)
                {
                    minimumFeature = 2;
                    minimumDistance = tempDistance;
                    tempAxis.x *= axisLengthInverse;
                    tempAxis.y *= axisLengthInverse;
                    tempAxis.z *= axisLengthInverse;
                    minimumAxis = tempAxis;
                }
            }

            //A.z x B.z
            rarb = aX * absBR.M23 + aY * absBR.M13 +
                   bX * absBR.M32 + bY * absBR.M31;
            tl = t.y * bR.M13 - t.x * bR.M23;
            if (tl > rarb)
            {
                distance = tl - rarb;
                axis = new TSVector(bO.M32 * aO.M33 - bO.M33 * aO.M32,
                                   bO.M33 * aO.M31 - bO.M31 * aO.M33,
                                   bO.M31 * aO.M32 - bO.M32 * aO.M31);
                return false;
            }
            if (tl < -rarb)
            {
                distance = -tl - rarb;
                axis = new TSVector(aO.M32 * bO.M33 - aO.M33 * bO.M32,
                                   aO.M33 * bO.M31 - aO.M31 * bO.M33,
                                   aO.M31 * bO.M32 - aO.M32 * bO.M31);
                return false;
            }
            //Inside
            if (tl > FP.Zero)
            {
                tempAxis = new TSVector(bO.M32 * aO.M33 - bO.M33 * aO.M32,
                                       bO.M33 * aO.M31 - bO.M31 * aO.M33,
                                       bO.M31 * aO.M32 - bO.M32 * aO.M31);
                axisLengthInverse = FP.One / tempAxis.magnitude;
                tempDistance = (tl - rarb) * axisLengthInverse;
                if (tempDistance > minimumDistance)
                {
                    minimumFeature = 2;
                    minimumDistance = tempDistance;
                    tempAxis.x *= axisLengthInverse;
                    tempAxis.y *= axisLengthInverse;
                    tempAxis.z *= axisLengthInverse;
                    minimumAxis = tempAxis;
                }
            }
            else
            {
                tempAxis = new TSVector(aO.M32 * bO.M33 - aO.M33 * bO.M32,
                                       aO.M33 * bO.M31 - aO.M31 * bO.M33,
                                       aO.M31 * bO.M32 - aO.M32 * bO.M31);

                axisLengthInverse = FP.One / tempAxis.magnitude;
                tempDistance = (-tl - rarb) * axisLengthInverse;
                if (tempDistance > minimumDistance)
                {
                    minimumFeature = 2;
                    minimumDistance = tempDistance;
                    tempAxis.x *= axisLengthInverse;
                    tempAxis.y *= axisLengthInverse;
                    tempAxis.z *= axisLengthInverse;
                    minimumAxis = tempAxis;
                }
            }

            #endregion

            if (minimumFeature == 2)
            {

                //Edge-edge contact conceptually only has one contact, but allowing it to create multiple due to penetration is more robust.
                GetEdgeEdgeContact(ref a, ref b, ref positionA, ref aO, ref positionB, ref bO, minimumDistance, ref minimumAxis, out contactData);

                //TSVector position;
                //FP depth;
                //int id;
                //                GetEdgeEdgeContact(a, b, ref transformA.Position, ref aO, ref transformB.Position, ref bO, ref minimumAxis, out position, out id);
                //#if ALLOWUNSAFE
                //                contactData.D1.Position = position;
                //                contactData.D1.Depth = minimumDistance; 
                //                contactData.D1.Id = id;
                //                contactData.Count = 1;
                //#else
                //                var toAdd = new BoxContactData();
                //                toAdd.Position = position;
                //                toAdd.Depth = minimumDistance;
                //                toAdd.Id = id;
                //                contactData.Add(ref toAdd);
                //#endif
            }
            else
            {
                minimumDistance -= antiEdgeBias;
                GetFaceContacts(ref a, ref b, ref positionA, ref aO, ref positionB, ref bO, minimumFeature == 0, ref minimumAxis, out contactData);

            }

            distance = minimumDistance;
            axis = minimumAxis;
            return true;
        }

#if ALLOWUNSAFE
        internal static void GetEdgeEdgeContact(ref BoxShape a, ref BoxShape b, ref TSVector positionA, ref TSMatrix orientationA, ref TSVector positionB, ref TSMatrix orientationB, FP depth, ref TSVector mtd, out BoxContactDataCache contactData)
#else
        internal static void GetEdgeEdgeContact(ref BoxShape a, ref BoxShape b, ref TSVector positionA, ref TSMatrix orientationA, ref TSVector positionB, ref TSMatrix orientationB, FP depth, ref TSVector mtd, out TinyStructList<BoxContactData> contactData)
#endif
        {
            //Edge-edge contacts conceptually can only create one contact in perfectly rigid collisions.
            //However, this is a discrete approximation of rigidity; things can penetrate each other.
            //If edge-edge only returns a single contact, there's a good chance that the box will get into
            //an oscillating state when under pressure.

            //To avoid the oscillation, we may sometimes need two edge contacts.
            //To determine which edges to use, compute 8 dot products.
            //One for each edge parallel to the contributing axis on each of the two shapes.
            //The resulting cases are:
            //One edge on A touching one edge on B.
            //Two edges on A touching one edge on B.
            //One edge on A touching two edges on B.
            //Two edges on A touching two edges on B.

            //The three latter cases SHOULD be covered by the face-contact system, but in practice,
            //they are not sufficiently covered because the system decides that the single edge-edge pair
            //should be used and drops the other contacts, producting the aforementioned oscillation.

            //All edge cross products result in the MTD, so no recalculation is necessary.

            //Of the four edges which are aligned with the local edge axis, pick the two
            //who have vertices which, when dotted with the local mtd, are greatest.

            //Compute the closest points between each edge pair.  For two edges each,
            //this comes out to four total closest point tests.
            //This is not a traditional closest point between segments test.
            //Completely ignore the pair if the closest points turn out to be beyond the intervals of the segments.

            //Use the offsets found from each test.
            //Test the A to B offset against the MTD, which is also known to be oriented in a certain way.
            //That known directionality allows easy computation of depth using MTD dot offset.
            //Do not use any contacts which have negative depth/positive distance.


            //Put the minimum translation direction into the local space of each object.
            TSVector mtdA, mtdB;
            TSVector negatedMtd;
            TSVector.Negate(ref mtd, out negatedMtd);
            TSVector.TransposedTransform(ref negatedMtd, ref orientationA, out mtdA);
            TSVector.TransposedTransform(ref mtd, ref orientationB, out mtdB);

#if !WINDOWS
            TSVector edgeAStart1 = TSVector.zero, edgeAEnd1 = TSVector.zero, edgeAStart2 = TSVector.zero, edgeAEnd2 = TSVector.zero;
            TSVector edgeBStart1 = TSVector.zero, edgeBEnd1 = TSVector.zero, edgeBStart2 = TSVector.zero, edgeBEnd2 = TSVector.zero;
#else
            TSVector edgeAStart1, edgeAEnd1, edgeAStart2, edgeAEnd2;
            TSVector edgeBStart1, edgeBEnd1, edgeBStart2, edgeBEnd2;
#endif

            TSVector halfSizeA = a.halfSize;
            FP aHalfWidth  = halfSizeA.z;
            FP aHalfHeight = halfSizeA.y;
            FP aHalfLength = halfSizeA.x;

            TSVector halfSizeB = b.halfSize;
            FP bHalfWidth  = halfSizeB.z;
            FP bHalfHeight = halfSizeB.y;
            FP bHalfLength = halfSizeB.x;

            //Letter stands for owner.  Number stands for edge (1 or 2).
            int edgeAStart1Id, edgeAEnd1Id, edgeAStart2Id, edgeAEnd2Id;
            int edgeBStart1Id, edgeBEnd1Id, edgeBStart2Id, edgeBEnd2Id;

            //This is an edge-edge collision, so one (AND ONLY ONE) of the components in the 
            //local direction must be very close to zero.  We can use an arbitrary fixed 
            //epsilon because the mtd is always unit length.

            #region Edge A

            if (FP.Abs(mtdA.x) < TSMath.Epsilon)
            {
                //mtd is in the Y-Z plane.
                //Perform an implicit dot with the edge location relative to the center.
                //Find the two edges furthest in the direction of the mtdA.
                var dots = new List<FP>
                {
                    -aHalfHeight * mtdA.y - aHalfLength * mtdA.z,
                    -aHalfHeight * mtdA.y + aHalfLength * mtdA.z,
                    aHalfHeight * mtdA.y - aHalfLength * mtdA.z,
                    aHalfHeight * mtdA.y + aHalfLength * mtdA.z
                };

                //Find the first and second highest indices.
                int highestIndex, secondHighestIndex;
                FindHighestIndices(ref dots, out highestIndex, out secondHighestIndex);
                //Use the indices to compute the edges.
                GetEdgeData(highestIndex, 0, aHalfWidth, aHalfHeight, aHalfLength, out edgeAStart1, out edgeAEnd1, out edgeAStart1Id, out edgeAEnd1Id);
                GetEdgeData(secondHighestIndex, 0, aHalfWidth, aHalfHeight, aHalfLength, out edgeAStart2, out edgeAEnd2, out edgeAStart2Id, out edgeAEnd2Id);


            }
            else if (FP.Abs(mtdA.y) < TSMath.Epsilon)
            {
                //mtd is in the X-Z plane
                //Perform an implicit dot with the edge location relative to the center.
                //Find the two edges furthest in the direction of the mtdA.
                var dots = new List<FP>
                {
                    -aHalfWidth * mtdA.x - aHalfLength * mtdA.z,
                    -aHalfWidth * mtdA.x + aHalfLength * mtdA.z,
                    aHalfWidth * mtdA.x - aHalfLength * mtdA.z,
                    aHalfWidth * mtdA.x + aHalfLength * mtdA.z
                };

                //Find the first and second highest indices.
                int highestIndex, secondHighestIndex;
                FindHighestIndices(ref dots, out highestIndex, out secondHighestIndex);
                //Use the indices to compute the edges.
                GetEdgeData(highestIndex, 1, aHalfWidth, aHalfHeight, aHalfLength, out edgeAStart1, out edgeAEnd1, out edgeAStart1Id, out edgeAEnd1Id);
                GetEdgeData(secondHighestIndex, 1, aHalfWidth, aHalfHeight, aHalfLength, out edgeAStart2, out edgeAEnd2, out edgeAStart2Id, out edgeAEnd2Id);
            }
            else
            {
                //mtd is in the X-Y plane
                //Perform an implicit dot with the edge location relative to the center.
                //Find the two edges furthest in the direction of the mtdA.
                var dots = new List<FP>
                {
                    -aHalfWidth * mtdA.x - aHalfHeight * mtdA.y,
                    -aHalfWidth * mtdA.x + aHalfHeight * mtdA.y,
                    aHalfWidth * mtdA.x - aHalfHeight * mtdA.y,
                    aHalfWidth * mtdA.x + aHalfHeight * mtdA.y
                };

                //Find the first and second highest indices.
                int highestIndex, secondHighestIndex;
                FindHighestIndices(ref dots, out highestIndex, out secondHighestIndex);
                //Use the indices to compute the edges.
                GetEdgeData(highestIndex, 2, aHalfWidth, aHalfHeight, aHalfLength, out edgeAStart1, out edgeAEnd1, out edgeAStart1Id, out edgeAEnd1Id);
                GetEdgeData(secondHighestIndex, 2, aHalfWidth, aHalfHeight, aHalfLength, out edgeAStart2, out edgeAEnd2, out edgeAStart2Id, out edgeAEnd2Id);
            }

            #endregion

            #region Edge B

            if (FP.Abs(mtdB.x) < TSMath.Epsilon)
            {
                //mtd is in the Y-Z plane.
                //Perform an implicit dot with the edge location relative to the center.
                //Find the two edges furthest in the direction of the mtdB.
                var dots = new List<FP>
                {
                    -bHalfHeight * mtdB.y - bHalfLength * mtdB.z,
                    -bHalfHeight * mtdB.y + bHalfLength * mtdB.z,
                    bHalfHeight * mtdB.y - bHalfLength * mtdB.z,
                    bHalfHeight * mtdB.y + bHalfLength * mtdB.z
                };

                //Find the first and second highest indices.
                int highestIndex, secondHighestIndex;
                FindHighestIndices(ref dots, out highestIndex, out secondHighestIndex);
                //Use the indices to compute the edges.
                GetEdgeData(highestIndex, 0, bHalfWidth, bHalfHeight, bHalfLength, out edgeBStart1, out edgeBEnd1, out edgeBStart1Id, out edgeBEnd1Id);
                GetEdgeData(secondHighestIndex, 0, bHalfWidth, bHalfHeight, bHalfLength, out edgeBStart2, out edgeBEnd2, out edgeBStart2Id, out edgeBEnd2Id);


            }
            else if (FP.Abs(mtdB.y) < TSMath.Epsilon)
            {
                //mtd is in the X-Z plane
                //Perform an implicit dot with the edge location relative to the center.
                //Find the two edges furthest in the direction of the mtdB.
                var dots = new List<FP>
                {
                    -bHalfWidth * mtdB.x - bHalfLength * mtdB.z,
                    -bHalfWidth * mtdB.x + bHalfLength * mtdB.z,
                    bHalfWidth * mtdB.x - bHalfLength * mtdB.z,
                    bHalfWidth * mtdB.x + bHalfLength * mtdB.z
                };

                //Find the first and second highest indices.
                int highestIndex, secondHighestIndex;
                FindHighestIndices(ref dots, out highestIndex, out secondHighestIndex);
                //Use the indices to compute the edges.
                GetEdgeData(highestIndex, 1, bHalfWidth, bHalfHeight, bHalfLength, out edgeBStart1, out edgeBEnd1, out edgeBStart1Id, out edgeBEnd1Id);
                GetEdgeData(secondHighestIndex, 1, bHalfWidth, bHalfHeight, bHalfLength, out edgeBStart2, out edgeBEnd2, out edgeBStart2Id, out edgeBEnd2Id);
            }
            else
            {
                //mtd is in the X-Y plane
                //Perform an implicit dot with the edge location relative to the center.
                //Find the two edges furthest in the direction of the mtdB.
                var dots = new List<FP>
                {
                    -bHalfWidth * mtdB.x - bHalfHeight * mtdB.y,
                    -bHalfWidth * mtdB.x + bHalfHeight * mtdB.y,
                    bHalfWidth * mtdB.x - bHalfHeight * mtdB.y,
                    bHalfWidth * mtdB.x + bHalfHeight * mtdB.y
                };

                //Find the first and second highest indices.
                int highestIndex, secondHighestIndex;
                FindHighestIndices(ref dots, out highestIndex, out secondHighestIndex);
                //Use the indices to compute the edges.
                GetEdgeData(highestIndex, 2, bHalfWidth, bHalfHeight, bHalfLength, out edgeBStart1, out edgeBEnd1, out edgeBStart1Id, out edgeBEnd1Id);
                GetEdgeData(secondHighestIndex, 2, bHalfWidth, bHalfHeight, bHalfLength, out edgeBStart2, out edgeBEnd2, out edgeBStart2Id, out edgeBEnd2Id);
            }

            #endregion


            TSVector.Transform(ref edgeAStart1, ref orientationA, out edgeAStart1);
            TSVector.Transform(ref edgeAEnd1, ref orientationA, out edgeAEnd1);
            TSVector.Transform(ref edgeBStart1, ref orientationB, out edgeBStart1);
            TSVector.Transform(ref edgeBEnd1, ref orientationB, out edgeBEnd1);

            TSVector.Transform(ref edgeAStart2, ref orientationA, out edgeAStart2);
            TSVector.Transform(ref edgeAEnd2, ref orientationA, out edgeAEnd2);
            TSVector.Transform(ref edgeBStart2, ref orientationB, out edgeBStart2);
            TSVector.Transform(ref edgeBEnd2, ref orientationB, out edgeBEnd2);

            TSVector.Add(ref edgeAStart1, ref positionA, out edgeAStart1);
            TSVector.Add(ref edgeAEnd1, ref positionA, out edgeAEnd1);
            TSVector.Add(ref edgeBStart1, ref positionB, out edgeBStart1);
            TSVector.Add(ref edgeBEnd1, ref positionB, out edgeBEnd1);

            TSVector.Add(ref edgeAStart2, ref positionA, out edgeAStart2);
            TSVector.Add(ref edgeAEnd2, ref positionA, out edgeAEnd2);
            TSVector.Add(ref edgeBStart2, ref positionB, out edgeBStart2);
            TSVector.Add(ref edgeBEnd2, ref positionB, out edgeBEnd2);

            TSVector onA, onB;
            TSVector offset;
            FP dot;
#if ALLOWUNSAFE
            var tempContactData = new BoxContactDataCache();
            unsafe
            {
                var contactDataPointer = &tempContactData.D1;
#else
            contactData = new TinyStructList<BoxContactData>();
#endif

            //Go through the pairs and add any contacts with positive depth that are within the segments' intervals.

            if (GetClosestPointsBetweenSegments(ref edgeAStart1, ref edgeAEnd1, ref edgeBStart1, ref edgeBEnd1, out onA, out onB))
            {
                TSVector.Subtract(ref onA, ref onB, out offset);
                TSVector.Dot(ref offset, ref mtd, out dot);
                if (dot < FP.Zero) //Distance must be negative.
                {
                    BoxContactData data;
                    data.Position = onA;
                    data.Depth = dot;
                    data.Id = GetContactId(edgeAStart1Id, edgeAEnd1Id, edgeBStart1Id, edgeBEnd1Id);
#if ALLOWUNSAFE
                        contactDataPointer[tempContactData.Count] = data;
                        tempContactData.Count++;
#else
                    contactData.Add(ref data);
#endif
                }

            }
            if (GetClosestPointsBetweenSegments(ref edgeAStart1, ref edgeAEnd1, ref edgeBStart2, ref edgeBEnd2, out onA, out onB))
            {
                TSVector.Subtract(ref onA, ref onB, out offset);
                TSVector.Dot(ref offset, ref mtd, out dot);
                if (dot < FP.Zero) //Distance must be negative.
                {
                    BoxContactData data;
                    data.Position = onA;
                    data.Depth = dot;
                    data.Id = GetContactId(edgeAStart1Id, edgeAEnd1Id, edgeBStart2Id, edgeBEnd2Id);
#if ALLOWUNSAFE
                        contactDataPointer[tempContactData.Count] = data;
                        tempContactData.Count++;
#else
                    contactData.Add(ref data);
#endif
                }

            }
            if (GetClosestPointsBetweenSegments(ref edgeAStart2, ref edgeAEnd2, ref edgeBStart1, ref edgeBEnd1, out onA, out onB))
            {
                TSVector.Subtract(ref onA, ref onB, out offset);
                TSVector.Dot(ref offset, ref mtd, out dot);
                if (dot < FP.Zero) //Distance must be negative.
                {
                    BoxContactData data;
                    data.Position = onA;
                    data.Depth = dot;
                    data.Id = GetContactId(edgeAStart2Id, edgeAEnd2Id, edgeBStart1Id, edgeBEnd1Id);
#if ALLOWUNSAFE
                        contactDataPointer[tempContactData.Count] = data;
                        tempContactData.Count++;
#else
                    contactData.Add(ref data);
#endif
                }

            }
            if (GetClosestPointsBetweenSegments(ref edgeAStart2, ref edgeAEnd2, ref edgeBStart2, ref edgeBEnd2, out onA, out onB))
            {
                TSVector.Subtract(ref onA, ref onB, out offset);
                TSVector.Dot(ref offset, ref mtd, out dot);
                if (dot < FP.Zero) //Distance must be negative.
                {
                    BoxContactData data;
                    data.Position = onA;
                    data.Depth = dot;
                    data.Id = GetContactId(edgeAStart2Id, edgeAEnd2Id, edgeBStart2Id, edgeBEnd2Id);
#if ALLOWUNSAFE
                        contactDataPointer[tempContactData.Count] = data;
                        tempContactData.Count++;
#else
                    contactData.Add(ref data);
#endif
                }

            }
#if ALLOWUNSAFE
            }
            contactData = tempContactData;
#endif

        }

        private static void GetEdgeData(int index, int axis, FP x, FP y, FP z, out TSVector edgeStart, out TSVector edgeEnd, out int edgeStartId, out int edgeEndId)
        {
            //Index defines which edge to use.
            //They follow this pattern:
            //0: --
            //1: -+
            //2: +-
            //3: ++

            //The axis index determines the dimensions to use.
            //0: plane with normal X
            //1: plane with normal Y
            //2: plane with normal Z

#if !WINDOWS
            edgeStart = TSVector.zero;
            edgeEnd = TSVector.zero;
#endif

            switch (index + axis * 4)
            {
                case 0:
                    //X--
                    edgeStart.x = -x;
                    edgeStart.y = -y;
                    edgeStart.z = -z;
                    edgeStartId = 0; //000

                    edgeEnd.x = x;
                    edgeEnd.y = -y;
                    edgeEnd.z = -z;
                    edgeEndId = 4; //100
                    break;
                case 1:
                    //X-+
                    edgeStart.x = -x;
                    edgeStart.y = -y;
                    edgeStart.z = z;
                    edgeStartId = 1; //001

                    edgeEnd.x = x;
                    edgeEnd.y = -y;
                    edgeEnd.z = z;
                    edgeEndId = 5; //101
                    break;
                case 2:
                    //X+-
                    edgeStart.x = -x;
                    edgeStart.y = y;
                    edgeStart.z = -z;
                    edgeStartId = 2; //010

                    edgeEnd.x = x;
                    edgeEnd.y = y;
                    edgeEnd.z = -z;
                    edgeEndId = 6; //110
                    break;
                case 3:
                    //X++
                    edgeStart.x = -x;
                    edgeStart.y = y;
                    edgeStart.z = z;
                    edgeStartId = 3; //011

                    edgeEnd.x = x;
                    edgeEnd.y = y;
                    edgeEnd.z = z;
                    edgeEndId = 7; //111
                    break;
                case 4:
                    //-Y-
                    edgeStart.x = -x;
                    edgeStart.y = -y;
                    edgeStart.z = -z;
                    edgeStartId = 0; //000

                    edgeEnd.x = -x;
                    edgeEnd.y = y;
                    edgeEnd.z = -z;
                    edgeEndId = 2; //010
                    break;
                case 5:
                    //-Y+
                    edgeStart.x = -x;
                    edgeStart.y = -y;
                    edgeStart.z = z;
                    edgeStartId = 1; //001

                    edgeEnd.x = -x;
                    edgeEnd.y = y;
                    edgeEnd.z = z;
                    edgeEndId = 3; //011
                    break;
                case 6:
                    //+Y-
                    edgeStart.x = x;
                    edgeStart.y = -y;
                    edgeStart.z = -z;
                    edgeStartId = 4; //100

                    edgeEnd.x = x;
                    edgeEnd.y = y;
                    edgeEnd.z = -z;
                    edgeEndId = 6; //110
                    break;
                case 7:
                    //+Y+
                    edgeStart.x = x;
                    edgeStart.y = -y;
                    edgeStart.z = z;
                    edgeStartId = 5; //101

                    edgeEnd.x = x;
                    edgeEnd.y = y;
                    edgeEnd.z = z;
                    edgeEndId = 7; //111
                    break;
                case 8:
                    //--Z
                    edgeStart.x = -x;
                    edgeStart.y = -y;
                    edgeStart.z = -z;
                    edgeStartId = 0; //000

                    edgeEnd.x = -x;
                    edgeEnd.y = -y;
                    edgeEnd.z = z;
                    edgeEndId = 1; //001
                    break;
                case 9:
                    //-+Z
                    edgeStart.x = -x;
                    edgeStart.y = y;
                    edgeStart.z = -z;
                    edgeStartId = 2; //010

                    edgeEnd.x = -x;
                    edgeEnd.y = y;
                    edgeEnd.z = z;
                    edgeEndId = 3; //011
                    break;
                case 10:
                    //+-Z
                    edgeStart.x = x;
                    edgeStart.y = -y;
                    edgeStart.z = -z;
                    edgeStartId = 4; //100

                    edgeEnd.x = x;
                    edgeEnd.y = -y;
                    edgeEnd.z = z;
                    edgeEndId = 5; //101
                    break;
                case 11:
                    //++Z
                    edgeStart.x = x;
                    edgeStart.y = y;
                    edgeStart.z = -z;
                    edgeStartId = 6; //110

                    edgeEnd.x = x;
                    edgeEnd.y = y;
                    edgeEnd.z = z;
                    edgeEndId = 7; //111
                    break;
                default:
                    throw new ArgumentException("Invalid index or axis.");
            }
        }

        static void FindHighestIndices(ref List<FP> dots, out int highestIndex, out int secondHighestIndex)
        {
            highestIndex = 0;
            FP highestValue = dots[0];
            for (int i = 1; i < 4; i++)
            {
                FP dot = dots[i];
                if (dot > highestValue)
                {
                    highestIndex = i;
                    highestValue = dot;
                }
            }
            secondHighestIndex = 0;
            FP secondHighestValue = -FP.MaxValue;
            for (int i = 0; i < 4; i++)
            {
                FP dot = dots[i];
                if (i != highestIndex && dot > secondHighestValue)
                {
                    secondHighestIndex = i;
                    secondHighestValue = dot;
                }
            }
        }

        /// <summary>
        /// Computes closest points c1 and c2 betwen segments p1q1 and p2q2.
        /// </summary>
        /// <param name="p1">First point of first segment.</param>
        /// <param name="q1">Second point of first segment.</param>
        /// <param name="p2">First point of second segment.</param>
        /// <param name="q2">Second point of second segment.</param>
        /// <param name="c1">Closest point on first segment.</param>
        /// <param name="c2">Closest point on second segment.</param>
        static bool GetClosestPointsBetweenSegments(ref TSVector p1, ref TSVector q1, ref TSVector p2, ref TSVector q2,
                                                           out TSVector c1, out TSVector c2)
        {
            //Segment direction vectors
            TSVector d1;
            TSVector.Subtract(ref q1, ref p1, out d1);
            TSVector d2;
            TSVector.Subtract(ref q2, ref p2, out d2);
            TSVector r;
            TSVector.Subtract(ref p1, ref p2, out r);
            //distance
            FP a = d1.sqrMagnitude;
            FP e = d2.sqrMagnitude;
            FP f;
            TSVector.Dot(ref d2, ref r, out f);

            FP s, t;

            if (a <= TSMath.Epsilon && e <= TSMath.Epsilon)
            {
                //These segments are more like points.
                c1 = p1;
                c2 = p2;
                return false;
            }
            if (a <= TSMath.Epsilon)
            {
                // First segment is basically a point.
                s = FP.Zero;
                t = f / e;
                if (t < FP.Zero || t > FP.One)
                {
                    c1 = TSVector.zero;
                    c2 = TSVector.zero;
                    return false;
                }
            }
            else
            {
                FP c = TSVector.Dot(d1, r);
                if (e <= TSMath.Epsilon)
                {
                    // Second segment is basically a point.
                    t = FP.Zero;
                    s = TSMath.Clamp(-c / a, FP.Zero, FP.One);
                }
                else
                {
                    FP b = TSVector.Dot(d1, d2);
                    FP denom = a * e - b * b;

                    // If segments not parallel, compute closest point on L1 to L2, and
                    // clamp to segment S1. Else pick some s (here FP.Half)
                    if (denom != FP.Zero)
                    {
                        s = (b * f - c * e) / denom;
                        if (s < FP.Zero || s > FP.One)
                        {
                            //Closest point would be outside of the segment.
                            c1 = TSVector.zero;
                            c2 = TSVector.zero;
                            return false;
                        }
                    }
                    else //Parallel, just use FP.Half
                        s = FP.Half;


                    t = (b * s + f) / e;

                    if (t < FP.Zero || t > FP.One)
                    {
                        //Closest point would be outside of the segment.
                        c1 = TSVector.zero;
                        c2 = TSVector.zero;
                        return false;
                    }
                }
            }

            TSVector.Multiply(ref d1, s, out c1);
            TSVector.Add(ref c1, ref p1, out c1);
            TSVector.Multiply(ref d2, t, out c2);
            TSVector.Add(ref c2, ref p2, out c2);
            return true;
        }

        //        internal static void GetEdgeEdgeContact(ref BoxShape a, ref BoxShape b, ref TSVector positionA, ref TSMatrix orientationA, ref TSVector positionB, ref TSMatrix orientationB, FP depth, ref TSVector mtd, out TinyStructList<BoxContactData> contactData)
        //        {
        //            //Put the minimum translation direction into the local space of each object.
        //            TSVector mtdA, mtdB;
        //            TSVector negatedMtd;
        //            TSVector.Negate(ref mtd, out negatedMtd);
        //            TSMatrix.TransformTranspose(ref negatedMtd, ref orientationA, out mtdA);
        //            TSMatrix.TransformTranspose(ref mtd, ref orientationB, out mtdB);


        //#if !WINDOWS
        //            TSVector edgeA1 = TSVector.zero, edgeA2 = TSVector.zero;
        //            TSVector edgeB1 = TSVector.zero, edgeB2 = TSVector.zero;
        //#else
        //            TSVector edgeA1, edgeA2;
        //            TSVector edgeB1, edgeB2;
        //#endif
        //            FP aHalfWidth = a.halfWidth;
        //            FP aHalfHeight = a.halfHeight;
        //            FP aHalfLength = a.halfLength;

        //            FP bHalfWidth = b.halfWidth;
        //            FP bHalfHeight = b.halfHeight;
        //            FP bHalfLength = b.halfLength;

        //            int edgeA1Id, edgeA2Id;
        //            int edgeB1Id, edgeB2Id;

        //            //This is an edge-edge collision, so one (AND ONLY ONE) of the components in the 
        //            //local direction must be very close to zero.  We can use an arbitrary fixed 
        //            //epsilon because the mtd is always unit length.

        //            #region Edge A

        //            if (FP.Abs(mtdA.x) < TSMath.Epsilon)
        //            {
        //                //mtd is in the Y-Z plane.
        //                if (mtdA.y > 0)
        //                {
        //                    if (mtdA.z > 0)
        //                    {
        //                        //++
        //                        edgeA1.x = -aHalfWidth;
        //                        edgeA1.y = aHalfHeight;
        //                        edgeA1.z = aHalfLength;

        //                        edgeA2.x = aHalfWidth;
        //                        edgeA2.y = aHalfHeight;
        //                        edgeA2.z = aHalfLength;

        //                        edgeA1Id = 6;
        //                        edgeA2Id = 7;
        //                    }
        //                    else
        //                    {
        //                        //+-
        //                        edgeA1.x = -aHalfWidth;
        //                        edgeA1.y = aHalfHeight;
        //                        edgeA1.z = -aHalfLength;

        //                        edgeA2.x = aHalfWidth;
        //                        edgeA2.y = aHalfHeight;
        //                        edgeA2.z = -aHalfLength;

        //                        edgeA1Id = 2;
        //                        edgeA2Id = 3;
        //                    }
        //                }
        //                else
        //                {
        //                    if (mtdA.z > 0)
        //                    {
        //                        //-+
        //                        edgeA1.x = -aHalfWidth;
        //                        edgeA1.y = -aHalfHeight;
        //                        edgeA1.z = aHalfLength;

        //                        edgeA2.x = aHalfWidth;
        //                        edgeA2.y = -aHalfHeight;
        //                        edgeA2.z = aHalfLength;

        //                        edgeA1Id = 4;
        //                        edgeA2Id = 5;
        //                    }
        //                    else
        //                    {
        //                        //--
        //                        edgeA1.x = -aHalfWidth;
        //                        edgeA1.y = -aHalfHeight;
        //                        edgeA1.z = -aHalfLength;

        //                        edgeA2.x = aHalfWidth;
        //                        edgeA2.y = -aHalfHeight;
        //                        edgeA2.z = -aHalfLength;

        //                        edgeA1Id = 0;
        //                        edgeA2Id = 1;
        //                    }
        //                }
        //            }
        //            else if (FP.Abs(mtdA.y) < TSMath.Epsilon)
        //            {
        //                //mtd is in the X-Z plane
        //                if (mtdA.x > 0)
        //                {
        //                    if (mtdA.z > 0)
        //                    {
        //                        //++
        //                        edgeA1.x = aHalfWidth;
        //                        edgeA1.y = -aHalfHeight;
        //                        edgeA1.z = aHalfLength;

        //                        edgeA2.x = aHalfWidth;
        //                        edgeA2.y = aHalfHeight;
        //                        edgeA2.z = aHalfLength;

        //                        edgeA1Id = 5;
        //                        edgeA2Id = 7;
        //                    }
        //                    else
        //                    {
        //                        //+-
        //                        edgeA1.x = aHalfWidth;
        //                        edgeA1.y = -aHalfHeight;
        //                        edgeA1.z = -aHalfLength;

        //                        edgeA2.x = aHalfWidth;
        //                        edgeA2.y = aHalfHeight;
        //                        edgeA2.z = -aHalfLength;

        //                        edgeA1Id = 1;
        //                        edgeA2Id = 3;
        //                    }
        //                }
        //                else
        //                {
        //                    if (mtdA.z > 0)
        //                    {
        //                        //-+
        //                        edgeA1.x = -aHalfWidth;
        //                        edgeA1.y = -aHalfHeight;
        //                        edgeA1.z = aHalfLength;

        //                        edgeA2.x = -aHalfWidth;
        //                        edgeA2.y = aHalfHeight;
        //                        edgeA2.z = aHalfLength;

        //                        edgeA1Id = 4;
        //                        edgeA2Id = 6;
        //                    }
        //                    else
        //                    {
        //                        //--
        //                        edgeA1.x = -aHalfWidth;
        //                        edgeA1.y = -aHalfHeight;
        //                        edgeA1.z = -aHalfLength;

        //                        edgeA2.x = -aHalfWidth;
        //                        edgeA2.y = aHalfHeight;
        //                        edgeA2.z = -aHalfLength;

        //                        edgeA1Id = 0;
        //                        edgeA2Id = 2;
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                //mtd is in the X-Y plane
        //                if (mtdA.x > 0)
        //                {
        //                    if (mtdA.y > 0)
        //                    {
        //                        //++
        //                        edgeA1.x = aHalfWidth;
        //                        edgeA1.y = aHalfHeight;
        //                        edgeA1.z = -aHalfLength;

        //                        edgeA2.x = aHalfWidth;
        //                        edgeA2.y = aHalfHeight;
        //                        edgeA2.z = aHalfLength;

        //                        edgeA1Id = 3;
        //                        edgeA2Id = 7;
        //                    }
        //                    else
        //                    {
        //                        //+-
        //                        edgeA1.x = aHalfWidth;
        //                        edgeA1.y = -aHalfHeight;
        //                        edgeA1.z = -aHalfLength;

        //                        edgeA2.x = aHalfWidth;
        //                        edgeA2.y = -aHalfHeight;
        //                        edgeA2.z = aHalfLength;

        //                        edgeA1Id = 1;
        //                        edgeA2Id = 5;
        //                    }
        //                }
        //                else
        //                {
        //                    if (mtdA.y > 0)
        //                    {
        //                        //-+
        //                        edgeA1.x = -aHalfWidth;
        //                        edgeA1.y = aHalfHeight;
        //                        edgeA1.z = -aHalfLength;

        //                        edgeA2.x = -aHalfWidth;
        //                        edgeA2.y = aHalfHeight;
        //                        edgeA2.z = aHalfLength;

        //                        edgeA1Id = 2;
        //                        edgeA2Id = 6;
        //                    }
        //                    else
        //                    {
        //                        //--
        //                        edgeA1.x = -aHalfWidth;
        //                        edgeA1.y = -aHalfHeight;
        //                        edgeA1.z = -aHalfLength;

        //                        edgeA2.x = -aHalfWidth;
        //                        edgeA2.y = -aHalfHeight;
        //                        edgeA2.z = aHalfLength;

        //                        edgeA1Id = 0;
        //                        edgeA2Id = 4;
        //                    }
        //                }
        //            }

        //            #endregion

        //            #region Edge B

        //            if (FP.Abs(mtdB.x) < TSMath.Epsilon)
        //            {
        //                //mtd is in the Y-Z plane.
        //                if (mtdB.y > 0)
        //                {
        //                    if (mtdB.z > 0)
        //                    {
        //                        //++
        //                        edgeB1.x = -bHalfWidth;
        //                        edgeB1.y = bHalfHeight;
        //                        edgeB1.z = bHalfLength;

        //                        edgeB2.x = bHalfWidth;
        //                        edgeB2.y = bHalfHeight;
        //                        edgeB2.z = bHalfLength;

        //                        edgeB1Id = 6;
        //                        edgeB2Id = 7;
        //                    }
        //                    else
        //                    {
        //                        //+-
        //                        edgeB1.x = -bHalfWidth;
        //                        edgeB1.y = bHalfHeight;
        //                        edgeB1.z = -bHalfLength;

        //                        edgeB2.x = bHalfWidth;
        //                        edgeB2.y = bHalfHeight;
        //                        edgeB2.z = -bHalfLength;

        //                        edgeB1Id = 2;
        //                        edgeB2Id = 3;
        //                    }
        //                }
        //                else
        //                {
        //                    if (mtdB.z > 0)
        //                    {
        //                        //-+
        //                        edgeB1.x = -bHalfWidth;
        //                        edgeB1.y = -bHalfHeight;
        //                        edgeB1.z = bHalfLength;

        //                        edgeB2.x = bHalfWidth;
        //                        edgeB2.y = -bHalfHeight;
        //                        edgeB2.z = bHalfLength;

        //                        edgeB1Id = 4;
        //                        edgeB2Id = 5;
        //                    }
        //                    else
        //                    {
        //                        //--
        //                        edgeB1.x = -bHalfWidth;
        //                        edgeB1.y = -bHalfHeight;
        //                        edgeB1.z = -bHalfLength;

        //                        edgeB2.x = bHalfWidth;
        //                        edgeB2.y = -bHalfHeight;
        //                        edgeB2.z = -bHalfLength;

        //                        edgeB1Id = 0;
        //                        edgeB2Id = 1;
        //                    }
        //                }
        //            }
        //            else if (FP.Abs(mtdB.y) < TSMath.Epsilon)
        //            {
        //                //mtd is in the X-Z plane
        //                if (mtdB.x > 0)
        //                {
        //                    if (mtdB.z > 0)
        //                    {
        //                        //++
        //                        edgeB1.x = bHalfWidth;
        //                        edgeB1.y = -bHalfHeight;
        //                        edgeB1.z = bHalfLength;

        //                        edgeB2.x = bHalfWidth;
        //                        edgeB2.y = bHalfHeight;
        //                        edgeB2.z = bHalfLength;

        //                        edgeB1Id = 5;
        //                        edgeB2Id = 7;
        //                    }
        //                    else
        //                    {
        //                        //+-
        //                        edgeB1.x = bHalfWidth;
        //                        edgeB1.y = -bHalfHeight;
        //                        edgeB1.z = -bHalfLength;

        //                        edgeB2.x = bHalfWidth;
        //                        edgeB2.y = bHalfHeight;
        //                        edgeB2.z = -bHalfLength;

        //                        edgeB1Id = 1;
        //                        edgeB2Id = 3;
        //                    }
        //                }
        //                else
        //                {
        //                    if (mtdB.z > 0)
        //                    {
        //                        //-+
        //                        edgeB1.x = -bHalfWidth;
        //                        edgeB1.y = -bHalfHeight;
        //                        edgeB1.z = bHalfLength;

        //                        edgeB2.x = -bHalfWidth;
        //                        edgeB2.y = bHalfHeight;
        //                        edgeB2.z = bHalfLength;

        //                        edgeB1Id = 4;
        //                        edgeB2Id = 6;
        //                    }
        //                    else
        //                    {
        //                        //--
        //                        edgeB1.x = -bHalfWidth;
        //                        edgeB1.y = -bHalfHeight;
        //                        edgeB1.z = -bHalfLength;

        //                        edgeB2.x = -bHalfWidth;
        //                        edgeB2.y = bHalfHeight;
        //                        edgeB2.z = -bHalfLength;

        //                        edgeB1Id = 0;
        //                        edgeB2Id = 2;
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                //mtd is in the X-Y plane
        //                if (mtdB.x > 0)
        //                {
        //                    if (mtdB.y > 0)
        //                    {
        //                        //++
        //                        edgeB1.x = bHalfWidth;
        //                        edgeB1.y = bHalfHeight;
        //                        edgeB1.z = -bHalfLength;

        //                        edgeB2.x = bHalfWidth;
        //                        edgeB2.y = bHalfHeight;
        //                        edgeB2.z = bHalfLength;

        //                        edgeB1Id = 3;
        //                        edgeB2Id = 7;
        //                    }
        //                    else
        //                    {
        //                        //+-
        //                        edgeB1.x = bHalfWidth;
        //                        edgeB1.y = -bHalfHeight;
        //                        edgeB1.z = -bHalfLength;

        //                        edgeB2.x = bHalfWidth;
        //                        edgeB2.y = -bHalfHeight;
        //                        edgeB2.z = bHalfLength;

        //                        edgeB1Id = 1;
        //                        edgeB2Id = 5;
        //                    }
        //                }
        //                else
        //                {
        //                    if (mtdB.y > 0)
        //                    {
        //                        //-+
        //                        edgeB1.x = -bHalfWidth;
        //                        edgeB1.y = bHalfHeight;
        //                        edgeB1.z = -bHalfLength;

        //                        edgeB2.x = -bHalfWidth;
        //                        edgeB2.y = bHalfHeight;
        //                        edgeB2.z = bHalfLength;

        //                        edgeB1Id = 2;
        //                        edgeB2Id = 6;
        //                    }
        //                    else
        //                    {
        //                        //--
        //                        edgeB1.x = -bHalfWidth;
        //                        edgeB1.y = -bHalfHeight;
        //                        edgeB1.z = -bHalfLength;

        //                        edgeB2.x = -bHalfWidth;
        //                        edgeB2.y = -bHalfHeight;
        //                        edgeB2.z = bHalfLength;

        //                        edgeB1Id = 0;
        //                        edgeB2Id = 4;
        //                    }
        //                }
        //            }

        //            #endregion

        //            //TODO: Since the above uniquely identifies the edge from each box based on two vertices,
        //            //get the edge feature id from vertexA id combined with vertexB id.
        //            //Vertex id's are 3 bit binary 'numbers' because ---, --+, -+-, etc.


        //            TSMatrix.Transform(ref edgeA1, ref orientationA, out edgeA1);
        //            TSMatrix.Transform(ref edgeA2, ref orientationA, out edgeA2);
        //            TSMatrix.Transform(ref edgeB1, ref orientationB, out edgeB1);
        //            TSMatrix.Transform(ref edgeB2, ref orientationB, out edgeB2);
        //            TSVector.Add(ref edgeA1, ref positionA, out edgeA1);
        //            TSVector.Add(ref edgeA2, ref positionA, out edgeA2);
        //            TSVector.Add(ref edgeB1, ref positionB, out edgeB1);
        //            TSVector.Add(ref edgeB2, ref positionB, out edgeB2);

        //            FP s, t;
        //            TSVector onA, onB;
        //            TSMath.GetClosestPointsBetweenSegments(ref edgeA1, ref edgeA2, ref edgeB1, ref edgeB2, out s, out t, out onA, out onB);
        //            //TSVector.Add(ref onA, ref onB, out point);
        //            //TSVector.Multiply(ref point, FP.Half, out point);
        //            point = onA;

        //            //depth = (onB.x - onA.x) * mtd.x + (onB.y - onA.y) * mtd.y + (onB.z - onA.z) * mtd.z;

        //            id = GetContactId(edgeA1Id, edgeA2Id, edgeB1Id, edgeB2Id);
        //        }

#if ALLOWUNSAFE
        internal static void GetFaceContacts(ref BoxShape a, ref BoxShape b, ref TSVector positionA, ref TSMatrix orientationA, ref TSVector positionB, ref TSMatrix orientationB, bool aIsFaceOwner, ref TSVector mtd, out BoxContactDataCache contactData)
#else
        internal static void GetFaceContacts(ref BoxShape a, ref BoxShape b, ref TSVector positionA, ref TSMatrix orientationA, ref TSVector positionB, ref TSMatrix orientationB, bool aIsFaceOwner, ref TSVector mtd, out TinyStructList<BoxContactData> contactData)
#endif
        {

            TSVector halfSizeA = a.halfSize;
            FP aHalfWidth = halfSizeA.z;
            FP aHalfHeight = halfSizeA.y;
            FP aHalfLength = halfSizeA.x;

            TSVector halfSizeB = b.halfSize;
            FP bHalfWidth  = halfSizeB.z;
            FP bHalfHeight = halfSizeB.y;
            FP bHalfLength = halfSizeB.x;

            BoxFace aBoxFace, bBoxFace;

            TSVector negatedMtd;
            TSVector.Negate(ref mtd, out negatedMtd);
            GetNearestFace(ref positionA, ref orientationA, ref negatedMtd, aHalfWidth, aHalfHeight, aHalfLength, out aBoxFace);


            GetNearestFace(ref positionB, ref orientationB, ref mtd, bHalfWidth, bHalfHeight, bHalfLength, out bBoxFace);

            if (aIsFaceOwner)
                ClipFacesDirect(ref aBoxFace, ref bBoxFace, ref negatedMtd, out contactData);
            else
                ClipFacesDirect(ref bBoxFace, ref aBoxFace, ref mtd, out contactData);

            if (contactData.Count > 4)
                PruneContactsMaxDistance(ref mtd, contactData, out contactData);
        }

#if ALLOWUNSAFE
        private static unsafe void PruneContactsMaxDistance(ref TSVector mtd, BoxContactDataCache input, out BoxContactDataCache output)
        {
            BoxContactData* data = &input.D1;
            int count = input.Count;
            //TODO: THE FOLLOWING has a small issue in release mode.
            //Find the deepest point.
            FP deepestDepth = -1;
            int deepestIndex = 0;
            for (int i = 0; i < count; i++)
            {
                if (data[i].Depth > deepestDepth)
                {
                    deepestDepth = data[i].Depth;
                    deepestIndex = i;
                }
            }

            //Identify the furthest point away from the deepest index.
            FP furthestDistance = -1;
            int furthestIndex = 0;
            for (int i = 0; i < count; i++)
            {
                FP distance;
                TSVector.DistanceSquared(ref data[deepestIndex].Position, ref data[i].Position, out distance);
                if (distance > furthestDistance)
                {
                    furthestDistance = distance;
                    furthestIndex = i;
                }

            }

            TSVector xAxis;
            TSVector.Subtract(ref data[furthestIndex].Position, ref data[deepestIndex].Position, out xAxis);

            TSVector yAxis;
            TSVector.Cross(ref mtd, ref xAxis, out yAxis);

            FP minY;
            FP maxY;
            int minYindex = 0;
            int maxYindex = 0;

            TSVector.Dot(ref data[0].Position, ref yAxis, out minY);
            maxY = minY;
            for (int i = 1; i < count; i++)
            {
                FP dot;
                TSVector.Dot(ref yAxis, ref data[i].Position, out dot);
                if (dot < minY)
                {
                    minY = dot;
                    minYindex = i;
                }
                else if (dot > maxY)
                {
                    maxY = dot;
                    maxYindex = i;
                }
            }

            output = new BoxContactDataCache
                         {
                             Count = 4,
                             D1 = data[deepestIndex],
                             D2 = data[furthestIndex],
                             D3 = data[minYindex],
                             D4 = data[maxYindex]
                         };


            //TSVector v;
            //var maximumOffset = TSVector.zero;
            //int maxIndexA = -1, maxIndexB = -1;
            //FP temp;
            //FP maximumDistanceSquared = -FP.MaxValue;
            //for (int i = 0; i < count; i++)
            //{
            //    for (int j = i + 1; j < count; j++)
            //    {
            //        TSVector.Subtract(ref data[j].Position, ref data[i].Position, out v);
            //        temp = v.LengthSquared();
            //        if (temp > maximumDistanceSquared)
            //        {
            //            maximumDistanceSquared = temp;
            //            maxIndexA = i;
            //            maxIndexB = j;
            //            maximumOffset = v;
            //        }
            //    }
            //}

            //TSVector otherDirection;
            //TSVector.Cross(ref mtd, ref maximumOffset, out otherDirection);
            //int minimumIndex = -1, maximumIndex = -1;
            //FP minimumDistance = FP.MaxValue, maximumDistance = -FP.MaxValue;

            //for (int i = 0; i < count; i++)
            //{
            //    if (i != maxIndexA && i != maxIndexB)
            //    {
            //        TSVector.Dot(ref data[i].Position, ref otherDirection, out temp);
            //        if (temp > maximumDistance)
            //        {
            //            maximumDistance = temp;
            //            maximumIndex = i;
            //        }
            //        if (temp < minimumDistance)
            //        {
            //            minimumDistance = temp;
            //            minimumIndex = i;
            //        }
            //    }
            //}

            //output = new BoxContactDataCache();
            //output.Count = 4;
            //output.D1 = data[maxIndexA];
            //output.D2 = data[maxIndexB];
            //output.D3 = data[minimumIndex];
            //output.D4 = data[maximumIndex];
        }
#else
        private static void PruneContactsMaxDistance(ref TSVector mtd, TinyStructList<BoxContactData> input, out TinyStructList<BoxContactData> output)
        {
            int count = input.Count;
            //Find the deepest point.
            BoxContactData data, deepestData;
            input.Get(0, out deepestData);
            for (int i = 1; i < count; i++)
            {
                input.Get(i, out data);
                if (data.Depth > deepestData.Depth)
                {
                    deepestData = data;
                }
            }

            //Identify the furthest point away from the deepest index.
            BoxContactData furthestData;
            input.Get(0, out furthestData);
            FP furthestDistance;
            TSVector.DistanceSquared(ref deepestData.Position, ref furthestData.Position, out furthestDistance);
            for (int i = 1; i < count; i++)
            {
                input.Get(i, out data);
                FP distance;
                TSVector.DistanceSquared(ref deepestData.Position, ref data.Position, out distance);
                if (distance > furthestDistance)
                {
                    furthestDistance = distance;
                    furthestData = data;
                }

            }

            TSVector xAxis;
            TSVector.Subtract(ref furthestData.Position, ref deepestData.Position, out xAxis);

            TSVector yAxis;
            TSVector.Cross(ref mtd, ref xAxis, out yAxis);

            FP minY;
            FP maxY;
            BoxContactData minData, maxData;
            input.Get(0, out minData);
            maxData = minData;

            TSVector.Dot(ref minData.Position, ref yAxis, out minY);
            maxY = minY;
            for (int i = 1; i < count; i++)
            {
                input.Get(i, out data);
                FP dot;
                TSVector.Dot(ref yAxis, ref data.Position, out dot);
                if (dot < minY)
                {
                    minY = dot;
                    minData = data;
                }
                else if (dot > maxY)
                {
                    maxY = dot;
                    maxData = data;
                }
            }

            output = new TinyStructList<BoxContactData>();
            output.Add(ref deepestData);
            output.Add(ref furthestData);
            output.Add(ref minData);
            output.Add(ref maxData);


            //int count = input.Count;
            //TSVector v;
            //var maximumOffset = TSVector.zero;
            //int maxIndexA = -1, maxIndexB = -1;
            //FP temp;
            //FP maximumDistanceSquared = -FP.MaxValue;
            //BoxContactData itemA, itemB;
            //for (int i = 0; i < count; i++)
            //{
            //    for (int j = i + 1; j < count; j++)
            //    {
            //        input.Get(j, out itemB);
            //        input.Get(i, out itemA);
            //        TSVector.Subtract(ref itemB.Position, ref itemA.Position, out v);
            //        temp = v.LengthSquared();
            //        if (temp > maximumDistanceSquared)
            //        {
            //            maximumDistanceSquared = temp;
            //            maxIndexA = i;
            //            maxIndexB = j;
            //            maximumOffset = v;
            //        }
            //    }
            //}

            //TSVector otherDirection;
            //TSVector.Cross(ref mtd, ref maximumOffset, out otherDirection);
            //int minimumIndex = -1, maximumIndex = -1;
            //FP minimumDistance = FP.MaxValue, maximumDistance = -FP.MaxValue;

            //for (int i = 0; i < count; i++)
            //{
            //    if (i != maxIndexA && i != maxIndexB)
            //    {
            //        input.Get(i, out itemA);
            //        TSVector.Dot(ref itemA.Position, ref otherDirection, out temp);
            //        if (temp > maximumDistance)
            //        {
            //            maximumDistance = temp;
            //            maximumIndex = i;
            //        }
            //        if (temp < minimumDistance)
            //        {
            //            minimumDistance = temp;
            //            minimumIndex = i;
            //        }
            //    }
            //}

            //output = new TinyStructList<BoxContactData>();
            //input.Get(maxIndexA, out itemA);
            //output.Add(ref itemA);
            //input.Get(maxIndexB, out itemA);
            //output.Add(ref itemA);
            //input.Get(minimumIndex, out itemA);
            //output.Add(ref itemA);
            //input.Get(maximumIndex, out itemA);
            //output.Add(ref itemA);
        }
#endif

#if ALLOWUNSAFE
        private static unsafe void ClipFacesDirect(ref BoxFace clipFace, ref BoxFace face, ref TSVector mtd, out BoxContactDataCache outputData)
        {
            var contactData = new BoxContactDataCache();
            BoxContactDataCache tempData; //Local version.
            BoxContactData* data = &contactData.D1;
            BoxContactData* temp = &tempData.D1;

            //Local directions on the clip face.  Their length is equal to the length of an edge.
            TSVector clipX, clipY;
            TSVector.Subtract(ref clipFace.V4, ref clipFace.V3, out clipX);
            TSVector.Subtract(ref clipFace.V2, ref clipFace.V3, out clipY);
            FP inverseClipWidth = FP.One / clipFace.Width;
            FP inverseClipHeight = FP.One / clipFace.Height;
            FP inverseClipWidthSquared = inverseClipWidth * inverseClipWidth;
            clipX.x *= inverseClipWidthSquared;
            clipX.y *= inverseClipWidthSquared;
            clipX.z *= inverseClipWidthSquared;
            FP inverseClipHeightSquared = inverseClipHeight * inverseClipHeight;
            clipY.x *= inverseClipHeightSquared;
            clipY.y *= inverseClipHeightSquared;
            clipY.z *= inverseClipHeightSquared;

            //Local directions on the opposing face.  Their length is equal to the length of an edge.
            TSVector faceX, faceY;
            TSVector.Subtract(ref face.V4, ref face.V3, out faceX);
            TSVector.Subtract(ref face.V2, ref face.V3, out faceY);
            FP inverseFaceWidth = FP.One / face.Width;
            FP inverseFaceHeight = FP.One / face.Height;
            FP inverseFaceWidthSquared = inverseFaceWidth * inverseFaceWidth;
            faceX.x *= inverseFaceWidthSquared;
            faceX.y *= inverseFaceWidthSquared;
            faceX.z *= inverseFaceWidthSquared;
            FP inverseFaceHeightSquared = inverseFaceHeight * inverseFaceHeight;
            faceY.x *= inverseFaceHeightSquared;
            faceY.y *= inverseFaceHeightSquared;
            faceY.z *= inverseFaceHeightSquared;

            TSVector clipCenter;
            TSVector.Add(ref clipFace.V1, ref clipFace.V3, out clipCenter);
            //Defer division until after dot product (2 multiplies instead of 3)
            FP clipCenterX, clipCenterY;
            TSVector.Dot(ref clipCenter, ref clipX, out clipCenterX);
            TSVector.Dot(ref clipCenter, ref clipY, out clipCenterY);
            clipCenterX *= FP.Zerop5;
            clipCenterY *= FP.Zerop5;

            TSVector faceCenter;
            TSVector.Add(ref face.V1, ref face.V3, out faceCenter);
            //Defer division until after dot product (2 multiplies instead of 3)
            FP faceCenterX, faceCenterY;
            TSVector.Dot(ref faceCenter, ref faceX, out faceCenterX);
            TSVector.Dot(ref faceCenter, ref faceY, out faceCenterY);
            faceCenterX *= FP.Zerop5;
            faceCenterY *= FP.Zerop5;

            //To test bounds, recall that clipX is the length of the X edge.
            //Going from the center to the max or min goes half of the length of X edge, or +/- 0.5.
            //Bias could be added here.
            //const FP extent = FP.Half; //FP.Half is the default, extra could be added for robustness or speed.
            FP extentX = FP.Zerop5 + FP.Zerop01 * inverseClipWidth;
            FP extentY = FP.Zerop5 + FP.Zerop01 * inverseClipHeight;
            //FP extentX = FP.Half + FP.EN2 * inverseClipXLength;
            //FP extentY = FP.Half + FP.EN2 * inverseClipYLength;
            FP clipCenterMaxX = clipCenterX + extentX;
            FP clipCenterMaxY = clipCenterY + extentY;
            FP clipCenterMinX = clipCenterX - extentX;
            FP clipCenterMinY = clipCenterY - extentY;

            extentX = FP.Zerop5 + FP.Zerop01 * inverseFaceWidth;
            extentY = FP.Zerop5 + FP.Zerop01 * inverseFaceHeight;
            //extentX = FP.Half + FP.EN2 * inverseFaceXLength;
            //extentY = FP.Half + FP.EN2 * inverseFaceYLength;
            FP faceCenterMaxX = faceCenterX + extentX;
            FP faceCenterMaxY = faceCenterY + extentY;
            FP faceCenterMinX = faceCenterX - extentX;
            FP faceCenterMinY = faceCenterY - extentY;

            //Find out where the opposing face is.
            FP dotX, dotY;

            //The four edges can be thought of as minX, maxX, minY and maxY.

            //Face v1
            TSVector.Dot(ref clipX, ref face.V1, out dotX);
            bool v1MaxXInside = dotX < clipCenterMaxX;
            bool v1MinXInside = dotX > clipCenterMinX;
            TSVector.Dot(ref clipY, ref face.V1, out dotY);
            bool v1MaxYInside = dotY < clipCenterMaxY;
            bool v1MinYInside = dotY > clipCenterMinY;

            //Face v2
            TSVector.Dot(ref clipX, ref face.V2, out dotX);
            bool v2MaxXInside = dotX < clipCenterMaxX;
            bool v2MinXInside = dotX > clipCenterMinX;
            TSVector.Dot(ref clipY, ref face.V2, out dotY);
            bool v2MaxYInside = dotY < clipCenterMaxY;
            bool v2MinYInside = dotY > clipCenterMinY;

            //Face v3
            TSVector.Dot(ref clipX, ref face.V3, out dotX);
            bool v3MaxXInside = dotX < clipCenterMaxX;
            bool v3MinXInside = dotX > clipCenterMinX;
            TSVector.Dot(ref clipY, ref face.V3, out dotY);
            bool v3MaxYInside = dotY < clipCenterMaxY;
            bool v3MinYInside = dotY > clipCenterMinY;

            //Face v4
            TSVector.Dot(ref clipX, ref face.V4, out dotX);
            bool v4MaxXInside = dotX < clipCenterMaxX;
            bool v4MinXInside = dotX > clipCenterMinX;
            TSVector.Dot(ref clipY, ref face.V4, out dotY);
            bool v4MaxYInside = dotY < clipCenterMaxY;
            bool v4MinYInside = dotY > clipCenterMinY;

            //Find out where the clip face is.
            //Clip v1
            TSVector.Dot(ref faceX, ref clipFace.V1, out dotX);
            bool clipv1MaxXInside = dotX < faceCenterMaxX;
            bool clipv1MinXInside = dotX > faceCenterMinX;
            TSVector.Dot(ref faceY, ref clipFace.V1, out dotY);
            bool clipv1MaxYInside = dotY < faceCenterMaxY;
            bool clipv1MinYInside = dotY > faceCenterMinY;

            //Clip v2
            TSVector.Dot(ref faceX, ref clipFace.V2, out dotX);
            bool clipv2MaxXInside = dotX < faceCenterMaxX;
            bool clipv2MinXInside = dotX > faceCenterMinX;
            TSVector.Dot(ref faceY, ref clipFace.V2, out dotY);
            bool clipv2MaxYInside = dotY < faceCenterMaxY;
            bool clipv2MinYInside = dotY > faceCenterMinY;

            //Clip v3
            TSVector.Dot(ref faceX, ref clipFace.V3, out dotX);
            bool clipv3MaxXInside = dotX < faceCenterMaxX;
            bool clipv3MinXInside = dotX > faceCenterMinX;
            TSVector.Dot(ref faceY, ref clipFace.V3, out dotY);
            bool clipv3MaxYInside = dotY < faceCenterMaxY;
            bool clipv3MinYInside = dotY > faceCenterMinY;

            //Clip v4
            TSVector.Dot(ref faceX, ref clipFace.V4, out dotX);
            bool clipv4MaxXInside = dotX < faceCenterMaxX;
            bool clipv4MinXInside = dotX > faceCenterMinX;
            TSVector.Dot(ref faceY, ref clipFace.V4, out dotY);
            bool clipv4MaxYInside = dotY < faceCenterMaxY;
            bool clipv4MinYInside = dotY > faceCenterMinY;

        #region Face Vertices

            if (v1MinXInside && v1MaxXInside && v1MinYInside && v1MaxYInside)
            {
                data[contactData.Count].Position = face.V1;
                data[contactData.Count].Id = face.Id1;
                contactData.Count++;
            }

            if (v2MinXInside && v2MaxXInside && v2MinYInside && v2MaxYInside)
            {
                data[contactData.Count].Position = face.V2;
                data[contactData.Count].Id = face.Id2;
                contactData.Count++;
            }

            if (v3MinXInside && v3MaxXInside && v3MinYInside && v3MaxYInside)
            {
                data[contactData.Count].Position = face.V3;
                data[contactData.Count].Id = face.Id3;
                contactData.Count++;
            }

            if (v4MinXInside && v4MaxXInside && v4MinYInside && v4MaxYInside)
            {
                data[contactData.Count].Position = face.V4;
                data[contactData.Count].Id = face.Id4;
                contactData.Count++;
            }

        #endregion

            //Compute depths.
            tempData = contactData;
            contactData.Count = 0;
            FP depth;
            FP clipFaceDot, faceDot;
            TSVector.Dot(ref clipFace.V1, ref mtd, out clipFaceDot);
            for (int i = 0; i < tempData.Count; i++)
            {
                TSVector.Dot(ref temp[i].Position, ref mtd, out faceDot);
                depth = faceDot - clipFaceDot;
                if (depth <= FP.Zero)
                {
                    data[contactData.Count].Position = temp[i].Position;
                    data[contactData.Count].Depth = depth;
                    data[contactData.Count].Id = temp[i].Id;
                    contactData.Count++;
                }
            }

            byte previousCount = contactData.Count;
            if (previousCount >= 4) //Early finish :)
            {
                outputData = contactData;
                return;
            }

        #region Clip face vertices

            TSVector v;
            FP a, b;
            TSVector.Dot(ref face.V1, ref face.Normal, out b);
            //CLIP FACE
            if (clipv1MinXInside && clipv1MaxXInside && clipv1MinYInside && clipv1MaxYInside)
            {
                TSVector.Dot(ref clipFace.V1, ref face.Normal, out a);
                TSVector.Multiply(ref face.Normal, a - b, out v);
                TSVector.Subtract(ref clipFace.V1, ref v, out v);
                data[contactData.Count].Position = v;
                data[contactData.Count].Id = clipFace.Id1 + 8;
                contactData.Count++;
            }

            if (clipv2MinXInside && clipv2MaxXInside && clipv2MinYInside && clipv2MaxYInside)
            {
                TSVector.Dot(ref clipFace.V2, ref face.Normal, out a);
                TSVector.Multiply(ref face.Normal, a - b, out v);
                TSVector.Subtract(ref clipFace.V2, ref v, out v);
                data[contactData.Count].Position = v;
                data[contactData.Count].Id = clipFace.Id2 + 8;
                contactData.Count++;
            }

            if (clipv3MinXInside && clipv3MaxXInside && clipv3MinYInside && clipv3MaxYInside)
            {
                TSVector.Dot(ref clipFace.V3, ref face.Normal, out a);
                TSVector.Multiply(ref face.Normal, a - b, out v);
                TSVector.Subtract(ref clipFace.V3, ref v, out v);
                data[contactData.Count].Position = v;
                data[contactData.Count].Id = clipFace.Id3 + 8;
                contactData.Count++;
            }

            if (clipv4MinXInside && clipv4MaxXInside && clipv4MinYInside && clipv4MaxYInside)
            {
                TSVector.Dot(ref clipFace.V4, ref face.Normal, out a);
                TSVector.Multiply(ref face.Normal, a - b, out v);
                TSVector.Subtract(ref clipFace.V4, ref v, out v);
                data[contactData.Count].Position = v;
                data[contactData.Count].Id = clipFace.Id4 + 8;
                contactData.Count++;
            }

        #endregion

            //Compute depths.
            tempData = contactData;
            contactData.Count = previousCount;

            for (int i = previousCount; i < tempData.Count; i++)
            {
                TSVector.Dot(ref temp[i].Position, ref mtd, out faceDot);
                depth = faceDot - clipFaceDot;
                if (depth <= FP.Zero)
                {
                    data[contactData.Count].Position = temp[i].Position;
                    data[contactData.Count].Depth = depth;
                    data[contactData.Count].Id = temp[i].Id;
                    contactData.Count++;
                }
            }

            previousCount = contactData.Count;
            if (previousCount >= 4) //Early finish :)
            {
                outputData = contactData;
                return;
            }

            //Intersect edges.

            //maxX maxY -> v1
            //minX maxY -> v2
            //minX minY -> v3
            //maxX minY -> v4

            //Once we get here there can only be 3 contacts or less.
            //Once 4 possible contacts have been added, switch to using safe increments.
            //FP dot;

        #region CLIP EDGE: v1 v2

            FaceEdge clipEdge;
            clipFace.GetEdge(0, out clipEdge);
            if (!v1MaxYInside)
            {
                if (v2MaxYInside)
                {
                    //ComputeIntersection(ref face.V1, ref face.V2, ref clipEdge, out v);
                    //TSVector.Dot(ref clipX, ref v, out dot);
                    //if (dot > clipCenterMinX && dot < clipCenterMaxX)
                    if (ComputeIntersection(ref face.V1, ref face.V2, ref clipEdge, out v))
                    {
                        data[contactData.Count].Position = v;
                        data[contactData.Count].Id = GetContactId(face.Id1, face.Id2, ref clipEdge);
                        contactData.Count++;
                    }
                }
                if (v4MaxYInside)
                {
                    //ComputeIntersection(ref face.V4, ref face.V1, ref clipEdge, out v);
                    //TSVector.Dot(ref clipX, ref v, out dot);
                    //if (dot > clipCenterMinX && dot < clipCenterMaxX)
                    if (ComputeIntersection(ref face.V4, ref face.V1, ref clipEdge, out v))
                    {
                        data[contactData.Count].Position = v;
                        data[contactData.Count].Id = GetContactId(face.Id4, face.Id1, ref clipEdge);
                        contactData.Count++;
                    }
                }
            }
            if (!v2MaxYInside)
            {
                if (v1MaxYInside)
                {
                    //ComputeIntersection(ref face.V1, ref face.V2, ref clipEdge, out v);
                    //TSVector.Dot(ref clipX, ref v, out dot);
                    //if (dot > clipCenterMinX && dot < clipCenterMaxX)
                    if (ComputeIntersection(ref face.V1, ref face.V2, ref clipEdge, out v))
                    {
                        data[contactData.Count].Position = v;
                        data[contactData.Count].Id = GetContactId(face.Id1, face.Id2, ref clipEdge);
                        contactData.Count++;
                    }
                }
                if (v3MaxYInside)
                {
                    //ComputeIntersection(ref face.V2, ref face.V3, ref clipEdge, out v);
                    //TSVector.Dot(ref clipX, ref v, out dot);
                    //if (dot > clipCenterMinX && dot < clipCenterMaxX)
                    if (ComputeIntersection(ref face.V2, ref face.V3, ref clipEdge, out v))
                    {
                        data[contactData.Count].Position = v;
                        data[contactData.Count].Id = GetContactId(face.Id2, face.Id3, ref clipEdge);
                        contactData.Count++;
                    }
                }
            }
            if (!v3MaxYInside)
            {
                if (v2MaxYInside)
                {
                    //ComputeIntersection(ref face.V2, ref face.V3, ref clipEdge, out v);
                    //TSVector.Dot(ref clipX, ref v, out dot);
                    //if (dot > clipCenterMinX && dot < clipCenterMaxX)
                    if (ComputeIntersection(ref face.V2, ref face.V3, ref clipEdge, out v))
                    {
                        data[contactData.Count].Position = v;
                        data[contactData.Count].Id = GetContactId(face.Id2, face.Id3, ref clipEdge);
                        contactData.Count++;
                    }
                }
                if (v4MaxYInside && contactData.Count < 8)
                {
                    //ComputeIntersection(ref face.V3, ref face.V4, ref clipEdge, out v);
                    //TSVector.Dot(ref clipX, ref v, out dot);
                    //if (dot > clipCenterMinX && dot < clipCenterMaxX)
                    if (ComputeIntersection(ref face.V3, ref face.V4, ref clipEdge, out v))
                    {
                        data[contactData.Count].Position = v;
                        data[contactData.Count].Id = GetContactId(face.Id3, face.Id4, ref clipEdge);
                        contactData.Count++;
                    }
                }
            }
            if (!v4MaxYInside)
            {
                if (v1MaxYInside && contactData.Count < 8)
                {
                    //ComputeIntersection(ref face.V4, ref face.V1, ref clipEdge, out v);
                    //TSVector.Dot(ref clipX, ref v, out dot);
                    //if (dot > clipCenterMinX && dot < clipCenterMaxX)
                    if (ComputeIntersection(ref face.V4, ref face.V1, ref clipEdge, out v))
                    {
                        data[contactData.Count].Position = v;
                        data[contactData.Count].Id = GetContactId(face.Id4, face.Id1, ref clipEdge);
                        contactData.Count++;
                    }
                }
                if (v3MaxYInside && contactData.Count < 8)
                {
                    //ComputeIntersection(ref face.V3, ref face.V4, ref clipEdge, out v);
                    //TSVector.Dot(ref clipX, ref v, out dot);
                    //if (dot > clipCenterMinX && dot < clipCenterMaxX)
                    if (ComputeIntersection(ref face.V3, ref face.V4, ref clipEdge, out v))
                    {
                        data[contactData.Count].Position = v;
                        data[contactData.Count].Id = GetContactId(face.Id3, face.Id4, ref clipEdge);
                        contactData.Count++;
                    }
                }
            }

        #endregion

        #region CLIP EDGE: v2 v3

            clipFace.GetEdge(1, out clipEdge);
            if (!v1MinXInside)
            {
                if (v2MinXInside && contactData.Count < 8)
                {
                    //test v1-v2 against minXminY-minXmaxY
                    //ComputeIntersection(ref face.V1, ref face.V2, ref clipEdge, out v);
                    //TSVector.Dot(ref clipY, ref v, out dot);
                    //if (dot > clipCenterMinY && dot < clipCenterMaxY)
                    if (ComputeIntersection(ref face.V1, ref face.V2, ref clipEdge, out v))
                    {
                        data[contactData.Count].Position = v;
                        data[contactData.Count].Id = GetContactId(face.Id1, face.Id2, ref clipEdge);
                        contactData.Count++;
                    }
                }
                if (v4MinXInside && contactData.Count < 8)
                {
                    //test v1-v3 against minXminY-minXmaxY
                    //ComputeIntersection(ref face.V4, ref face.V1, ref clipEdge, out v);
                    //TSVector.Dot(ref clipY, ref v, out dot);
                    //if (dot > clipCenterMinY && dot < clipCenterMaxY)
                    if (ComputeIntersection(ref face.V4, ref face.V1, ref clipEdge, out v))
                    {
                        data[contactData.Count].Position = v;
                        data[contactData.Count].Id = GetContactId(face.Id4, face.Id1, ref clipEdge);
                        contactData.Count++;
                    }
                }
            }
            if (!v2MinXInside)
            {
                if (v1MinXInside && contactData.Count < 8)
                {
                    //test v1-v2 against minXminY-minXmaxY
                    //ComputeIntersection(ref face.V1, ref face.V2, ref clipEdge, out v);
                    //TSVector.Dot(ref clipY, ref v, out dot);
                    //if (dot > clipCenterMinY && dot < clipCenterMaxY)
                    if (ComputeIntersection(ref face.V1, ref face.V2, ref clipEdge, out v))
                    {
                        data[contactData.Count].Position = v;
                        data[contactData.Count].Id = GetContactId(face.Id1, face.Id2, ref clipEdge);
                        contactData.Count++;
                    }
                }
                if (v3MinXInside && contactData.Count < 8)
                {
                    //test v2-v4 against minXminY-minXmaxY
                    //ComputeIntersection(ref face.V2, ref face.V3, ref clipEdge, out v);
                    //TSVector.Dot(ref clipY, ref v, out dot);
                    //if (dot > clipCenterMinY && dot < clipCenterMaxY)
                    if (ComputeIntersection(ref face.V2, ref face.V3, ref clipEdge, out v))
                    {
                        data[contactData.Count].Position = v;
                        data[contactData.Count].Id = GetContactId(face.Id2, face.Id3, ref clipEdge);
                        contactData.Count++;
                    }
                }
            }
            if (!v3MinXInside)
            {
                if (v2MinXInside && contactData.Count < 8)
                {
                    //test v1-v3 against minXminY-minXmaxY
                    //ComputeIntersection(ref face.V2, ref face.V3, ref clipEdge, out v);
                    //TSVector.Dot(ref clipY, ref v, out dot);
                    //if (dot > clipCenterMinY && dot < clipCenterMaxY)
                    if (ComputeIntersection(ref face.V2, ref face.V3, ref clipEdge, out v))
                    {
                        data[contactData.Count].Position = v;
                        data[contactData.Count].Id = GetContactId(face.Id2, face.Id3, ref clipEdge);
                        contactData.Count++;
                    }
                }
                if (v4MinXInside && contactData.Count < 8)
                {
                    //test v3-v4 against minXminY-minXmaxY
                    //ComputeIntersection(ref face.V3, ref face.V4, ref clipEdge, out v);
                    //TSVector.Dot(ref clipY, ref v, out dot);
                    //if (dot > clipCenterMinY && dot < clipCenterMaxY)
                    if (ComputeIntersection(ref face.V3, ref face.V4, ref clipEdge, out v))
                    {
                        data[contactData.Count].Position = v;
                        data[contactData.Count].Id = GetContactId(face.Id3, face.Id4, ref clipEdge);
                        contactData.Count++;
                    }
                }
            }
            if (!v4MinXInside)
            {
                if (v1MinXInside && contactData.Count < 8)
                {
                    //test v2-v4 against minXminY-minXmaxY
                    //ComputeIntersection(ref face.V4, ref face.V1, ref clipEdge, out v);
                    //TSVector.Dot(ref clipY, ref v, out dot);
                    //if (dot > clipCenterMinY && dot < clipCenterMaxY)
                    if (ComputeIntersection(ref face.V4, ref face.V1, ref clipEdge, out v))
                    {
                        data[contactData.Count].Position = v;
                        data[contactData.Count].Id = GetContactId(face.Id4, face.Id1, ref clipEdge);
                        contactData.Count++;
                    }
                }
                if (v3MinXInside && contactData.Count < 8)
                {
                    //test v3-v4 against minXminY-minXmaxY
                    //ComputeIntersection(ref face.V3, ref face.V4, ref clipEdge, out v);
                    //TSVector.Dot(ref clipY, ref v, out dot);
                    //if (dot > clipCenterMinY && dot < clipCenterMaxY)
                    if (ComputeIntersection(ref face.V3, ref face.V4, ref clipEdge, out v))
                    {
                        data[contactData.Count].Position = v;
                        data[contactData.Count].Id = GetContactId(face.Id3, face.Id4, ref clipEdge);
                        contactData.Count++;
                    }
                }
            }

        #endregion

        #region CLIP EDGE: v3 v4

            clipFace.GetEdge(2, out clipEdge);
            if (!v1MinYInside)
            {
                if (v2MinYInside && contactData.Count < 8)
                {
                    //ComputeIntersection(ref face.V1, ref face.V2, ref clipEdge, out v);
                    //TSVector.Dot(ref clipX, ref v, out dot);
                    //if (dot > clipCenterMinX && dot < clipCenterMaxX)
                    if (ComputeIntersection(ref face.V1, ref face.V2, ref clipEdge, out v))
                    {
                        data[contactData.Count].Position = v;
                        data[contactData.Count].Id = GetContactId(face.Id1, face.Id2, ref clipEdge);
                        contactData.Count++;
                    }
                }
                if (v4MinYInside && contactData.Count < 8)
                {
                    //ComputeIntersection(ref face.V4, ref face.V1, ref clipEdge, out v);
                    //TSVector.Dot(ref clipX, ref v, out dot);
                    //if (dot > clipCenterMinX && dot < clipCenterMaxX)
                    if (ComputeIntersection(ref face.V4, ref face.V1, ref clipEdge, out v))
                    {
                        data[contactData.Count].Position = v;
                        data[contactData.Count].Id = GetContactId(face.Id4, face.Id1, ref clipEdge);
                        contactData.Count++;
                    }
                }
            }
            if (!v2MinYInside)
            {
                if (v1MinYInside && contactData.Count < 8)
                {
                    //ComputeIntersection(ref face.V1, ref face.V2, ref clipEdge, out v);
                    //TSVector.Dot(ref clipX, ref v, out dot);
                    //if (dot > clipCenterMinX && dot < clipCenterMaxX)
                    if (ComputeIntersection(ref face.V1, ref face.V2, ref clipEdge, out v))
                    {
                        data[contactData.Count].Position = v;
                        data[contactData.Count].Id = GetContactId(face.Id1, face.Id2, ref clipEdge);
                        contactData.Count++;
                    }
                }
                if (v3MinYInside && contactData.Count < 8)
                {
                    //ComputeIntersection(ref face.V2, ref face.V3, ref clipEdge, out v);
                    //TSVector.Dot(ref clipX, ref v, out dot);
                    //if (dot > clipCenterMinX && dot < clipCenterMaxX)
                    if (ComputeIntersection(ref face.V2, ref face.V3, ref clipEdge, out v))
                    {
                        data[contactData.Count].Position = v;
                        data[contactData.Count].Id = GetContactId(face.Id2, face.Id3, ref clipEdge);
                        contactData.Count++;
                    }
                }
            }
            if (!v3MinYInside)
            {
                if (v2MinYInside && contactData.Count < 8)
                {
                    //ComputeIntersection(ref face.V2, ref face.V3, ref clipEdge, out v);
                    //TSVector.Dot(ref clipX, ref v, out dot);
                    //if (dot > clipCenterMinX && dot < clipCenterMaxX)
                    if (ComputeIntersection(ref face.V2, ref face.V3, ref clipEdge, out v))
                    {
                        data[contactData.Count].Position = v;
                        data[contactData.Count].Id = GetContactId(face.Id2, face.Id3, ref clipEdge);
                        contactData.Count++;
                    }
                }
                if (v4MinYInside && contactData.Count < 8)
                {
                    //ComputeIntersection(ref face.V3, ref face.V4, ref clipEdge, out v);
                    //TSVector.Dot(ref clipX, ref v, out dot);
                    //if (dot > clipCenterMinX && dot < clipCenterMaxX)
                    if (ComputeIntersection(ref face.V3, ref face.V4, ref clipEdge, out v))
                    {
                        data[contactData.Count].Position = v;
                        data[contactData.Count].Id = GetContactId(face.Id3, face.Id4, ref clipEdge);
                        contactData.Count++;
                    }
                }
            }
            if (!v4MinYInside)
            {
                if (v3MinYInside && contactData.Count < 8)
                {
                    //ComputeIntersection(ref face.V3, ref face.V4, ref clipEdge, out v);
                    //TSVector.Dot(ref clipX, ref v, out dot);
                    //if (dot > clipCenterMinX && dot < clipCenterMaxX)
                    if (ComputeIntersection(ref face.V3, ref face.V4, ref clipEdge, out v))
                    {
                        data[contactData.Count].Position = v;
                        data[contactData.Count].Id = GetContactId(face.Id3, face.Id4, ref clipEdge);
                        contactData.Count++;
                    }
                }
                if (v1MinYInside && contactData.Count < 8)
                {
                    //ComputeIntersection(ref face.V4, ref face.V1, ref clipEdge, out v);
                    //TSVector.Dot(ref clipX, ref v, out dot);
                    //if (dot > clipCenterMinX && dot < clipCenterMaxX)
                    if (ComputeIntersection(ref face.V4, ref face.V1, ref clipEdge, out v))
                    {
                        data[contactData.Count].Position = v;
                        data[contactData.Count].Id = GetContactId(face.Id4, face.Id1, ref clipEdge);
                        contactData.Count++;
                    }
                }
            }

        #endregion

        #region CLIP EDGE: v4 v1

            clipFace.GetEdge(3, out clipEdge);
            if (!v1MaxXInside)
            {
                if (v2MaxXInside && contactData.Count < 8)
                {
                    //ComputeIntersection(ref face.V1, ref face.V2, ref clipEdge, out v);
                    //TSVector.Dot(ref clipY, ref v, out dot);
                    //if (dot > clipCenterMinY && dot < clipCenterMaxY)
                    if (ComputeIntersection(ref face.V1, ref face.V2, ref clipEdge, out v))
                    {
                        data[contactData.Count].Position = v;
                        data[contactData.Count].Id = GetContactId(face.Id1, face.Id2, ref clipEdge);
                        contactData.Count++;
                    }
                }
                if (v4MaxXInside && contactData.Count < 8)
                {
                    //ComputeIntersection(ref face.V4, ref face.V1, ref clipEdge, out v);
                    //TSVector.Dot(ref clipY, ref v, out dot);
                    //if (dot > clipCenterMinY && dot < clipCenterMaxY)
                    if (ComputeIntersection(ref face.V4, ref face.V1, ref clipEdge, out v))
                    {
                        data[contactData.Count].Position = v;
                        data[contactData.Count].Id = GetContactId(face.Id4, face.Id1, ref clipEdge);
                        contactData.Count++;
                    }
                }
            }
            if (!v2MaxXInside)
            {
                if (v1MaxXInside && contactData.Count < 8)
                {
                    //ComputeIntersection(ref face.V1, ref face.V2, ref clipEdge, out v);
                    //TSVector.Dot(ref clipY, ref v, out dot);
                    //if (dot > clipCenterMinY && dot < clipCenterMaxY)
                    if (ComputeIntersection(ref face.V1, ref face.V2, ref clipEdge, out v))
                    {
                        data[contactData.Count].Position = v;
                        data[contactData.Count].Id = GetContactId(face.Id1, face.Id2, ref clipEdge);
                        contactData.Count++;
                    }
                }
                if (v3MaxXInside && contactData.Count < 8)
                {
                    //ComputeIntersection(ref face.V2, ref face.V3, ref clipEdge, out v);
                    //TSVector.Dot(ref clipY, ref v, out dot);
                    //if (dot > clipCenterMinY && dot < clipCenterMaxY)
                    if (ComputeIntersection(ref face.V2, ref face.V3, ref clipEdge, out v))
                    {
                        data[contactData.Count].Position = v;
                        data[contactData.Count].Id = GetContactId(face.Id2, face.Id3, ref clipEdge);
                        contactData.Count++;
                    }
                }
            }
            if (!v3MaxXInside)
            {
                if (v2MaxXInside && contactData.Count < 8)
                {
                    //ComputeIntersection(ref face.V2, ref face.V3, ref clipEdge, out v);
                    //TSVector.Dot(ref clipY, ref v, out dot);
                    //if (dot > clipCenterMinY && dot < clipCenterMaxY)
                    if (ComputeIntersection(ref face.V2, ref face.V3, ref clipEdge, out v))
                    {
                        data[contactData.Count].Position = v;
                        data[contactData.Count].Id = GetContactId(face.Id2, face.Id3, ref clipEdge);
                        contactData.Count++;
                    }
                }
                if (v4MaxXInside && contactData.Count < 8)
                {
                    //ComputeIntersection(ref face.V3, ref face.V4, ref clipEdge, out v);
                    //TSVector.Dot(ref clipY, ref v, out dot);
                    //if (dot > clipCenterMinY && dot < clipCenterMaxY)
                    if (ComputeIntersection(ref face.V3, ref face.V4, ref clipEdge, out v))
                    {
                        data[contactData.Count].Position = v;
                        data[contactData.Count].Id = GetContactId(face.Id3, face.Id4, ref clipEdge);
                        contactData.Count++;
                    }
                }
            }
            if (!v4MaxXInside)
            {
                if (v1MaxXInside && contactData.Count < 8)
                {
                    //ComputeIntersection(ref face.V4, ref face.V1, ref clipEdge, out v);
                    //TSVector.Dot(ref clipY, ref v, out dot);
                    //if (dot > clipCenterMinY && dot < clipCenterMaxY)
                    if (ComputeIntersection(ref face.V4, ref face.V1, ref clipEdge, out v))
                    {
                        data[contactData.Count].Position = v;
                        data[contactData.Count].Id = GetContactId(face.Id4, face.Id1, ref clipEdge);
                        contactData.Count++;
                    }
                }
                if (v3MaxXInside && contactData.Count < 8)
                {
                    //ComputeIntersection(ref face.V3, ref face.V4, ref clipEdge, out v);
                    //TSVector.Dot(ref clipY, ref v, out dot);
                    //if (dot > clipCenterMinY && dot < clipCenterMaxY)
                    if (ComputeIntersection(ref face.V3, ref face.V4, ref clipEdge, out v))
                    {
                        data[contactData.Count].Position = v;
                        data[contactData.Count].Id = GetContactId(face.Id3, face.Id4, ref clipEdge);
                        contactData.Count++;
                    }
                }
            }

        #endregion

            //Compute depths.
            tempData = contactData;
            contactData.Count = previousCount;

            for (int i = previousCount; i < tempData.Count; i++)
            {
                TSVector.Dot(ref temp[i].Position, ref mtd, out faceDot);
                depth = faceDot - clipFaceDot;
                if (depth <= FP.Zero)
                {
                    data[contactData.Count].Position = temp[i].Position;
                    data[contactData.Count].Depth = depth;
                    data[contactData.Count].Id = temp[i].Id;
                    contactData.Count++;
                }
            }
            outputData = contactData;
        }
#else
        private static void ClipFacesDirect(ref BoxFace clipFace, ref BoxFace face, ref TSVector mtd, out TinyStructList<BoxContactData> contactData)
        {
            contactData = new TinyStructList<BoxContactData>();

            //Local directions on the clip face.  Their length is equal to the length of an edge.
            TSVector clipX, clipY;
            TSVector.Subtract(ref clipFace.V4, ref clipFace.V3, out clipX);
            TSVector.Subtract(ref clipFace.V2, ref clipFace.V3, out clipY);
            FP inverseClipWidth = 1 / clipFace.Width;
            FP inverseClipHeight = 1 / clipFace.Height;
            FP inverseClipWidthSquared = inverseClipWidth * inverseClipWidth;
            clipX.x *= inverseClipWidthSquared;
            clipX.y *= inverseClipWidthSquared;
            clipX.z *= inverseClipWidthSquared;
            FP inverseClipHeightSquared = inverseClipHeight * inverseClipHeight;
            clipY.x *= inverseClipHeightSquared;
            clipY.y *= inverseClipHeightSquared;
            clipY.z *= inverseClipHeightSquared;

            //Local directions on the opposing face.  Their length is equal to the length of an edge.
            TSVector faceX, faceY;
            TSVector.Subtract(ref face.V4, ref face.V3, out faceX);
            TSVector.Subtract(ref face.V2, ref face.V3, out faceY);
            FP inverseFaceWidth = 1 / face.Width;
            FP inverseFaceHeight = 1 / face.Height;
            FP inverseFaceWidthSquared = inverseFaceWidth * inverseFaceWidth;
            faceX.x *= inverseFaceWidthSquared;
            faceX.y *= inverseFaceWidthSquared;
            faceX.z *= inverseFaceWidthSquared;
            FP inverseFaceHeightSquared = inverseFaceHeight * inverseFaceHeight;
            faceY.x *= inverseFaceHeightSquared;
            faceY.y *= inverseFaceHeightSquared;
            faceY.z *= inverseFaceHeightSquared;

            TSVector clipCenter;
            TSVector.Add(ref clipFace.V1, ref clipFace.V3, out clipCenter);
            //Defer division until after dot product (2 multiplies instead of 3)
            FP clipCenterX, clipCenterY;
            TSVector.Dot(ref clipCenter, ref clipX, out clipCenterX);
            TSVector.Dot(ref clipCenter, ref clipY, out clipCenterY);
            clipCenterX *= FP.Half;
            clipCenterY *= FP.Half;

            TSVector faceCenter;
            TSVector.Add(ref face.V1, ref face.V3, out faceCenter);
            //Defer division until after dot product (2 multiplies instead of 3)
            FP faceCenterX, faceCenterY;
            TSVector.Dot(ref faceCenter, ref faceX, out faceCenterX);
            TSVector.Dot(ref faceCenter, ref faceY, out faceCenterY);
            faceCenterX *= FP.Half;
            faceCenterY *= FP.Half;

            //To test bounds, recall that clipX is the length of the X edge.
            //Going from the center to the max or min goes half of the length of X edge, or +/- 0.5.
            //Bias could be added here.
            //const FP extent = FP.Half; //FP.Half is the default, extra could be added for robustness or speed.
            FP extentX = FP.Half + FP.EN2 * inverseClipWidth;
            FP extentY = FP.Half + FP.EN2 * inverseClipHeight;
            //FP extentX = FP.Half + FP.EN2 * inverseClipXLength;
            //FP extentY = FP.Half + FP.EN2 * inverseClipYLength;
            FP clipCenterMaxX = clipCenterX + extentX;
            FP clipCenterMaxY = clipCenterY + extentY;
            FP clipCenterMinX = clipCenterX - extentX;
            FP clipCenterMinY = clipCenterY - extentY;

            extentX = FP.Half + FP.EN2 * inverseFaceWidth;
            extentY = FP.Half + FP.EN2 * inverseFaceHeight;
            //extentX = FP.Half + FP.EN2 * inverseFaceXLength;
            //extentY = FP.Half + FP.EN2 * inverseFaceYLength;
            FP faceCenterMaxX = faceCenterX + extentX;
            FP faceCenterMaxY = faceCenterY + extentY;
            FP faceCenterMinX = faceCenterX - extentX;
            FP faceCenterMinY = faceCenterY - extentY;

            //Find out where the opposing face is.
            FP dotX, dotY;

            //The four edges can be thought of as minX, maxX, minY and maxY.

            //Face v1
            TSVector.Dot(ref clipX, ref face.V1, out dotX);
            bool v1MaxXInside = dotX < clipCenterMaxX;
            bool v1MinXInside = dotX > clipCenterMinX;
            TSVector.Dot(ref clipY, ref face.V1, out dotY);
            bool v1MaxYInside = dotY < clipCenterMaxY;
            bool v1MinYInside = dotY > clipCenterMinY;

            //Face v2
            TSVector.Dot(ref clipX, ref face.V2, out dotX);
            bool v2MaxXInside = dotX < clipCenterMaxX;
            bool v2MinXInside = dotX > clipCenterMinX;
            TSVector.Dot(ref clipY, ref face.V2, out dotY);
            bool v2MaxYInside = dotY < clipCenterMaxY;
            bool v2MinYInside = dotY > clipCenterMinY;

            //Face v3
            TSVector.Dot(ref clipX, ref face.V3, out dotX);
            bool v3MaxXInside = dotX < clipCenterMaxX;
            bool v3MinXInside = dotX > clipCenterMinX;
            TSVector.Dot(ref clipY, ref face.V3, out dotY);
            bool v3MaxYInside = dotY < clipCenterMaxY;
            bool v3MinYInside = dotY > clipCenterMinY;

            //Face v4
            TSVector.Dot(ref clipX, ref face.V4, out dotX);
            bool v4MaxXInside = dotX < clipCenterMaxX;
            bool v4MinXInside = dotX > clipCenterMinX;
            TSVector.Dot(ref clipY, ref face.V4, out dotY);
            bool v4MaxYInside = dotY < clipCenterMaxY;
            bool v4MinYInside = dotY > clipCenterMinY;

            //Find out where the clip face is.
            //Clip v1
            TSVector.Dot(ref faceX, ref clipFace.V1, out dotX);
            bool clipv1MaxXInside = dotX < faceCenterMaxX;
            bool clipv1MinXInside = dotX > faceCenterMinX;
            TSVector.Dot(ref faceY, ref clipFace.V1, out dotY);
            bool clipv1MaxYInside = dotY < faceCenterMaxY;
            bool clipv1MinYInside = dotY > faceCenterMinY;

            //Clip v2
            TSVector.Dot(ref faceX, ref clipFace.V2, out dotX);
            bool clipv2MaxXInside = dotX < faceCenterMaxX;
            bool clipv2MinXInside = dotX > faceCenterMinX;
            TSVector.Dot(ref faceY, ref clipFace.V2, out dotY);
            bool clipv2MaxYInside = dotY < faceCenterMaxY;
            bool clipv2MinYInside = dotY > faceCenterMinY;

            //Clip v3
            TSVector.Dot(ref faceX, ref clipFace.V3, out dotX);
            bool clipv3MaxXInside = dotX < faceCenterMaxX;
            bool clipv3MinXInside = dotX > faceCenterMinX;
            TSVector.Dot(ref faceY, ref clipFace.V3, out dotY);
            bool clipv3MaxYInside = dotY < faceCenterMaxY;
            bool clipv3MinYInside = dotY > faceCenterMinY;

            //Clip v4
            TSVector.Dot(ref faceX, ref clipFace.V4, out dotX);
            bool clipv4MaxXInside = dotX < faceCenterMaxX;
            bool clipv4MinXInside = dotX > faceCenterMinX;
            TSVector.Dot(ref faceY, ref clipFace.V4, out dotY);
            bool clipv4MaxYInside = dotY < faceCenterMaxY;
            bool clipv4MinYInside = dotY > faceCenterMinY;

            #region Face Vertices
            BoxContactData item = new BoxContactData();
            if (v1MinXInside && v1MaxXInside && v1MinYInside && v1MaxYInside)
            {
                item.Position = face.V1;
                item.Id = face.Id1;
                contactData.Add(ref item);
            }

            if (v2MinXInside && v2MaxXInside && v2MinYInside && v2MaxYInside)
            {
                item.Position = face.V2;
                item.Id = face.Id2;
                contactData.Add(ref item);
            }

            if (v3MinXInside && v3MaxXInside && v3MinYInside && v3MaxYInside)
            {
                item.Position = face.V3;
                item.Id = face.Id3;
                contactData.Add(ref item);
            }

            if (v4MinXInside && v4MaxXInside && v4MinYInside && v4MaxYInside)
            {
                item.Position = face.V4;
                item.Id = face.Id4;
                contactData.Add(ref item);
            }

            #endregion

            //Compute depths.
            TinyStructList<BoxContactData> tempData = contactData;
            contactData.Clear();
            FP clipFaceDot, faceDot;
            TSVector.Dot(ref clipFace.V1, ref mtd, out clipFaceDot);
            for (int i = 0; i < tempData.Count; i++)
            {
                tempData.Get(i, out item);
                TSVector.Dot(ref item.Position, ref mtd, out faceDot);
                item.Depth = faceDot - clipFaceDot;
                if (item.Depth <= 0)
                {
                    contactData.Add(ref item);
                }
            }

            int previousCount = contactData.Count;
            if (previousCount >= 4) //Early finish :)
            {
                return;
            }

            #region Clip face vertices

            TSVector v;
            FP a, b;
            TSVector.Dot(ref face.V1, ref face.Normal, out b);
            //CLIP FACE
            if (clipv1MinXInside && clipv1MaxXInside && clipv1MinYInside && clipv1MaxYInside)
            {
                TSVector.Dot(ref clipFace.V1, ref face.Normal, out a);
                TSVector.Multiply(ref face.Normal, a - b, out v);
                TSVector.Subtract(ref clipFace.V1, ref v, out v);
                item.Position = v;
                item.Id = clipFace.Id1 + 8;
                contactData.Add(ref item);
            }

            if (clipv2MinXInside && clipv2MaxXInside && clipv2MinYInside && clipv2MaxYInside)
            {
                TSVector.Dot(ref clipFace.V2, ref face.Normal, out a);
                TSVector.Multiply(ref face.Normal, a - b, out v);
                TSVector.Subtract(ref clipFace.V2, ref v, out v);
                item.Position = v;
                item.Id = clipFace.Id2 + 8;
                contactData.Add(ref item);
            }

            if (clipv3MinXInside && clipv3MaxXInside && clipv3MinYInside && clipv3MaxYInside)
            {
                TSVector.Dot(ref clipFace.V3, ref face.Normal, out a);
                TSVector.Multiply(ref face.Normal, a - b, out v);
                TSVector.Subtract(ref clipFace.V3, ref v, out v);
                item.Position = v;
                item.Id = clipFace.Id3 + 8;
                contactData.Add(ref item);
            }

            if (clipv4MinXInside && clipv4MaxXInside && clipv4MinYInside && clipv4MaxYInside)
            {
                TSVector.Dot(ref clipFace.V4, ref face.Normal, out a);
                TSVector.Multiply(ref face.Normal, a - b, out v);
                TSVector.Subtract(ref clipFace.V4, ref v, out v);
                item.Position = v;
                item.Id = clipFace.Id4 + 8;
                contactData.Add(ref item);
            }

            #endregion

            //Compute depths.
            int postClipCount = contactData.Count;
            tempData = contactData;
            for (int i = postClipCount - 1; i >= previousCount; i--) //TODO: >=?
                contactData.RemoveAt(i);


            for (int i = previousCount; i < tempData.Count; i++)
            {
                tempData.Get(i, out item);
                TSVector.Dot(ref item.Position, ref mtd, out faceDot);
                item.Depth = faceDot - clipFaceDot;
                if (item.Depth <= 0)
                {
                    contactData.Add(ref item);
                }
            }

            previousCount = contactData.Count;
            if (previousCount >= 4) //Early finish :)
            {
                return;
            }
            //Intersect edges.

            //maxX maxY -> v1
            //minX maxY -> v2
            //minX minY -> v3
            //maxX minY -> v4

            //Once we get here there can only be 3 contacts or less.
            //Once 4 possible contacts have been added, switch to using safe increments.
            //FP dot;

            #region CLIP EDGE: v1 v2

            FaceEdge clipEdge;
            clipFace.GetEdge(0, out clipEdge);
            if (!v1MaxYInside)
            {
                if (v2MaxYInside)
                {
                    //ComputeIntersection(ref face.V1, ref face.V2, ref clipEdge, out v);
                    //TSVector.Dot(ref clipX, ref v, out dot);
                    //if (dot > clipCenterMinX && dot < clipCenterMaxX)
                    if (ComputeIntersection(ref face.V1, ref face.V2, ref clipEdge, out v))
                    {
                        item.Position = v;
                        item.Id = GetContactId(face.Id1, face.Id2, ref clipEdge);
                        contactData.Add(ref item);
                    }
                }
                if (v4MaxYInside)
                {
                    //ComputeIntersection(ref face.V4, ref face.V1, ref clipEdge, out v);
                    //TSVector.Dot(ref clipX, ref v, out dot);
                    //if (dot > clipCenterMinX && dot < clipCenterMaxX)
                    if (ComputeIntersection(ref face.V4, ref face.V1, ref clipEdge, out v))
                    {
                        item.Position = v;
                        item.Id = GetContactId(face.Id4, face.Id1, ref clipEdge);
                        contactData.Add(ref item);
                    }
                }
            }
            if (!v2MaxYInside)
            {
                if (v1MaxYInside)
                {
                    //ComputeIntersection(ref face.V1, ref face.V2, ref clipEdge, out v);
                    //TSVector.Dot(ref clipX, ref v, out dot);
                    //if (dot > clipCenterMinX && dot < clipCenterMaxX)
                    if (ComputeIntersection(ref face.V1, ref face.V2, ref clipEdge, out v))
                    {
                        item.Position = v;
                        item.Id = GetContactId(face.Id1, face.Id2, ref clipEdge);
                        contactData.Add(ref item);
                    }
                }
                if (v3MaxYInside)
                {
                    //ComputeIntersection(ref face.V2, ref face.V3, ref clipEdge, out v);
                    //TSVector.Dot(ref clipX, ref v, out dot);
                    //if (dot > clipCenterMinX && dot < clipCenterMaxX)
                    if (ComputeIntersection(ref face.V2, ref face.V3, ref clipEdge, out v))
                    {
                        item.Position = v;
                        item.Id = GetContactId(face.Id2, face.Id3, ref clipEdge);
                        contactData.Add(ref item);
                    }
                }
            }
            if (!v3MaxYInside)
            {
                if (v2MaxYInside)
                {
                    //ComputeIntersection(ref face.V2, ref face.V3, ref clipEdge, out v);
                    //TSVector.Dot(ref clipX, ref v, out dot);
                    //if (dot > clipCenterMinX && dot < clipCenterMaxX)
                    if (ComputeIntersection(ref face.V2, ref face.V3, ref clipEdge, out v))
                    {
                        item.Position = v;
                        item.Id = GetContactId(face.Id2, face.Id3, ref clipEdge);
                        contactData.Add(ref item);
                    }
                }
                if (v4MaxYInside && contactData.Count < 8)
                {
                    //ComputeIntersection(ref face.V3, ref face.V4, ref clipEdge, out v);
                    //TSVector.Dot(ref clipX, ref v, out dot);
                    //if (dot > clipCenterMinX && dot < clipCenterMaxX)
                    if (ComputeIntersection(ref face.V3, ref face.V4, ref clipEdge, out v))
                    {
                        item.Position = v;
                        item.Id = GetContactId(face.Id3, face.Id4, ref clipEdge);
                        contactData.Add(ref item);
                    }
                }
            }
            if (!v4MaxYInside)
            {
                if (v1MaxYInside && contactData.Count < 8)
                {
                    //ComputeIntersection(ref face.V4, ref face.V1, ref clipEdge, out v);
                    //TSVector.Dot(ref clipX, ref v, out dot);
                    //if (dot > clipCenterMinX && dot < clipCenterMaxX)
                    if (ComputeIntersection(ref face.V4, ref face.V1, ref clipEdge, out v))
                    {
                        item.Position = v;
                        item.Id = GetContactId(face.Id4, face.Id1, ref clipEdge);
                        contactData.Add(ref item);
                    }
                }
                if (v3MaxYInside && contactData.Count < 8)
                {
                    //ComputeIntersection(ref face.V3, ref face.V4, ref clipEdge, out v);
                    //TSVector.Dot(ref clipX, ref v, out dot);
                    //if (dot > clipCenterMinX && dot < clipCenterMaxX)
                    if (ComputeIntersection(ref face.V3, ref face.V4, ref clipEdge, out v))
                    {
                        item.Position = v;
                        item.Id = GetContactId(face.Id3, face.Id4, ref clipEdge);
                        contactData.Add(ref item);
                    }
                }
            }

            #endregion

            #region CLIP EDGE: v2 v3

            clipFace.GetEdge(1, out clipEdge);
            if (!v1MinXInside)
            {
                if (v2MinXInside && contactData.Count < 8)
                {
                    //test v1-v2 against minXminY-minXmaxY
                    //ComputeIntersection(ref face.V1, ref face.V2, ref clipEdge, out v);
                    //TSVector.Dot(ref clipY, ref v, out dot);
                    //if (dot > clipCenterMinY && dot < clipCenterMaxY)
                    if (ComputeIntersection(ref face.V1, ref face.V2, ref clipEdge, out v))
                    {
                        item.Position = v;
                        item.Id = GetContactId(face.Id1, face.Id2, ref clipEdge);
                        contactData.Add(ref item);
                    }
                }
                if (v4MinXInside && contactData.Count < 8)
                {
                    //test v1-v3 against minXminY-minXmaxY
                    //ComputeIntersection(ref face.V4, ref face.V1, ref clipEdge, out v);
                    //TSVector.Dot(ref clipY, ref v, out dot);
                    //if (dot > clipCenterMinY && dot < clipCenterMaxY)
                    if (ComputeIntersection(ref face.V4, ref face.V1, ref clipEdge, out v))
                    {
                        item.Position = v;
                        item.Id = GetContactId(face.Id4, face.Id1, ref clipEdge);
                        contactData.Add(ref item);
                    }
                }
            }
            if (!v2MinXInside)
            {
                if (v1MinXInside && contactData.Count < 8)
                {
                    //test v1-v2 against minXminY-minXmaxY
                    //ComputeIntersection(ref face.V1, ref face.V2, ref clipEdge, out v);
                    //TSVector.Dot(ref clipY, ref v, out dot);
                    //if (dot > clipCenterMinY && dot < clipCenterMaxY)
                    if (ComputeIntersection(ref face.V1, ref face.V2, ref clipEdge, out v))
                    {
                        item.Position = v;
                        item.Id = GetContactId(face.Id1, face.Id2, ref clipEdge);
                        contactData.Add(ref item);
                    }
                }
                if (v3MinXInside && contactData.Count < 8)
                {
                    //test v2-v4 against minXminY-minXmaxY
                    //ComputeIntersection(ref face.V2, ref face.V3, ref clipEdge, out v);
                    //TSVector.Dot(ref clipY, ref v, out dot);
                    //if (dot > clipCenterMinY && dot < clipCenterMaxY)
                    if (ComputeIntersection(ref face.V2, ref face.V3, ref clipEdge, out v))
                    {
                        item.Position = v;
                        item.Id = GetContactId(face.Id2, face.Id3, ref clipEdge);
                        contactData.Add(ref item);
                    }
                }
            }
            if (!v3MinXInside)
            {
                if (v2MinXInside && contactData.Count < 8)
                {
                    //test v1-v3 against minXminY-minXmaxY
                    //ComputeIntersection(ref face.V2, ref face.V3, ref clipEdge, out v);
                    //TSVector.Dot(ref clipY, ref v, out dot);
                    //if (dot > clipCenterMinY && dot < clipCenterMaxY)
                    if (ComputeIntersection(ref face.V2, ref face.V3, ref clipEdge, out v))
                    {
                        item.Position = v;
                        item.Id = GetContactId(face.Id2, face.Id3, ref clipEdge);
                        contactData.Add(ref item);
                    }
                }
                if (v4MinXInside && contactData.Count < 8)
                {
                    //test v3-v4 against minXminY-minXmaxY
                    //ComputeIntersection(ref face.V3, ref face.V4, ref clipEdge, out v);
                    //TSVector.Dot(ref clipY, ref v, out dot);
                    //if (dot > clipCenterMinY && dot < clipCenterMaxY)
                    if (ComputeIntersection(ref face.V3, ref face.V4, ref clipEdge, out v))
                    {
                        item.Position = v;
                        item.Id = GetContactId(face.Id3, face.Id4, ref clipEdge);
                        contactData.Add(ref item);
                    }
                }
            }
            if (!v4MinXInside)
            {
                if (v1MinXInside && contactData.Count < 8)
                {
                    //test v2-v4 against minXminY-minXmaxY
                    //ComputeIntersection(ref face.V4, ref face.V1, ref clipEdge, out v);
                    //TSVector.Dot(ref clipY, ref v, out dot);
                    //if (dot > clipCenterMinY && dot < clipCenterMaxY)
                    if (ComputeIntersection(ref face.V4, ref face.V1, ref clipEdge, out v))
                    {
                        item.Position = v;
                        item.Id = GetContactId(face.Id4, face.Id1, ref clipEdge);
                        contactData.Add(ref item);
                    }
                }
                if (v3MinXInside && contactData.Count < 8)
                {
                    //test v3-v4 against minXminY-minXmaxY
                    //ComputeIntersection(ref face.V3, ref face.V4, ref clipEdge, out v);
                    //TSVector.Dot(ref clipY, ref v, out dot);
                    //if (dot > clipCenterMinY && dot < clipCenterMaxY)
                    if (ComputeIntersection(ref face.V3, ref face.V4, ref clipEdge, out v))
                    {
                        item.Position = v;
                        item.Id = GetContactId(face.Id3, face.Id4, ref clipEdge);
                        contactData.Add(ref item);
                    }
                }
            }

            #endregion

            #region CLIP EDGE: v3 v4

            clipFace.GetEdge(2, out clipEdge);
            if (!v1MinYInside)
            {
                if (v2MinYInside && contactData.Count < 8)
                {
                    //ComputeIntersection(ref face.V1, ref face.V2, ref clipEdge, out v);
                    //TSVector.Dot(ref clipX, ref v, out dot);
                    //if (dot > clipCenterMinX && dot < clipCenterMaxX)
                    if (ComputeIntersection(ref face.V1, ref face.V2, ref clipEdge, out v))
                    {
                        item.Position = v;
                        item.Id = GetContactId(face.Id1, face.Id2, ref clipEdge);
                        contactData.Add(ref item);
                    }
                }
                if (v4MinYInside && contactData.Count < 8)
                {
                    //ComputeIntersection(ref face.V4, ref face.V1, ref clipEdge, out v);
                    //TSVector.Dot(ref clipX, ref v, out dot);
                    //if (dot > clipCenterMinX && dot < clipCenterMaxX)
                    if (ComputeIntersection(ref face.V4, ref face.V1, ref clipEdge, out v))
                    {
                        item.Position = v;
                        item.Id = GetContactId(face.Id4, face.Id1, ref clipEdge);
                        contactData.Add(ref item);
                    }
                }
            }
            if (!v2MinYInside)
            {
                if (v1MinYInside && contactData.Count < 8)
                {
                    //ComputeIntersection(ref face.V1, ref face.V2, ref clipEdge, out v);
                    //TSVector.Dot(ref clipX, ref v, out dot);
                    //if (dot > clipCenterMinX && dot < clipCenterMaxX)
                    if (ComputeIntersection(ref face.V1, ref face.V2, ref clipEdge, out v))
                    {
                        item.Position = v;
                        item.Id = GetContactId(face.Id1, face.Id2, ref clipEdge);
                        contactData.Add(ref item);
                    }
                }
                if (v3MinYInside && contactData.Count < 8)
                {
                    //ComputeIntersection(ref face.V2, ref face.V3, ref clipEdge, out v);
                    //TSVector.Dot(ref clipX, ref v, out dot);
                    //if (dot > clipCenterMinX && dot < clipCenterMaxX)
                    if (ComputeIntersection(ref face.V2, ref face.V3, ref clipEdge, out v))
                    {
                        item.Position = v;
                        item.Id = GetContactId(face.Id2, face.Id3, ref clipEdge);
                        contactData.Add(ref item);
                    }
                }
            }
            if (!v3MinYInside)
            {
                if (v2MinYInside && contactData.Count < 8)
                {
                    //ComputeIntersection(ref face.V2, ref face.V3, ref clipEdge, out v);
                    //TSVector.Dot(ref clipX, ref v, out dot);
                    //if (dot > clipCenterMinX && dot < clipCenterMaxX)
                    if (ComputeIntersection(ref face.V2, ref face.V3, ref clipEdge, out v))
                    {
                        item.Position = v;
                        item.Id = GetContactId(face.Id2, face.Id3, ref clipEdge);
                        contactData.Add(ref item);
                    }
                }
                if (v4MinYInside && contactData.Count < 8)
                {
                    //ComputeIntersection(ref face.V3, ref face.V4, ref clipEdge, out v);
                    //TSVector.Dot(ref clipX, ref v, out dot);
                    //if (dot > clipCenterMinX && dot < clipCenterMaxX)
                    if (ComputeIntersection(ref face.V3, ref face.V4, ref clipEdge, out v))
                    {
                        item.Position = v;
                        item.Id = GetContactId(face.Id3, face.Id4, ref clipEdge);
                        contactData.Add(ref item);
                    }
                }
            }
            if (!v4MinYInside)
            {
                if (v3MinYInside && contactData.Count < 8)
                {
                    //ComputeIntersection(ref face.V3, ref face.V4, ref clipEdge, out v);
                    //TSVector.Dot(ref clipX, ref v, out dot);
                    //if (dot > clipCenterMinX && dot < clipCenterMaxX)
                    if (ComputeIntersection(ref face.V3, ref face.V4, ref clipEdge, out v))
                    {
                        item.Position = v;
                        item.Id = GetContactId(face.Id3, face.Id4, ref clipEdge);
                        contactData.Add(ref item);
                    }
                }
                if (v1MinYInside && contactData.Count < 8)
                {
                    //ComputeIntersection(ref face.V4, ref face.V1, ref clipEdge, out v);
                    //TSVector.Dot(ref clipX, ref v, out dot);
                    //if (dot > clipCenterMinX && dot < clipCenterMaxX)
                    if (ComputeIntersection(ref face.V4, ref face.V1, ref clipEdge, out v))
                    {
                        item.Position = v;
                        item.Id = GetContactId(face.Id4, face.Id1, ref clipEdge);
                        contactData.Add(ref item);
                    }
                }
            }

            #endregion

            #region CLIP EDGE: v4 v1

            clipFace.GetEdge(3, out clipEdge);
            if (!v1MaxXInside)
            {
                if (v2MaxXInside && contactData.Count < 8)
                {
                    //ComputeIntersection(ref face.V1, ref face.V2, ref clipEdge, out v);
                    //TSVector.Dot(ref clipY, ref v, out dot);
                    //if (dot > clipCenterMinY && dot < clipCenterMaxY)
                    if (ComputeIntersection(ref face.V1, ref face.V2, ref clipEdge, out v))
                    {
                        item.Position = v;
                        item.Id = GetContactId(face.Id1, face.Id2, ref clipEdge);
                        contactData.Add(ref item);
                    }
                }
                if (v4MaxXInside && contactData.Count < 8)
                {
                    //ComputeIntersection(ref face.V4, ref face.V1, ref clipEdge, out v);
                    //TSVector.Dot(ref clipY, ref v, out dot);
                    //if (dot > clipCenterMinY && dot < clipCenterMaxY)
                    if (ComputeIntersection(ref face.V4, ref face.V1, ref clipEdge, out v))
                    {
                        item.Position = v;
                        item.Id = GetContactId(face.Id4, face.Id1, ref clipEdge);
                        contactData.Add(ref item);
                    }
                }
            }
            if (!v2MaxXInside)
            {
                if (v1MaxXInside && contactData.Count < 8)
                {
                    //ComputeIntersection(ref face.V1, ref face.V2, ref clipEdge, out v);
                    //TSVector.Dot(ref clipY, ref v, out dot);
                    //if (dot > clipCenterMinY && dot < clipCenterMaxY)
                    if (ComputeIntersection(ref face.V1, ref face.V2, ref clipEdge, out v))
                    {
                        item.Position = v;
                        item.Id = GetContactId(face.Id1, face.Id2, ref clipEdge);
                        contactData.Add(ref item);
                    }
                }
                if (v3MaxXInside && contactData.Count < 8)
                {
                    //ComputeIntersection(ref face.V2, ref face.V3, ref clipEdge, out v);
                    //TSVector.Dot(ref clipY, ref v, out dot);
                    //if (dot > clipCenterMinY && dot < clipCenterMaxY)
                    if (ComputeIntersection(ref face.V2, ref face.V3, ref clipEdge, out v))
                    {
                        item.Position = v;
                        item.Id = GetContactId(face.Id2, face.Id3, ref clipEdge);
                        contactData.Add(ref item);
                    }
                }
            }
            if (!v3MaxXInside)
            {
                if (v2MaxXInside && contactData.Count < 8)
                {
                    //ComputeIntersection(ref face.V2, ref face.V3, ref clipEdge, out v);
                    //TSVector.Dot(ref clipY, ref v, out dot);
                    //if (dot > clipCenterMinY && dot < clipCenterMaxY)
                    if (ComputeIntersection(ref face.V2, ref face.V3, ref clipEdge, out v))
                    {
                        item.Position = v;
                        item.Id = GetContactId(face.Id2, face.Id3, ref clipEdge);
                        contactData.Add(ref item);
                    }
                }
                if (v4MaxXInside && contactData.Count < 8)
                {
                    //ComputeIntersection(ref face.V3, ref face.V4, ref clipEdge, out v);
                    //TSVector.Dot(ref clipY, ref v, out dot);
                    //if (dot > clipCenterMinY && dot < clipCenterMaxY)
                    if (ComputeIntersection(ref face.V3, ref face.V4, ref clipEdge, out v))
                    {
                        item.Position = v;
                        item.Id = GetContactId(face.Id3, face.Id4, ref clipEdge);
                        contactData.Add(ref item);
                    }
                }
            }
            if (!v4MaxXInside)
            {
                if (v1MaxXInside && contactData.Count < 8)
                {
                    //ComputeIntersection(ref face.V4, ref face.V1, ref clipEdge, out v);
                    //TSVector.Dot(ref clipY, ref v, out dot);
                    //if (dot > clipCenterMinY && dot < clipCenterMaxY)
                    if (ComputeIntersection(ref face.V4, ref face.V1, ref clipEdge, out v))
                    {
                        item.Position = v;
                        item.Id = GetContactId(face.Id4, face.Id1, ref clipEdge);
                        contactData.Add(ref item);
                    }
                }
                if (v3MaxXInside && contactData.Count < 8)
                {
                    //ComputeIntersection(ref face.V3, ref face.V4, ref clipEdge, out v);
                    //TSVector.Dot(ref clipY, ref v, out dot);
                    //if (dot > clipCenterMinY && dot < clipCenterMaxY)
                    if (ComputeIntersection(ref face.V3, ref face.V4, ref clipEdge, out v))
                    {
                        item.Position = v;
                        item.Id = GetContactId(face.Id3, face.Id4, ref clipEdge);
                        contactData.Add(ref item);
                    }
                }
            }

            #endregion

            //Compute depths.
            postClipCount = contactData.Count;
            tempData = contactData;
            for (int i = postClipCount - 1; i >= previousCount; i--)
                contactData.RemoveAt(i);

            for (int i = previousCount; i < tempData.Count; i++)
            {
                tempData.Get(i, out item);
                TSVector.Dot(ref item.Position, ref mtd, out faceDot);
                item.Depth = faceDot - clipFaceDot;
                if (item.Depth <= 0)
                {
                    contactData.Add(ref item);
                }
            }
        }
        //private static void ClipFacesDirect(ref BoxFace clipFace, ref BoxFace face, ref TSVector mtd, out TinyStructList<BoxContactData> contactData)
        //{
        //    contactData = new TinyStructList<BoxContactData>();
        //    //BoxContactData* data = &contactData.d1;
        //    //BoxContactData* temp = &tempData.d1;

        //    //Local directions on the clip face.  Their length is equal to the length of an edge.
        //    TSVector clipX, clipY;
        //    TSVector.Subtract(ref clipFace.V4, ref clipFace.V3, out clipX);
        //    TSVector.Subtract(ref clipFace.V2, ref clipFace.V3, out clipY);
        //    FP inverse = 1 / clipX.LengthSquared();
        //    clipX.x *= inverse;
        //    clipX.y *= inverse;
        //    clipX.z *= inverse;
        //    inverse = 1 / clipY.LengthSquared();
        //    clipY.x *= inverse;
        //    clipY.y *= inverse;
        //    clipY.z *= inverse;

        //    //Local directions on the opposing face.  Their length is equal to the length of an edge.
        //    TSVector faceX, faceY;
        //    TSVector.Subtract(ref face.V4, ref face.V3, out faceX);
        //    TSVector.Subtract(ref face.V2, ref face.V3, out faceY);
        //    inverse = 1 / faceX.LengthSquared();
        //    faceX.x *= inverse;
        //    faceX.y *= inverse;
        //    faceX.z *= inverse;
        //    inverse = 1 / faceY.LengthSquared();
        //    faceY.x *= inverse;
        //    faceY.y *= inverse;
        //    faceY.z *= inverse;

        //    TSVector clipCenter;
        //    TSVector.Add(ref clipFace.V1, ref clipFace.V3, out clipCenter);
        //    //Defer division until after dot product (2 multiplies instead of 3)
        //    FP clipCenterX, clipCenterY;
        //    TSVector.Dot(ref clipCenter, ref clipX, out clipCenterX);
        //    TSVector.Dot(ref clipCenter, ref clipY, out clipCenterY);
        //    clipCenterX *= FP.Half;
        //    clipCenterY *= FP.Half;

        //    TSVector faceCenter;
        //    TSVector.Add(ref face.V1, ref face.V3, out faceCenter);
        //    //Defer division until after dot product (2 multiplies instead of 3)
        //    FP faceCenterX, faceCenterY;
        //    TSVector.Dot(ref faceCenter, ref faceX, out faceCenterX);
        //    TSVector.Dot(ref faceCenter, ref faceY, out faceCenterY);
        //    faceCenterX *= FP.Half;
        //    faceCenterY *= FP.Half;

        //    //To test bounds, recall that clipX is the length of the X edge.
        //    //Going from the center to the max or min goes half of the length of X edge, or +/- 0.5.
        //    //Bias could be added here.
        //    FP extent = FP.Half; //FP.Half is the default, extra could be added for robustness or speed.
        //    FP clipCenterMaxX = clipCenterX + extent;
        //    FP clipCenterMaxY = clipCenterY + extent;
        //    FP clipCenterMinX = clipCenterX - extent;
        //    FP clipCenterMinY = clipCenterY - extent;

        //    FP faceCenterMaxX = faceCenterX + extent;
        //    FP faceCenterMaxY = faceCenterY + extent;
        //    FP faceCenterMinX = faceCenterX - extent;
        //    FP faceCenterMinY = faceCenterY - extent;

        //    //Find out where the opposing face is.
        //    FP dotX, dotY;

        //    //The four edges can be thought of as minX, maxX, minY and maxY.

        //    //Face v1
        //    TSVector.Dot(ref clipX, ref face.V1, out dotX);
        //    bool v1MaxXInside = dotX < clipCenterMaxX;
        //    bool v1MinXInside = dotX > clipCenterMinX;
        //    TSVector.Dot(ref clipY, ref face.V1, out dotY);
        //    bool v1MaxYInside = dotY < clipCenterMaxY;
        //    bool v1MinYInside = dotY > clipCenterMinY;

        //    //Face v2
        //    TSVector.Dot(ref clipX, ref face.V2, out dotX);
        //    bool v2MaxXInside = dotX < clipCenterMaxX;
        //    bool v2MinXInside = dotX > clipCenterMinX;
        //    TSVector.Dot(ref clipY, ref face.V2, out dotY);
        //    bool v2MaxYInside = dotY < clipCenterMaxY;
        //    bool v2MinYInside = dotY > clipCenterMinY;

        //    //Face v3
        //    TSVector.Dot(ref clipX, ref face.V3, out dotX);
        //    bool v3MaxXInside = dotX < clipCenterMaxX;
        //    bool v3MinXInside = dotX > clipCenterMinX;
        //    TSVector.Dot(ref clipY, ref face.V3, out dotY);
        //    bool v3MaxYInside = dotY < clipCenterMaxY;
        //    bool v3MinYInside = dotY > clipCenterMinY;

        //    //Face v4
        //    TSVector.Dot(ref clipX, ref face.V4, out dotX);
        //    bool v4MaxXInside = dotX < clipCenterMaxX;
        //    bool v4MinXInside = dotX > clipCenterMinX;
        //    TSVector.Dot(ref clipY, ref face.V4, out dotY);
        //    bool v4MaxYInside = dotY < clipCenterMaxY;
        //    bool v4MinYInside = dotY > clipCenterMinY;

        //    //Find out where the clip face is.
        //    //Clip v1
        //    TSVector.Dot(ref faceX, ref clipFace.V1, out dotX);
        //    bool clipv1MaxXInside = dotX < faceCenterMaxX;
        //    bool clipv1MinXInside = dotX > faceCenterMinX;
        //    TSVector.Dot(ref faceY, ref clipFace.V1, out dotY);
        //    bool clipv1MaxYInside = dotY < faceCenterMaxY;
        //    bool clipv1MinYInside = dotY > faceCenterMinY;

        //    //Clip v2
        //    TSVector.Dot(ref faceX, ref clipFace.V2, out dotX);
        //    bool clipv2MaxXInside = dotX < faceCenterMaxX;
        //    bool clipv2MinXInside = dotX > faceCenterMinX;
        //    TSVector.Dot(ref faceY, ref clipFace.V2, out dotY);
        //    bool clipv2MaxYInside = dotY < faceCenterMaxY;
        //    bool clipv2MinYInside = dotY > faceCenterMinY;

        //    //Clip v3
        //    TSVector.Dot(ref faceX, ref clipFace.V3, out dotX);
        //    bool clipv3MaxXInside = dotX < faceCenterMaxX;
        //    bool clipv3MinXInside = dotX > faceCenterMinX;
        //    TSVector.Dot(ref faceY, ref clipFace.V3, out dotY);
        //    bool clipv3MaxYInside = dotY < faceCenterMaxY;
        //    bool clipv3MinYInside = dotY > faceCenterMinY;

        //    //Clip v4
        //    TSVector.Dot(ref faceX, ref clipFace.V4, out dotX);
        //    bool clipv4MaxXInside = dotX < faceCenterMaxX;
        //    bool clipv4MinXInside = dotX > faceCenterMinX;
        //    TSVector.Dot(ref faceY, ref clipFace.V4, out dotY);
        //    bool clipv4MaxYInside = dotY < faceCenterMaxY;
        //    bool clipv4MinYInside = dotY > faceCenterMinY;

        //    var item = new BoxContactData();

        //    #region Face Vertices

        //    if (v1MinXInside && v1MaxXInside && v1MinYInside && v1MaxYInside)
        //    {
        //        item.Position = face.V1;
        //        item.Id = face.Id1;
        //        contactData.Add(ref item);
        //    }

        //    if (v2MinXInside && v2MaxXInside && v2MinYInside && v2MaxYInside)
        //    {
        //        item.Position = face.V2;
        //        item.Id = face.Id2;
        //        contactData.Add(ref item);
        //    }

        //    if (v3MinXInside && v3MaxXInside && v3MinYInside && v3MaxYInside)
        //    {
        //        item.Position = face.V3;
        //        item.Id = face.Id3;
        //        contactData.Add(ref item);
        //    }

        //    if (v4MinXInside && v4MaxXInside && v4MinYInside && v4MaxYInside)
        //    {
        //        item.Position = face.V4;
        //        item.Id = face.Id4;
        //        contactData.Add(ref item);
        //    }

        //    #endregion

        //    //Compute depths.
        //    TinyStructList<BoxContactData> tempData = contactData;
        //    contactData.Clear();
        //    FP clipFaceDot, faceDot;
        //    TSVector.Dot(ref clipFace.V1, ref mtd, out clipFaceDot);
        //    for (int i = 0; i < tempData.Count; i++)
        //    {
        //        tempData.Get(i, out item);
        //        TSVector.Dot(ref item.Position, ref mtd, out faceDot);
        //        item.Depth = faceDot - clipFaceDot;
        //        if (item.Depth <= 0)
        //        {
        //            contactData.Add(ref item);
        //        }
        //    }

        //    int previousCount = contactData.Count;
        //    if (previousCount >= 4) //Early finish :)
        //    {
        //        return;
        //    }

        //    #region Clip face vertices

        //    TSVector faceNormal;
        //    TSVector.Cross(ref faceY, ref faceX, out faceNormal);
        //    //inverse = 1 / faceNormal.LengthSquared();
        //    //faceNormal.x *= inverse;
        //    //faceNormal.y *= inverse;
        //    //faceNormal.z *= inverse;
        //    faceNormal.Normalize();
        //    TSVector v;
        //    FP a, b;
        //    TSVector.Dot(ref face.V1, ref faceNormal, out b);
        //    //CLIP FACE
        //    if (clipv1MinXInside && clipv1MaxXInside && clipv1MinYInside && clipv1MaxYInside)
        //    {
        //        TSVector.Dot(ref clipFace.V1, ref faceNormal, out a);
        //        TSVector.Multiply(ref faceNormal, a - b, out v);
        //        TSVector.Subtract(ref clipFace.V1, ref v, out v);
        //        item.Position = v;
        //        item.Id = clipFace.Id1 + 8;
        //        contactData.Add(ref item);
        //    }

        //    if (clipv2MinXInside && clipv2MaxXInside && clipv2MinYInside && clipv2MaxYInside)
        //    {
        //        TSVector.Dot(ref clipFace.V2, ref faceNormal, out a);
        //        TSVector.Multiply(ref faceNormal, a - b, out v);
        //        TSVector.Subtract(ref clipFace.V2, ref v, out v);
        //        item.Position = v;
        //        item.Id = clipFace.Id2 + 8;
        //        contactData.Add(ref item);
        //    }

        //    if (clipv3MinXInside && clipv3MaxXInside && clipv3MinYInside && clipv3MaxYInside)
        //    {
        //        TSVector.Dot(ref clipFace.V3, ref faceNormal, out a);
        //        TSVector.Multiply(ref faceNormal, a - b, out v);
        //        TSVector.Subtract(ref clipFace.V3, ref v, out v);
        //        item.Position = v;
        //        item.Id = clipFace.Id3 + 8;
        //        contactData.Add(ref item);
        //    }

        //    if (clipv4MinXInside && clipv4MaxXInside && clipv4MinYInside && clipv4MaxYInside)
        //    {
        //        TSVector.Dot(ref clipFace.V4, ref faceNormal, out a);
        //        TSVector.Multiply(ref faceNormal, a - b, out v);
        //        TSVector.Subtract(ref clipFace.V4, ref v, out v);
        //        item.Position = v;
        //        item.Id = clipFace.Id4 + 8;
        //        contactData.Add(ref item);
        //    }

        //    #endregion

        //    //Compute depths.
        //    int postClipCount = contactData.Count;
        //    tempData = contactData;
        //    for (int i = postClipCount - 1; i >= previousCount; i--) //TODO: >=?
        //        contactData.RemoveAt(i);


        //    for (int i = previousCount; i < tempData.Count; i++)
        //    {
        //        tempData.Get(i, out item);
        //        TSVector.Dot(ref item.Position, ref mtd, out faceDot);
        //        item.Depth = faceDot - clipFaceDot;
        //        if (item.Depth <= 0)
        //        {
        //            contactData.Add(ref item);
        //        }
        //    }

        //    previousCount = contactData.Count;
        //    if (previousCount >= 4) //Early finish :)
        //    {
        //        return;
        //    }

        //    //Intersect edges.

        //    //maxX maxY -> v1
        //    //minX maxY -> v2
        //    //minX minY -> v3
        //    //maxX minY -> v4

        //    //Once we get here there can only be 3 contacts or less.
        //    //Once 4 possible contacts have been added, switch to using safe increments.
        //    FP dot;

        //    #region CLIP EDGE: v1 v2

        //    FaceEdge clipEdge;
        //    clipFace.GetEdge(0, ref mtd, out clipEdge);
        //    if (!v1MaxYInside)
        //    {
        //        if (v2MaxYInside)
        //        {
        //            ComputeIntersection(ref face.V1, ref face.V2, ref clipEdge, out v);
        //            TSVector.Dot(ref clipX, ref v, out dot);
        //            if (dot > clipCenterMinX && dot < clipCenterMaxX)
        //            {
        //                item.Position = v;
        //                item.Id = GetContactId(face.Id1, face.Id2, ref clipEdge);
        //                contactData.Add(ref item);
        //            }
        //        }
        //        if (v4MaxYInside)
        //        {
        //            ComputeIntersection(ref face.V4, ref face.V1, ref clipEdge, out v);
        //            TSVector.Dot(ref clipX, ref v, out dot);
        //            if (dot > clipCenterMinX && dot < clipCenterMaxX)
        //            {
        //                item.Position = v;
        //                item.Id = GetContactId(face.Id4, face.Id1, ref clipEdge);
        //                contactData.Add(ref item);
        //            }
        //        }
        //    }
        //    if (!v2MaxYInside)
        //    {
        //        if (v1MaxYInside)
        //        {
        //            ComputeIntersection(ref face.V1, ref face.V2, ref clipEdge, out v);
        //            TSVector.Dot(ref clipX, ref v, out dot);
        //            if (dot > clipCenterMinX && dot < clipCenterMaxX)
        //            {
        //                item.Position = v;
        //                item.Id = GetContactId(face.Id1, face.Id2, ref clipEdge);
        //                contactData.Add(ref item);
        //            }
        //        }
        //        if (v3MaxYInside)
        //        {
        //            ComputeIntersection(ref face.V2, ref face.V3, ref clipEdge, out v);
        //            TSVector.Dot(ref clipX, ref v, out dot);
        //            if (dot > clipCenterMinX && dot < clipCenterMaxX)
        //            {
        //                item.Position = v;
        //                item.Id = GetContactId(face.Id2, face.Id3, ref clipEdge);
        //                contactData.Add(ref item);
        //            }
        //        }
        //    }
        //    if (!v3MaxYInside)
        //    {
        //        if (v2MaxYInside)
        //        {
        //            ComputeIntersection(ref face.V2, ref face.V3, ref clipEdge, out v);
        //            TSVector.Dot(ref clipX, ref v, out dot);
        //            if (dot > clipCenterMinX && dot < clipCenterMaxX)
        //            {
        //                item.Position = v;
        //                item.Id = GetContactId(face.Id2, face.Id3, ref clipEdge);
        //                contactData.Add(ref item);
        //            }
        //        }
        //        if (v4MaxYInside && contactData.Count < 8)
        //        {
        //            ComputeIntersection(ref face.V3, ref face.V4, ref clipEdge, out v);
        //            TSVector.Dot(ref clipX, ref v, out dot);
        //            if (dot > clipCenterMinX && dot < clipCenterMaxX)
        //            {
        //                item.Position = v;
        //                item.Id = GetContactId(face.Id3, face.Id4, ref clipEdge);
        //                contactData.Add(ref item);
        //            }
        //        }
        //    }
        //    if (!v4MaxYInside)
        //    {
        //        if (v1MaxYInside && contactData.Count < 8)
        //        {
        //            ComputeIntersection(ref face.V4, ref face.V1, ref clipEdge, out v);
        //            TSVector.Dot(ref clipX, ref v, out dot);
        //            if (dot > clipCenterMinX && dot < clipCenterMaxX)
        //            {
        //                item.Position = v;
        //                item.Id = GetContactId(face.Id4, face.Id1, ref clipEdge);
        //                contactData.Add(ref item);
        //            }
        //        }
        //        if (v3MaxYInside && contactData.Count < 8)
        //        {
        //            ComputeIntersection(ref face.V3, ref face.V4, ref clipEdge, out v);
        //            TSVector.Dot(ref clipX, ref v, out dot);
        //            if (dot > clipCenterMinX && dot < clipCenterMaxX)
        //            {
        //                item.Position = v;
        //                item.Id = GetContactId(face.Id3, face.Id4, ref clipEdge);
        //                contactData.Add(ref item);
        //            }
        //        }
        //    }

        //    #endregion

        //    #region CLIP EDGE: v2 v3

        //    clipFace.GetEdge(1, ref mtd, out clipEdge);
        //    if (!v1MinXInside)
        //    {
        //        if (v2MinXInside && contactData.Count < 8)
        //        {
        //            //test v1-v2 against minXminY-minXmaxY
        //            ComputeIntersection(ref face.V1, ref face.V2, ref clipEdge, out v);
        //            TSVector.Dot(ref clipY, ref v, out dot);
        //            if (dot > clipCenterMinY && dot < clipCenterMaxY)
        //            {
        //                item.Position = v;
        //                item.Id = GetContactId(face.Id1, face.Id2, ref clipEdge);
        //                contactData.Add(ref item);
        //            }
        //        }
        //        if (v4MinXInside && contactData.Count < 8)
        //        {
        //            //test v1-v3 against minXminY-minXmaxY
        //            ComputeIntersection(ref face.V4, ref face.V1, ref clipEdge, out v);
        //            TSVector.Dot(ref clipY, ref v, out dot);
        //            if (dot > clipCenterMinY && dot < clipCenterMaxY)
        //            {
        //                item.Position = v;
        //                item.Id = GetContactId(face.Id4, face.Id1, ref clipEdge);
        //                contactData.Add(ref item);
        //            }
        //        }
        //    }
        //    if (!v2MinXInside)
        //    {
        //        if (v1MinXInside && contactData.Count < 8)
        //        {
        //            ComputeIntersection(ref face.V1, ref face.V2, ref clipEdge, out v);
        //            TSVector.Dot(ref clipY, ref v, out dot);
        //            if (dot > clipCenterMinY && dot < clipCenterMaxY)
        //            {
        //                item.Position = v;
        //                item.Id = GetContactId(face.Id1, face.Id2, ref clipEdge);
        //                contactData.Add(ref item);
        //            }
        //        }
        //        if (v3MinXInside && contactData.Count < 8)
        //        {
        //            ComputeIntersection(ref face.V2, ref face.V3, ref clipEdge, out v);
        //            TSVector.Dot(ref clipY, ref v, out dot);
        //            if (dot > clipCenterMinY && dot < clipCenterMaxY)
        //            {
        //                item.Position = v;
        //                item.Id = GetContactId(face.Id2, face.Id3, ref clipEdge);
        //                contactData.Add(ref item);
        //            }
        //        }
        //    }
        //    if (!v3MinXInside)
        //    {
        //        if (v2MinXInside && contactData.Count < 8)
        //        {
        //            ComputeIntersection(ref face.V2, ref face.V3, ref clipEdge, out v);
        //            TSVector.Dot(ref clipY, ref v, out dot);
        //            if (dot > clipCenterMinY && dot < clipCenterMaxY)
        //            {
        //                item.Position = v;
        //                item.Id = GetContactId(face.Id2, face.Id3, ref clipEdge);
        //                contactData.Add(ref item);
        //            }
        //        }
        //        if (v4MinXInside && contactData.Count < 8)
        //        {
        //            ComputeIntersection(ref face.V3, ref face.V4, ref clipEdge, out v);
        //            TSVector.Dot(ref clipY, ref v, out dot);
        //            if (dot > clipCenterMinY && dot < clipCenterMaxY)
        //            {
        //                item.Position = v;
        //                item.Id = GetContactId(face.Id3, face.Id4, ref clipEdge);
        //                contactData.Add(ref item);
        //            }
        //        }
        //    }
        //    if (!v4MinXInside)
        //    {
        //        if (v1MinXInside && contactData.Count < 8)
        //        {
        //            //test v2-v4 against minXminY-minXmaxY
        //            ComputeIntersection(ref face.V4, ref face.V1, ref clipEdge, out v);
        //            TSVector.Dot(ref clipY, ref v, out dot);
        //            if (dot > clipCenterMinY && dot < clipCenterMaxY)
        //            {
        //                item.Position = v;
        //                item.Id = GetContactId(face.Id4, face.Id1, ref clipEdge);
        //                contactData.Add(ref item);
        //            }
        //        }
        //        if (v3MinXInside && contactData.Count < 8)
        //        {
        //            //test v3-v4 against minXminY-minXmaxY
        //            ComputeIntersection(ref face.V3, ref face.V4, ref clipEdge, out v);
        //            TSVector.Dot(ref clipY, ref v, out dot);
        //            if (dot > clipCenterMinY && dot < clipCenterMaxY)
        //            {
        //                item.Position = v;
        //                item.Id = GetContactId(face.Id3, face.Id4, ref clipEdge);
        //                contactData.Add(ref item);
        //            }
        //        }
        //    }

        //    #endregion

        //    #region CLIP EDGE: v3 v4

        //    clipFace.GetEdge(2, ref mtd, out clipEdge);
        //    if (!v1MinYInside)
        //    {
        //        if (v2MinYInside && contactData.Count < 8)
        //        {
        //            ComputeIntersection(ref face.V1, ref face.V2, ref clipEdge, out v);
        //            TSVector.Dot(ref clipX, ref v, out dot);
        //            if (dot > clipCenterMinX && dot < clipCenterMaxX)
        //            {
        //                item.Position = v;
        //                item.Id = GetContactId(face.Id1, face.Id2, ref clipEdge);
        //                contactData.Add(ref item);
        //            }
        //        }
        //        if (v4MinYInside && contactData.Count < 8)
        //        {
        //            ComputeIntersection(ref face.V4, ref face.V1, ref clipEdge, out v);
        //            TSVector.Dot(ref clipX, ref v, out dot);
        //            if (dot > clipCenterMinX && dot < clipCenterMaxX)
        //            {
        //                item.Position = v;
        //                item.Id = GetContactId(face.Id4, face.Id1, ref clipEdge);
        //                contactData.Add(ref item);
        //            }
        //        }
        //    }
        //    if (!v2MinYInside)
        //    {
        //        if (v1MinYInside && contactData.Count < 8)
        //        {
        //            ComputeIntersection(ref face.V1, ref face.V2, ref clipEdge, out v);
        //            TSVector.Dot(ref clipX, ref v, out dot);
        //            if (dot > clipCenterMinX && dot < clipCenterMaxX)
        //            {
        //                item.Position = v;
        //                item.Id = GetContactId(face.Id1, face.Id2, ref clipEdge);
        //                contactData.Add(ref item);
        //            }
        //        }
        //        if (v3MinYInside && contactData.Count < 8)
        //        {
        //            ComputeIntersection(ref face.V2, ref face.V3, ref clipEdge, out v);
        //            TSVector.Dot(ref clipX, ref v, out dot);
        //            if (dot > clipCenterMinX && dot < clipCenterMaxX)
        //            {
        //                item.Position = v;
        //                item.Id = GetContactId(face.Id2, face.Id3, ref clipEdge);
        //                contactData.Add(ref item);
        //            }
        //        }
        //    }
        //    if (!v3MinYInside)
        //    {
        //        if (v2MinYInside && contactData.Count < 8)
        //        {
        //            ComputeIntersection(ref face.V2, ref face.V3, ref clipEdge, out v);
        //            TSVector.Dot(ref clipX, ref v, out dot);
        //            if (dot > clipCenterMinX && dot < clipCenterMaxX)
        //            {
        //                item.Position = v;
        //                item.Id = GetContactId(face.Id2, face.Id3, ref clipEdge);
        //                contactData.Add(ref item);
        //            }
        //        }
        //        if (v4MinYInside && contactData.Count < 8)
        //        {
        //            ComputeIntersection(ref face.V3, ref face.V4, ref clipEdge, out v);
        //            TSVector.Dot(ref clipX, ref v, out dot);
        //            if (dot > clipCenterMinX && dot < clipCenterMaxX)
        //            {
        //                item.Position = v;
        //                item.Id = GetContactId(face.Id3, face.Id4, ref clipEdge);
        //                contactData.Add(ref item);
        //            }
        //        }
        //    }
        //    if (!v4MinYInside)
        //    {
        //        if (v3MinYInside && contactData.Count < 8)
        //        {
        //            ComputeIntersection(ref face.V3, ref face.V4, ref clipEdge, out v);
        //            TSVector.Dot(ref clipX, ref v, out dot);
        //            if (dot > clipCenterMinX && dot < clipCenterMaxX)
        //            {
        //                item.Position = v;
        //                item.Id = GetContactId(face.Id3, face.Id4, ref clipEdge);
        //                contactData.Add(ref item);
        //            }
        //        }
        //        if (v1MinYInside && contactData.Count < 8)
        //        {
        //            ComputeIntersection(ref face.V4, ref face.V1, ref clipEdge, out v);
        //            TSVector.Dot(ref clipX, ref v, out dot);
        //            if (dot > clipCenterMinX && dot < clipCenterMaxX)
        //            {
        //                item.Position = v;
        //                item.Id = GetContactId(face.Id4, face.Id1, ref clipEdge);
        //                contactData.Add(ref item);
        //            }
        //        }
        //    }

        //    #endregion

        //    #region CLIP EDGE: v4 v1

        //    clipFace.GetEdge(3, ref mtd, out clipEdge);
        //    if (!v1MaxXInside)
        //    {
        //        if (v2MaxXInside && contactData.Count < 8)
        //        {
        //            ComputeIntersection(ref face.V1, ref face.V2, ref clipEdge, out v);
        //            TSVector.Dot(ref clipY, ref v, out dot);
        //            if (dot > clipCenterMinY && dot < clipCenterMaxY)
        //            {
        //                item.Position = v;
        //                item.Id = GetContactId(face.Id1, face.Id2, ref clipEdge);
        //                contactData.Add(ref item);
        //            }
        //        }
        //        if (v4MaxXInside && contactData.Count < 8)
        //        {
        //            ComputeIntersection(ref face.V4, ref face.V1, ref clipEdge, out v);
        //            TSVector.Dot(ref clipY, ref v, out dot);
        //            if (dot > clipCenterMinY && dot < clipCenterMaxY)
        //            {
        //                item.Position = v;
        //                item.Id = GetContactId(face.Id4, face.Id1, ref clipEdge);
        //                contactData.Add(ref item);
        //            }
        //        }
        //    }
        //    if (!v2MaxXInside)
        //    {
        //        if (v1MaxXInside && contactData.Count < 8)
        //        {
        //            ComputeIntersection(ref face.V1, ref face.V2, ref clipEdge, out v);
        //            TSVector.Dot(ref clipY, ref v, out dot);
        //            if (dot > clipCenterMinY && dot < clipCenterMaxY)
        //            {
        //                item.Position = v;
        //                item.Id = GetContactId(face.Id1, face.Id2, ref clipEdge);
        //                contactData.Add(ref item);
        //            }
        //        }
        //        if (v3MaxXInside && contactData.Count < 8)
        //        {
        //            ComputeIntersection(ref face.V2, ref face.V3, ref clipEdge, out v);
        //            TSVector.Dot(ref clipY, ref v, out dot);
        //            if (dot > clipCenterMinY && dot < clipCenterMaxY)
        //            {
        //                item.Position = v;
        //                item.Id = GetContactId(face.Id2, face.Id3, ref clipEdge);
        //                contactData.Add(ref item);
        //            }
        //        }
        //    }
        //    if (!v3MaxXInside)
        //    {
        //        if (v2MaxXInside && contactData.Count < 8)
        //        {
        //            ComputeIntersection(ref face.V2, ref face.V3, ref clipEdge, out v);
        //            TSVector.Dot(ref clipY, ref v, out dot);
        //            if (dot > clipCenterMinY && dot < clipCenterMaxY)
        //            {
        //                item.Position = v;
        //                item.Id = GetContactId(face.Id2, face.Id3, ref clipEdge);
        //                contactData.Add(ref item);
        //            }
        //        }
        //        if (v4MaxXInside && contactData.Count < 8)
        //        {
        //            ComputeIntersection(ref face.V3, ref face.V4, ref clipEdge, out v);
        //            TSVector.Dot(ref clipY, ref v, out dot);
        //            if (dot > clipCenterMinY && dot < clipCenterMaxY)
        //            {
        //                item.Position = v;
        //                item.Id = GetContactId(face.Id3, face.Id4, ref clipEdge);
        //                contactData.Add(ref item);
        //            }
        //        }
        //    }
        //    if (!v4MaxXInside)
        //    {
        //        if (v1MaxXInside && contactData.Count < 8)
        //        {
        //            ComputeIntersection(ref face.V4, ref face.V1, ref clipEdge, out v);
        //            TSVector.Dot(ref clipY, ref v, out dot);
        //            if (dot > clipCenterMinY && dot < clipCenterMaxY)
        //            {
        //                item.Position = v;
        //                item.Id = GetContactId(face.Id4, face.Id1, ref clipEdge);
        //                contactData.Add(ref item);
        //            }
        //        }
        //        if (v3MaxXInside && contactData.Count < 8)
        //        {
        //            ComputeIntersection(ref face.V3, ref face.V4, ref clipEdge, out v);
        //            TSVector.Dot(ref clipY, ref v, out dot);
        //            if (dot > clipCenterMinY && dot < clipCenterMaxY)
        //            {
        //                item.Position = v;
        //                item.Id = GetContactId(face.Id3, face.Id4, ref clipEdge);
        //                contactData.Add(ref item);
        //            }
        //        }
        //    }

        //    #endregion

        //    //Compute depths.
        //    postClipCount = contactData.Count;
        //    tempData = contactData;
        //    for (int i = postClipCount - 1; i >= previousCount; i--)
        //        contactData.RemoveAt(i);

        //    for (int i = previousCount; i < tempData.Count; i++)
        //    {
        //        tempData.Get(i, out item);
        //        TSVector.Dot(ref item.Position, ref mtd, out faceDot);
        //        item.Depth = faceDot - clipFaceDot;
        //        if (item.Depth <= 0)
        //        {
        //            contactData.Add(ref item);
        //        }
        //    }
        //}
#endif

        private static bool ComputeIntersection(ref TSVector edgeA1, ref TSVector edgeA2, ref FaceEdge clippingEdge, out TSVector intersection)
        {
            //Intersect the incoming edge (edgeA1, edgeA2) with the clipping edge's PLANE.  Nicely given by one of its positions and its 'perpendicular,'
            //which is its normal.

            TSVector offset;
            TSVector.Subtract(ref clippingEdge.A, ref edgeA1, out offset);

            TSVector edgeDirection;
            TSVector.Subtract(ref edgeA2, ref edgeA1, out edgeDirection);
            FP distanceToPlane;
            TSVector.Dot(ref offset, ref clippingEdge.Perpendicular, out distanceToPlane);
            FP edgeDirectionLength;
            TSVector.Dot(ref edgeDirection, ref clippingEdge.Perpendicular, out edgeDirectionLength);
            FP t = distanceToPlane / edgeDirectionLength;
            if (t < FP.Zero || t > FP.One)
            {
                //It's outside of the incoming edge!
                intersection = TSVector.zero;
                return false;
            }
            TSVector.Multiply(ref edgeDirection, t, out offset);
            TSVector.Add(ref offset, ref edgeA1, out intersection);

            TSVector.Subtract(ref intersection, ref clippingEdge.A, out offset);
            TSVector.Subtract(ref clippingEdge.B, ref clippingEdge.A, out edgeDirection);
            TSVector.Dot(ref edgeDirection, ref offset, out t);
            if (t < FP.Zero || t > edgeDirection.sqrMagnitude)
            {
                //It's outside of the clipping edge!
                return false;
            }
            return true;
        }

        private static void GetNearestFace(ref TSVector position, ref TSMatrix orientation, ref TSVector mtd, FP halfWidth, FP halfHeight, FP halfLength, out BoxFace boxFace)
        {
            boxFace = new BoxFace();

            FP xDot = orientation.M11 * mtd.x +
                         orientation.M12 * mtd.y +
                         orientation.M13 * mtd.z;
            FP yDot = orientation.M21 * mtd.x +
                         orientation.M22 * mtd.y +
                         orientation.M23 * mtd.z;
            FP zDot = orientation.M31 * mtd.x +
                         orientation.M32 * mtd.y +
                         orientation.M33 * mtd.z;

            FP absX = FP.Abs(xDot);
            FP absY = FP.Abs(yDot);
            FP absZ = FP.Abs(zDot);

            TSMatrix4x4 worldTransform;
            TSMatrix.ToMatrix4X4(ref orientation, out worldTransform);
            worldTransform.M41 = position.x;
            worldTransform.M42 = position.y;
            worldTransform.M43 = position.z;
            worldTransform.M44 = FP.One;

            TSVector candidate;
            int bit;
            if (absX > absY && absX > absZ)
            {
                //"X" faces are candidates
                if (xDot < FP.Zero)
                {
                    halfWidth = -halfWidth;
                    bit = 0;
                }
                else
                    bit = 1;
                candidate = new TSVector(halfWidth, halfHeight, halfLength);
                TSVector.Transform(ref candidate, ref worldTransform, out candidate);
                boxFace.V1 = candidate;
                candidate = new TSVector(halfWidth, -halfHeight, halfLength);
                TSVector.Transform(ref candidate, ref worldTransform, out candidate);
                boxFace.V2 = candidate;
                candidate = new TSVector(halfWidth, -halfHeight, -halfLength);
                TSVector.Transform(ref candidate, ref worldTransform, out candidate);
                boxFace.V3 = candidate;
                candidate = new TSVector(halfWidth, halfHeight, -halfLength);
                TSVector.Transform(ref candidate, ref worldTransform, out candidate);
                boxFace.V4 = candidate;

                if (xDot < FP.Zero)
                    boxFace.Normal = orientation.Left;
                else
                    boxFace.Normal = orientation.Right;

                boxFace.Width = halfHeight * 2;
                boxFace.Height = halfLength * 2;

                boxFace.Id1 = bit + 2 + 4;
                boxFace.Id2 = bit + 4;
                boxFace.Id3 = bit + 2;
                boxFace.Id4 = bit;
            }
            else if (absY > absX && absY > absZ)
            {
                //"Y" faces are candidates
                if (yDot < FP.Zero)
                {
                    halfHeight = -halfHeight;
                    bit = 0;
                }
                else
                    bit = 2;
                candidate = new TSVector(halfWidth, halfHeight, halfLength);
                TSVector.Transform(ref candidate, ref worldTransform, out candidate);
                boxFace.V1 = candidate;
                candidate = new TSVector(-halfWidth, halfHeight, halfLength);
                TSVector.Transform(ref candidate, ref worldTransform, out candidate);
                boxFace.V2 = candidate;
                candidate = new TSVector(-halfWidth, halfHeight, -halfLength);
                TSVector.Transform(ref candidate, ref worldTransform, out candidate);
                boxFace.V3 = candidate;
                candidate = new TSVector(halfWidth, halfHeight, -halfLength);
                TSVector.Transform(ref candidate, ref worldTransform, out candidate);
                boxFace.V4 = candidate;

                if (yDot < FP.Zero)
                    boxFace.Normal = orientation.Down;
                else
                    boxFace.Normal = orientation.Up;

                boxFace.Width = halfWidth * 2;
                boxFace.Height = halfLength * 2;

                boxFace.Id1 = 1 + bit + 4;
                boxFace.Id2 = bit + 4;
                boxFace.Id3 = 1 + bit;
                boxFace.Id4 = bit;
            }
            else if (absZ > absX && absZ > absY)
            {
                //"Z" faces are candidates
                if (zDot < FP.Zero)
                {
                    halfLength = -halfLength;
                    bit = 0;
                }
                else
                    bit = 4;
                candidate = new TSVector(halfWidth, halfHeight, halfLength);
                TSVector.Transform(ref candidate, ref worldTransform, out candidate);
                boxFace.V1 = candidate;
                candidate = new TSVector(-halfWidth, halfHeight, halfLength);
                TSVector.Transform(ref candidate, ref worldTransform, out candidate);
                boxFace.V2 = candidate;
                candidate = new TSVector(-halfWidth, -halfHeight, halfLength);
                TSVector.Transform(ref candidate, ref worldTransform, out candidate);
                boxFace.V3 = candidate;
                candidate = new TSVector(halfWidth, -halfHeight, halfLength);
                TSVector.Transform(ref candidate, ref worldTransform, out candidate);
                boxFace.V4 = candidate;

                if (zDot < FP.Zero)
                    boxFace.Normal = orientation.Forward;
                else
                    boxFace.Normal = orientation.Backward;

                boxFace.Width = halfWidth * 2;
                boxFace.Height = halfHeight * 2;

                boxFace.Id1 = 1 + 2 + bit;
                boxFace.Id2 = 2 + bit;
                boxFace.Id3 = 1 + bit;
                boxFace.Id4 = bit;
            }
        }


        private struct BoxFace
        {
            public int Id1, Id2, Id3, Id4;
            public TSVector V1, V2, V3, V4;
            public TSVector Normal;
            public FP Width, Height;

            public int GetId(int i)
            {
                switch (i)
                {
                    case 0:
                        return Id1;
                    case 1:
                        return Id2;
                    case 2:
                        return Id3;
                    case 3:
                        return Id4;
                }
                return -1;
            }

            public void GetVertex(int i, out TSVector v)
            {
                switch (i)
                {
                    case 0:
                        v = V1;
                        return;
                    case 1:
                        v = V2;
                        return;
                    case 2:
                        v = V3;
                        return;
                    case 3:
                        v = V4;
                        return;
                }
                v = TSVector.Negate(TSVector.one) * FP.MaxValue;
            }

            internal void GetEdge(int i, out FaceEdge clippingEdge)
            {
                TSVector insidePoint;
                switch (i)
                {
                    case 0:
                        clippingEdge.A = V1;
                        clippingEdge.B = V2;
                        insidePoint = V3;
                        clippingEdge.Id = GetEdgeId(Id1, Id2);
                        break;
                    case 1:
                        clippingEdge.A = V2;
                        clippingEdge.B = V3;
                        insidePoint = V4;
                        clippingEdge.Id = GetEdgeId(Id2, Id3);
                        break;
                    case 2:
                        clippingEdge.A = V3;
                        clippingEdge.B = V4;
                        insidePoint = V1;
                        clippingEdge.Id = GetEdgeId(Id3, Id4);
                        break;
                    case 3:
                        clippingEdge.A = V4;
                        clippingEdge.B = V1;
                        insidePoint = V2;
                        clippingEdge.Id = GetEdgeId(Id4, Id1);
                        break;
                    default:
                        throw new IndexOutOfRangeException();
                }
                //TODO: Edge direction and perpendicular not normalized.
                TSVector edgeDirection;
                TSVector.Subtract(ref clippingEdge.B, ref clippingEdge.A, out edgeDirection);
                edgeDirection.Normalize();
                TSVector.Cross(ref edgeDirection, ref Normal, out clippingEdge.Perpendicular);

                FP dot;
                TSVector offset;
                TSVector.Subtract(ref insidePoint, ref clippingEdge.A, out offset);
                TSVector.Dot(ref clippingEdge.Perpendicular, ref offset, out dot);
                if (dot > FP.Zero)
                {
                    clippingEdge.Perpendicular.x = -clippingEdge.Perpendicular.x;
                    clippingEdge.Perpendicular.y = -clippingEdge.Perpendicular.y;
                    clippingEdge.Perpendicular.z = -clippingEdge.Perpendicular.z;
                }
                TSVector.Dot(ref clippingEdge.A, ref clippingEdge.Perpendicular, out clippingEdge.EdgeDistance);
            }
        }

        private static int GetContactId(int vertexAEdgeA, int vertexBEdgeA, int vertexAEdgeB, int vertexBEdgeB)
        {
            return GetEdgeId(vertexAEdgeA, vertexBEdgeA) * 2549 + GetEdgeId(vertexAEdgeB, vertexBEdgeB) * 2857;
        }

        private static int GetContactId(int vertexAEdgeA, int vertexBEdgeA, ref FaceEdge clippingEdge)
        {
            return GetEdgeId(vertexAEdgeA, vertexBEdgeA) * 2549 + clippingEdge.Id * 2857;
        }

        private static int GetEdgeId(int id1, int id2)
        {
            return (id1 + 1) * 571 + (id2 + 1) * 577;
        }

        private struct FaceEdge : IEquatable<FaceEdge>
        {
            public TSVector A, B;
            public FP EdgeDistance;
            public int Id;
            public TSVector Perpendicular;

            #region IEquatable<FaceEdge> Members

            public bool Equals(FaceEdge other)
            {
                return other.Id == Id;
            }

            #endregion

            public bool IsPointInside(ref TSVector point)
            {
                FP distance;
                TSVector.Dot(ref point, ref Perpendicular, out distance);
                return distance < EdgeDistance; // +1; //TODO: Bias this a little?
            }
        }
    }
}
