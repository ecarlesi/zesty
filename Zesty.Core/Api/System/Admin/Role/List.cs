using System;
using System.Collections.Generic;
using Zesty.Core.Entities;

namespace Zesty.Core.Api.System.Admin.Role
{
    public class List : ApiHandlerBase
    {
        public override ApiHandlerOutput Process(ApiInputHandler input)
        {
            ListResponse response = new ListResponse()
            {
                Roles = Business.Role.List()
            };

            return GetOutput(response);
        }
    }

    public class ListResponse
    {
        public List<Entities.Role> Roles { get; set; }
    }
}
