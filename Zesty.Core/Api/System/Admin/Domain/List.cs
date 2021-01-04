using System;
using System.Collections.Generic;
using Zesty.Core.Entities;

namespace Zesty.Core.Api.System.Admin.Domain
{
    public class List : ApiHandlerBase
    {
        public override ApiHandlerOutput Process(ApiInputHandler input)
        {
            ListResponse response = new ListResponse()
            {
                Domains = Business.Domain.List()
            };

            return GetOutput(response);
        }
    }

    public class ListResponse
    {
        public List<Entities.Domain> Domains { get; set; }
    }
}
