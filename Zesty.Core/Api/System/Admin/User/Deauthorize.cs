using System;
using Zesty.Core.Entities;

namespace Zesty.Core.Api.System.Admin.User
{
    public class Deauthorize : ApiHandlerBase
    {
        public override ApiHandlerOutput Process(ApiInputHandler input)
        {
            DeauthorizeRequest request = GetEntity<DeauthorizeRequest>(input);

            Business.User.Deauthorize(
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

            return GetOutput();
        }
    }

    public class DeauthorizeRequest
    {
        [Required]
        public string User { get; set; }
        [Required]
        public string Domain { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
