using System;
using System.Text;
using JWT.Algorithms;
using JWT.Builder;
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

            if (request.Bearer == "true" && loginOutput.User != null)
            {
                string secret = HashHelper.GetSha256(request.Password);

                var p = loginOutput.User.Properties;

                loginOutput.User.Properties.Clear();

                string token = JwtBuilder.Create()
                      .WithAlgorithm(new HMACSHA256Algorithm())
                      .WithSecret(secret)
                      .AddClaim("exp", DateTimeOffset.UtcNow.AddHours(12).ToUnixTimeSeconds())
                      .AddClaim("user", response.Output.User)
                      .Encode();

                logger.Debug($"token generated: {token}");

                Business.User.SaveBearer(loginOutput.User.Id, token);

                response.Bearer = token;

                loginOutput.User.Properties = p;
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
