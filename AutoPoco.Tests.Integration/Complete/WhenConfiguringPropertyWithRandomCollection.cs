using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Engine;
using AutoPoco.Testing;
using NUnit.Framework;

namespace AutoPoco.Tests.Integration.Complete
{
    [TestFixture]
    public class WhenConfiguringPropertyWithRandomCollection
    {
        IGenerationSession mSession;

        [SetUp]
        public void Setup()
        {
            mSession = AutoPocoContainer.Configure(x =>
            {
                x.Conventions(c => c.UseDefaultConventions());
                x.Include<SimpleNode>()
                    .Setup(y => y.Children).Collection(0, 3);
            })
            .CreateSession();
        }

        [Test]
        public void Collection_Is_Set_With_Valid_Number_In_It()
        {
            for(int x = 0; x < 10 ; x++)
            {
                var node = mSession.Next<SimpleNode>();
                Assert.True(node.Children.Count >= 0 && node.Children.Count <= 3);
            }
        }
    }
}
