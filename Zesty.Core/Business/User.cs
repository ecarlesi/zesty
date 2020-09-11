using System;
using Zesty.Core.Common;
using Zesty.Core.Entities.Settings;
using Zesty.Core.Exceptions;

namespace Zesty.Core.Business
{
    public static class User
    {
        private static NLog.Logger logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

        public static Entities.User Login(string username, string domain, string password)
        {
            logger.Info($"Login request for user {username} on domain {domain}");

            Entities.User user = StorageManager.Instance.Login(username, domain, password);

            if (user == null)
            {
                return null;
            }

            int passwordDays = (int)DateTime.Now.Subtract(user.PasswordChanged).TotalDays;

            if (passwordDays >= Settings.Current.PasswordLifetimeInDays)
            {
                throw new PasswordExpiredException();
            }

            user.Properties = StorageManager.Instance.LoadProperties(user);

            string json = JsonHelper.Serialize(user);

            logger.Info(json);

            return user;
        }
    }
}
