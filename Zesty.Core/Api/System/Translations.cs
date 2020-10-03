using System;
using System.Collections.Generic;
using Zesty.Core.Common;
using Zesty.Core.Entities;

namespace Zesty.Core.Api.System
{
    public class Translations : ApiHandlerBase
    {
        public override ApiHandlerOutput Process(ApiInputHandler input)
        {
            string language = input.Context.Request.Query["language"];

            if (String.IsNullOrWhiteSpace(language))
            {
                throw new ApplicationException(Messages.LanguageMissing);
            }

            TranslationResponse response = new TranslationResponse()
            {
                List = StorageManager.Instance.GetTranslations(language)
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
                CachePolicy = ApiCachePolicy.Disable
            };
        }
    }

    public class TranslationResponse
    {
        public List<Translation> List { get; set; }
    }
}
