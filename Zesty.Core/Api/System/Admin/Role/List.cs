using System.Collections.Generic;
using Zesty.Core.Entities;

namespace Zesty.Core.Api.System.Admin.Role
{
    public class List : ApiHandlerBase
    {
        public override ApiHandlerOutput Process(ApiInputHandler input)
        {
            return GetOutput(new ListResponse()
            {
                Roles = Business.Role.List()
            });
        }
    }

    public class ListResponse
    {
        public List<Entities.Role> Roles { get; set; }
    }
}
