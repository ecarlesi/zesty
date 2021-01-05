using System;

namespace Zesty.Core.Business
{
    static class History
    {
        private static IStorage storage = StorageManager.Instance;

        internal static void Save(Entities.HistoryItem resource)
        {
            storage.Save(resource);
        }
    }
}
