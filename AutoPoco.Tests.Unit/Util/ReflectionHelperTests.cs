using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AutoPoco.Util;
using System.Reflection;
using AutoPoco.Configuration;
using AutoPoco.Testing;
using AutoPoco.DataSources;
using System.Linq.Expressions;

namespace AutoPoco.Tests.Unit.Util
{
    [TestFixture]
    public class ReflectionHelperTests
    {
        [Test]
        public void GetProperty_ReturnsPropertyInfo()
        {
            PropertyInfo info = ReflectionHelper.GetProperty<SimplePropertyClass>(x => x.SomeProperty);

            Assert.AreEqual("SomeProperty", info.Name);
        }

        [Test]
        public void GetMember_AsProperty_ReturnsMember()
        {
            EngineTypeMember member = ReflectionHelper.GetMember<SimplePropertyClass>(x => x.SomeProperty);
            Assert.AreEqual("SomeProperty", member.Name);
            Assert.IsTrue(member.IsProperty);
        }

        [Test]
        public void GetMember_AsField_ReturnsField()
        {
            EngineTypeMember member = ReflectionHelper.GetMember<SimpleFieldClass>(x => x.SomeField);
            Assert.AreEqual("SomeField", member.Name);
            Assert.IsTrue(member.IsField);
        }

        [Test]
        public void GetField_ReturnsFieldInfo()
        {
            FieldInfo info = ReflectionHelper.GetField<SimpleFieldClass>(x => x.SomeField);

            Assert.AreEqual("SomeField", info.Name);
        }
        
    }
}
