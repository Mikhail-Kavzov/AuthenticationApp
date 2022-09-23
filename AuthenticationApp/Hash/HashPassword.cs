using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
namespace AuthenticationApp.Hash
{
    public static class HashPassword
    {
        public static string GenerateHash(string password)
        {
            using var alg = SHA256.Create();
            var hashBytes = alg.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashBytes);
        }
    }
}
