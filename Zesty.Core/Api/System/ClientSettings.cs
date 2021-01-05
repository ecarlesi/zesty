using System.Collections.Generic;
using Zesty.Core.Entities;

namespace Zesty.Core.Api.System
{
    public class ClientSettings : ApiHandlerBase
    {
        public override ApiHandlerOutput Process(ApiInputHandler input)
        {
            return GetOutput(new ClientSettingsResponse()
            {
                Settings = Business.ClientSettings.All()
            }, true);
        }
    }

    public class ClientSettingsResponse
    {
        public Dictionary<string, string> Settings { get; set; }
    }
}
