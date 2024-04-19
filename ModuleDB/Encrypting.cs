using System.Security.Cryptography;
using System.Text;

namespace FoodApp.ModuleDB
{
    class Encrypting
    {
        public static string HashPassword(string password)
        {
            MD5 mD5 = MD5.Create();

            byte[] b = Encoding.UTF8.GetBytes(password);
            byte[] hash = mD5.ComputeHash(b);

            StringBuilder stringBuilder = new StringBuilder();
            foreach (var a in hash)
            {
                stringBuilder.Append(a.ToString("X2"));
            }

            return Convert.ToString(stringBuilder);
        }
    }
}
