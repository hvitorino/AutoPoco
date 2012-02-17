using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AutoPoco.DataSources;

namespace AutoPoco.Tests.Unit.DataSources
{
    [TestFixture]
    public class IntegerIdSourceTests
    {
        [Test]
        public void Next_ReturnsIncrementalResults()
        {
            IntegerIdSource source = new IntegerIdSource();
            int value1 = source.Next(null);
            int value2 = source.Next(null);
            Assert.Greater(value2, value1);
        }
    }
}
