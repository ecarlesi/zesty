using System;
using System.Collections.Generic;

namespace Zesty.Core.Business
{
    public class Domain
    {
        private static NLog.Logger logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

        public static List<Entities.Domain> List()
        {
            return StorageManager.Instance.GetDomainsList();
        }
    }
}
