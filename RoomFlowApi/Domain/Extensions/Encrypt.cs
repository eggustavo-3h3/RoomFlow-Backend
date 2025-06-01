using System.Security.Cryptography;
using System.Text;

namespace RoomFlowApi.Domain.Extensions
{
    public static class Encrypt
    {
        public static string EncryptPassword(this string password)
        {
            if (string.IsNullOrEmpty(password))
                return string.Empty;

            password += "2ca3867d-b10e-4a8c-8801-f28f464b2c2f";
            var passwordTmp = password;
            var md5 = MD5.Create();
            var data = md5.ComputeHash(Encoding.Default.GetBytes(passwordTmp));
            var sbString = new StringBuilder();
            foreach (var t in data)
                sbString.Append(t.ToString("x2"));

            return sbString.ToString();
        }
    }
}
