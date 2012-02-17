using AutoPoco.DataSources;
using NUnit.Framework;

namespace AutoPoco.Tests.Unit.DataSources
{
    [TestFixture]
    public class LoremIpsumTests
    {
        [Test]
        public void Next_Returns_A_Paragragh()
        {
            LoremIpsumSource source = new LoremIpsumSource();
            var value = source.Next(null);
            Assert.IsNotNullOrEmpty(value);
        }

        [Test]
        public void Next_Returns_Two_Paragragh()
        {
            LoremIpsumSource source = new LoremIpsumSource();
            LoremIpsumSource source2 = new LoremIpsumSource(2);
            
            var value = source.Next(null);
            var value2 = source2.Next(null);
            Assert.IsTrue(value2.Length > value.Length);
        }
    }
}