using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Shared.Helpers
{
    public class HashHelper
    {
        public static string ComputeHash(string normalText, int workFactor = 10)
        {
            var salt = RandomNumberGenerator.GetBytes(16);
            var hash = Rfc2898DeriveBytes.Pbkdf2(normalText, salt, 10000, HashAlgorithmName.SHA256, 32);
            return Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hash);
        }

        public static bool VerifyHash(string normalText, string hashedText)
        {
            var parts = hashedText.Split(':');
            var salt = Convert.FromBase64String(parts[0]);
            var hash = Convert.FromBase64String(parts[1]);

            var computedHash = Rfc2898DeriveBytes.Pbkdf2(normalText, salt, 10000, HashAlgorithmName.SHA256, 32);
            return computedHash.SequenceEqual(hash);
        }
    }
}
