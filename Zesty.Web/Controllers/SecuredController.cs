using Microsoft.AspNetCore.Mvc;
using Zesty.Core;
using Zesty.Core.Business;
using Zesty.Core.Common;
using Zesty.Core.Controllers;

namespace Zesty.Web.Controllers
{
    public class SecuredController : SecureController
    {
        public ActionResult Hello()
        {
            return Content($"Hi {Context.Current.User.Username}", "text/html");
        }

        public ActionResult Login()
        {
            Context.Current.User = Core.Business.User.Login("eca", "Domain A", "password");

            Session.Set(Context.Current.User);

            return Redirect("Hello");
        }

        public ActionResult Logout()
        {
            Authorization.Logout(base.CurrentHttpContext);

            return Content($"Logged out!", "text/html");
        }
    }
}