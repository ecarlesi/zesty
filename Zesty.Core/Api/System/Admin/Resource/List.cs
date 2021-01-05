using System;
using System.Collections.Generic;
using Zesty.Core.Entities;

namespace Zesty.Core.Api.System.Admin.Resource
{
    public class List : ApiHandlerBase
    {
        public override ApiHandlerOutput Process(ApiInputHandler input)
        {
            ListResponse response = new ListResponse()
            {
                Resources = Business.Resource.ResourceList()
            };

            return GetOutput(response);
        }
    }

    public class ListResponse
    {
        public List<Entities.Resource> Resources { get; set; }
    }
}
