using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Testing;
using NUnit.Framework;
using AutoPoco.Engine;

namespace AutoPoco.Tests.Integration.Complete
{
    [TestFixture]
    public class WhenSettingALimitOnRecursiveTrees
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
                    .Setup(y => y.Parent).Default();
            })
            .CreateSession(10);
        }


        [Test]
        public void Requesting_Recursive_Type_Honours_Length_Limit()
        {
            var node = mSession.Next<SimpleNode>();
            int x = 0;
            while (node != null)
            {
                node = node.Parent;
                x++;

                if (x > 10) { Assert.Fail("Recursive limit was not honoured"); break; }
            }
        }
    }
}
