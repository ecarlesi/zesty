using System.Collections.Generic;
using Zesty.Core.Entities;

namespace Zesty.Core.Api.System.Admin.Resource
{
    public class List : ApiHandlerBase
    {
        public override ApiHandlerOutput Process(ApiInputHandler input)
        {
            return GetOutput(new ListResponse()
            {
                Resources = Business.Resource.ResourceList()
            });
        }
    }

    public class ListResponse
    {
        public List<Entities.Resource> Resources { get; set; }
    }
}
