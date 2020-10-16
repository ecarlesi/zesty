using System;
using System.Collections.Generic;
using System.Linq;
using Zesty.Core.Common;
using Zesty.Core.Entities;

namespace Zesty.Core.Api.System
{
    public class SetResetToken : ApiHandlerBase
    {
        public override ApiHandlerOutput Process(ApiInputHandler input)
        {
            string email = input.Context.Request.Query["email"];

            SetResetTokenResponse response = new SetResetTokenResponse();

            try
            {
                Guid token = Business.User.SetResetToken(email);

                List<Translation> translations = StorageManager.Instance.GetTranslations("en");

                string subject = translations.Where(x => x.Original == "Reset password").FirstOrDefault().Translated;
                string body = translations.Where(x => x.Original == "Password reset token: {0}").FirstOrDefault().Translated;

                body = String.Format(body, token.ToString());

                Common.SmtpClient.Send(email, subject, body);

                response.Message = Messages.Success;
            }
            catch(Exception e)
            {
                logger.Error(e);

                response.Message = Messages.Failure;
            }

            return new ApiHandlerOutput()
            {
                Output = response,
                Type = ApiHandlerOutputType.JSon,
                ResourceHistoryOutput = new ApiResourceHistoryOutput()
                {
                    Item = new HistoryItem()
                    {
                        Resource = input.Resource,
                        Text = JsonHelper.Serialize(response),
                        User = Context.Current.User,
                        Actor = this.GetType().ToString()
                    },
                    ResourceHistoryPolicy = ApiResourceHistoryPolicy.None
                },
                CachePolicy = ApiCachePolicy.Disable
            };
        }
    }

    public class SetResetTokenResponse
    {
        public string Message { get; set; }
    }
}
