using System;
using Zesty.Core.Entities;

namespace Zesty.Core.Api.System.Admin.Domain
{
    public class Add : ApiHandlerBase
    {
        public override ApiHandlerOutput Process(ApiInputHandler input)
        {
            AddRequest request = GetEntity<AddRequest>(input);

            Entities.Domain domain = new Entities.Domain()
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                ParentDomainId = String.IsNullOrWhiteSpace(request.Parent) ? Guid.Empty : Guid.Parse(request.Parent)
            };

            Business.Domain.Add(domain);

            AddResponse response = new AddResponse()
            {
                Domain = domain
            };

            return GetOutput(response);
        }
    }

    public class AddRequest
    {
        [Required]
        public string Name { get; set; }
        public string Parent { get; set; }
    }

    public class AddResponse
    {
        public Entities.Domain Domain { get; set; }
    }
}
