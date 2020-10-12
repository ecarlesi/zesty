using Microsoft.AspNetCore.Http;

namespace Zesty.Core.Entities
{
    public class ApiInputHandler
    {
        public string Resource { get; set; }
        public string Body { get; set; }
        public HttpContext Context { get; set; }

        public string Get(string parameterName)
        {
            if (Context == null || Context.Request == null)
            {
                return "";
            }

            return Context.Request.Query[parameterName];
        }
    }
}
