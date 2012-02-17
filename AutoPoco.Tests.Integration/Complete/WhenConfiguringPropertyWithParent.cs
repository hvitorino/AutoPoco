using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AutoPoco;
using AutoPoco.Engine;
using AutoPoco.Testing;
using NUnit.Framework;

namespace AutoPoco.Tests.Integration.Complete
{
    [TestFixture]
    public class WhenConfiguringPropertyWithParent
    {
        IGenerationSession mSession;

        [SetUp]
        public void Setup()
        {
            mSession = AutoPocoContainer.Configure(x =>
            {
                x.Conventions(c =>
                {
                    c.UseDefaultConventions();
                });
                x.Include<SimpleNode>()
                    .Setup(y => y.Parent).FromParent()
                    .Setup(y => y.Children).Collection(1, 1);


            })
            .CreateSession();
        }


        [Test]
        public void Property_Is_Set_With_Null_Value_If_No_Parent_Exists()
        {
            var node = mSession.Next<SimpleNode>();
            Assert.Null(node.Parent);
        }

        [Test]
        public  void Property_Is_Set_With_Parent_Value_If_Parent_Exists()
        {
            var node = mSession.Next<SimpleNode>();
            Assert.AreEqual(node, node.Children.First().Parent);
        }
    }
}
