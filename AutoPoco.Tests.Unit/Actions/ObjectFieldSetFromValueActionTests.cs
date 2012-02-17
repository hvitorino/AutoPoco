using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AutoPoco.Testing;
using AutoPoco.Actions;
using AutoPoco.Util;
using AutoPoco.Configuration;

namespace AutoPoco.Tests.Unit.Actions
{
    [TestFixture]
    public class ObjectFieldSetFromValueActionTests
    {
        [Test]
        public void Enact_SetsFieldWithValue()
        {
            ObjectFieldSetFromValueAction action = new ObjectFieldSetFromValueAction((EngineTypeFieldMember)
                ReflectionHelper.GetMember<SimpleFieldClass>(x => x.SomeField), "Test");

            SimpleFieldClass target = new SimpleFieldClass();
            action.Enact(null, target);

            Assert.AreEqual("Test", target.SomeField);
        }
    }
}
