using System.Security.Cryptography;

namespace CropStats.Models
{
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