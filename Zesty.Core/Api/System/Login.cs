using Zesty.Core.Common;
using Zesty.Core.Entities;

namespace Zesty.Core.Api.System
{
    public class Login : ApiHandlerBase
    {
        public override ApiHandlerOutput Process(ApiInputHandler input)
        {
            LoginRequest request = GetEntity<LoginRequest>(input);

            LoginResponse response = new LoginResponse()
            {
                Output = Business.User.Login(request.Username, request.Domain, request.Password)
            };

            if (response.Output != null && response.Output.Result == LoginResult.Success && response.Output.User != null)
            {
                input.Context.Session.Set(response.Output.User);
            }

            return new ApiHandlerOutput()
            {
                Output = response,
                Type = ApiHandlerOutputType.JSon,
                ResourceHistoryOutput = new ApiResourceHistoryOutput()
                {
                    Item = new HistoryItem()
                    {
                        Resource = input.Resource,
                        Text = JsonHelper.Serialize(response),
                        User = Context.Current.User,
                        Actor = this.GetType().ToString()
                    },
                    ResourceHistoryPolicy = ApiResourceHistoryPolicy.None
                },
                CachePolicy = ApiCachePolicy.Disable
            };
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Domain { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponse
    {
        public LoginOutput Output { get; set; }
    }
}
