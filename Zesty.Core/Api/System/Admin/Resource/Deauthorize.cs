using System;
using Zesty.Core.Entities;

namespace Zesty.Core.Api.System.Admin.Resource
{
    public class Deauthorize : ApiHandlerBase
    {
        public override ApiHandlerOutput Process(ApiInputHandler input)
        {
            DeauthorizeRequest request = GetEntity<DeauthorizeRequest>(input);

            Business.Resource.Deauthorize(Guid.Parse(request.Resource), Guid.Parse(request.Role));

            return GetOutput();
        }
    }

    public class DeauthorizeRequest
    {
        [Required]
        public string Resource { get; set; }
        [Required]
        public string Role { get; set; }
    }
}
