using Zesty.Core.Common;
using Zesty.Core.Entities.Settings;

namespace Zesty.Core
{
    public class StorageManager
    {
        public static IStorage Instance { get; private set; }

        static StorageManager()
        {
            Instance = InstanceHelper.Create<IStorage>(Settings.Current.StorageType);
        }
    }
}
