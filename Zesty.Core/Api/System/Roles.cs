using System;
using System.Collections.Generic;
using System.Linq;
using Zesty.Core.Common;
using Zesty.Core.Entities;
using Zesty.Core.Exceptions;

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
        public List<string> Roles { get; set; }
    }

    public class RolesRequest
    {
        [Required]
        public string Domain { get; set; }
    }
}
