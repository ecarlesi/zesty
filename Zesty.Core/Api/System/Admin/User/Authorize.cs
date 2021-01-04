using System;
using Zesty.Core.Entities;

namespace Zesty.Core.Api.System.Admin.User
{
    public class Authorize : ApiHandlerBase
    {
        public override ApiHandlerOutput Process(ApiInputHandler input)
        {
            AuthorizeRequest request = GetEntity<AuthorizeRequest>(input);

            Business.User.Authorize(
                new Entities.User()
                {
                    Id = Guid.Parse(request.User)
                },
                new Entities.Authorization()
                {
                    Domain = new Entities.Domain()
                    {
                        Id = Guid.Parse(request.Domain)
                    },
                    Role = new Entities.Role()
                    {
                        Id = Guid.Parse(request.Role)
                    }
                });

            return GetOutput(new { });
        }
    }

    public class AuthorizeRequest
    {
        [Required]
        public string User { get; set; }
        [Required]
        public string Domain { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
