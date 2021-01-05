using System;
using System.Collections.Generic;
using Zesty.Core.Entities;
using Zesty.Core.Exceptions;

namespace Zesty.Core.Api.System
{
    public class Domains : ApiHandlerBase
    {
        public override ApiHandlerOutput Process(ApiInputHandler input)
        {
            RequireUser();

            return GetOutput(new DomainsResponse()
            {
                Domains = Business.User.GetDomains(Context.Current.User.Username)
            });
        }
    }

    public class DomainsResponse
    {
        public List<Entities.Domain> Domains { get; set; }
    }
}
