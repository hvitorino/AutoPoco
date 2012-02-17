using System.Linq;
using AutoPoco.DataSources;
using NUnit.Framework;

namespace AutoPoco.Tests.Unit.DataSources
{
    [TestFixture]
    public class EnumerableSourceTests
    {
        [Test]
        public void Next_Returns_A_Enumration_Of_A_Hundred()
        {
            var source = new EnumerableSource<RandomStringSource, string>(100, new object[] { 4, 20});
            var value = source.Next(null);
            Assert.AreEqual(100, value.Count());
        }
    }
}