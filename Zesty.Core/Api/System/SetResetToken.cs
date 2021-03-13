using System;
using System.Collections.Generic;
using System.Linq;
using Zesty.Core.Entities;
using Zesty.Core.Exceptions;

namespace Zesty.Core.Api.System
{
    public class SetResetToken : ApiHandlerBase
    {
        public override ApiHandlerOutput Process(ApiInputHandler input)
        {
            string email = input.Context.Request.Query["email"];

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ApiInvalidArgumentException("email");
            }

            Guid token = Business.User.SetResetToken(email);

            if (token != Guid.Empty)
            {
                List<Translation> translations = Business.Languages.GetTranslations("en");

                string subject = translations.Where(x => x.Original == "Reset password").FirstOrDefault().Translated;
                string body = translations.Where(x => x.Original == "Password reset token: {0}").FirstOrDefault().Translated;

                body = String.Format(body, token.ToString());

                Common.SmtpClient.Send(email, subject, body);

                logger.Info($"Password reset email set to email address {email}");
            }

            return GetOutput();
        }
    }
}
