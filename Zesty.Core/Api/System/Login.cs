using Zesty.Core.Common;
using Zesty.Core.Entities;
using Zesty.Core.Exceptions;

namespace Zesty.Core.Api.System
{
    public class Login : ApiHandlerBase
    {
        public override ApiHandlerOutput Process(ApiInputHandler input)
        {
            LoginRequest request = GetEntity<LoginRequest>(input);

            LoginOutput loginOutput = Business.User.Login(request.Username, request.Password);

            if (loginOutput.Result == LoginResult.Failed)
            {
                throw new ApiAccessDeniedException(Messages.LoginFailed);
            }
            else if (loginOutput.Result == LoginResult.PasswordExpired)
            {
                throw new ApiAccessDeniedException(Messages.PasswordExpired);
            }

            LoginResponse response = new LoginResponse()
            {
                Output = loginOutput
            };

            input.Context.Session.Set(response.Output.User);

            return GetOutput(response);
        }
    }

    public class LoginRequest
    {
        [Required]
        public string Username { get; set; }
        public string Domain { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public int Number { get; set; }
    }

    public class LoginResponse
    {
        public LoginOutput Output { get; set; }
    }
}
