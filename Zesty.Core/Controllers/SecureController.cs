using System.Linq;
using System.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Zesty.Core.Business;
using Zesty.Core.Common;
using Zesty.Core.Entities.Settings;

namespace Zesty.Core.Controllers
{
    [ResponseCache(Duration = -1, Location = ResponseCacheLocation.None, NoStore = true)]
    public class SecureController : AnonymousController
    {
        private static NLog.Logger logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            string scheme = HttpContext.Request.Scheme;
            string host = HttpContext.Request.Host.Value;
            string path = HttpContext.Request.Path;
            string queryString = HttpContext.Request.QueryString.HasValue ? HttpContext.Request.QueryString.Value : "";

            string url = $"{scheme}://{host}{path}{queryString}";

            logger.Info($"Request: {url}");

            string item = Settings.Current.UrlWhitelist.Where(x => x == path).FirstOrDefault();

            if (item != null)
            {
                return;
            }

            Entities.User user = Session.Get<Entities.User>(Keys.SessionUser);

            if (user == null)
            {
                logger.Info($"User is null with session id {Session.Id}");

                Session.Clear();

                if (Settings.Current.ThrowsOnAccessDenied)
                {
                    logger.Warn($"Access denied for resource {path}");

                    throw new SecurityException(Messages.AccessDenied);
                }
                else
                {
                    ErrorMessage = Messages.AccessDenied;

                    Redirect(Settings.Current.RedirectPathOnAccessDenied);
                }
            }
            else
            {
                bool canAccess = Authorization.CanAccess(path, user);

                logger.Info($"User {user.Username} can access path {path}: {canAccess}");

                if (!canAccess)
                {
                    Session.Clear();

                    if (Settings.Current.ThrowsOnAuthorizationFailed)
                    {
                        logger.Warn($"Access denied for resource {path}");

                        throw new SecurityException(Messages.AuthorizationFailed);
                    }
                    else
                    {
                        ErrorMessage = Messages.AuthorizationFailed;

                        Redirect(Settings.Current.RedirectPathOnAccessDenied);
                    }
                }
                else
                {
                    Context.Current.User = user;

                    if (Authorization.RequireToken(path))
                    {
                        string tokenValue = CurrentHttpContext.Request.Query["t"];

                        logger.Info($"Token: {tokenValue}");

                        if (!Authorization.IsValid(user.Id, CurrentHttpContext.Session.Id, tokenValue))
                        {
                            if (Settings.Current.ThrowsOnAuthorizationFailed)
                            {
                                logger.Warn($"Access denied for resource {path}");

                                throw new SecurityException(Messages.TokenMissing);
                            }
                            else
                            {
                                ErrorMessage = Messages.AuthorizationFailed;

                                Redirect(Settings.Current.RedirectPathOnAccessDenied);
                            }
                        }
                    }
                }
            }
        }
    }
}
