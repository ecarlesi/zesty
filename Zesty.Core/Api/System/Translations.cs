using System;
using System.Collections.Generic;
using Zesty.Core.Entities;
using Zesty.Core.Exceptions;

namespace Zesty.Core.Api.System
{
    public class Translations : ApiHandlerBase
    {
        public override ApiHandlerOutput Process(ApiInputHandler input)
        {
            string language = input.Context.Request.Query["language"];

            if (String.IsNullOrWhiteSpace(language))
            {
                throw new ApiInvalidArgumentException(Messages.LanguageMissing);
            }

            return GetOutput(new TranslationResponse()
            {
                List = StorageManager.Instance.GetTranslations(language)
            });
        }
    }

    public class TranslationResponse
    {
        public List<Translation> List { get; set; }
    }
}
