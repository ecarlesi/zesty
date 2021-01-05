using Zesty.Core.Entities;

namespace Zesty.Core.Api.System
{
    public class Check : ApiHandlerBase
    {
        public override ApiHandlerOutput Process(ApiInputHandler input)
        {
            return GetOutput(new { Message = Messages.Success });
        }
    }
}
