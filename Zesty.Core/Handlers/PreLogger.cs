using Microsoft.AspNetCore.Http;

namespace Zesty.Core.Handlers
{
    public class PreLogger : IExecutionHandler
    {
        private static NLog.Logger logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

        public void Process(HttpContext context)
        {
            logger.Info($"PreLogger executed");
        }
    }
}
