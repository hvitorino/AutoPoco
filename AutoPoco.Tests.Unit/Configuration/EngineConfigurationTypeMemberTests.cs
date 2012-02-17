using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AutoPoco.Configuration;
using AutoPoco.Util;
using AutoPoco.Testing;
using Moq;

namespace AutoPoco.Tests.Unit.Configuration
{
    [TestFixture]
    public class EngineConfigurationTypeMemberTests
    {
        [Test]
        public void GetSource_ReturnsSource()
        {
            EngineConfigurationTypeMember member = new EngineConfigurationTypeMember(ReflectionHelper.GetMember<SimpleUser>(x => x.FirstName));
            Mock<IEngineConfigurationDatasource> sourceMock = new Mock<IEngineConfigurationDatasource>();
            member.SetDatasource(sourceMock.Object);

            var source2 = member.GetDatasources().First();

            Assert.AreEqual(sourceMock.Object, source2);
        }
    }
}
