using Zesty.Core;
using Zesty.Core.Common;
using Zesty.Core.Entities;

namespace Zesty.Web.Api
{
    public class Logout : ApiHandlerBase
    {
        public override ApiHandlerOutput Process(ApiInputHandler input)
        {
            Core.Business.Authorization.Logout(input.Context);

            LogoutResponse response = new LogoutResponse()
            {
                Message = "done"
            };

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


    public class LogoutResponse
    {
        public string Message { get; set; }
    }
}
