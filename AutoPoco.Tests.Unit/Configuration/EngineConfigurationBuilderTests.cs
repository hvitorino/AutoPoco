using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Configuration;
using AutoPoco.DataSources;

using NUnit.Framework;
using AutoPoco.Engine;
using AutoPoco.Testing;
using AutoPoco.Configuration.Providers;
using Moq;

namespace AutoPoco.Tests.Unit.Configuration
{
    [TestFixture]
    public class EngineConfigurationBuilderTests
    {
        [Test]
        public void Generic_Include_ReturnsTypeConfiguration()
        {
            EngineConfigurationBuilder config = new EngineConfigurationBuilder();
            IEngineConfigurationTypeBuilder<SimpleUser> user = config.Include<SimpleUser>();

            Assert.NotNull(user);
        }

        [Test]
        public void NonGeneric_Include_ReturnsTypeConfiguration()
        {
            EngineConfigurationBuilder config = new EngineConfigurationBuilder();
            IEngineConfigurationTypeBuilder builder = config.Include(typeof(SimpleUser));

            Assert.NotNull(builder);
        }

        [Test]
        public void Conventions_InvokesAction()
        {
            EngineConfigurationBuilder config = new EngineConfigurationBuilder();
            bool wasInvoked = false;
            config.Conventions(x =>
            {
                wasInvoked = true;
            });

            Assert.IsTrue(wasInvoked);
        }

        [Test]
        public void RegisterTypeProvider_RegistersTypeProvider()
        {
            EngineConfigurationBuilder config = new EngineConfigurationBuilder();
            Mock<IEngineConfigurationTypeProvider> providerMock = new Mock<IEngineConfigurationTypeProvider>();
            config.RegisterTypeProvider(providerMock.Object);

            Assert.IsTrue(config.GetConfigurationTypes().Contains(providerMock.Object));
        }
    }
}
