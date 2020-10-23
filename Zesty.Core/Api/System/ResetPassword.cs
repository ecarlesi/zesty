using System;
using Zesty.Core.Entities;
using Zesty.Core.Exceptions;

namespace Zesty.Core.Api.System
{
    public class ResetPassword : ApiHandlerBase
    {
        public override ApiHandlerOutput Process(ApiInputHandler input)
        {
            ResetPasswordRequest request = base.GetEntity<ResetPasswordRequest>(input);

            if (!Business.User.ResetPassword(Guid.Parse(request.Token), request.Password))
            {
                throw new ApiInvalidArgumentException(Messages.TokenMissing);
            }

            return GetOutput(new ResetPasswordResponse()
            {
                Result = Messages.Success
            });
        }
    }

    public class ResetPasswordRequest
    {
        [Required]
        public string Token { get; set; }
        [Required]
        public string Password { get; set; }
    }

    public class ResetPasswordResponse
    {
        public string Result { get; set; }
    }
}
