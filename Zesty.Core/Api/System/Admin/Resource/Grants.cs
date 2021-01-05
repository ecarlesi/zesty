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

            GrantsResponse response = new GrantsResponse()
            {
                Resources = Business.Resource.ResourceList(roleId)
            };

            return GetOutput(response);
        }
    }

    public class GrantsResponse
    {
        public List<Entities.Resource> Resources { get; set; }
    }
}
