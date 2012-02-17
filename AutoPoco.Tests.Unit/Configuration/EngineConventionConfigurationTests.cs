using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AutoPoco.Configuration;
using AutoPoco.Conventions;
using AutoPoco.Testing;

namespace AutoPoco.Tests.Unit.Configuration
{
    [TestFixture]
    public class EngineConventionConfigurationTests
    {
        [Test]
        public void Register_AddsConvention()
        {
            EngineConventionConfiguration config = new EngineConventionConfiguration();
            config.Register(typeof(SimpleMemberConvention));

            Type addedType = config.Find<SimpleMemberConvention>().Single();
            Assert.AreEqual(typeof(SimpleMemberConvention), addedType);
        }

        [Test]
        public void RegisterGeneric_AddsConvention()
        {
            EngineConventionConfiguration config = new EngineConventionConfiguration();
            config.Register<SimpleMemberConvention>();

            Type addedType = config.Find<SimpleMemberConvention>().Single();
            Assert.AreEqual(typeof(SimpleMemberConvention), addedType);
        }

        [Test]
        public void UseDefaultConventions_AddsDefaultConventions()
        {
            EngineConventionConfiguration config = new EngineConventionConfiguration();
            config.UseDefaultConventions();

            Type addedType = config.Find<DefaultDatetimeMemberConvention>().Single();
            Assert.AreEqual(typeof(DefaultDatetimeMemberConvention), addedType);
        }

        [Test]
        public void ScanAssemblyWithType_AddsAssemblyConventions()
        {
            EngineConventionConfiguration config = new EngineConventionConfiguration();
            config.ScanAssemblyWithType<SimpleMemberConvention>();

            Type addedType = config.Find<SimpleTypeConvention>().Single();
            Assert.AreEqual(typeof(SimpleTypeConvention), addedType);
        }

        [Test]
        public void ScanAssembly_AddsAssemblyConvention()
        {
            EngineConventionConfiguration config = new EngineConventionConfiguration();
            config.ScanAssembly(typeof(SimpleMemberConvention).Assembly);

            Type addedType = config.Find<SimpleTypeConvention>().Single();
            Assert.AreEqual(typeof(SimpleTypeConvention), addedType);
        }


        [Test]
        public void Find_ReturnsAllConventions()
        {
            EngineConventionConfiguration config = new EngineConventionConfiguration();
            config.ScanAssembly(typeof(SimpleMemberConvention).Assembly);

            var conventionTypes = config.Find<IConvention>().ToArray();
            Assert.AreEqual(2, conventionTypes.Length);
        }

    }
}
