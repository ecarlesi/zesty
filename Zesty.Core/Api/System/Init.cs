using System;
using Microsoft.AspNetCore.Http;
using Zesty.Core.Common;
using Zesty.Core.Entities;

namespace Zesty.Core.Api.System
{
    public class Init : ApiHandlerBase
    {
        public override ApiHandlerOutput Process(ApiInputHandler input)
        {
            input.Context.Session.SetString("init", Guid.NewGuid().ToString());

            InitResponse response = new InitResponse()
            {
                Message = Messages.Success
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
                    ResourceHistoryPolicy = ApiResourceHistoryPolicy.Save
                },
                CachePolicy = ApiCachePolicy.Enable
            };
        }
    }

    public class InitResponse
    {
        public string Message { get; set; }
    }
}
