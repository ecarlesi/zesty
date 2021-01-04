using System;
using Zesty.Core.Entities;

namespace Zesty.Core.Api.System.Admin.User
{
    public class Delete : ApiHandlerBase
    {
        public override ApiHandlerOutput Process(ApiInputHandler input)
        {
            Business.User.Delete(Guid.Parse(Get(input, "id")));

            return GetOutput(new { });
        }
    }
}
