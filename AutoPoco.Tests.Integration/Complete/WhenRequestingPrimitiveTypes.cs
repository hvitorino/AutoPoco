using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace AutoPoco.Tests.Integration.Complete
{
    [TestFixture]
    public class WhenRequestingPrimitiveTypes
    {
        [Test]
        public void Requesting_String_Uses_Default_Source()
        {
            var session = AutoPocoContainer.Configure(x => { }).CreateSession();
            var result = session.Next<string>();
            Assert.NotNull(result);
        }

        [Test]
        public void Requesting_Int_Uses_Default_Source()
        {
            var session = AutoPocoContainer.Configure(x => { }).CreateSession();
            var result = session.Next<int>();
            Assert.AreEqual(0, result);
        }

        [Test]
        public void Requesting_Double_Uses_Default_Source()
        {
            var session = AutoPocoContainer.Configure(x => { }).CreateSession();
            var result = session.Next<double>();
            Assert.AreEqual(0, result);
        }

        [Test]
        public void Requesting_Float_Uses_Default_Source()
        {
            var session = AutoPocoContainer.Configure(x => { }).CreateSession();
            var result = session.Next<float>();
            Assert.AreEqual(0, result);
        }


        [Test]
        public void Requesting_Char_Uses_Default_Source()
        {
            var session = AutoPocoContainer.Configure(x => { }).CreateSession();
            var result = session.Next<char>();
            Assert.AreEqual(0, result);
        }

        [Test]
        public void Requesting_Bool_Uses_Default_Source()
        {
            var session = AutoPocoContainer.Configure(x => { }).CreateSession();
            var result = session.Next<bool>();
            Assert.AreEqual(false, result);
        }

        [Test]
        public void Requesting_Short_Uses_Default_Source()
        {
            var session = AutoPocoContainer.Configure(x => { }).CreateSession();
            var result = session.Next<short>();
            Assert.AreEqual(0, result);
        }

        [Test]
        public void Requesting_Decimal_Uses_Default_Source()
        {
            var session = AutoPocoContainer.Configure(x => { }).CreateSession();
            var result = session.Next<decimal>();
            Assert.AreEqual(0, result);
        }

    }
}
