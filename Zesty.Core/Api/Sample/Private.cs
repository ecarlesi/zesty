using Zesty.Core.Common;
using Zesty.Core.Entities;

namespace Zesty.Core.Api.Sample
{
    public class Private : ApiHandlerBase
    {
        public override ApiHandlerOutput Process(ApiInputHandler input)
        {
            PrivateOutput output = new PrivateOutput()
            {
                Message = $"Response from private API: {Context.Current.User.Username}"
            };

            return new ApiHandlerOutput()
            {
                Output = output,
                Type = ApiHandlerOutputType.JSon,
                ResourceHistoryOutput = new ApiResourceHistoryOutput()
                {
                    Item = new HistoryItem()
                    {
                        Resource = input.Resource,
                        Text = JsonHelper.Serialize(output),
                        User = Context.Current.User,
                        Actor = this.GetType().ToString()
                    },
                    ResourceHistoryPolicy = ApiResourceHistoryPolicy.None
                },
                CachePolicy = ApiCachePolicy.Disable
            };
        }
    }

    public class PrivateOutput
    {
        public string Message { get; set; }
    }
}
