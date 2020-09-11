using Zesty.Core;
using Zesty.Core.Common;
using Zesty.Core.Entities;

namespace Zesty.Web.Api
{
    public class Login : ApiHandlerBase
    {
        public override ApiHandlerOutput Process(ApiInputHandler input)
        {
            LoginRequest request = GetEntity<LoginRequest>(input);

            LoginResponse response = new LoginResponse();

            response.User = Core.Business.User.Login(request.Username, request.Domain, request.Password);

            input.Context.Session.Set(response.User);

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
        public User User { get; set; }
    }
}
