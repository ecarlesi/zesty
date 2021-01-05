using System.Collections.Generic;
using Zesty.Core.Entities;

namespace Zesty.Core.Api.System
{
    public class Resources : ApiHandlerBase
    {
        public override ApiHandlerOutput Process(ApiInputHandler input)
        {
            RequireDomain();

            return GetOutput(new ResourcesResponse()
            {
                Resources = Business.Resource.GetResources(Context.Current.User.Username, Context.Current.User.Domain.Id)
            });
        }
    }

    public class ResourcesResponse
    {
        public List<Resource> Resources { get; set; }
    }
}
