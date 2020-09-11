using System;
using System.Security;
using Microsoft.AspNetCore.Http;

namespace Zesty.Core.Business
{
    public class Authorization
    {
        private static NLog.Logger logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

        public static void Logout(HttpContext context)
        {
            Context.Current.Reset();

            context.Session.Clear();
            context.Response.Headers.Clear();
            context.Request.Headers.Clear();
        }

        internal static bool CanAccess(string path, Entities.User user)
        {
            //TODO add cache support

            bool isPublic = StorageManager.Instance.IsPublicResource(path);

            if (isPublic)
            {
                return true;
            }

            if (user == null)
            {
                logger.Warn($"Access denied for resource {path}");

                throw new SecurityException(Messages.AccessDenied);
            }

            //TODO add cache support

            return StorageManager.Instance.CanAccess(path, user);
        }

        public static string GetToken(string sessionId, bool reusable)
        {
            string tokenValue = (Guid.NewGuid().ToString() + Guid.NewGuid().ToString()).Replace("-", "");

            StorageManager.Instance.SaveToken(Context.Current.User, sessionId, tokenValue, reusable);

            return tokenValue;
        }

        internal static bool RequireToken(string path)
        {
            //TODO add cache support

            return StorageManager.Instance.RequireToken(path);
        }

        internal static bool IsValid(Guid userId, string sessionId, string tokenValue)
        {
            return StorageManager.Instance.IsValid(userId, sessionId, tokenValue);
        }
    }
}
