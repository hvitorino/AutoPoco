using System.Diagnostics;
using AutoPoco.DataSources;
using NUnit.Framework;

namespace AutoPoco.Tests.Unit.DataSources
{
    [TestFixture]
    public class UsStateSourceTests
    {
        [Test]
        public void Next_Returns_A_State()
        {
            UsStatesSource source = new UsStatesSource();
            var value = source.Next(null);
            Assert.IsNotNullOrEmpty(value);
            Assert.IsTrue(value.Length > 2);
            Debug.WriteLine(string.Format("Welcome to {0}", value));
        }

        [Test]
        public void Next_Returns_A_State_Abbreviation()
        {
            UsStatesSource source = new UsStatesSource(true);
            var value = source.Next(null);
            Assert.IsNotNullOrEmpty(value);
            Assert.IsTrue(value.Length == 2);
            Debug.WriteLine(string.Format("Welcome to {0}", value));
        }
    }
}