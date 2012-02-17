using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AutoPoco.Engine;
using Moq;
using AutoPoco.Testing;
using System.Linq.Expressions;

namespace AutoPoco.Tests.Unit.Engine
{
    [TestFixture]
    public class CollectionSequenceSelectionContextTests
    {
        Mock<ICollectionContext<SimpleUser, List<SimpleUser>>> mParentContextMock;

        [SetUp]
        public void CreateObjects()
        {
            mParentContextMock = new Mock<ICollectionContext<SimpleUser, List<SimpleUser>>>();
        }

        [Test]
        public void Impose_ImposesMemberOnAllCurrentItems()
        {
            // Add 20 mocks to a list
            List<Mock<IObjectGenerator<SimpleUser>>> mocks = new List<Mock<IObjectGenerator<SimpleUser>>>();
            for (int x = 0; x < 20; x++)
            {
                mocks.Add(new Mock<IObjectGenerator<SimpleUser>>());
            }            

            // Set up
            CollectionSequenceSelectionContext<SimpleUser, List<SimpleUser>> context =
                new CollectionSequenceSelectionContext<SimpleUser, List<SimpleUser>>(
                    mParentContextMock.Object,
                    mocks.Select(x => (IObjectGenerator<SimpleUser>)x.Object),
                    10);

            // Call
            Expression<Func<SimpleUser, string>> expr = x => x.LastName;
            context.Impose(expr, "Test");

            // Verify the first 10 in the sequence were called
            for(int x =0 ; x < 10 ; x++)
            {
                var mock = mocks[x];
                mock.Verify(m => m.Impose(It.Is<Expression<Func<SimpleUser, string>>>(y => y == expr), It.Is<String>(y => y == "Test")), Times.Once());
            }

            // Verify the last 10 in the sequence were not called
            for (int x = 10; x < 20; x++)
            {
                var mock = mocks[x];
                mock.Verify(m => m.Impose(It.Is<Expression<Func<SimpleUser, string>>>(y => y == expr), It.Is<String>(y => y == "Test")), Times.Never());
            }
        }

        [Test]
        public void AfterCreationRemainingIsExpectedValue()
        {
            // Add 20 mocks to a list
            List<Mock<IObjectGenerator<SimpleUser>>> mocks = new List<Mock<IObjectGenerator<SimpleUser>>>();
            for (int x = 0; x < 20; x++)
            {
                mocks.Add(new Mock<IObjectGenerator<SimpleUser>>());
            }

            // Set up
            CollectionSequenceSelectionContext<SimpleUser, List<SimpleUser>> context =
                new CollectionSequenceSelectionContext<SimpleUser, List<SimpleUser>>(
                    mParentContextMock.Object,
                        mocks.Select(x => (IObjectGenerator<SimpleUser>)x.Object), 
                        10);
            
            Assert.AreEqual(10, context.Remaining);
        }

        [Test]
        public void AfterNextRemainingIsExpectedValue()
        {
            // Add 20 mocks to a list
            List<Mock<IObjectGenerator<SimpleUser>>> mocks = new List<Mock<IObjectGenerator<SimpleUser>>>();
            for (int x = 0; x < 20; x++)
            {
                mocks.Add(new Mock<IObjectGenerator<SimpleUser>>());
            }

            // Set up
            CollectionSequenceSelectionContext<SimpleUser, List<SimpleUser>> context =
                new CollectionSequenceSelectionContext<SimpleUser, List<SimpleUser>>(
                    mParentContextMock.Object,
                        mocks.Select(x => (IObjectGenerator<SimpleUser>)x.Object), 
                        10);

            // Forward ho
            context.Next(5);

            Assert.AreEqual(5, context.Remaining);
        }

        [Test]
        public void All_ReturnsParent()
        {
            // Set up
            CollectionSequenceSelectionContext<SimpleUser, List<SimpleUser>> context =
                new CollectionSequenceSelectionContext<SimpleUser, List<SimpleUser>>(
                    mParentContextMock.Object,
                        new List<IObjectGenerator<SimpleUser>>(),
                        0);

            var parent =  context.All();
            Assert.AreEqual(mParentContextMock.Object, parent);
        }
            
        [Test]
        public void Impose_ReturnsSelf()
        {
            // Set up
            CollectionSequenceSelectionContext<SimpleUser, List<SimpleUser>> context =
                new CollectionSequenceSelectionContext<SimpleUser, List<SimpleUser>>(
                    mParentContextMock.Object,
                        new List<IObjectGenerator<SimpleUser>>(),
                        0);

            // Call
            Expression<Func<SimpleUser, string>> expr = x => x.LastName;
            var returnContext = context.Impose(expr, "Test");
            
            // Verify
            Assert.AreEqual(context, returnContext);
        }
    }
}
