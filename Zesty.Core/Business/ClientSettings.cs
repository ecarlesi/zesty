using System;
using System.Collections.Generic;

namespace Zesty.Core.Business
{
    static class ClientSettings
    {
        internal static Dictionary<string, string> All()
        {
            return StorageManager.Instance.GetClientSettings();
        }
    }
}
