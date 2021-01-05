using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace Zesty.Core.Common
{
    public static class Cache
    {
        private static object Lock = new object();
        private static int LifetimeInMinutes = 5;
        private static readonly List<CacheItem> items = new List<CacheItem>();
        private static readonly NLog.Logger logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

        static Cache()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            IConfigurationRoot config = builder.Build();

            string minute = config["Zesty:CacheLifetimeInMinutes"];

            if (!string.IsNullOrWhiteSpace(minute))
            {
                try
                {
                    LifetimeInMinutes = int.Parse(minute);
                }
                catch { }
            }

            logger.Info($"LifetimeInMinutes: {LifetimeInMinutes}");
        }

        public static void Store<T>(CacheKey key, T value, StorePolicy storePolicy)
        {
            logger.Debug($"Request to store with key {key.Key}@{key.Domain}");

            T present = Get<T>(key);

            if (present == null)
            {
                lock (Lock)
                {
                    CacheItem cacheItem = items.Where(x => x.Key.Domain == key.Domain && x.Key.Key == key.Key).FirstOrDefault();

                    if (cacheItem == null)
                    {
                        cacheItem = new CacheItem()
                        {
                            Inserted = DateTime.Now,
                            Key = key,
                            Value = value
                        };

                        items.Add(cacheItem);

                        logger.Debug($"Stored with key {key.Key}@{key.Domain}");
                    }
                }
            }
            else
            {
                if (storePolicy == StorePolicy.SkipIfExists)
                {
                    logger.Debug($"Store skipped with key {key.Key}@{key.Domain}");

                    return;
                }

                Remove(key);

                lock (Lock)
                {
                    CacheItem cacheItem = items.Where(x => x.Key.Domain == key.Domain && x.Key.Key == key.Key).FirstOrDefault();

                    if (cacheItem == null)
                    {
                        cacheItem = new CacheItem()
                        {
                            Inserted = DateTime.Now,
                            Key = key,
                            Value = value
                        };

                        items.Add(cacheItem);

                        logger.Debug($"Stored with key {key.Key}@{key.Domain}");
                    }
                }
            }
        }

        private static void Remove(CacheKey key)
        {
            logger.Debug($"Remove with key {key.Key}@{key.Domain}");

            CacheItem present = items.Where(x => x.Key.Domain == key.Domain && x.Key.Key == key.Key).FirstOrDefault();

            if (present != null)
            {
                lock (Lock)
                {
                    present = items.Where(x => x.Key.Domain == key.Domain && x.Key.Key == key.Key).FirstOrDefault();

                    if (present != null)
                    {
                        items.Remove(present);

                        logger.Debug($"Removed key {key.Key}@{key.Domain}");
                    }
                }
            }
        }

        public static T Get<T>(CacheKey key)
        {
            logger.Debug($"Get with key {key.Key}@{key.Domain}");

            CacheItem present = items.Where(x => x.Key.Domain == key.Domain && x.Key.Key == key.Key).FirstOrDefault();

            if (present == null)
            {
                logger.Debug($"Key not found {key.Key}@{key.Domain}");

                return default;
            }

            DateTime expired = present.Inserted.AddMinutes(LifetimeInMinutes);

            logger.Debug($"Object with key {key.Key}@{key.Domain} expire at {expired}");

            if (expired < DateTime.Now)
            {
                logger.Debug($"Object expired with key {key.Key}@{key.Domain}");

                lock (Lock)
                {
                    if (items.Contains(present))
                    {
                        items.Remove(present);

                        logger.Debug($"Object expired removed with key {key.Key}@{key.Domain}");
                    }
                }

                return default;
            }

            logger.Debug($"Object found with key {key.Key}@{key.Domain}");

            return (T)present.Value;
        }
    }

    public enum StorePolicy
    {
        SkipIfExists, ReplaceIfExists
    }

    public class CacheKey
    {
        public string Domain { get; set; }
        public string Key { get; set; }
    }

    public class CacheItem
    {
        public CacheKey Key { get; set; }
        public DateTime Inserted { get; set; }
        public object Value { get; set; }
    }

    public static class CacheDomains
    {
        public static readonly string Settings = "Settings";
    }
}
