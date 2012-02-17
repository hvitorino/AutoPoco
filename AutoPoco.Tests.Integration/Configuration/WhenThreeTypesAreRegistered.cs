using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AutoPoco.Testing;
using AutoPoco.Configuration;

namespace AutoPoco.Tests.Integration.Configuration
{
    [TestFixture]
    public class WhenThreeTypesAreRegistered : ConfigurationBaseTest
    {
        protected IEngineConfiguration Configuration
        {
            get;
            private set;
        }


        [SetUp]
        public void AddTypesAndBuild()
        {
            this.Builder.Include<SimpleUser>();
            this.Builder.Include<SimplePropertyClass>();
            this.Builder.Include<SimpleFieldClass>();
            this.Configuration = new EngineConfigurationFactory().Create(this.Builder, this.Builder.ConventionProvider);
        }

        [Test]
        public void ConfigurationContainsFourTypes()
        {
            var types = this.Configuration.GetRegisteredTypes();
            Assert.AreEqual(4, types.Count());
        }

        [Test]
        public void ConfigurationContainsValidTypesIncludingSystemObject()
        {
            var simpleUserType = this.Configuration.GetRegisteredType(typeof(SimpleUser));
            var simplePropertyType = this.Configuration.GetRegisteredType(typeof(SimplePropertyClass));
            var simpleFieldType = this.Configuration.GetRegisteredType(typeof(SimpleFieldClass));
            var objectType = this.Configuration.GetRegisteredType(typeof(Object));
            
            Assert.NotNull(simpleUserType);
            Assert.NotNull(simplePropertyType);
            Assert.NotNull(simpleFieldType);
            Assert.NotNull(objectType);
        }
    }
}
