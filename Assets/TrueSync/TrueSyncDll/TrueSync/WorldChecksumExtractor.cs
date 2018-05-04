using System;
using System.Collections.Generic;
using System.Text;

namespace TrueSync
{
	public class WorldChecksumExtractor : ChecksumExtractor
	{
		private StringBuilder sb = new StringBuilder();

		public WorldChecksumExtractor(IPhysicsManagerBase physicsManager) : base(physicsManager)
		{
		}

		protected override string GetChecksum()
		{
			this.sb.Length = 0;
			List<IBody> list = this.physicsManager.GetWorld().Bodies();
			int i = 0;
			int count = list.Count;
            FP checkSum = FP.Zero;
			while (i < count)
			{
				IBody body = list[i];
                checkSum += body.Checksum();
				i++;
			}
            this.sb.Append(checkSum);
			return this.sb.ToString();
		}
	}
}
