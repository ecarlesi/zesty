using System;
using Zesty.Core.Entities;

namespace Zesty.Core.Api.System.Admin.User
{
    public class Get : ApiHandlerBase
    {
        public override ApiHandlerOutput Process(ApiInputHandler input)
        {
            string userId = Get(input, "id");

            Entities.User user = Business.User.Get(userId);

            if (user == null)
            {
                ThrowNotFound(userId);
            }

            return GetOutput(new GetResponse()
            {
                User = user
            });
        }
    }

    public class GetResponse
    {
        public Entities.User User { get; set; }
    }
}
