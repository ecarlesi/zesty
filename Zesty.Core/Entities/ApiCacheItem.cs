using System;
namespace Zesty.Core.Entities
{
    internal class ApiCacheItem
    {
        public string Resource { get; set; }
        public string Payload { get; set; }
        public ApiHandlerOutput Output { get; set; }
        public DateTime Created { get; set; }
    }
}
