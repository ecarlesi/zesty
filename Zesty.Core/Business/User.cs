using System;
using System.Collections.Generic;
using Zesty.Core.Common;
using Zesty.Core.Entities;
using Zesty.Core.Entities.Settings;

namespace Zesty.Core.Business
{
    public static class User
    {
        private static NLog.Logger logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

        public static Guid SetResetToken(string email)
        {
            return StorageManager.Instance.SetResetToken(email);
        }

        public static bool ResetPassword(Guid token, string password)
        {
            return StorageManager.Instance.ResetPassword(token, password);
        }

        public static Entities.User Get(Guid resetToken)
        {
            return StorageManager.Instance.GetUserByResetToken(resetToken);
        }

        public static bool ChangePassword(string username, string currentPassword, string newPassword)
        {
            return StorageManager.Instance.ChangePassword(username, currentPassword, newPassword);
        }

        public static List<Entities.Domain> GetDomains(string username)
        {
            return StorageManager.Instance.GetDomains(username);
        }

        public static List<string> GetRoles(string username, string domain)
        {
            return StorageManager.Instance.GetRoles(username, domain);
        }

        public static LoginOutput Login(string username, string password)
        {
            logger.Info($"Login request for user {username}");

            LoginOutput output = new LoginOutput()
            {
                Result = LoginResult.Success,
                User = StorageManager.Instance.Login(username, password)
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
