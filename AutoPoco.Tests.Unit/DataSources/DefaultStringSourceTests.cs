using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AutoPoco.DataSources;

namespace AutoPoco.Tests.Unit.DataSources
{
    [TestFixture]
    public class DefaultStringSourceTests
    {
        [Test]
        public void Next_ReturnsEmptyString()
        {
            DefaultStringSource source = new DefaultStringSource();
            string value = source.Next(null);
            Assert.AreEqual("", value);
        }
    }
}
