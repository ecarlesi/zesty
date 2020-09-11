using System;

namespace Zesty.Core.Business
{
    static class History
    {
        internal static void Save(Entities.HistoryItem resource)
        {
            StorageManager.Instance.Save(resource);
        }
    }
}
