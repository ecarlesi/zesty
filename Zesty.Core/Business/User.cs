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

        internal static void Authorize(Entities.User user, Entities.Authorization authorization)
        {
            StorageManager.Instance.Add(user, authorization);
        }

        internal static void Deauthorize(Entities.User user, Entities.Authorization authorization)
        {
            StorageManager.Instance.Remove(user, authorization);
        }

        internal static void Update(Entities.User user)
        {
            StorageManager.Instance.Update(user);
        }

        internal static Entities.User Get(string user)
        {
            Entities.User u = StorageManager.Instance.GetUser(user);

            u.Authorizations = new List<Entities.Authorization>();

            List<Entities.Domain> domains = StorageManager.Instance.GetDomains(u.Username);

            foreach (Entities.Domain domain in domains)
            {
                List<Entities.Role> roles = GetRoles(u.Username, domain.Id);

                foreach (Entities.Role role in roles)
                {
                    u.Authorizations.Add(new Entities.Authorization() { Domain = domain, Role = role });
                }
            }

            return u;
        }

        internal static List<Entities.User> List()
        {
            List<Entities.User> users = StorageManager.Instance.Users();

            foreach (Entities.User user in users)
            {
                user.Authorizations = new List<Entities.Authorization>();

                List<Entities.Domain> domains = StorageManager.Instance.GetDomains(user.Username);

                foreach (Entities.Domain domain in domains)
                {
                    List<Entities.Role> roles = GetRoles(user.Username, domain.Id);

                    foreach (Entities.Role role in roles)
                    {
                        user.Authorizations.Add(new Entities.Authorization() { Domain = domain, Role = role });
                    }
                }
            }

            return users;
        }

        internal static void HardDelete(Guid id)
        {
            StorageManager.Instance.HardDeleteUser(id);
        }

        internal static void Delete(Guid id)
        {
            StorageManager.Instance.DeleteUser(id);
        }

        internal static void Add(Entities.User user)
        {
            StorageManager.Instance.Add(user);
        }

        internal static void ChangePassword(Guid id, string oldPassword, string password)
        {
            StorageManager.Instance.ChangePassword(id, oldPassword, password);
        }

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

        public static List<Entities.Role> GetRoles(string username, Guid domain)
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

                if (passwordDays >= Settings.GetInt("PasswordLifetimeInDays"))
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
