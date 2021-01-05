using System;
using Zesty.Core.Entities;

namespace Zesty.Core.Api.Sample
{
    public class Free : ApiHandlerBase
    {
        public override ApiHandlerOutput Process(ApiInputHandler input)
        {
            return GetOutput(new { Message = Guid.NewGuid() });
        }
    }

    public class FreeResponse
    {
        public string Message { get; set; }
    }
}
