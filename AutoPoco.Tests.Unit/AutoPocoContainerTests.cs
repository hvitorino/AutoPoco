using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Moq;
using AutoPoco.Configuration.Providers;
using AutoPoco.Engine;
using AutoPoco.Util;
using AutoPoco.Testing;

namespace AutoPoco.Tests.Unit
{
    [TestFixture]
    public class AutoPocoContainerTests
    {
        [Test]
        public void Configure_RunsActions()
        {
            bool hasRun = false;
            AutoPocoContainer.Configure(x =>
            {
                hasRun = true;
            });
            Assert.IsTrue(hasRun);
        }

        [Test]
        public void Configure_ReturnsFactory()
        {
            IGenerationSessionFactory factory = AutoPocoContainer.Configure(x =>
            {
                
            });
            Assert.NotNull(factory);
        }
    }
}
