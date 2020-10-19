using System;
using Zesty.Core.Common;
using Zesty.Core.Entities;
using Zesty.Core.Exceptions;

namespace Zesty.Core.Api.System
{
    public class ResetPassword : ApiHandlerBase
    {
        public override ApiHandlerOutput Process(ApiInputHandler input)
        {
            ResetPasswordRequest request = base.GetEntity<ResetPasswordRequest>(input);

            if (!Business.User.ResetPassword(request.Token, request.Password))
            {
                throw new ApiInvalidArgumentException(Messages.TokenMissing);
            }

            ResetPasswordResponse response = new ResetPasswordResponse()
            {
                Result = Messages.Success
            };

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

    public class ResetPasswordRequest
    {
        public Guid Token { get; set; }
        public string Password { get; set; }
    }

    public class ResetPasswordResponse
    {
        public string Result { get; set; }
    }
}
