using System;
using System.Collections.Generic;
using Zesty.Core.Common;
using Zesty.Core.Entities;
using Zesty.Core.Exceptions;

namespace Zesty.Core.Api.System
{
    public class Roles : ApiHandlerBase
    {
        public override ApiHandlerOutput Process(ApiInputHandler input)
        {
            RolesRequest request = GetEntity<RolesRequest>(input);

            if (Context.Current == null || Context.Current.User == null || String.IsNullOrWhiteSpace(Context.Current.User.Username) || request == null || String.IsNullOrWhiteSpace(request.Domain))
            {
                throw new ApiApplicationErrorException("User or domain not found");
            }

            RolesResponse response = new RolesResponse();

            response.Roles = Business.User.GetRoles(Context.Current.User.Username, request.Domain);

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

    public class RolesResponse
    {
        public List<string> Roles { get; set; }
    }

    public class RolesRequest
    {
        public string Domain { get; set; }
    }
}
