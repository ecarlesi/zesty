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
        private static List<CacheItem> items = new List<CacheItem>();

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
        }

        public static void Store<T>(CacheKey key, T value, StorePolicy storePolicy)
        {
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
                    }
                }
            }
            else
            {
                if (storePolicy == StorePolicy.SkipIfExists)
                {
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
                    }
                }
            }
        }

        private static void Remove(CacheKey key)
        {
            CacheItem present = items.Where(x => x.Key.Domain == key.Domain && x.Key.Key == key.Key).FirstOrDefault();

            if (present != null)
            {
                lock (Lock)
                {
                    present = items.Where(x => x.Key.Domain == key.Domain && x.Key.Key == key.Key).FirstOrDefault();

                    if (present != null)
                    {
                        items.Remove(present);
                    }
                }
            }
        }

        public static T Get<T>(CacheKey key)
        {
            CacheItem present = items.Where(x => x.Key.Domain == key.Domain && x.Key.Key == key.Key).FirstOrDefault();

            if (present == null)
            {
                return default;
            }

            if (present.Inserted.AddMinutes(LifetimeInMinutes) > DateTime.Now)
            {
                lock (Lock)
                {
                    if (items.Contains(present))
                    {
                        items.Remove(present);
                    }
                }

                return default;
            }

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
