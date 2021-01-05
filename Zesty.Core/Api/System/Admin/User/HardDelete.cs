using System;
using Zesty.Core.Entities;

namespace Zesty.Core.Api.System.Admin.User
{
    public class HardDelete : ApiHandlerBase
    {
        public override ApiHandlerOutput Process(ApiInputHandler input)
        {
            Business.User.HardDelete(Guid.Parse(Get(input, "id")));

            return GetOutput();
        }
    }
}
