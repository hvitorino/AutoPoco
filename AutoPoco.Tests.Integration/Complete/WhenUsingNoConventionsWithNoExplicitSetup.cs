using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AutoPoco.Engine;
using AutoPoco.Testing;

namespace AutoPoco.Tests.Integration.Complete
{
    [TestFixture]
    public class WhenUsingNoConventionsWithNoExplicitSetup
    {
        IGenerationSession mSession;

        [SetUp]
        public void Setup()
        {
            mSession = AutoPocoContainer.Configure(x =>
            {
                x.Include<DefaultPropertyClass>();
                x.Include<DefaultFieldClass>();
            })
            .CreateSession();
        }

        [Test]
        public void DefaultPropertyClass_StringIsNull()
        {
            DefaultPropertyClass propertyClass = mSession.Single<DefaultPropertyClass>().Get();
            Assert.AreEqual(null, propertyClass.String);
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
        public void DefaultFieldClass_StringIsNull()
        {
            DefaultFieldClass propertyClass = mSession.Single<DefaultFieldClass>().Get();
            Assert.AreEqual(null, propertyClass.String);
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
