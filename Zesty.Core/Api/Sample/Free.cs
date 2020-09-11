using System;
using Zesty.Core.Common;
using Zesty.Core.Entities;

namespace Zesty.Core.Api.Sample
{
    public class Free : ApiHandlerBase
    {
        public override ApiHandlerOutput Process(ApiInputHandler input)
        {
            FreeOutput output = new FreeOutput()
            {
                Message = Guid.NewGuid().ToString()
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
                    ResourceHistoryPolicy = ApiResourceHistoryPolicy.Save
                },
                CachePolicy = ApiCachePolicy.Enable
            };
        }
    }

    public class FreeOutput
    {
        public string Message { get; set; }
    }
}
