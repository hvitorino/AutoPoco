using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AutoPoco.Configuration;
using Moq;
using AutoPoco.Configuration.Providers;

namespace AutoPoco.Tests.Unit.Configuration
{
    [TestFixture]
    public class EngineConfigurationFactoryTests
    {
        [Test]
        public void Create_WithEmptySetup_ReturnsConfiguration()
        {
           var configurationProviderMock = new Mock<IEngineConfigurationProvider>();
           var conventionProviderMock = new Mock<IEngineConventionProvider>();
           var factory = new EngineConfigurationFactory();

           IEngineConfiguration configuration = factory.Create(
                configurationProviderMock.Object,
                conventionProviderMock.Object);

           Assert.NotNull(configuration);
        }
    }
}
