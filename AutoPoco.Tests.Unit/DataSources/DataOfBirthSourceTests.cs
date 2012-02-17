using System;
using AutoPoco.DataSources;
using NUnit.Framework;

namespace AutoPoco.Tests.Unit.DataSources
{
    [TestFixture]
    public class DataOfBirthSourceTests
    {
        [Test]
        public void Next_Returns_An_Age_Between_Min_And_Max()
        {
            DateOfBirthSource source = new DateOfBirthSource(15, 18);
            var value = source.Next(null);

            Assert.IsTrue( value.Year <= DateTime.Now.AddYears(-15).Year &&
                           value.Year >= DateTime.Now.AddYears(-18).Year );
        }
    }
}