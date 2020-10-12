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

            if (string.IsNullOrWhiteSpace(request.Value))
            {
                if (Context.Current.User.Properties.ContainsKey(request.Name))
                {
                    Context.Current.User.Properties.Remove(request.Name);
                }
            }
            else
            {
                if (Context.Current.User.Properties.ContainsKey(request.Name))
                {
                    Context.Current.User.Properties[request.Name] = request.Value;
                }
                else
                {
                    Context.Current.User.Properties.Add(request.Name, request.Value);
                }
            }

            input.Context.Session.Set(Context.Current.User);

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
