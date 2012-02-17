using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AutoPoco.Engine;
using AutoPoco.Testing;
using Moq;
using System.Linq.Expressions;

namespace AutoPoco.Tests.Unit.Engine
{
    [TestFixture]
    public class CollectionContextTests
    {
        [Test]
        public void Impose_ImposesMemberOnAllItems()
        {
           // Add 20 mocks to a list
            List<Mock<IObjectGenerator<SimpleUser>>> mocks = new List<Mock<IObjectGenerator<SimpleUser>>>();
            for (int x = 0; x < 20; x++)
            {
                mocks.Add(new Mock<IObjectGenerator<SimpleUser>>());
            }

            // Set up
            CollectionContext<SimpleUser, List<SimpleUser>> context =
                new CollectionContext<SimpleUser, List<SimpleUser>>(
                        mocks.Select(x => (IObjectGenerator<SimpleUser>)x.Object));
            
            // Call
            Expression<Func<SimpleUser, string>> expr = x => x.LastName;
            context.Impose(expr, "Test");

            // Verify they were all called
            foreach (var mock in mocks)
            {
                mock.Verify(x => x.Impose(It.Is<Expression<Func<SimpleUser, string>>>(y => y == expr), It.Is<String>(y => y == "Test")), Times.Once());
            }
        }

        [Test]
        public void Impose_ReturnsSelf()
        {
            // Set up
            CollectionContext<SimpleUser, List<SimpleUser>> context =
                new CollectionContext<SimpleUser, List<SimpleUser>>(
                        new List<IObjectGenerator<SimpleUser>>());

            // Call
            Expression<Func<SimpleUser, string>> expr = x => x.LastName;
            var returnContext = context.Impose(expr, "Test");

            // Verify
            Assert.AreEqual(context, returnContext);
        }

        [Test]
        public void First_ReturnsSequence()
        {
            // Set up
            CollectionContext<SimpleUser, List<SimpleUser>> context =
                new CollectionContext<SimpleUser, List<SimpleUser>>(
                        new List<IObjectGenerator<SimpleUser>>());

            ICollectionSequenceSelectionContext<SimpleUser, List<SimpleUser>> sequence = context.First(10);
            Assert.NotNull(sequence);
        }

        [Test]
        public void Random_ReturnsSequence()
        {
            // Set up
            CollectionContext<SimpleUser, List<SimpleUser>> context =
                new CollectionContext<SimpleUser, List<SimpleUser>>(
                        new List<IObjectGenerator<SimpleUser>>());

            ICollectionSequenceSelectionContext<SimpleUser, List<SimpleUser>> sequence = context.Random(10);
            Assert.NotNull(sequence);
        }

        [Test]
        public void Get_List_ReturnsList()
        {
            // Add 20 mocks to a list
            List<Mock<IObjectGenerator<SimpleUser>>> mocks = new List<Mock<IObjectGenerator<SimpleUser>>>();
            for (int x = 0; x < 20; x++)
            {
                mocks.Add(new Mock<IObjectGenerator<SimpleUser>>());
            }

            // Set up
            CollectionContext<SimpleUser, List<SimpleUser>> context =
                new CollectionContext<SimpleUser, List<SimpleUser>>(
                        mocks.Select(x => (IObjectGenerator<SimpleUser>)x.Object));

            List<SimpleUser> users= context.Get();
            Assert.AreEqual(20, users.Count);  
        }

        [Test]
        public void Get_Array_ReturnsArray()
        {
            // Add 20 mocks to a list
            List<Mock<IObjectGenerator<SimpleUser>>> mocks = new List<Mock<IObjectGenerator<SimpleUser>>>();
            for (int x = 0; x < 20; x++)
            {
                mocks.Add(new Mock<IObjectGenerator<SimpleUser>>());
            }

            // Set up
            CollectionContext<SimpleUser, SimpleUser[]> context =
                new CollectionContext<SimpleUser, SimpleUser[]>(
                        mocks.Select(x => (IObjectGenerator<SimpleUser>)x.Object));

            SimpleUser[] users = context.Get();
            Assert.AreEqual(20, users.Length);  
        }
    }
}
