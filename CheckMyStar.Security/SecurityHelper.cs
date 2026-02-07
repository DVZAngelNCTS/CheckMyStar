using System.Security.Cryptography;

namespace CheckMyStar.Security
{
    public static class SecurityHelper
    {
        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha256.ComputeHash(passwordBytes);

                return Convert.ToHexString(hashBytes);
            }
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            string computedHash = SecurityHelper.HashPassword(password);

            return computedHash == hashedPassword;
        }
    }
}
