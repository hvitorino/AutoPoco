using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Moq;
using AutoPoco.Engine;
using AutoPoco.Actions;
using AutoPoco.Util;
using AutoPoco.Testing;
using AutoPoco.Configuration;

namespace AutoPoco.Tests.Unit.Actions
{
    [TestFixture]
    public class ObjectPropertySetFromSourceActionTests
    {
        private Mock<IDatasource> mSourceMock;
        private GenerationContext mContext;
        private IGenerationContextNode mParentNode;
        private ObjectPropertySetFromSourceAction mAction;

        [SetUp]
        public void SetupObjects()
        {
            mSourceMock = new Mock<IDatasource>();
            mParentNode = new TypeGenerationContextNode(null, null);
            mContext = new GenerationContext(null, mParentNode);
            mAction = new ObjectPropertySetFromSourceAction((EngineTypePropertyMember)
               ReflectionHelper.GetMember<SimplePropertyClass>(x => x.SomeProperty), mSourceMock.Object);
        }

        [Test]
        public void Enact_SetsFieldWithSourceValue()
        {
            mSourceMock.Setup(x => x.Next(It.IsAny<IGenerationContext>())).Returns("Test");

            SimplePropertyClass target = new SimplePropertyClass();
            mAction.Enact(mContext, target);

            Assert.AreEqual("Test", target.SomeProperty);
        }

        [Test]
        public void Enact_Provides_Source_With_Wrapped_Up_Session()
        {
            SimplePropertyClass target = new SimplePropertyClass();
            mAction.Enact(mContext, target);
            
            mSourceMock.Verify(x => x.Next(It.Is<IGenerationContext>(y =>
                y.Node is TypePropertyGenerationContextNode &&
                y.Node.Parent == mParentNode)),
                               Times.Once());
        }
    }
}
