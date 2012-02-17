using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Engine;
using Moq;
using NUnit.Framework;
using AutoPoco.Configuration;
using AutoPoco.Testing;
using AutoPoco.Util;

namespace AutoPoco.Tests.Unit.Configuration
{
    [TestFixture]
    public class EngineConfigurationTypeTests
    {
        [Test]
        public void RegisterMember_MemberAlreadyRegistered_ThrowsException()
        {
            EngineConfigurationType type = new EngineConfigurationType(typeof(SimpleUser));
            type.RegisterMember(ReflectionHelper.GetMember<SimpleUser>(x => x.EmailAddress));

            Assert.Throws<ArgumentException>(() =>
            {
                type.RegisterMember(ReflectionHelper.GetMember<SimpleUser>(x => x.EmailAddress));
            });
        }

        [Test]
        public void RegisterMember_GetRegisteredMembers_ReturnsMembers()
        {
            EngineConfigurationType type = new EngineConfigurationType(typeof(SimpleUser));
            type.RegisterMember(ReflectionHelper.GetMember<SimpleUser>(x => x.EmailAddress));
            type.RegisterMember(ReflectionHelper.GetMember<SimpleUser>(x => x.FirstName));


            var members = type.GetRegisteredMembers();
            Assert.AreEqual(2, members.Count());
        }

        [Test]
        public void Set_Factory_With_Type_Sets_Factory()
        {
            EngineConfigurationType type = new EngineConfigurationType(typeof(SimpleUser));
            Mock<IEngineConfigurationDatasource> source = new Mock<IEngineConfigurationDatasource>();
            type.SetFactory(source.Object);
            var factory = type.GetFactory();
            Assert.AreEqual(source.Object, factory);
        }

        public class TestFactory : IDatasource<SimpleCtorClass>
        {
            public object Next(IGenerationContext context)
            {
                throw new NotImplementedException();
            }
        }
    }
}
