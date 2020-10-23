using Zesty.Core.Entities;

namespace Zesty.Core.Api.System
{
    public class Logout : ApiHandlerBase
    {
        public override ApiHandlerOutput Process(ApiInputHandler input)
        {
            Business.Authorization.Logout(input.Context);

            return GetOutput(new LogoutResponse()
            {
                Message = Messages.Success
            });
        }
    }


    public class LogoutResponse
    {
        public string Message { get; set; }
    }
}
