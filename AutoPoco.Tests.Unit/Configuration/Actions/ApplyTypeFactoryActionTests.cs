using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Configuration;
using AutoPoco.DataSources;
using AutoPoco.Testing;
using NUnit.Framework;
using AutoPoco.Configuration.TypeRegistrationActions;
using AutoPoco.Configuration.Providers;
using Moq;

namespace AutoPoco.Tests.Unit.Configuration.Actions
{
    [TestFixture]
    public class ApplyTypeFactoryActionTests
    {
        [Test]
        public void When_Action_Is_Applied_Factory_Is_Applied_From_Configuration()
        {
            var provider = new Mock<IEngineConfigurationProvider>();
            var targetType = new Mock<IEngineConfigurationType>();
            var targetTypeProvider = new Mock<IEngineConfigurationTypeProvider>();
            var factory = new Mock<IEngineConfigurationDatasource>();

            targetType.SetupGet(x => x.RegisteredType).Returns(typeof (SimpleUser));
            targetTypeProvider.Setup(x => x.GetConfigurationType()).Returns(typeof (SimpleUser));

            provider.Setup(x => x.GetConfigurationTypes()).Returns(new[] {targetTypeProvider.Object});
            targetTypeProvider.Setup(x => x.GetFactory()).Returns(factory.Object);

            ApplyTypeFactoryAction action = new ApplyTypeFactoryAction(provider.Object);
            action.Apply(targetType.Object);

            targetType.Verify(x => x.SetFactory(factory.Object), Times.Once());
        }

        [Test]
        public void When_Action_Is_Applied_Factory_With_No_Configuration_Or_Convention_Available_DefaultFactory_Is_Applied()
        {
            var provider = new Mock<IEngineConfigurationProvider>();
            var targetType = new Mock<IEngineConfigurationType>();
            targetType.SetupGet(x => x.RegisteredType).Returns(typeof (SimpleUser));
            provider.Setup(x => x.GetConfigurationTypes()).Returns(new IEngineConfigurationTypeProvider[] {});
            
            ApplyTypeFactoryAction action = new ApplyTypeFactoryAction(provider.Object);
            action.Apply(targetType.Object);
            
            targetType.Verify(x => x.SetFactory(
                It.Is<IEngineConfigurationDatasource>(y=> y.Build() is FallbackObjectFactory<SimpleUser>)),
                Times.Once());
        }

        [Test]
        public void When_Action_Is_Applied_With_Convention_Convention_Applies_Factory_Is_Applied()
        {
               
        }
    }
}
