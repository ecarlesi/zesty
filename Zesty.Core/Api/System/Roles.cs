using System;
using System.Collections.Generic;
using Zesty.Core.Entities;

namespace Zesty.Core.Api.System
{
    public class Roles : ApiHandlerBase
    {
        public override ApiHandlerOutput Process(ApiInputHandler input)
        {
            RolesRequest request = GetEntity<RolesRequest>(input);

            return GetOutput(new RolesResponse()
            {
                Roles = Business.User.GetRoles(Context.Current.User.Username, Guid.Parse(request.Domain))
            });
        }
    }

    public class RolesResponse
    {
        public List<Entities.Role> Roles { get; set; }
    }

    public class RolesRequest
    {
        [Required]
        public string Domain { get; set; }
    }
}
