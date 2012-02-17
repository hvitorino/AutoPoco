using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AutoPoco.DataSources;

namespace AutoPoco.Tests.Unit.DataSources
{
    [TestFixture]
    public class ValueSourceTests
    {
        private ValueSource mSource;

        [SetUp]
        public void Setup()
        {
            mSource = new ValueSource(10);
        }

        [Test]
        public void Next_ReturnsValue()
        {
            Assert.AreEqual(10, mSource.Next(null));
        }
    }
}
