using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AutoPoco.Configuration;
using AutoPoco.DataSources;
using AutoPoco.Util;
using AutoPoco.Testing;

namespace AutoPoco.Tests.Unit.Configuration
{
    [TestFixture]
    public class EngineConfigurationTypeMemberBuilderTests
    {
        [Test]
        public void NotGeneric_Use_ReturnsTypeBuilder()
        {
            EngineConfigurationTypeBuilder configuration = new EngineConfigurationTypeBuilder(typeof(SimpleUser));
            EngineConfigurationTypeMemberBuilder propertyConfiguration = new EngineConfigurationTypeMemberBuilder(null, configuration);

            IEngineConfigurationTypeBuilder returnedConfiguration = propertyConfiguration.Use(typeof(SimpleDataSource));

            Assert.AreEqual(configuration, returnedConfiguration);
        }

        [Test]
        public void NotGeneric_UseInvalidDataSource_ThrowsArgumentException()
        {
            EngineConfigurationTypeBuilder configuration = new EngineConfigurationTypeBuilder(typeof(SimpleUser));
            EngineConfigurationTypeMemberBuilder propertyConfiguration = new EngineConfigurationTypeMemberBuilder(null, configuration);

            Assert.Throws<ArgumentException>(() => { propertyConfiguration.Use(typeof(SimpleUser)); });
        }

        [Test]
        public void NotGeneric_UseWithArgs_ReturnsTypeBuilder()
        {
            EngineConfigurationTypeBuilder configuration = new EngineConfigurationTypeBuilder(typeof(SimpleUser));
            EngineConfigurationTypeMemberBuilder propertyConfiguration = new EngineConfigurationTypeMemberBuilder(null, configuration);

            IEngineConfigurationTypeBuilder returnedConfiguration = propertyConfiguration.Use(typeof(SimpleDataSource),0, 1, 10);

            Assert.AreEqual(configuration, returnedConfiguration);
        }

        [Test]
        public void NotGeneric_UseInvalidDataSourceWithArgs_ThrowsArgumentException()
        {
            EngineConfigurationTypeBuilder configuration = new EngineConfigurationTypeBuilder(typeof(SimpleUser));
            EngineConfigurationTypeMemberBuilder propertyConfiguration = new EngineConfigurationTypeMemberBuilder(null, configuration);

            Assert.Throws<ArgumentException>(() => { propertyConfiguration.Use(typeof(SimpleUser), 0, 1, 10); }); 
        }

        [Test]
        public void NotGeneric_Default_ReturnsTypeBuilder()
        {
            EngineConfigurationTypeBuilder configuration = new EngineConfigurationTypeBuilder(typeof(SimpleUser));
            EngineConfigurationTypeMemberBuilder propertyConfiguration = new EngineConfigurationTypeMemberBuilder(null, configuration);

            IEngineConfigurationTypeBuilder returnedConfiguration = propertyConfiguration.Default();

            Assert.AreEqual(configuration, returnedConfiguration);
        }

        [Test]
        public void NotGeneric_Default_ResetsSource()
        {
            EngineConfigurationTypeBuilder configuration = new EngineConfigurationTypeBuilder(typeof(SimpleUser));
            EngineConfigurationTypeMemberBuilder propertyConfiguration = new EngineConfigurationTypeMemberBuilder(null, configuration);

            propertyConfiguration.Use(typeof(SimpleDataSource));
            propertyConfiguration.Default();

            Assert.AreEqual(0, propertyConfiguration.GetDatasources().Count());
        }

        [Test]
        public void Generic_Use_ReturnsTypeBuilder()
        {
            EngineConfigurationTypeBuilder<SimpleUser> configuration = new EngineConfigurationTypeBuilder<SimpleUser>();
            EngineConfigurationTypeMemberBuilder<SimpleUser, string> propertyConfiguration = new EngineConfigurationTypeMemberBuilder<SimpleUser, string>(null, configuration);

            IEngineConfigurationTypeBuilder<SimpleUser> returnedConfiguration = propertyConfiguration.Use<SimpleDataSource>();
            
            Assert.AreEqual(configuration, returnedConfiguration);           
        }

