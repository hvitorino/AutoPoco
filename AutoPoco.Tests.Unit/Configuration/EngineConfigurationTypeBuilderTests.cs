using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Engine;
using NUnit.Framework;
using AutoPoco.Configuration;
using AutoPoco.DataSources;
using AutoPoco.Testing;
using Moq;
using AutoPoco.Configuration.Providers;

namespace AutoPoco.Tests.Unit.Configuration
{
    [TestFixture]
    public class EngineConfigurationTypeBuilderTests
    {
        [Test]
        public void Generic_Setup_Ctor_With_Factory_Sets_Factory()
        {
            var configuration = new EngineConfigurationTypeBuilder<SimpleCtorClass>();
            configuration.ConstructWith<TestFactory>();

            var t = ((IEngineConfigurationTypeProvider) configuration).GetFactory();
            Assert.AreEqual(typeof (TestFactory), t.Build().GetType());
        }

        [Test]
        public void Generic_Setup_Ctor_With_Factory_With_Args_Sets_Factory_With_Args()
        {
            var configuration = new EngineConfigurationTypeBuilder<SimpleCtorClass>();
            configuration.ConstructWith<TestFactory>("one", "two");

            var t = (TestFactory)((IEngineConfigurationTypeProvider) configuration).GetFactory().Build();

            Assert.AreEqual("one", t.ArgOne);
            Assert.AreEqual("two", t.ArgTwo);
        }

        [Test]
        public void NonGeneric_Setup_WithProperty_ReturnsMemberConfiguration()
        {
            IEngineConfigurationTypeBuilder configuration = new EngineConfigurationTypeBuilder(typeof(SimplePropertyClass));
            IEngineConfigurationTypeMemberBuilder memberConfiguration = configuration.SetupProperty("SomeProperty");

            Assert.NotNull(memberConfiguration);
        }

        [Test]
        public void NonGeneric_Setup_WithField_ReturnsMemberConfiguration()
        {
            IEngineConfigurationTypeBuilder configuration = new EngineConfigurationTypeBuilder(typeof(SimpleFieldClass));
            IEngineConfigurationTypeMemberBuilder memberConfiguration = configuration.SetupField("SomeField");

            Assert.NotNull(memberConfiguration);
        }

        [Test]
        public void NonGeneric_Setup_WithNonExistentProperty_ThrowsArgumentException()
        {
            IEngineConfigurationTypeBuilder configuration = new EngineConfigurationTypeBuilder(typeof(SimplePropertyClass));

            Assert.Throws<ArgumentException>(() => { configuration.SetupProperty("SomeNonExistantProperty"); });
        }

        [Test]
        public void NonGeneric_Setup_WithNonExistentField_ThrowsArgumentException()
        {
            IEngineConfigurationTypeBuilder configuration = new EngineConfigurationTypeBuilder(typeof(SimpleFieldClass));
            Assert.Throws<ArgumentException>(() => { configuration.SetupProperty("SomeNonExistantField"); });
        }
        
        [Test]
        public void NonGeneric_SetupMethod_WithNonExistentMethod_ThrowsArgumentException()
        {
            EngineConfigurationTypeBuilder configuration = new EngineConfigurationTypeBuilder(typeof(SimpleMethodClass));
            Assert.Throws<ArgumentException>(() => { configuration.SetupMethod("DoesNotExist"); });
        }
        
        [Test]
        public void Generic_Invoke_WithFunc_ReturnsConfiguration()
        {
            EngineConfigurationTypeBuilder<SimpleMethodClass> configuration = new EngineConfigurationTypeBuilder<SimpleMethodClass>();
            IEngineConfigurationTypeBuilder<SimpleMethodClass> returnValue = configuration.Invoke(x => x.SetSomething("Something"));

            Assert.AreEqual(configuration, returnValue);  
        }

        [Test]
        public void Generic_Invoke_WithAction_ReturnsConfiguration()
        {
            EngineConfigurationTypeBuilder<SimpleMethodClass> configuration = new EngineConfigurationTypeBuilder<SimpleMethodClass>();
            IEngineConfigurationTypeBuilder<SimpleMethodClass> returnValue = configuration.Invoke(x => x.ReturnSomething());

            Assert.AreEqual(configuration, returnValue);
        }

