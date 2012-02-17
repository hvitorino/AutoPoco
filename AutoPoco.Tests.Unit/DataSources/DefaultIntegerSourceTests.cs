using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AutoPoco.DataSources;

namespace AutoPoco.Tests.Unit.DataSources
{
    [TestFixture]
    public class DefaultIntegerSourceTests
    {
        [Test]
        public void Next_ReturnsZero()
        {
            DefaultIntegerSource source = new DefaultIntegerSource();
            int value = source.Next(null);
            Assert.AreEqual(0, value);
        }
    }
}
