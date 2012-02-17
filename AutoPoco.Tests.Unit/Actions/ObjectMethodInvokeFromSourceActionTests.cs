using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AutoPoco.Engine;
using Moq;
using AutoPoco.Actions;
using AutoPoco.Configuration;
using AutoPoco.Testing;
using AutoPoco.Util;

namespace AutoPoco.Tests.Unit.Actions
{
    [TestFixture]
    public class ObjectMethodInvokeFromSourceActionTests
    {
        private Mock<IDatasource> mSourceMock;
        private GenerationContext mContext;
        private IGenerationContextNode mParentNode;
        private EngineTypeMethodMember mDoubleArgMethod;
        private ObjectMethodInvokeFromSourceAction mDoubleArgAction;

        [SetUp]
        public void SetupObjects()
        {
            mSourceMock = new Mock<IDatasource>();
            mParentNode = new TypeGenerationContextNode(null, null);
            mContext = new GenerationContext(null, mParentNode);

            mDoubleArgMethod = (EngineTypeMethodMember)ReflectionHelper.GetMember(typeof(SimpleMethodClass).GetMethod("SetSomething", new Type[] { typeof(string), typeof(string) }));

            mDoubleArgAction = new ObjectMethodInvokeFromSourceAction(mDoubleArgMethod, new IDatasource[] { mSourceMock.Object, mSourceMock.Object });
        }


        [Test]
        public void SharedDataSourceWithTwoParams_Next_Invoked_With_Wrapped_Up_SessionTwice()
        {
            SimpleMethodClass target = new SimpleMethodClass();
            mDoubleArgAction.Enact(mContext, target);

            mSourceMock.Verify(x => x.Next(It.Is<IGenerationContext>(y => y.Node is TypeMethodGenerationContextNode && y.Node.Parent == mParentNode)), Times.Exactly(2));
        }

        [Test]
        public void SharedDataSourceWithTwoParams_FirstParamPassedCorrectly()
        {
            SimpleMethodClass target = new SimpleMethodClass();
            int callCount = 0;
            mSourceMock.Setup(x => x.Next(It.IsAny<IGenerationContext>())).Returns(() =>
            {
                callCount++;
                return callCount.ToString();
            });

            mDoubleArgAction.Enact(mContext, target);


            Assert.AreEqual("1", target.Value);
        }

        [Test]
        public void SharedDataSourceWithTwoParams_SecondParamPassedCorrectly()
        {
            SimpleMethodClass target = new SimpleMethodClass();
            int callCount = 0;
            mSourceMock.Setup(x => x.Next(It.IsAny<IGenerationContext>())).Returns(() =>
            {
                callCount++;
                return callCount.ToString();
            });

            mDoubleArgAction.Enact(mContext, target);


            Assert.AreEqual("2", target.OtherValue);
        }



    }
}
