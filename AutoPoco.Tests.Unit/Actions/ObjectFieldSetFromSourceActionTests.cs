using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AutoPoco.Actions;
using AutoPoco.Util;
using AutoPoco.Testing;
using AutoPoco.Configuration;
using Moq;
using AutoPoco.Engine;

namespace AutoPoco.Tests.Unit.Actions
{
    [TestFixture]
    public class ObjectFieldSetFromSourceActionTests
    {
        private Mock<IDatasource> mSourceMock;
        private GenerationContext mContext;
        private IGenerationContextNode mParentNode;
        private ObjectFieldSetFromSourceAction mAction;

        [SetUp]
        public void SetupObjects()
        {
            mSourceMock = new Mock<IDatasource>();
            mParentNode = new TypeGenerationContextNode(null, null);
            mContext = new GenerationContext(null, mParentNode);

            mAction = new ObjectFieldSetFromSourceAction((EngineTypeFieldMember)
               ReflectionHelper.GetMember<SimpleFieldClass>(x => x.SomeField), mSourceMock.Object);
        }

        [Test]
        public void Enact_SetsFieldWithSourceValue()
        {
            mSourceMock.Setup(x => x.Next(It.IsAny<IGenerationContext>())).Returns("Test");

            SimpleFieldClass target = new SimpleFieldClass();
            mAction.Enact(mContext, target);
            Assert.AreEqual("Test", target.SomeField);
        }

        [Test]
        public void Enact_CreateTypeFieldContext_Into_Session()
        {
            SimpleFieldClass target = new SimpleFieldClass();

            mAction.Enact(mContext, target);

            mSourceMock.Verify(x => x.Next(It.Is<IGenerationContext>(y =>
                y.Node is TypeFieldGenerationContextNode &&
                y.Node.Parent == mParentNode)),
                               Times.Once());

        }
    }
}
