using System;
using System.Text.Json;

namespace Zesty.Core.Common
{
    public class JsonHelper
    {
        private static readonly JsonSerializerOptions options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true, AllowTrailingCommas = true };

        public static T Deserialize<T>(string json)
        {
            return JsonSerializer.Deserialize<T>(json, options);
        }

        public static object Deserialize(string json, Type returnType)
        {
            return JsonSerializer.Deserialize(json, returnType);
        }

        public static string Serialize(object o)
        {
            return JsonSerializer.Serialize(o, options);
        }
    }
}
