using System;
using System.Collections.Generic;
using Zesty.Core.Entities;

namespace Zesty.Core.Api.System.Admin.Resource
{
    public class Grants : ApiHandlerBase
    {
        public override ApiHandlerOutput Process(ApiInputHandler input)
        {
            Guid roleId = Guid.Parse(Get(input, "r"));

            return GetOutput(new GrantsResponse()
            {
                Resources = Business.Resource.ResourceList(roleId)
            });
        }
    }

    public class GrantsResponse
    {
        public List<Entities.Resource> Resources { get; set; }
    }
}
