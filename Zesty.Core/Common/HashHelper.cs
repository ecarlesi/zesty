using System.Security.Cryptography;
using System.Text;
using Zesty.Core.Entities.Settings;

namespace Zesty.Core.Common
{
    static class HashHelper
    {
        internal static string GetSha256(string text)
        {
            byte[] b = Encoding.Default.GetBytes(text);

            using (SHA256 calculator = SHA256.Create())
            {
                byte[] c = calculator.ComputeHash(b);

                StringBuilder stringBuilder = new StringBuilder();

                for (int i = 0; i < c.Length; i++)
                {
                    stringBuilder.Append($"{c[i]:X2}");
                }

                return stringBuilder.ToString();
            }
        }
    }
}
