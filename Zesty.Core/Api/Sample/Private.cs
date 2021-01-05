using Zesty.Core.Entities;

namespace Zesty.Core.Api.Sample
{
    public class Private : ApiHandlerBase
    {
        public override ApiHandlerOutput Process(ApiInputHandler input)
        {
            return GetOutput(new { Message = $"Response from private API: {Context.Current.User.Username}" });
        }
    }
}
