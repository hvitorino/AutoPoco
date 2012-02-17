using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace AutoPoco.Tests.Integration.Bugs
{
    [TestFixture]
    public class WhenConfiguringClosedGenericCollection
    {
        [Test]
        public void Object_With_Collection_As_A_Property_Can_Be_Requested_From_Session()
        {
            var session = AutoPocoContainer.Configure(c =>
                c.Include<TestObject>()
                .Setup(x => x.Children).Collection(3, 3)).CreateSession();

            var result = session.Next<TestObject>();
            Assert.NotNull(result);
            Assert.AreEqual(3, result.Children.Count);
        }

        public class TestObject
        {
            public ClosedGenericCollection Children { get; set; }
        }
        public class ClosedGenericCollection : List<int> { }

    }
}
