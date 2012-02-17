using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AutoPoco.DataSources;

namespace AutoPoco.Tests.Unit.DataSources
{
    [TestFixture]
    public class RandomStringSourceTests
    {
        [Test]
        [TestCase(5,10)]
        [TestCase(0,10)]
        [TestCase(0,2)]
        public void Next_ReturnsStringOfCorrectSize(int min, int max)
        {
            RandomStringSource source = new RandomStringSource(min, max);
            for (int x = 0; x < 10; x++)
            {
                string value = source.Next(null);
                Assert.GreaterOrEqual(value.Length, min );
                Assert.LessOrEqual(value.Length, max);
            }            
        }

        [Test]
        public void Next_ReturnsSameStringFromTwoSources()
        {
            RandomStringSource sourceOne = new RandomStringSource(0, 10);
            RandomStringSource sourceTwo = new RandomStringSource(0, 10);

            for (int x = 0; x < 10; x++)
            {
                String one = sourceOne.Next(null);
                String two = sourceTwo.Next(null);

                Assert.AreEqual(one, two);
            }
        }
    }
}
