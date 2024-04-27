using System.Security.Cryptography;
using System.Text;

namespace Domain.Helpers
{
    public static class PasswordUtils
    {
        public const int SALT_LENGTH = 512;

        public static string GenerateSalt(int length)
        {
            byte[] salt = new byte[length];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return Convert.ToBase64String(salt);
        }

        public static string HashPassword(string plainTextPassword, string salt)
        {
            byte[] saltBytes = Encoding.UTF8.GetBytes(salt);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(plainTextPassword);

            byte[] hashBytes;
            using (var pbkdf2 = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000, HashAlgorithmName.SHA512))
            {
                hashBytes = pbkdf2.GetBytes(512 / 8);
            }

            return Convert.ToBase64String(hashBytes);
        }

        public static bool VerifyPassword(string plainTextPassword, string hashedPassword, string salt)
        {
            byte[] saltBytes = Encoding.UTF8.GetBytes(salt);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(plainTextPassword);

            byte[] hashBytes;
            using (var pbkdf2 = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000, HashAlgorithmName.SHA512))
            {
                hashBytes = pbkdf2.GetBytes(512 / 8);
            }

            string hashToCheck = Convert.ToBase64String(hashBytes);
            return hashToCheck == hashedPassword;
        }
    }
}
