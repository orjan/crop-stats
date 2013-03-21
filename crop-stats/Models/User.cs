using System.Security.Cryptography;

namespace crop_stats.Models
{
    public class User
    {
        public string Id { get; set; }

        public Password Password { get; set; }
    }

    public class Password
    {
        public byte[] Hash { get; set; }
        public byte[] Salt { get; set; }
    
        public void X()
        {
            var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            // rngCryptoServiceProvider.
        }
    }

    
}