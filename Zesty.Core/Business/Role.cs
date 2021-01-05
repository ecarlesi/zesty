using System.Collections.Generic;

namespace Zesty.Core.Business
{
    class Role
    {
        private static IStorage storage = StorageManager.Instance;

        internal static List<Entities.Role> List()
        {
            return storage.GetRoles();
        }

        internal static void Add(Entities.Role role)
        {
            storage.Add(role);
        }
    }
}
