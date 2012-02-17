using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AutoPoco.DataSources;

namespace AutoPoco.Tests.Unit.DataSources
{
    [TestFixture]
    public class FirstNameSourceTests
    {
        [Test]
        public void Next_ReturnsString()
        {
            FirstNameSource source = new FirstNameSource();
            String name = source.Next(null);

            Assert.NotNull(name);
        }
    }
}