        [Test]
        public void NonGeneric_SetupMethod_ReturnsConfiguration()
        {
            EngineConfigurationTypeBuilder configuration = new EngineConfigurationTypeBuilder(typeof(SimpleMethodClass));
            IEngineConfigurationTypeBuilder returnValue = configuration.SetupMethod("ReturnSomething");

            Assert.AreEqual(configuration, returnValue);
        }

        [Test]
        public void NonGeneric_SetupMethodWithParameters_ReturnsConfiguration()
        {
            EngineConfigurationTypeBuilder configuration = new EngineConfigurationTypeBuilder(typeof(SimpleMethodClass));
            var context = new MethodInvocationContext();
            context.AddArgumentValue("Hello");
            IEngineConfigurationTypeBuilder returnValue = configuration.SetupMethod("SetSomething", context);

            Assert.AreEqual(configuration, returnValue);
        }

        [Test]
        public void Generic_Setup_WithProperty_ReturnsMemberConfiguration()
        {
            EngineConfigurationTypeBuilder<SimplePropertyClass> configuration = new EngineConfigurationTypeBuilder<SimplePropertyClass>();
            IEngineConfigurationTypeMemberBuilder<SimplePropertyClass, String> memberConfiguration = configuration.Setup(x => x.SomeProperty);

            Assert.NotNull(memberConfiguration);
        }

        [Test]
        public void Generic_Setup_WithField_ReturnsMemberConfiguration()
        {
            EngineConfigurationTypeBuilder<SimpleFieldClass> configuration = new EngineConfigurationTypeBuilder<SimpleFieldClass>();
            IEngineConfigurationTypeMemberBuilder<SimpleFieldClass, String> memberConfiguration = configuration.Setup(x => x.SomeField);

            Assert.NotNull(memberConfiguration);
        }

        [Test]
        public void GetConfigurationType_ReturnsType()
        {
            IEngineConfigurationTypeProvider configuration = new EngineConfigurationTypeBuilder<SimpleFieldClass>();
            Type type = configuration.GetConfigurationType();

            Assert.AreEqual(typeof(SimpleFieldClass), type);
        }

        [Test]
        public void GetConfigurationMembers_ReturnsMembers()
        {
            EngineConfigurationTypeBuilder<SimpleFieldClass> configuration = new EngineConfigurationTypeBuilder<SimpleFieldClass>();

            configuration.Setup(x => x.SomeField);
            ((IEngineConfigurationTypeBuilder)configuration).SetupField("SomeOtherField");

            var members = ((IEngineConfigurationTypeProvider)configuration).GetConfigurationMembers();

            Assert.AreEqual(2, members.Count());
        }

        [Test]
        public void GetConfigurationMembersWithMethods_ReturnsMembers()
        {
            EngineConfigurationTypeBuilder<SimpleMethodClass> configuration = new EngineConfigurationTypeBuilder<SimpleMethodClass>();

            configuration.Invoke(x => x.SetSomething("Literal"));
            configuration.SetupMethod("ReturnSomething");

            var members = ((IEngineConfigurationTypeProvider)configuration).GetConfigurationMembers();

            Assert.AreEqual(2, members.Count());
        }

        [Test]
        public void Invoke_WithInvalidMethodCall_ThrowsArgumentException()
        {
            EngineConfigurationTypeBuilder<SimpleMethodClass> configuration = new EngineConfigurationTypeBuilder<SimpleMethodClass>();
            Assert.Throws<ArgumentException>(() =>
            {
                configuration.Invoke(x => x.SetSomething(new SimpleMethodClass().ReturnSomething()));
            });
        }

        [Test]
        public void Invoke_WithInvalidGenericMethodCall_ThrowsArgumentException()
        {
            EngineConfigurationTypeBuilder<SimpleMethodClass> configuration = new EngineConfigurationTypeBuilder<SimpleMethodClass>();
            Assert.Throws<ArgumentException>(() =>
            {
                configuration.Invoke(x => x.SetSomething(TestGenericClass.Something<string>()));
            });
        }

        public static class TestGenericClass
        {
            public static T Something<T>() { return default(T); }
        }

        public class TestFactory : IDatasource< SimpleCtorClass>
        {
            public readonly string ArgOne;
            public readonly string ArgTwo;

            public TestFactory()
            {
            }

            public TestFactory(string argOne, string argTwo)
            {
                this.ArgOne = argOne;
                this.ArgTwo = argTwo;
            }

            public object Next(IGenerationContext context)
            {
                throw new NotImplementedException();
            }
        }
    }
}
