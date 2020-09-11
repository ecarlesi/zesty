using System;
namespace Zesty.Core.Common
{
    class InstanceHelper
    {
        internal static T Create<T>(string typeName)
        {
            Type type = Type.GetType(typeName);

            if (type == null)
            {
                throw new TypeLoadException(typeName);
            }

            return (T)Activator.CreateInstance(type);
        }
    }
}
