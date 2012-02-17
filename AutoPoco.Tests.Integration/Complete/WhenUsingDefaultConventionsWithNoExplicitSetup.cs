using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AutoPoco.Testing;
using AutoPoco.Engine;

namespace AutoPoco.Tests.Integration.Complete
{
    [TestFixture]
    public class WhenUsingDefaultConventionsWithNoExplicitSetup
    {
        IGenerationSession mSession;

        [SetUp]
        public void Setup()
        {
            mSession = AutoPocoContainer.Configure(x =>
            {
                x.Conventions(c =>
                {
                    c.UseDefaultConventions();
                });
                x.Include<SimpleUser>();
                x.Include<SimpleUserRole>();
                x.Include<SimpleFieldClass>();
                x.Include<SimplePropertyClass>();
                x.Include<DefaultPropertyClass>();
                x.Include<DefaultFieldClass>();
            })
            .CreateSession();
        }

        [Test]
        public void SimpleUser_RoleNotNull()
        {
            SimpleUser user = mSession.Single<SimpleUser>().Get();
            Assert.NotNull(user.Role);
        }

        [Test]
        public void SimpleUser_EmailIsNotNull()
        {
           SimpleUser user = mSession.Single<SimpleUser>().Get();
           Assert.NotNull(user.EmailAddress);
        }

        [Test]
        public void SimpleUser_RoleIsNotNull()
        {
            SimpleUser user = mSession.Single<SimpleUser>().Get();
            Assert.NotNull(user.Role);
        }

        [Test]
        public void SimpleUser_FirstNameNotNull()
        {
            SimpleUser user = mSession.Single<SimpleUser>().Get();
            Assert.NotNull(user.FirstName);
        }

        [Test]
        public void SimpleUser_LastNameNotNull()
        {
            SimpleUser user = mSession.Single<SimpleUser>().Get();
            Assert.NotNull(user.LastName);
        }

        [Test]
        public void SimpleFieldClass_SomePropertyNotNull()
        {
            SimpleFieldClass fieldClass = mSession.Single<SimpleFieldClass>().Get();
            Assert.NotNull(fieldClass.SomeField);
        }

        [Test]
        public void SimpleFieldClass_SomeOtherPropertyNotNull()
        {
            SimpleFieldClass fieldClass = mSession.Single<SimpleFieldClass>().Get();
            Assert.NotNull(fieldClass.SomeOtherField);
        }


        [Test]
        public void DefaultPropertyClass_StringIsEmpty()
        {
            DefaultPropertyClass propertyClass = mSession.Single<DefaultPropertyClass>().Get();
            Assert.AreEqual("", propertyClass.String);
        }

        [Test]
        public void DefaultPropertyClass_FloatEqualsZero()
        {
            DefaultPropertyClass propertyClass = mSession.Single<DefaultPropertyClass>().Get();
            Assert.AreEqual(0, propertyClass.Float);
        }

        [Test]
        public void DefaultPropertyClass_IntegerEqualsZero()
        {
            DefaultPropertyClass propertyClass = mSession.Single<DefaultPropertyClass>().Get();
            Assert.AreEqual(0, propertyClass.Integer);
        }

        [Test]
        public void DefaultPropertyClass_DateTimeIsMin()
        {
            DefaultPropertyClass propertyClass = mSession.Single<DefaultPropertyClass>().Get();
            Assert.AreEqual(DateTime.MinValue, propertyClass.Date);
        }


        [Test]
        public void DefaultFieldClass_StringIsEmpty()
        {
            DefaultFieldClass propertyClass = mSession.Single<DefaultFieldClass>().Get();
            Assert.AreEqual("", propertyClass.String);
        }

        [Test]
        public void DefaultFieldClass_FloatEqualsZero()
        {
            DefaultFieldClass propertyClass = mSession.Single<DefaultFieldClass>().Get();
            Assert.AreEqual(0, propertyClass.Float);
        }

        [Test]
        public void DefaultFieldClass_IntegerEqualsZero()
        {
            DefaultFieldClass propertyClass = mSession.Single<DefaultFieldClass>().Get();
            Assert.AreEqual(0, propertyClass.Integer);
        }

        [Test]
        public void DefaultFieldClass_DateTimeIsMin()
        {
            DefaultFieldClass propertyClass = mSession.Single<DefaultFieldClass>().Get();
            Assert.AreEqual(DateTime.MinValue, propertyClass.Date);
        }
    }
}
