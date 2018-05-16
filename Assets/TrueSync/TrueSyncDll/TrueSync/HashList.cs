using System;
using System.Collections;
using System.Collections.Generic;

namespace TrueSync
{
    public class HashList<T> : C5.HashedLinkedList<T>
    {
        public void AddRange(IEnumerable<T> iCollection)
        {
            base.AddAll(iCollection);
        }
    }
}
