namespace Zesty.Core.Business
{
    static class Resource
    {
        internal static string GetType(string resourceName)
        {
            //TODO add cache support

            return StorageManager.Instance.GetType(resourceName);
        }
    }
}
