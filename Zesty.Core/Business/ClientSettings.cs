using System.Collections.Generic;

namespace Zesty.Core.Business
{
    static class ClientSettings
    {
        private static IStorage storage = StorageManager.Storage;

        internal static Dictionary<string, string> All()
        {
            return storage.GetClientSettings();
        }
    }
}
