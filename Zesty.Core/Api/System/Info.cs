using Zesty.Core.Entities;

namespace Zesty.Core.Api.System
{
    public class Info : ApiHandlerBase
    {
        public override ApiHandlerOutput Process(ApiInputHandler input)
        {
            return GetOutput(new InfoResponse()
            {
                User = Context.Current.User
            });
        }
    }

    public class InfoResponse
    {
        public User User { get; set; }
    }
}
