using Zesty.Core.Common;
using Zesty.Core.Entities;

namespace Zesty.Core.Api.System
{
    public class Token : ApiHandlerBase
    {
        public override ApiHandlerOutput Process(ApiInputHandler input)
        {
            TokenRequest request = GetEntity<TokenRequest>(input);

            TokenResponse response = new TokenResponse();

            response.Text = Business.Authorization.GetToken(input.Context.Session.Id, request == null ? false : request.IsReusable);

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

    public class TokenResponse
    {
        public string Text { get; set; }
    }

    public class TokenRequest
    {
        public bool IsReusable { get; set; }
    }
}
