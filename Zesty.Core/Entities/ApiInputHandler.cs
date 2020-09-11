using Microsoft.AspNetCore.Http;

namespace Zesty.Core.Entities
{
    public class ApiInputHandler
    {
        public string Resource { get; set; }
        public string Body { get; set; }
        public HttpContext Context { get; set; }
    }
}
