using Zesty.Core.Entities;

namespace Zesty.Core.Api.System
{
    public class Check : ApiHandlerBase
    {
        public override ApiHandlerOutput Process(ApiInputHandler input)
        {
            CheckResponse response = new CheckResponse()
            {
                Message = Messages.Success
            };

            return GetOutput(response);
        }
    }

    public class CheckResponse
    {
        public string Message { get; set; }
    }
}
