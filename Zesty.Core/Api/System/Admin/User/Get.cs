using System;
using Zesty.Core.Entities;

namespace Zesty.Core.Api.System.Admin.User
{
    public class Get : ApiHandlerBase
    {
        public override ApiHandlerOutput Process(ApiInputHandler input)
        {
            GetResponse response = new GetResponse()
            {
                User = Business.User.Get(Get(input, "id"))
            };

            return GetOutput(response);
        }
    }

    public class GetResponse
    {
        public Entities.User User { get; set; }
    }
}
