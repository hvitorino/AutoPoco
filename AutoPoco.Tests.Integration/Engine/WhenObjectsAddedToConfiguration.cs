using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Testing;
using AutoPoco.Util;
using AutoPoco.Configuration;
using AutoPoco.DataSources;
using NUnit.Framework;

namespace AutoPoco.Tests.Integration.Engine
{
    [TestFixture]
    public class WhenObjectsAddedToConfiguration : GenerationSessionFactoryTestBase
    {
        protected override void PopulateConfiguration()
        {
            this.Configuration.RegisterType(typeof(SimpleUser));
            var simpleUserConfig = this.Configuration.GetRegisteredType(typeof(SimpleUser));

            var emailMember = ReflectionHelper.GetMember<SimpleUser>(x => x.EmailAddress);
            var firstNameMember = ReflectionHelper.GetMember<SimpleUser>(x=>x.FirstName);
            var lastNameMember = ReflectionHelper.GetMember<SimpleUser>(x => x.LastName);

            simpleUserConfig.RegisterMember(emailMember);
            simpleUserConfig.RegisterMember(firstNameMember);
            simpleUserConfig.RegisterMember(lastNameMember);

            
            var emailSourceFactory = new DatasourceFactory(typeof(ValueSource));
            emailSourceFactory.SetParams("test@test.com");
            var firstNameFactory = new DatasourceFactory(typeof(ValueSource));
            firstNameFactory.SetParams("first");
            var lastNameFactory = new DatasourceFactory(typeof(ValueSource));
            lastNameFactory.SetParams("last");

            simpleUserConfig.GetRegisteredMember(emailMember).SetDatasource(emailSourceFactory);
            simpleUserConfig.GetRegisteredMember(firstNameMember).SetDatasource(firstNameFactory);
            simpleUserConfig.GetRegisteredMember(lastNameMember).SetDatasource(lastNameFactory);


            this.Configuration.RegisterType(typeof(SimpleFieldClass));
            var simpleFieldConfig = this.Configuration.GetRegisteredType(typeof(SimpleFieldClass));

            var someFieldMember = ReflectionHelper.GetMember<SimpleFieldClass>(x => x.SomeField);
            var someOtherField = ReflectionHelper.GetMember<SimpleFieldClass>(x => x.SomeOtherField);

            simpleFieldConfig.RegisterMember(someFieldMember);
            simpleFieldConfig.RegisterMember(someOtherField);

            var someFieldFactory = new DatasourceFactory(typeof(ValueSource));
            someFieldFactory.SetParams("one");
            var someOtherFieldFactory = new DatasourceFactory(typeof(ValueSource));
            someOtherFieldFactory.SetParams("other");

            simpleFieldConfig.GetRegisteredMember(someFieldMember).SetDatasource(someFieldFactory);
            simpleFieldConfig.GetRegisteredMember(someOtherField).SetDatasource(someOtherFieldFactory);
        }


        [Test]
        public void CreateSimpleFieldClass_SomeFieldIsSet()
        {
            var simpleFieldClass = this.GenerationSession.Single<SimpleFieldClass>().Get();
            Assert.AreEqual("one", simpleFieldClass.SomeField);
        }

        [Test]
        public void CreateSimpleFieldClass_SomeOtherFieldIsSet()
        {
            var simpleFieldClass = this.GenerationSession.Single<SimpleFieldClass>().Get();
            Assert.AreEqual("other", simpleFieldClass.SomeOtherField);
        }

        [Test]
        public void CreateUser_EmailAddressIsSet()
        {
            var user = this.GenerationSession.Single<SimpleUser>().Get();
            Assert.AreEqual("test@test.com", user.EmailAddress);
        }

        [Test]
        public void CreateUser_FirstNameIsSet()
        {
            var user = this.GenerationSession.Single<SimpleUser>().Get();
            Assert.AreEqual("first", user.FirstName);
        }

        [Test]
        public void CreateUser_LastNameIsSet()
        {
            var user = this.GenerationSession.Single<SimpleUser>().Get();
            Assert.AreEqual("last", user.LastName);
        }
    }
}
