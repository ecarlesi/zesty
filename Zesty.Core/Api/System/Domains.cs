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
            DomainsResponse response = new DomainsResponse();

            if (Context.Current == null || Context.Current.User == null || String.IsNullOrWhiteSpace(Context.Current.User.Username))
            {
                throw new ApiApplicationErrorException("User not found");
            }

            response.Domains = Business.User.GetDomains(Context.Current.User.Username);

            return GetOutput(response);
        }
    }

    public class DomainsResponse
    {
        public List<Entities.Domain> Domains { get; set; }
    }
}
