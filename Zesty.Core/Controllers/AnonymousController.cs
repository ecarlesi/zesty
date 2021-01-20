using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Zesty.Core.Common;
using Zesty.Core.Entities;
using Zesty.Core.Entities.Settings;

namespace Zesty.Core.Controllers
{
    [ResponseCache(Duration = -1, Location = ResponseCacheLocation.None, NoStore = true)]
    public class AnonymousController : Controller
    {
        private static readonly NLog.Logger logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

        private TimeKeeper timeKeeper = new TimeKeeper();

        protected ISession Session
        {
            get
            {
                return HttpContext.Session;
            }
        }

        protected HttpContext CurrentHttpContext
        {
            get
            {
                return HttpContext;
            }
        }

        protected string ErrorMessage
        {
            get
            {
                if (Session == null)
                {
                    return null;
                }

                if (Session.GetString(Keys.SessionLastError) == null)
                {
                    return null;
                }

                string errorMessage = Session.GetString(Keys.SessionLastError);

                Session.Remove(Keys.SessionLastError);

                return errorMessage;
            }
            set
            {
                Session.SetString(Keys.SessionLastError, value);
            }
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);

            Context.Current.Reset();

            string scheme = HttpContext.Request.Scheme;
            string host = HttpContext.Request.Host.Value;
            string path = HttpContext.Request.Path;
            string queryString = HttpContext.Request.QueryString.HasValue ? HttpContext.Request.QueryString.Value : "";

            string url = $"{scheme}://{host}{path}{queryString}";

            logger.Info($"Request {url} with session id {Session.Id}");

            HandlerProcessor.Process(Settings.List("PreExecutionHandler"), context.HttpContext);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);

            double executionMS = timeKeeper.Stop().TotalMilliseconds;

            logger.Info($"Execution require {executionMS} ms");

            Trace.Write(new TraceItem()
            {
                Millis = executionMS
            },
            context.HttpContext);

            HandlerProcessor.Process(Settings.List("PostExecutionHandler"), context.HttpContext);
        }
    }
}
