using System;
using System.Collections.Generic;

namespace TrueSync
{
	internal class ResourcePoolListSyncedData : ResourcePool<ResourcePoolItemList<SyncedData>>
	{
		protected override ResourcePoolItemList<SyncedData> NewInstance()
		{
			return new ResourcePoolItemList<SyncedData>();
		}
	}
}
