using System;
using System.Collections.Generic;

namespace Zesty.Core.Business
{
    static class Resource
    {
        internal static string GetType(string resourceName)
        {
            //TODO add cache support

            return StorageManager.Instance.GetType(resourceName);
        }

        internal static List<Entities.Resource> GetResources(string username, Guid domainId)
        {
            //TODO add cache support

            return StorageManager.Instance.GetResources(username, domainId);
        }

        internal static List<Entities.Resource> ResourceList()
        {
            //TODO add cache support

            return StorageManager.Instance.GetResources();
        }

        internal static List<Entities.Resource> ResourceList(Guid roleId)
        {
            //TODO add cache support

            return StorageManager.Instance.GetResources(roleId);
        }

        internal static void Authorize(Guid resourceId, Guid roleId)
        {
            StorageManager.Instance.AuthorizeResource(resourceId, roleId);
        }

        internal static void Deauthorize(Guid resourceId, Guid roleId)
        {
            StorageManager.Instance.DeauthorizeResource(resourceId, roleId);
        }
    }
}
