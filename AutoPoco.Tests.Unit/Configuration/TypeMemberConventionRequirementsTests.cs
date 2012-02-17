using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AutoPoco.Configuration;
using Moq;
using AutoPoco.Util;
using AutoPoco.Testing;

namespace AutoPoco.Tests.Unit.Configuration
{

    public abstract class SomeFoo
    {
        public abstract string Something
        {
            get;
            protected set;
        }
    }

    public class Foo : SomeFoo
    {
        public override string Something
        {
            get { throw new NotImplementedException(); }
            protected set { Console.WriteLine("Blah"); }
        }
    }

    [TestFixture]
    public class TypeMemberConventionRequirementsTests
    {
        [Test]
        [TestCase("EmailAddress", true)]
        [TestCase("hello", false)]
        [TestCase("2", false)]
        public void ApplyPropertyNameRule_RuleIsApplied(String test, bool result)
        {
            TypePropertyConventionRequirements context = new TypePropertyConventionRequirements();
            context.Name(x => x == test);
            EngineTypePropertyMember member = (EngineTypePropertyMember)ReflectionHelper.GetMember<SimpleUser>(x => x.EmailAddress);

            Assert.AreEqual(result, context.IsValid(member));
        }

        [Test]
        [TestCase("SomeField", true)]
        [TestCase("hello", false)]
        [TestCase("2", false)]
        public void ApplyFieldNameRule_RuleIsApplied(String test, bool result)
        {
            TypeFieldConventionRequirements context = new TypeFieldConventionRequirements();
            context.Name(x => x == test);
            EngineTypeFieldMember member = (EngineTypeFieldMember)ReflectionHelper.GetMember<SimpleFieldClass>(x => x.SomeField);

            Assert.AreEqual(result, context.IsValid(member));
        }

        [Test]
        [TestCase(typeof(string), true)]
        [TestCase(typeof(bool), false)]
        [TestCase(typeof(SimpleUser), false)]
        public void ApplyPropertyTypeRule_RuleIsApplied(Type test, bool result)
        {
            TypePropertyConventionRequirements context = new TypePropertyConventionRequirements();
            context.Type(x => x == test);
            EngineTypePropertyMember member = (EngineTypePropertyMember)ReflectionHelper.GetMember<SimpleUser>(x => x.EmailAddress);

            Assert.AreEqual(result, context.IsValid(member));
        }

        [Test]
        [TestCase(typeof(string), true)]
        [TestCase(typeof(bool), false)]
        [TestCase(typeof(SimpleUser), false)]
        public void ApplyFieldTypeRule_RuleIsApplied(Type test, bool result)
        {
            TypeFieldConventionRequirements context = new TypeFieldConventionRequirements();
            context.Type(x => x == test);
            EngineTypeFieldMember member = (EngineTypeFieldMember)ReflectionHelper.GetMember<SimpleFieldClass>(x => x.SomeField);

            Assert.AreEqual(result, context.IsValid(member));
        }
    }
}
