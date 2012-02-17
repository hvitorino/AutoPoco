using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AutoPoco.Engine;
using AutoPoco.Testing;

namespace AutoPoco.Tests.Integration.Complete
{
    [TestFixture]
    public class WhenUsingFullAssemblyScanningWithDefaultConventions
    {
        IGenerationSession mSession;

        [SetUp]
        public void Setup()
        {
            // As default as it gets
            mSession = AutoPocoContainer.Configure(x =>
            {
                x.Conventions(c =>
                {
                    c.UseDefaultConventions();
                });
                x.AddFromAssemblyContainingType<SimpleUser>();
            })
            .CreateSession();
        }

        [Test]
        public void SimpleUser_CanBeCreated()
        {
            SimpleUser user = mSession.Single<SimpleUser>().Get();
            Assert.NotNull(user);
        }

        [Test]
        public void SimpleFieldClass_CanBeCreated()
        {
            SimpleFieldClass obj = mSession.Single<SimpleFieldClass>().Get();
            Assert.NotNull(obj);
        }

        [Test]
        public void SimplePropertyClass_CanBeCreated()
        {
            SimplePropertyClass obj = mSession.Single<SimplePropertyClass>().Get();
            Assert.NotNull(obj);
        }

        [Test]
        public void SimpleMethodClass_Invoke_CallsMethodWithParams()
        {
            SimpleMethodClass target = mSession.Single<SimpleMethodClass>()
                .Invoke(x => x.SetSomething("Something"))
                .Get();

            Assert.AreEqual("Something", target.Value);
        }

        [Test]
        public void SimpleMethodClass_Invoke_CallsMethodWithoutParams()
        {
            SimpleMethodClass target = mSession.Single<SimpleMethodClass>()
                .Invoke(x => x.ReturnSomething())
                .Get();

            Assert.AreEqual(true, target.ReturnSomethingCalled);
        }

        [Test]
        public void SimpleMethodClass_InvokeOnFirstCollection_CallsAction()
        {
            IList<SimpleMethodClass> items = mSession.List<SimpleMethodClass>(100)
                .First(50)
                    .Invoke(x => x.SetSomething("Something"))
                .Next(50)
                    .Invoke(x => x.SetSomething("SomethingElse"))
                .All()
                .Get();

            Assert.True(items.Where(x => x.Value == "Something").Count() == 50 && items.Where(x => x.Value == "SomethingElse").Count() == 50);
        }

        [Test]
        public void SimpleMethodClass_InvokeOnRandomCollection_CallsAction()
        {
            IList<SimpleMethodClass> items = mSession.List<SimpleMethodClass>(100)
                .Random(50)
                    .Invoke(x => x.SetSomething("Something"))
                .Next(50)
                    .Invoke(x => x.SetSomething("SomethingElse"))
                .All()
                .Get();

            Assert.True(items.Where(x => x.Value == "Something").Count() == 50 && items.Where(x => x.Value == "SomethingElse").Count() == 50);
        }

        [Test]
        public void SimpleMethodClass_InvokeOnEntireCollection_CallsAction()
        {
            IList<SimpleMethodClass> items = mSession.List<SimpleMethodClass>(100)
                    .Invoke(x => x.SetSomething("Something"))   
                .Get();

            Assert.True(items.Where(x => x.Value == "Something").Count() == 100);
        }

        [Test]
        public void SimpleMethodClass_InvokeOnFirstCollection_CallsFunc()
        {
            IList<SimpleMethodClass> items = mSession.List<SimpleMethodClass>(100)
                .First(50)
                    .Invoke(x => x.ReturnSomething())
                .All()
                .Get();

            Assert.True(items.Where(x => x.ReturnSomethingCalled).Count() == 50);
        }

        [Test]
        public void SimpleMethodClass_InvokeOnRandomCollection_CallsFunc()
        {
            IList<SimpleMethodClass> items = mSession.List<SimpleMethodClass>(100)
                .Random(50)
                     .Invoke(x => x.ReturnSomething())
                .All()
                .Get();

            Assert.True(items.Where(x => x.ReturnSomethingCalled).Count() == 50);
        }

        [Test]
        public void SimpleMethodClass_InvokeOnEntireCollection_CallsFunc()
        {
            IList<SimpleMethodClass> items = mSession.List<SimpleMethodClass>(100)
                     .Invoke(x => x.ReturnSomething())
                .Get();

            Assert.True(items.Where(x => x.ReturnSomethingCalled).Count() == 100);
        }
    }    
   
}
