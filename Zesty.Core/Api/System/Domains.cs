using System;
using System.Collections.Generic;
using Zesty.Core.Common;
using Zesty.Core.Entities;
using Zesty.Core.Exceptions;

namespace Zesty.Core.Api.System
{
    public class Domains : ApiHandlerBase
    {
        public override ApiHandlerOutput Process(ApiInputHandler input)
        {
            DomainsResponse response = new DomainsResponse();

            if (Context.Current == null || Context.Current.User == null || String.IsNullOrWhiteSpace(Context.Current.User.Username))
            {
                throw new ApiApplicationErrorException("User not found");
            }

            response.Domains = Business.User.GetDomains(Context.Current.User.Username);

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

    public class DomainsResponse
    {
        public List<string> Domains { get; set; }
    }
}
