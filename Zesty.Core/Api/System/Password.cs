using System;
using Zesty.Core.Entities;
using Zesty.Core.Exceptions;

namespace Zesty.Core.Api.System
{
    public class Password : ApiHandlerBase
    {
        public override ApiHandlerOutput Process(ApiInputHandler input)
        {
            PasswordRequest request = GetEntity<PasswordRequest>(input);

            LoginOutput loginOutput = Business.User.Login(Context.Current.User.Username, request.Old);

            if (loginOutput.Result == LoginResult.Failed)
            {
                throw new ApiAccessDeniedException(Messages.WrongPassword);
            }

            if (request.Old == request.New)
            {
                throw new ApplicationException(Messages.PasswordChangeSame);
            }

            if (request.New != request.Confirm)
            {
                throw new ApplicationException(Messages.PasswordDontMatch);
            }

            Business.User.ChangePassword(Context.Current.User.Id, request.Old, request.New);

            return GetOutput(new PasswordResponse()
            {
                Message = "Success"
            });
        }
    }

    public class PasswordRequest
    {
        [Required]
        public string Old { get; set; }
        [Required]
        public string New { get; set; }
        [Required]
        public string Confirm { get; set; }
    }

    public class PasswordResponse
    {
        public string Message { get; set; }
    }
}
