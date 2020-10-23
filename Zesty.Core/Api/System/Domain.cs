using System.Collections.Generic;
using System.Linq;
using Zesty.Core.Common;
using Zesty.Core.Entities;
using Zesty.Core.Exceptions;

namespace Zesty.Core.Api.System
{
    public class Domain : ApiHandlerBase
    {
        public override ApiHandlerOutput Process(ApiInputHandler input)
        {
            DomainRequest request = GetEntity<DomainRequest>(input);

            List<Entities.Domain> domains = Business.User.GetDomains(Context.Current.User.Username);

            Entities.Domain domain = domains.Where(x => x.Id.ToString() == request.Domain || x.Name == request.Domain).FirstOrDefault();

            if (domain == null)
            {
                throw new ApiInvalidArgumentException(request.Domain);
            }

            Context.Current.User.Domain = domain;

            DomainResponse response = new DomainResponse()
            {
                User = Context.Current.User
            };

            input.Context.Session.Set(Context.Current.User);

            return GetOutput(response);
        }
    }

    public class DomainRequest
    {
        [Required]
        public string Domain { get; set; }
    }

    public class DomainResponse
    {
        public User User { get; set; }
    }
}
