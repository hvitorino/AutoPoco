using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AutoPoco.Testing;
using AutoPoco.Configuration;
using AutoPoco.DataSources;
using AutoPoco.Util;
using AutoPoco.Engine;

namespace AutoPoco.Tests.Integration.Configuration
{
    [TestFixture]
    public class WhenMethodIsRegisteredWithArguments : ConfigurationBaseTest
    {
        protected IEngineConfiguration Configuration
        {
            get;
            private set;
        }
        
        private IEngineConfigurationType mEngineConfigurationType;
        private EngineTypeMethodMember mSingleArgMethod;
        private EngineTypeMethodMember mDoubleArgMethod;
        
        [SetUp]
        public void SetupObjects()
        {
            this.Builder.Include<SimpleMethodClass>()
                .Invoke(x=>x.SetSomething("Literal"))
                .Invoke(x=>x.SetSomething(
                    Use.Source<String, FirstNameSource>(),
                    Use.Source<String, LastNameSource>()));

            Configuration = new EngineConfigurationFactory().Create(this.Builder, this.Builder.ConventionProvider);

            // Get some info for the tests
            mEngineConfigurationType = Configuration.GetRegisteredType(typeof(SimpleMethodClass));
            mSingleArgMethod = (EngineTypeMethodMember)ReflectionHelper.GetMember(
                typeof(SimpleMethodClass).GetMethod("SetSomething", new Type[] { typeof(string) }));
            mDoubleArgMethod = (EngineTypeMethodMember)ReflectionHelper.GetMember(
                typeof(SimpleMethodClass).GetMethod("SetSomething", new Type[] { typeof(string), typeof(string) }));
        }

        [Test]
        public void BothMethodsAreRegistered()
        {
            Assert.AreEqual(2, mEngineConfigurationType.GetRegisteredMembers().Count());
        }

        [Test]
        public void MethodWithLiteralArgument_ExposedInConfiguration()
        {
            IEngineConfigurationTypeMember member = mEngineConfigurationType.GetRegisteredMember(mSingleArgMethod);
            Assert.NotNull(member);
        }

        [Test]
        public void MethodWithLiteralArgument_HasOneDatasource()
        {
            IEngineConfigurationTypeMember member = mEngineConfigurationType.GetRegisteredMember(mSingleArgMethod);
            Assert.AreEqual(1, member.GetDatasources().Count());
        }

        [Test]
        public void MethodWithLiteralArgument_HasValueDatasource()
        {
            IEngineConfigurationTypeMember member = mEngineConfigurationType.GetRegisteredMember(mSingleArgMethod);

            IEngineConfigurationDatasource configurationSource = member.GetDatasources().Single();
            IDatasource source = configurationSource.Build();

            Assert.AreEqual( typeof(ValueSource), source.GetType());
        }
    
        [Test]
        public void MethodWithDatasourceArgument_ExposedInConfiguration()
        {
            IEngineConfigurationTypeMember member = mEngineConfigurationType.GetRegisteredMember(mDoubleArgMethod);
            Assert.NotNull(member);
        }

        [Test]
        public void MethodWithTwoArguments_HasOneTwosources()
        {
            IEngineConfigurationTypeMember member = mEngineConfigurationType.GetRegisteredMember(mDoubleArgMethod);

            Assert.AreEqual(2, member.GetDatasources().Count());
        }

        [Test]
        [TestCase(0, typeof(FirstNameSource))]
        [TestCase(1, typeof(LastNameSource))]
        public void MethodWithTwoArguments_HasCorrectDatasource(int skip, Type expectedType)
        {
            IEngineConfigurationTypeMember member = mEngineConfigurationType.GetRegisteredMember(mDoubleArgMethod);
            IEngineConfigurationDatasource sourceConfig = member.GetDatasources().Skip(skip).First();
            IDatasource source = sourceConfig.Build();

            Assert.AreEqual(expectedType, source.GetType());
        }
    }
}
