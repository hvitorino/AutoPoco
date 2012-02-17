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
    public class WhenTypeFieldConventionApplied : ConfigurationBaseTest
    {
        private IEngineConfiguration mConfiguration;
        private IEngineConfigurationType mType;

        private IEngineConfigurationTypeMember mTestField;
        private IEngineConfigurationTypeMember mTestIgnoreField;

        [SetUp]
        public void Setup()
        {
            this.Builder.Conventions(x =>
            {
                x.Register<TestFieldConvention>();
            });
            this.Builder.Include<TestFieldClass>()
                .Setup(x => x.Test).Default()
                .Setup(x => x.TestIgnore);

            mConfiguration = new EngineConfigurationFactory().Create(this.Builder, this.Builder.ConventionProvider);
            mType = mConfiguration.GetRegisteredType(typeof(TestFieldClass));
            mTestField = mType.GetRegisteredMembers().Where(x => x.Member.Name == "Test").Single();
            mTestIgnoreField = mType.GetRegisteredMembers().Where(x => x.Member.Name == "TestIgnore").Single();
        }


        [Test]
        public void FieldSourceIsSetFromConvention()
        {
            var source = mTestField.GetDatasources().First().Build();
            Assert.AreEqual(typeof(TestDataSource), source.GetType());
        }

        [Test]
        public void IgnoredFieldSourceIsNotSetFromConvention()
        {
            var source = mTestIgnoreField.GetDatasources().SingleOrDefault();
            Assert.Null(source);
        }

        public class TestFieldClass
        {
            public string Test;
            public string TestIgnore;
        }

        public class TestFieldConvention : ITypeFieldConvention
        {
            public void Apply(ITypeFieldConventionContext context)
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
