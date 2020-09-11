using System;
using Zesty.Core.Common;
using Zesty.Core.Entities;
using Zesty.Core.Entities.Settings;

namespace Zesty.Core.Business
{
    public static class User
    {
        private static NLog.Logger logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

        public static LoginOutput Login(string username, string domain, string password)
        {
            logger.Info($"Login request for user {username} on domain {domain}");

            LoginOutput output = new LoginOutput()
            {
                Result = LoginResult.Success,
                User = StorageManager.Instance.Login(username, domain, password)
            };

            if (output.User == null)
            {
                output.Result = LoginResult.Failed;
            }
            else
            {
                int passwordDays = (int)DateTime.Now.Subtract(output.User.PasswordChanged).TotalDays;

                if (passwordDays >= Settings.Current.PasswordLifetimeInDays)
                {
                    output.Result = LoginResult.PasswordExpired;
                }
                else
                {
                    output.User.Properties = StorageManager.Instance.LoadProperties(output.User);
                }
            }

            string json = JsonHelper.Serialize(output);

            logger.Info(json);

            return output;
        }
    }
}
