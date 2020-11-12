using System;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Zesty.Core.Handlers
{
    public class PostLogger : IExecutionHandler
    {
        private static NLog.Logger logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

        public void Process(HttpContext context)
        {
            logger.Info($"PostLogger executed");

#if DEBUG

            ISession session = context.Session;

            string resourceName = context.Request.Path.Value;
            string body = new StreamReader(context.Request.Body).ReadToEndAsync().Result;

            logger.Debug($"Resource: {resourceName}");
            logger.Debug($"Body: {body}");
            logger.Debug($"Session ID: {session.Id}");
#endif
        }
    }
}
