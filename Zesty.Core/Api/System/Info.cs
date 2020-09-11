using System;
using Zesty.Core.Common;
using Zesty.Core.Entities;

namespace Zesty.Core.Api.System
{
    public class Info : ApiHandlerBase
    {
        public override ApiHandlerOutput Process(ApiInputHandler input)
        {
            InfoResponse response = new InfoResponse()
            {
                User = Context.Current.User
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

    public class InfoResponse
    {
        public User User { get; set; }
    }
}
