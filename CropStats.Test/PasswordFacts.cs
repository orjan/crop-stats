using CropStats.Models;
using Xunit;
using Xunit.Extensions;

namespace CropStats
{
    public class PasswordFacts
    {
        [Auto, Theory]
        public void ShouldGenerateSaltOf128Bytes(PasswordBuilder builder, string secret)
        {
            Password password = builder.CreatePassword(secret);

            Assert.Equal(128, password.Salt.Length);
            Assert.DoesNotContain((byte) 0, password.Salt);
        }

        [Auto, Theory]
        public void ShouldBeAbleToValidateLogin(PasswordBuilder builder, string secret)
        {
            Password password = builder.CreatePassword(secret);

            var isValidSecret = builder.IsValidPassword(password, secret);

            Assert.True(isValidSecret);
        }
        
        [Auto, Theory]
        public void ShouldFailLoginIfPasswordIsWrong(PasswordBuilder builder, string secret, string guess)
        {
            Password password = builder.CreatePassword(secret);

            var isValidSecret = builder.IsValidPassword(password, guess);

            Assert.False(isValidSecret);
        }

    }
}