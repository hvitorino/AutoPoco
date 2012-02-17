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
    public class WhenRequestingUnknownType
    {
        IGenerationSession mGenerationSession;

        [SetUp]
        public void SetupTest()
        {
           mGenerationSession  = AutoPocoContainer.CreateDefaultSession();
        }

        [Test]
        public void With_Basic_Type_Valid_Object_Is_Returned()
        {
            SimpleUser user = mGenerationSession.Single<SimpleUser>().Get();
            Assert.NotNull(user);
        }

        [Test]
        public void With_Derived_Type_Base_Properties_Are_Filled()
        {
            SimpleDerivedClass obj = mGenerationSession.Single<SimpleDerivedClass>().Get();
            Assert.NotNull(obj.BaseProperty);
        }
        
        [Test]
        public void With_Implemented_Type_Interface_Properties_Are_Filled()
        {
            SimpleDerivedClass obj = mGenerationSession.Single<SimpleDerivedClass>().Get();
            Assert.NotNull(obj.InterfaceValue);
        }
    }
}
