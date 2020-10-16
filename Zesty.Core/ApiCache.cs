using System;
using System.Collections.Generic;
using System.Linq;
using Zesty.Core.Entities;
using Zesty.Core.Entities.Settings;

namespace Zesty.Core
{
    internal class ApiCache
    {
        private static object LOCK = new object();

        private static double lifetimeMS = Settings.GetInt("ApiCacheLifetimeInMinutes") * 60 * 1000;

        private static List<ApiCacheItem> cache = new List<ApiCacheItem>();

        public static ApiCacheItem Get(ApiInputHandler input)
        {
            ApiCacheItem item = cache.Where(x => x.Resource == input.Resource && x.Payload == input.Body).FirstOrDefault();

            if (item == null)
            {
                return null;
            }

            if (DateTime.Now.Subtract(item.Created).TotalMilliseconds > lifetimeMS)
            {
                lock (LOCK)
                {
                    if (cache.Contains(item))
                    {
                        cache.Remove(item);
                    }
                }

                return null;
            }

            return item;
        }

        public static void Store(ApiCacheItem item)
        {
            cache.Add(item);
        }
    }
}
