using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Zesty.Core.Entities.Settings
{
    public class Settings
    {
        static Settings()
        {
            IConfigurationBuilder builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            IConfigurationRoot config = builder.Build();

            Current = config.GetSection("Zesty").Get<Settings>();

            logger.Info(Messages.SettingsLoaded);
        }

        private static NLog.Logger logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

        private static object Lock = new object();

        public static Settings Current { get; set; }

        public bool ThrowsOnAccessDenied { get; set; }
        public bool ThrowsOnAuthorizationFailed { get; set; }

        public string ConnectionString { get; set; }
        public string RedirectPathOnAccessDenied { get; set; }
        public string StorageType { get; set; }
        public string[] CorsOrigins { get; set; }

        public int PasswordLifetimeInDays { get; set; }
        public int ApiCacheLifetimeInMinutes { get; set; }
        public int SessionLifetimeInMinutes { get; set; }

        public List<string> UrlWhitelist { get; set; }
        public List<string> PreExecutionHandler { get; set; }
        public List<string> PostExecutionHandler { get; set; }

        public SmtpClient SmtpClient { get; set; }
    }
}
