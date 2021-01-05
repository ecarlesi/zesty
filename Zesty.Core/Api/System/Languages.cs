using System.Collections.Generic;
using Zesty.Core.Entities;

namespace Zesty.Core.Api.System
{
    public class Languages : ApiHandlerBase
    {
        public override ApiHandlerOutput Process(ApiInputHandler input)
        {
            return GetOutput(new LanguagesResponse()
            {
                List = Business.Languages.List()
            });
        }
    }

    public class LanguagesResponse
    {
        public List<Entities.Language> List { get; set; }
    }
}
