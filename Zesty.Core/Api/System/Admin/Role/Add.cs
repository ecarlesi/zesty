using System;
using Zesty.Core.Entities;

namespace Zesty.Core.Api.System.Admin.Role
{
    public class Add : ApiHandlerBase
    {
        public override ApiHandlerOutput Process(ApiInputHandler input)
        {
            AddRequest request = GetEntity<AddRequest>(input);

            Entities.Role role = new Entities.Role()
            {
                Id = Guid.NewGuid(),
                Name = request.Name
            };

            Business.Role.Add(role);

            AddResponse response = new AddResponse()
            {
                Role = role
            };

            return GetOutput(response);
        }
    }

    public class AddRequest
    {
        [Required]
        public string Name { get; set; }
    }

    public class AddResponse
    {
        public Entities.Role Role { get; set; }
    }
}
