using System;
namespace Zesty.Core.Entities
{
    public class ApiHandlerOutput
    {
        public ApiHandlerOutputType Type { get; set; }
        public object Output { get; set; }
        public ApiCachePolicy CachePolicy { get; set; }
        public ApiResourceHistoryOutput ResourceHistoryOutput { get; set; }
    }
}
