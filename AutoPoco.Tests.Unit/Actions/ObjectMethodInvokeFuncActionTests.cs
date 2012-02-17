using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AutoPoco.Testing;
using AutoPoco.Actions;

namespace AutoPoco.Tests.Unit.Actions
{
    [TestFixture]
    public class ObjectMethodInvokeFuncActionTests
    {
        [Test]
        public void Enact_CallsFunc()
        {
            ObjectMethodInvokeFuncAction<SimpleMethodClass, String> action = new ObjectMethodInvokeFuncAction<SimpleMethodClass, String>(x => x.ReturnSomething());

            SimpleMethodClass target = new SimpleMethodClass();
            action.Enact(null, target);

            Assert.True(target.ReturnSomethingCalled);
        }
    }
}