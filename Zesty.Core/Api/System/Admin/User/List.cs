using System;
using System.Collections.Generic;
using Zesty.Core.Entities;

namespace Zesty.Core.Api.System.Admin.User
{
    public class List : ApiHandlerBase
    {
        public override ApiHandlerOutput Process(ApiInputHandler input)
        {
            ListResposne response = new ListResposne()
            {
                Users = Business.User.List()
            };

            return GetOutput(response);
        }
    }

    public class ListResposne
    {
        public List<Entities.User> Users { get; set; }
    }
}
