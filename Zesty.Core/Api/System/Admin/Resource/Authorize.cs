using System;
using Zesty.Core.Entities;

namespace Zesty.Core.Api.System.Admin.Resource
{
    public class Authorize : ApiHandlerBase
    {
        public override ApiHandlerOutput Process(ApiInputHandler input)
        {
            AuthorizeRequest request = GetEntity<AuthorizeRequest>(input);

            Business.Resource.Authorize(Guid.Parse(request.Resource), Guid.Parse(request.Role));

            return GetOutput();
        }
    }

    public class AuthorizeRequest
    {
        [Required]
        public string Resource { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
