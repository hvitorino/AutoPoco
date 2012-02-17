using System.Diagnostics;
using AutoPoco.DataSources;
using NUnit.Framework;

namespace AutoPoco.Tests.Unit.DataSources
{
    [TestFixture]
    public class CreditCardSourceTests
    {
        [Test]
        public void Next_Returns_An_American_Express()
        {
            CreditCardSource source = new CreditCardSource(CreditCardSource.CreditCardType.AmericanExpress);
            var value = source.Next(null);
            Assert.IsTrue(value == "3782 822463 10005");
        }

        [Test]
        public void Next_Returns_A_Discover_Card()
        {
            CreditCardSource source = new CreditCardSource(CreditCardSource.CreditCardType.Discover);
            var value = source.Next(null);
            Assert.IsTrue(value == "6011 1111 1111 1117");
        }

        [Test]
        public void Next_Returns_A_MasterCard()
        {
            CreditCardSource source = new CreditCardSource(CreditCardSource.CreditCardType.MasterCard);
            var value = source.Next(null);
            Assert.IsTrue(value == "5105 1051 0510 5100");
        }

        [Test]
        public void Next_Returns_A_Visa()
        {
            CreditCardSource source = new CreditCardSource(CreditCardSource.CreditCardType.Visa);
            var value = source.Next(null);
            Assert.IsTrue(value == "4111 1111 1111 1111");
        }

        [Test]
        public void Next_Returns_A_Random()
        {
            CreditCardSource source = new CreditCardSource();
            var value = source.Next(null);
            Debug.WriteLine(string.Format("the credit card number was : {0}", value));
            Assert.IsNotNullOrEmpty(value);
        }


    }
}