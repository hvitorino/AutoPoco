using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AutoPoco.Actions;
using AutoPoco.Testing;

namespace AutoPoco.Tests.Unit.Actions
{
    [TestFixture]
    public class ObjectMethodInvokeActionActionTests
    {
        [Test]
        public void Enact_SetsPropertyWithValue()
        {
            ObjectMethodInvokeActionAction<SimpleMethodClass> action = new ObjectMethodInvokeActionAction<SimpleMethodClass>(x => x.SetSomething("Something"));            
            SimpleMethodClass target = new SimpleMethodClass();
            action.Enact(null, target);

            Assert.AreEqual("Something", target.Value);
        }
    }
}
