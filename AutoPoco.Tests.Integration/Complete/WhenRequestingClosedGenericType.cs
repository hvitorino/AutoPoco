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
    public class WhenRequestingClosedGenericType
    {
        IGenerationSession mSession;

        [SetUp]
        public void Setup()
        {
            mSession = AutoPocoContainer.Configure(x =>
            {
                x.Include<OpenGeneric<Object>>();
            })
            .CreateSession();
        }

        [Test]
        public void Created_Object_Is_Returned()
        {
            OpenGeneric<Object> created = mSession.Single<OpenGeneric<Object>>().Get();
            Assert.NotNull(created);
        }
    }
}
