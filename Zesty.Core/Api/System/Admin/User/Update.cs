using System;
using Zesty.Core.Entities;

namespace Zesty.Core.Api.System.Admin.User
{
    public class Update : ApiHandlerBase
    {
        public override ApiHandlerOutput Process(ApiInputHandler input)
        {
            UpdateRequest request = GetEntity<UpdateRequest>(input);

            Entities.User user = new Entities.User()
            {
                Id = Guid.Parse(request.Id),
                Username = request.Username,
                Email = request.Email,
                Firstname = request.Firstname,
                Lastname = request.Lastname
            };

            Business.User.Update(user);

            return GetOutput();
        }
    }

    public class UpdateRequest
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
    }
}
