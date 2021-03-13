using System.Collections.Generic;

namespace Zesty.Core.Business
{
    public static class Languages
    {
        private static IStorage storage = StorageManager.Storage;

        public static List<Entities.Language> List()
        {
            //TODO add cache

            return storage.GetLanguages();
        }

        public static List<Entities.Translation> GetTranslations(string language)
        {
            //TODO add cache

            return storage.GetTranslations(language);
        }
    }
}
