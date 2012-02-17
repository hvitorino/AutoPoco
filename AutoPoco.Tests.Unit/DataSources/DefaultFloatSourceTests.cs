using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AutoPoco.DataSources;

namespace AutoPoco.Tests.Unit.DataSources
{
    [TestFixture]
    public class DefaultFloatSourceTests
    {
        [Test]
        public void Next_ReturnsZero()
        {
            DefaultFloatSource source = new DefaultFloatSource();
            float value = source.Next(null);
            Assert.AreEqual(0, value);
        }
    }
}
