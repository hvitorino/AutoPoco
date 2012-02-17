using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AutoPoco.Engine;
using AutoPoco.Testing;

namespace AutoPoco.Tests.Integration.Complete
{
    [TestFixture]
    public class WhenOverridingDataSourceOnObjectCreation
    {
        IGenerationSession mSession;
        IList<SimpleUser> mResults;

        [SetUp]
        public void Setup()
        {
            mSession = AutoPocoContainer.Configure(x =>
            {
                x.Conventions(c =>
                {
                    c.UseDefaultConventions();
                });
                x.AddFromAssemblyContainingType<SimpleUser>();
            })
            .CreateSession();

            mResults = mSession.List<SimpleUser>(5)
                .Source(x => x.EmailAddress, new DummySource())
                .Get();

        }

        [Test]
        public void OverriddenDataSourceIsUsedForAllUsers()
        {
            for (int x = 0; x < 5; x++)
            {
                var user = mResults[x];
                Assert.AreEqual("Test" + x, user.EmailAddress);
            }
        }


        private class DummySource : IDatasource<string>
        {
            private int count = 0;

            public object Next(IGenerationContext context)
            {
                return "Test" + count++;
            }
        }  
    }
}
