using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CropStats.Models
{
    public class Password
    {
        public byte[] Hash { get; set; }
        public byte[] Salt { get; set; }
    }

    public class PasswordBuilder
    {
        private readonly HashAlgorithm hashAlgorithm;
        private readonly RNGCryptoServiceProvider rngCryptoServiceProvider;

        public PasswordBuilder()
        {
            rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            hashAlgorithm = new SHA256Managed();
        }

        public Password CreatePassword(string secret)
        {
            byte[] salt = GenerateSalt();

            byte[] hash = GenerateSecretHash(secret, salt);

            return new Password
            {
                Salt = salt,
                Hash = hash
            };
        }

        public bool IsValidPassword(Password password, string secret)
        {
            byte[] bytes = GenerateSecretHash(secret, password.Salt);
            return bytes.SequenceEqual(password.Hash);
        }

        private byte[] GenerateSecretHash(string secret, IEnumerable<byte> salt)
        {
            byte[] password = Encoding.UTF8.GetBytes(secret);
            byte[] hash = hashAlgorithm.ComputeHash(password.Concat(salt).ToArray());
            return hash;
        }

        private byte[] GenerateSalt()
        {
            var salt = new byte[128];
            rngCryptoServiceProvider.GetNonZeroBytes(salt);
            return salt;
        }
    }
}