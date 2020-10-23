using System.Collections.Generic;
using Zesty.Core.Entities;

namespace Zesty.Core.Api.System
{
    public class ClientSettings : ApiHandlerBase
    {
        public override ApiHandlerOutput Process(ApiInputHandler input)
        {
            ClientSettingsResponse response = new ClientSettingsResponse()
            {
                Settings = StorageManager.Instance.GetClientSettings()
            };

            return new ApiHandlerOutput()
            {
                Output = response,
                Type = ApiHandlerOutputType.JSon,
                ResourceHistoryOutput = null,
                CachePolicy = ApiCachePolicy.Enable
            };
        }
    }

    public class ClientSettingsResponse
    {
        public Dictionary<string, string> Settings { get; set; }
    }
}
