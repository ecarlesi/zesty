using Zesty.Core.Common;
using Zesty.Core.Entities.Settings;

namespace Zesty.Core
{
    internal class StorageManager
    {
        internal static IStorage Storage { get; private set; }

        static StorageManager()
        {
            Storage = InstanceHelper.Create<IStorage>(Settings.Current.StorageImplementationType);
        }
    }
}
