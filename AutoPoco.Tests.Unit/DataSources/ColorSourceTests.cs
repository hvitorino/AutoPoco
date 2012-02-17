using AutoPoco.DataSources;
using NUnit.Framework;

namespace AutoPoco.Tests.Unit.DataSources
{
    [TestFixture]
    public class ColorSourceTests
    {
        [Test]
        public void Next_Returns_A_Color()
        {
            ColorSource source = new ColorSource();
            var value = source.Next(null);
            Assert.IsFalse(value.IsEmpty);
        }
    }
}