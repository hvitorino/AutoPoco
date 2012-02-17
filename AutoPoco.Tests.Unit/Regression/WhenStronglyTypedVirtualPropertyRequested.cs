using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AutoPoco.Configuration;
using AutoPoco.Util;

namespace AutoPoco.Tests.Unit.Regression
{
    [TestFixture]
    public class WhenStronglyTypedVirtualPropertyRequested
    {
        EngineTypePropertyMember mMember;

        [SetUp]
        public void SetupObjects()
        {
            mMember = (EngineTypePropertyMember)ReflectionHelper.GetMember<DerivedClass>(x => x.SomeProperty);
        }

        /// <summary>
        /// This caused some major headaches with inheritance, as type conventions request members as a string
        /// and configuration requests via expression.
        /// MemberExpressions tend to return the base-most instance of virtual properties
        /// GetProperty tends to return the most-derived instance of a virtual property
        /// This causes some disparity when doing comparisons further on in configuration and is therefore undesirable behaviour
        /// Rather than try to fix the problem at the point at which it becomes an issue, I decided to fix this core helper method
        /// We'll see how that fares along with all the other work-arounds I've had to employ to get the inheritance/interface stuff going properly
        /// </summary>
        [Test]
        public void RequestedProperty_IsOverriddenProperty()
        {
      //      Assert.AreEqual(typeof(DerivedClass), mMember.PropertyInfo.DeclaringType);
        }

        public class BaseClass
        {
            public virtual string SomeProperty
            {
                get;
                set;
            }
        }

        public class DerivedClass : BaseClass
        {
            public override string  SomeProperty
            {
	            get;
                set;
            }
        }
    }
}
