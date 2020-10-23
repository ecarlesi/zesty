using Zesty.Core.Entities;

namespace Zesty.Core.Api.System
{
    public class Token : ApiHandlerBase
    {
        public override ApiHandlerOutput Process(ApiInputHandler input)
        {
            TokenRequest request = GetEntity<TokenRequest>(input);

            return GetOutput(new TokenResponse()
            {
                Text = Business.Authorization.GetToken(input.Context.Session.Id, request == null ? false : request.IsReusable)
            });
        }
    }

    public class TokenResponse
    {
        public string Text { get; set; }
    }

    public class TokenRequest
    {
        public bool IsReusable { get; set; }
    }
}
