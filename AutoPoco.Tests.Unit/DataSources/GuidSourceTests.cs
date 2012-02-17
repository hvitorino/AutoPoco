using System;
using AutoPoco.DataSources;
using NUnit.Framework;

namespace AutoPoco.Tests.Unit.DataSources
{
    [TestFixture]
    public class GuidSourceTests
    {
        [Test]
        public void Next_Returns_A_Guid()
        {
            GuidSource source = new GuidSource();
            var value = source.Next(null);
            Assert.IsFalse(value.CompareTo(Guid.Empty) == 0);
        }


        [Test]
        public void Next_WithTwoSources_Returns_RepeatedGuids()
        {
            GuidSource source1 = new GuidSource();
            var value1 = source1.Next(null);

            GuidSource source2 = new GuidSource();
            var value2 = source2.Next(null);

            Assert.AreEqual(value1, value2);
        }

        [Test]
        public void Next_WithOneSource_ReturnsDifferentGuids()
        {
            GuidSource source = new GuidSource();
            var value1 = source.Next(null);
            var value2 = source.Next(null);
            Assert.AreNotEqual(value1, value2);
        }
    }
}