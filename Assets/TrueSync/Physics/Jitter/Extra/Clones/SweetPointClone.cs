namespace TrueSync.Physics3D {
    
    public class SweetPointClone : ResourcePoolItem
    {
		public IBroadphaseEntity body;
		public bool begin;
		public int axis;

		public void Clone(SweepPoint sp) {
			body = sp.Body;
			begin = sp.Begin;
			axis = sp.Axis;
		}

		public void Restore(SweepPoint sp) {
			sp.Body = body;
			sp.Begin = begin;
			sp.Axis = axis;
		}

        public void CleanUp()
        {
            this.body = null;
            this.begin = false;
            this.axis = -1;
        }
    }

}