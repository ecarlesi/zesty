using System;
using Zesty.Core.Entities;

namespace Zesty.Core.Api.System.Admin.User
{
    public class Delete : ApiHandlerBase
    {
        public override ApiHandlerOutput Process(ApiInputHandler input)
        {
            RequireUser();

            Guid userId = Guid.Parse(Get(input, "id"));

            if (Context.Current.User.Id == userId)
            {
                ThrowApplicationError("Cannot delete current user");
            }

            Business.User.Delete(userId);

            return GetOutput();
        }
    }
}
