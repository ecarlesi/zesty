using System.Collections.Generic;
using Zesty.Core.Entities;

namespace Zesty.Core.Api.System.Admin.Domain
{
    public class List : ApiHandlerBase
    {
        public override ApiHandlerOutput Process(ApiInputHandler input)
        {
            return GetOutput(new ListResponse()
            {
                Domains = Business.Domain.List()
            });
        }
    }

    public class ListResponse
    {
        public List<Entities.Domain> Domains { get; set; }
    }
}
