using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoFakeItEasy;
using Ploeh.AutoFixture.Xunit;

namespace CropStats
{
    public class AutoAttribute : AutoDataAttribute
    {
        public AutoAttribute() : base(new Fixture().Customize(new AutoFakeItEasyCustomization()))
        {
        }
    }
}