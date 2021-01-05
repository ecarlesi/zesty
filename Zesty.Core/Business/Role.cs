using System;
using System.Collections.Generic;

namespace Zesty.Core.Business
{
    public class Role
    {
        private static NLog.Logger logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

        public static List<Entities.Role> List()
        {
            return StorageManager.Instance.GetRoles();
        }

        public static void Add(Entities.Role role)
        {
            StorageManager.Instance.Add(role);
        }
    }
}
