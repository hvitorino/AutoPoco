using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AutoPoco.Configuration;
using AutoPoco.Engine;

namespace AutoPoco.Tests.Integration.Configuration
{
    [TestFixture]
    public class WhenTypePropertyConventionApplied : ConfigurationBaseTest
    {
        private IEngineConfiguration mConfiguration;
        private IEngineConfigurationType mType;
        private IEngineConfigurationTypeMember mTestProperty;
        private IEngineConfigurationTypeMember mTestIgnoreProperty;

        [SetUp]
        public void Setup()
        {
            this.Builder.Conventions(x =>
            {
                x.Register<TestPropertyConvention>();
            });
            this.Builder.Include<TestPropertyClass>()
                .Setup(x => x.Test).Default()
                .Setup(x => x.TestIgnore);

            mConfiguration = new EngineConfigurationFactory().Create(this.Builder, this.Builder.ConventionProvider);
            mType = mConfiguration.GetRegisteredType(typeof(TestPropertyClass));
            mTestProperty = mType.GetRegisteredMembers().Where(x => x.Member.Name == "Test").Single();
            mTestIgnoreProperty = mType.GetRegisteredMembers().Where(x => x.Member.Name == "TestIgnore").Single();
        }


        [Test]
        public void TestPropertySourceIsSetFromConvention()
        {
            var source = mTestProperty.GetDatasources().First().Build();
            Assert.AreEqual(typeof(TestDataSource), source.GetType());
        }

        [Test]
        public void IgnoredPropertySourceIsNotSetFromConvention()
        {
            var source = mTestIgnoreProperty.GetDatasources().SingleOrDefault();
            Assert.Null(source);
        }

        public class TestPropertyClass
        {
            public string Test
            {
                get;
                set;
            }

            public string TestIgnore
            {
                get;
                set;
            }
        }

        public class TestPropertyConvention : ITypePropertyConvention
        {
            public void Apply(ITypePropertyConventionContext context)
            {
                context.SetSource<TestDataSource>();
            }

            public void SpecifyRequirements(ITypeMemberConventionRequirements requirements)
            {
                requirements.Name(x => x == "Test");
            }
        }

        public class TestDataSource : IDatasource
        {
            public object Next(IGenerationContext context)
            {
                return null;
            }
        }
    }
}
