using System;
using Zesty.Core.Entities;
using Zesty.Core.Exceptions;

namespace Zesty.Core.Api.System
{
    public class UserByResetToken : ApiHandlerBase
    {
        public override ApiHandlerOutput Process(ApiInputHandler input)
        {
            Guid token = Guid.Parse(input.Context.Request.Query["token"]);

            UserByResetTokenResponse response = new UserByResetTokenResponse()
            {
                User = Business.User.Get(token)
            };

            if (response.User == null)
            {
                throw new ApiNotFoundException(token.ToString());
            }

            return GetOutput(response);
        }
    }

    public class UserByResetTokenResponse
    {
        public User User { get; set; }
    }
}