        [Test]
        public void Generic_UseWithArgs_ReturnsTypeBuilder()
        {
            EngineConfigurationTypeBuilder<SimpleUser> configuration = new EngineConfigurationTypeBuilder<SimpleUser>();
            EngineConfigurationTypeMemberBuilder<SimpleUser, string> propertyConfiguration = new EngineConfigurationTypeMemberBuilder<SimpleUser, string>(null, configuration);

            IEngineConfigurationTypeBuilder<SimpleUser> returnedConfiguration = propertyConfiguration.Use<SimpleDataSource>(0,1,10);

            Assert.AreEqual(configuration, returnedConfiguration);
        }

        [Test]
        public void Generic_Default_ReturnsTypeBuilder()
        {
            EngineConfigurationTypeBuilder<SimpleUser> configuration = new EngineConfigurationTypeBuilder<SimpleUser>();
            EngineConfigurationTypeMemberBuilder<SimpleUser, string> propertyConfiguration = new EngineConfigurationTypeMemberBuilder<SimpleUser, string>(null, configuration);

            IEngineConfigurationTypeBuilder<SimpleUser> returnedConfiguration = propertyConfiguration.Default();

            Assert.AreEqual(configuration, returnedConfiguration);
        }

        [Test]
        public void Generic_Default_ResetsSource()
        {
            EngineConfigurationTypeBuilder<SimpleUser> configuration = new EngineConfigurationTypeBuilder<SimpleUser>();
            EngineConfigurationTypeMemberBuilder<SimpleUser, string> propertyConfiguration = new EngineConfigurationTypeMemberBuilder<SimpleUser, string>(null, configuration);

             propertyConfiguration.Use<SimpleDataSource>();
             propertyConfiguration.Default();

            Assert.AreEqual(0, propertyConfiguration.GetDatasources().Count());
        }

        [Test]
        public void Generic_GetConfigurationMember_ReturnsConfigurationMember()
        {
            EngineConfigurationTypeBuilder<SimpleUser> configuration = new EngineConfigurationTypeBuilder<SimpleUser>();
            EngineTypeMember member = ReflectionHelper.GetMember<SimpleUser>(x => x.EmailAddress);

            EngineConfigurationTypeMemberBuilder<SimpleUser, string> propertyConfiguration = new EngineConfigurationTypeMemberBuilder<SimpleUser, string>(
                member, configuration);

           EngineTypeMember returnMember =  propertyConfiguration.GetConfigurationMember();
           Assert.AreEqual(member, returnMember);

        }

        [Test]
        public void Generic_GetConfigurationAction_Invalid_ReturnsNULL()
        {
            EngineConfigurationTypeBuilder<SimpleUser> configuration = new EngineConfigurationTypeBuilder<SimpleUser>();
            EngineTypeMember member = ReflectionHelper.GetMember<SimpleUser>(x => x.EmailAddress);

            EngineConfigurationTypeMemberBuilder<SimpleUser, string> propertyConfiguration = new EngineConfigurationTypeMemberBuilder<SimpleUser, string>(
                member, configuration);

            IEngineConfigurationDatasource returnAction = propertyConfiguration.GetDatasources().FirstOrDefault();
            Assert.Null(returnAction);
        }

        [Test]
        public void Generic_GetConfigurationAction_Valid_ReturnsConfigurationAction()
        {
            EngineConfigurationTypeBuilder<SimpleUser> configuration = new EngineConfigurationTypeBuilder<SimpleUser>();
            EngineTypeMember member = ReflectionHelper.GetMember<SimpleUser>(x => x.EmailAddress);

            EngineConfigurationTypeMemberBuilder<SimpleUser, string> propertyConfiguration = new EngineConfigurationTypeMemberBuilder<SimpleUser, string>(
                member, configuration);
            propertyConfiguration.Use<SimpleDataSource>();

            IEngineConfigurationDatasource returnAction = propertyConfiguration.GetDatasources().FirstOrDefault();
            Assert.NotNull(returnAction);
        }
    }
}
