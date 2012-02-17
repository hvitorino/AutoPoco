using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AutoPoco.Configuration;
using AutoPoco.Testing;

namespace AutoPoco.Tests.Unit.Configuration
{
    [TestFixture]
    public class DataSourceFactoryTests
    {
        [Test]
        public void Build_ReturnsNewFactory()
        {
            DatasourceFactory factory = new DatasourceFactory(typeof(BlankDataSource));
            BlankDataSource source = factory.Build() as BlankDataSource;

            Assert.NotNull(source);
        }
    }
}
