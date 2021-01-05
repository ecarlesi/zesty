using System.Collections.Generic;
using Zesty.Core.Entities;

namespace Zesty.Core.Api.System.Admin.User
{
    public class List : ApiHandlerBase
    {
        public override ApiHandlerOutput Process(ApiInputHandler input)
        {
            return GetOutput(new ListResposne()
            {
                Users = Business.User.List()
            });
        }
    }

    public class ListResposne
    {
        public List<Entities.User> Users { get; set; }
    }
}
