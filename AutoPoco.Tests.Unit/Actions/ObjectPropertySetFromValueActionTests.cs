using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AutoPoco.Actions;
using AutoPoco.Util;
using AutoPoco.Testing;
using AutoPoco.Configuration;

namespace AutoPoco.Tests.Unit.Actions
{
    [TestFixture]
    public class ObjectPropertySetFromValueActionTests
    {
        [Test]
        public void Enact_SetsFieldWithValue()
        {
            ObjectPropertySetFromValueAction action = new ObjectPropertySetFromValueAction((EngineTypePropertyMember)
                ReflectionHelper.GetMember<SimplePropertyClass>(x => x.SomeProperty), "Test");

            SimplePropertyClass target = new SimplePropertyClass();
            action.Enact(null, target);

            Assert.AreEqual("Test", target.SomeProperty);
        }
    }
}
