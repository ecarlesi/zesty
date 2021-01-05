using System.Collections.Generic;
using Zesty.Core.Entities;

namespace Zesty.Core.Api.System
{
    public class Translations : ApiHandlerBase
    {
        public override ApiHandlerOutput Process(ApiInputHandler input)
        {
            string language = Get(input, "language");

            return GetOutput(new TranslationResponse()
            {
                List = Business.Languages.GetTranslations(language)
            });
        }
    }

    public class TranslationResponse
    {
        public List<Translation> List { get; set; }
    }
}
