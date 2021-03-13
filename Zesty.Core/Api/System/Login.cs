using System;
using System.Text;
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

            if (request.Bearer == "true")
            {
                string bearer = GetNewBearer();
                Business.User.CreateBearer(response.Output.User.Id, input.Context.Session.Id, bearer);
                response.Bearer = bearer;
                Context.Current.Bearer = bearer;

                logger.Info($"Bearer created: {bearer}");
            }

            input.Context.Session.Set(response.Output.User);

            return GetOutput(response);
        }

        private static string GetNewBearer()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < 5; i++)
            {
                sb.Append(Guid.NewGuid().ToString().Replace("-", ""));
            }

            return sb.ToString();
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
        public string Bearer { get; set; }
    }

    public class LoginResponse
    {
        public LoginOutput Output { get; set; }
        public string Bearer { get; set; }
    }
}
