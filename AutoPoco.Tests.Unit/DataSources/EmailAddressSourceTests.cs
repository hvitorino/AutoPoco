using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AutoPoco.DataSources;

namespace AutoPoco.Tests.Unit.DataSources
{
    [TestFixture]
    public class EmailAddressSourceTests
    {
        [Test]
        public void Next_ReturnsString()
        {
            EmailAddressSource source = new EmailAddressSource();
            String email = source.Next(null);

            Assert.NotNull(email);
        }

        [Test]
        public void Next_ReturnsDifferentResults()
        {
            EmailAddressSource source = new EmailAddressSource();
            string emailOne = source.Next(null);
            string emailTwo = source.Next(null);

            Assert.AreNotEqual(emailOne, emailTwo);
        }
    }
}
