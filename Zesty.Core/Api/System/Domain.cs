using System;
using Zesty.Core.Common;
using Zesty.Core.Entities;

namespace Zesty.Core.Api.System
{
    public class Domain : ApiHandlerBase
    {
        public override ApiHandlerOutput Process(ApiInputHandler input)
        {
            DomainRequest request = GetEntity<DomainRequest>(input);

            Context.Current.User.Domain = request.Domain;

            DomainResponse response = new DomainResponse()
            {
                User = Context.Current.User
            };

            input.Context.Session.Set(Context.Current.User);

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

    public class DomainRequest
    {
        public string Domain { get; set; }
    }

    public class DomainResponse
    {
        public User User { get; set; }
    }
}
