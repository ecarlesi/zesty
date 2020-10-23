using System.Collections.Generic;
using Zesty.Core.Entities;

namespace Zesty.Core.Api.System
{
    public class Resources : ApiHandlerBase
    {
        public override ApiHandlerOutput Process(ApiInputHandler input)
        {
            IsNotNull(Context.Current.User.Domain, "domain");

            ResourcesResponse response = new ResourcesResponse()
            {
                Resources = Business.Resource.GetResources(Context.Current.User.Username, Context.Current.User.Domain.Id)
            };

            return GetOutput(response);
        }
    }

    public class ResourcesResponse
    {
        public List<Resource> Resources { get; set; }
    }
}
