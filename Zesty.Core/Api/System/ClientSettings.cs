using System.Collections.Generic;
using Zesty.Core.Common;
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
                ResourceHistoryOutput = new ApiResourceHistoryOutput()
                {
                    Item = new HistoryItem()
                    {
                        Resource = input.Resource,
                        Text = JsonHelper.Serialize(response),
                        User = Context.Current.User,
                        Actor = this.GetType().ToString()
                    },
                    ResourceHistoryPolicy = ApiResourceHistoryPolicy.None
                },
                CachePolicy = ApiCachePolicy.Enable
            };
        }
    }

    public class ClientSettingsResponse
    {
        public Dictionary<string, string> Settings { get; set; }
    }
}
