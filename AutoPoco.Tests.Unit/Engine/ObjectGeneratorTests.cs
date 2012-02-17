using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

using AutoPoco.Engine;
using AutoPoco.Util;
using AutoPoco.Configuration;
using AutoPoco.Testing;
using Moq;
using AutoPoco.DataSources;

namespace AutoPoco.Tests.Unit.Engine
{
    [TestFixture]
    public class ObjectGeneratorTests
    {
        string mTestPropertyValue = "TestValue";
        ObjectGenerator<SimpleUser> mUserGenerator;
        ObjectGenerator<SimpleMethodClass> mMethodGenerator;
        private GenerationContext mGenerationContext;

        [SetUp]
        public void TestSetup()
        {
            Mock<IEngineConfigurationType> type = new Mock<IEngineConfigurationType>();
            type.SetupGet(x => x.RegisteredType).Returns(typeof(SimpleUser));
            ObjectBuilder builder = new ObjectBuilder(type.Object);
            builder.AddAction(
                new ObjectPropertySetFromSourceAction(
                    ReflectionHelper.GetMember<SimpleUser>(x=>x.FirstName) as EngineTypePropertyMember,
                    new SimpleDataSource(mTestPropertyValue)
                    ));
            builder.AddAction(
                new ObjectPropertySetFromSourceAction(
                    ReflectionHelper.GetMember<SimpleUser>(x => x.LastName) as EngineTypePropertyMember,
                    new SimpleDataSource(mTestPropertyValue)
                    ));
            builder.AddAction(
                new ObjectPropertySetFromSourceAction(
                    ReflectionHelper.GetMember<SimpleUser>(x => x.EmailAddress) as EngineTypePropertyMember,
                    new SimpleDataSource(mTestPropertyValue)
                    ));

            Mock<IGenerationConfiguration> builderRepository = new Mock<IGenerationConfiguration>();
            builderRepository.Setup(x=>x.GetBuilderForType(typeof(SimpleUser))).Returns(builder);
            builderRepository.SetupGet(x => x.RecursionLimit).Returns(10);

            mGenerationContext = new GenerationContext(builderRepository.Object);
            mMethodGenerator = new ObjectGenerator<SimpleMethodClass>(
                mGenerationContext, builder);

            mUserGenerator = new ObjectGenerator<SimpleUser>(mGenerationContext, builder);               
        }

        [Test]
        public void Single_ReturnsSingleObject()
        {
            SimpleUser user = mUserGenerator.Get();
            Assert.AreEqual(mTestPropertyValue, user.EmailAddress);
            Assert.AreEqual(mTestPropertyValue, user.FirstName);
            Assert.AreEqual(mTestPropertyValue, user.LastName);
        }

        [Test]
        public void Single_Wraps_Up_Context_With_Type_Context()
        {
            Mock<IObjectAction> action = new Mock<IObjectAction>();
            Object actionObject = null;

            mUserGenerator.AddAction(action.Object);
            SimpleUser user = mUserGenerator.Get();

            action.Verify(x => x.Enact(It.Is<IGenerationContext>(
                y => y.Node is TypeGenerationContextNode), user), Times.Once());
        }

        [Test]
        public void Single_AppliesExtraActions()
        {
            Mock<IObjectAction> action = new Mock<IObjectAction>();
            Object actionObject = null;
            action.Setup(x => x.Enact(It.IsAny<IGenerationContext>(), It.IsAny<Object>()))
                .Callback((IGenerationSession session, Object dest) =>
                {
                    actionObject = dest;
                });
            mUserGenerator.AddAction(action.Object);
            SimpleUser user = mUserGenerator.Get();

            Assert.AreEqual(actionObject, user);
        }

        [Test]
        public void Impose_OverridesDataSource()
        {
            String newValue = "SomethingElse";
            SimpleUser user = mUserGenerator.Impose(x => x.EmailAddress, newValue).Get();

            Assert.AreEqual(newValue, user.EmailAddress);
            Assert.AreEqual(mTestPropertyValue, user.FirstName);
            Assert.AreEqual(mTestPropertyValue, user.LastName);
        }

        [Test]
        public void Impose_ReturnsGenerator()
        {
             IObjectGenerator<SimpleUser> generator = mUserGenerator.Impose(x => x.EmailAddress, "");

             Assert.AreEqual(mUserGenerator, generator);
        }

        [Test]
        public void Invoke_WithFunc_ReturnsGenerator()
        {
            IObjectGenerator<SimpleMethodClass> generator = mMethodGenerator.Invoke(
                x => x.ReturnSomething());

            Assert.AreEqual(mMethodGenerator, generator);
        }

        [Test]
        public void Invoke_WithAction_ReturnsGenerator()
        {
            IObjectGenerator<SimpleMethodClass> generator = mMethodGenerator.Invoke(
                x => x.SetSomething("Test"));

            Assert.AreEqual(mMethodGenerator, generator);
        }
    }
}
