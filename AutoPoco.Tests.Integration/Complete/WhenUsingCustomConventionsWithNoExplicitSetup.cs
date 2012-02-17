using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AutoPoco.Engine;
using AutoPoco.Configuration;

namespace AutoPoco.Tests.Integration.Complete
{
    [TestFixture]
    public class WhenUsingCustomConventionsWithNoExplicitSetup
    {
        IGenerationSession mSession;

        [SetUp]
        public void Setup()
        {
            mSession = AutoPocoContainer.Configure(x =>
            {
                x.Conventions(c =>
                {
                    c.Register<TestTypeConvention>();
                    c.Register<TestPropertyConvention>();
                    c.Register<TestFieldConvention>();
                });
                x.Include<TestType>();
               
            })
            .CreateSession();
        }

        [Test]
        public void TestProperty_HasTestValue()
        {
           TestType testType = mSession.Single<TestType>().Get();
           Assert.AreEqual("Test", testType.TestProperty);
        }

        [Test]
        public void TestField_HasTestValue()
        {
            TestType testType = mSession.Single<TestType>().Get();
            Assert.AreEqual("Test", testType.TestField);
        }

        [Test]
        public void TestEmptyField_IsNull()
        {
            TestType testType = mSession.Single<TestType>().Get();
            Assert.IsNull(testType.TestEmptyField);
        }

        [Test]
        public void TestEmptyProperty_IsNull()
        {
            TestType testType = mSession.Single<TestType>().Get();
            Assert.IsNull(testType.TestEmptyProperty);
        }

        public class TestType
        {
            public string TestProperty
            {
                get;
                set;
            }

            public string TestField;

            public string TestEmptyProperty
            {
                get;
                set;
            }

            public string TestEmptyField;
        }

        public class TestTypeConvention : ITypeConvention
        {
            public void Apply(ITypeConventionContext context)
            {
                context.RegisterProperty(typeof(TestType).GetProperty("TestProperty"));
                context.RegisterField(typeof(TestType).GetField("TestField"));
            }
        }

        public class TestFieldConvention : ITypeFieldConvention
        {
            public void Apply(ITypeFieldConventionContext context)
            {
                context.SetValue("Test");
            }

            public void SpecifyRequirements(ITypeMemberConventionRequirements requirements)
            {
               
            }
        }

        public class TestPropertyConvention : ITypePropertyConvention
        {
            public void Apply(ITypePropertyConventionContext context)
            {
                context.SetValue("Test");
            }

            public void SpecifyRequirements(ITypeMemberConventionRequirements requirements)
            {
                
            }
        }




    }
}
