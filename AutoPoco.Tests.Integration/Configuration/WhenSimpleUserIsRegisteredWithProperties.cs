using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AutoPoco.Configuration;
using AutoPoco.Testing;
using AutoPoco.Util;

namespace AutoPoco.Tests.Integration.Configuration
{
    [TestFixture]
    public class WhenSimpleUserIsRegisteredWithProperties : ConfigurationBaseTest
    {
        protected IEngineConfiguration Configuration
        {
            get;
            private set;
        }

        [SetUp]
        public void AddSimpleUser()
        {
            this.Builder.Include<SimpleUser>()
                .Setup(x => x.EmailAddress).Default()
                .Setup(x => x.FirstName).Default()
                .Setup(x => x.LastName).Default();

            Configuration = new EngineConfigurationFactory().Create(this.Builder, this.Builder.ConventionProvider);      
        }

        [Test]
        public void ContainsOnlyTheRegisteredProperties()
        {
            var type = this.Configuration.GetRegisteredType(typeof(SimpleUser));
            var members = type.GetRegisteredMembers();
            Assert.AreEqual(3, members.Count());
        }

        [Test]
        public void ContainsEmailAddress()
        {
            var type = this.Configuration.GetRegisteredType(typeof(SimpleUser));
            var emailAddressProperty = type.GetRegisteredMember(ReflectionHelper.GetMember<SimpleUser>(x => x.EmailAddress));

            Assert.NotNull(emailAddressProperty);
        }

        [Test]
        public void ContainsFirstName()
        {
            var type = this.Configuration.GetRegisteredType(typeof(SimpleUser));
            var firstNameProperty = type.GetRegisteredMember(ReflectionHelper.GetMember<SimpleUser>(x => x.FirstName));

            Assert.NotNull(firstNameProperty);
        }

        [Test]
        public void ContainsLastName()
        {
            var type = this.Configuration.GetRegisteredType(typeof(SimpleUser));
            var lastNameProperty = type.GetRegisteredMember(ReflectionHelper.GetMember<SimpleUser>(x => x.LastName));

            Assert.NotNull(lastNameProperty);
        }
    }
}
