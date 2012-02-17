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
    public class WhenCreatingObjectsWithHaveAnInterfaceThatIsNotConfigured
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
                x.Include<SimpleBaseClass>();                    
            })
            .CreateSession();
        }

        [Test]
        public void Default_Interface_Rules_Cascade_Onto_Implementing_Types()
        {
            var obj = mSession.Single<SimpleBaseClass>().Get();
            Assert.IsNotNull(obj.InterfaceValue);
        }
    }
}
