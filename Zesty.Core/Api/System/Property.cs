using Zesty.Core.Common;
using Zesty.Core.Entities;

namespace Zesty.Core.Api.System
{
    public class Property : ApiHandlerBase
    {
        public override ApiHandlerOutput Process(ApiInputHandler input)
        {
            PropertyRequest request = base.GetEntity<PropertyRequest>(input);

            StorageManager.Instance.SetProperty(request.Name, request.Value, Context.Current.User);

            PropertyResponse response = new PropertyResponse() { Message = Messages.Success };

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
                CachePolicy = ApiCachePolicy.Disable
            };
        }
    }

    public class PropertyResponse
    {
        public string Message { get; set; }
    }

    public class PropertyRequest
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
