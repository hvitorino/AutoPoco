using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Engine;
using AutoPoco.Testing;
using AutoPoco.DataSources;
using NUnit.Framework;
using AutoPoco.Configuration;

namespace AutoPoco.Tests.Integration.Complete
{
    public class WhenUsingDefaultConventionsWithExplicitSetup
    {
        IGenerationSession mSession;

        [SetUp]
        public void Setup()
        {
            mSession = AutoPocoContainer.Configure(x =>
            {
                x.Conventions(c =>
                {
                    c.UseDefaultConventions();
                });

                x.Include<SimpleMethodClass>()
                    .Invoke(c => c.SetSomething(
                        Use.Source<String, RandomStringSource>(5, 10),
                        Use.Source<String, LastNameSource>()))
                    .Invoke(c => c.ReturnSomething());

                x.Include<SimpleUser>()
                    .Setup(c => c.EmailAddress).Use<EmailAddressSource>()
                    .Setup(c => c.FirstName).Use<FirstNameSource>()
                    .Setup(c => c.LastName).Use<LastNameSource>();             

                x.Include<SimpleUserRole>()
                    .Setup(c => c.Name).Random(5, 10);                             

                x.Include<SimpleFieldClass>();
                x.Include<SimplePropertyClass>();           
                x.Include<DefaultPropertyClass>();
                x.Include<DefaultFieldClass>();                             
               
            })
            .CreateSession();
          
        }

        [Test]
        public void Single_SimpleMethodClass_ReturnSomething_Invoked()
        {
            SimpleMethodClass result = mSession.Single<SimpleMethodClass>().Get();
            Assert.True(result.ReturnSomethingCalled);
        }

        [Test]
        public void Single_SimpleMethodClass_SetSomething_SetsValueCorrectlyFromSource()
        {
            SimpleMethodClass result = mSession.Single<SimpleMethodClass>().Get();
            Assert.IsTrue(result.Value.Length >= 5 && result.Value.Length <= 10);
        }

        [Test]
        public void Single_SimpleMethodClass_SetSomething_SetsOtherValueCorrectlyFromSource()
        {
            SimpleMethodClass result = mSession.Single<SimpleMethodClass>().Get();
            Assert.IsTrue(result.OtherValue.Length >= 2);
        }

        [Test]
        public void Single_SimpleUserRole_HasRandomName()
        {
            SimpleUserRole role = mSession.Single<SimpleUserRole>().Get();
            Assert.GreaterOrEqual(role.Name.Length, 5);
            Assert.LessOrEqual(role.Name.Length, 10);
        }

        [Test]
        public void Single_SimpleUser_HasValidEmailAddress()
        {
           SimpleUser user = mSession.Single<SimpleUser>().Get();
           Assert.IsTrue(user.EmailAddress.Contains("@"));
        }

        [Test]
        public void Single_SimpleSeveralUsers_HaveUniqueEmailAddresses()
        {
            SimpleUser[] users = mSession.List<SimpleUser>(10).Get().ToArray();

            Assert.True(
                users.Where(x => users.Count(y => y.EmailAddress == x.EmailAddress) > 1).Count() == 0);
        }

        [Test]
        public void Single_SimpleUser_ImposeCustomEmailAddress_HasCustomEmailAddress()
        {
            SimpleUser user = mSession.Single<SimpleUser>()
                .Impose(x => x.EmailAddress, "override@override.com")
                .Impose(x => x.FirstName, "Override")
                .Impose(x => x.LastName, "Override")
                .Get();

            Assert.AreEqual("override@override.com", user.EmailAddress);
        }

        [Test]
        public void Single_SimpleUser_HasValidFirstName()
        {
            SimpleUser user = mSession.Single<SimpleUser>().Get();
            Assert.IsTrue(user.FirstName.Length > 2);
        }

        [Test]
        public void Single_SimpleUser_HasValidLastName()
        {
            SimpleUser user = mSession.Single<SimpleUser>().Get();
            Assert.IsTrue(user.LastName.Length > 2);
        }

        [Test]
        public void SimpleFieldClass_SomePropertyNotNull()
        {
            SimpleFieldClass fieldClass = mSession.Single<SimpleFieldClass>().Get();
            Assert.NotNull(fieldClass.SomeField);
        }

        [Test]
        public void SimpleFieldClass_SomeOtherPropertyNotNull()
        {
            SimpleFieldClass fieldClass = mSession.Single<SimpleFieldClass>().Get();
            Assert.NotNull(fieldClass.SomeOtherField);
        }
        
        [Test]
        public void DefaultPropertyClass_StringIsEmpty()
        {
            DefaultPropertyClass propertyClass = mSession.Single<DefaultPropertyClass>().Get();
            Assert.AreEqual("", propertyClass.String);
        }

        [Test]
        public void DefaultPropertyClass_FloatEqualsZero()
        {
            DefaultPropertyClass propertyClass = mSession.Single<DefaultPropertyClass>().Get();
            Assert.AreEqual(0, propertyClass.Float);
        }

        [Test]
        public void DefaultPropertyClass_IntegerEqualsZero()
        {
            DefaultPropertyClass propertyClass = mSession.Single<DefaultPropertyClass>().Get();
            Assert.AreEqual(0, propertyClass.Integer);
        }

        [Test]
        public void DefaultPropertyClass_DateTimeIsMin()
        {
            DefaultPropertyClass propertyClass = mSession.Single<DefaultPropertyClass>().Get();
            Assert.AreEqual(DateTime.MinValue, propertyClass.Date);
        }
        
        [Test]
        public void DefaultFieldClass_StringIsEmpty()
        {
            DefaultFieldClass propertyClass = mSession.Single<DefaultFieldClass>().Get();
            Assert.AreEqual("", propertyClass.String);
        }

        [Test]
        public void DefaultFieldClass_FloatEqualsZero()
        {
            DefaultFieldClass propertyClass = mSession.Single<DefaultFieldClass>().Get();
            Assert.AreEqual(0, propertyClass.Float);
        }

        [Test]
        public void DefaultFieldClass_IntegerEqualsZero()
        {
            DefaultFieldClass propertyClass = mSession.Single<DefaultFieldClass>().Get();
            Assert.AreEqual(0, propertyClass.Integer);
        }

        [Test]
        public void DefaultFieldClass_DateTimeIsMin()
        {
            DefaultFieldClass propertyClass = mSession.Single<DefaultFieldClass>().Get();
            Assert.AreEqual(DateTime.MinValue, propertyClass.Date);
        }

        [Test]
        public void List_SimpleUser_ReturnsList()
        {
            var list = mSession.List<SimpleUser>(10)
                .First(5)
                    .Impose(x => x.LastName, "first")
                .Next(5)
                    .Impose(x => x.LastName, "last")
             .All().Get();

            Assert.AreEqual(10, list.Count);
            Assert.AreEqual(5, list.Count(x => x.LastName == "first"));
            Assert.AreEqual(5, list.Count(x => x.LastName == "last"));
        }

        [Test]
        public void List_SimpleUser_FirstHasUniqueName()
        { 
        
        }

        [Test]
        public void List_SimpleUser_RandomHaveSameName()
        {

        }
    }
}
