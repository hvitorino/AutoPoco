using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AutoPoco.Conventions;
using AutoPoco.Configuration;
using Moq;
using System.Reflection;
using AutoPoco.Testing;

namespace AutoPoco.Tests.Unit.Conventions
{
    [TestFixture]
    public class DefaultTypeConventionTests
    {
        DefaultTypeConvention mConvention;
        Mock<ITypeConventionContext> mTypeConventionContext;

        [SetUp]
        public void SetupObjects()
        {
            mConvention = new DefaultTypeConvention();
            mTypeConventionContext = new Mock<ITypeConventionContext>();         
        }

        [Test]
        public void Apply_Ignores_Properties_Without_Public_Setter()
        {
            mTypeConventionContext.SetupGet(x => x.Target).Returns(typeof(ClassWithPrivateSetters));
            mConvention.Apply(mTypeConventionContext.Object);
            mTypeConventionContext.Verify(x => x.RegisterProperty(It.IsAny<PropertyInfo>()), Times.Never());
        }

        [Test]
        public void Apply_IgnoresBaseProperties()
        {
            int count = 0;
            mTypeConventionContext.SetupGet(x => x.Target).Returns(typeof(Class));   
            mTypeConventionContext.Setup(x => x.RegisterProperty(It.IsAny<PropertyInfo>()))
                .Callback(() =>
                {
                    count++;
                });

            mConvention.Apply(mTypeConventionContext.Object);

            Assert.AreEqual(1, count);
        }

        [Test]
        public void Apply_IgnoresBaseFields()
        {
            int count = 0;
            mTypeConventionContext.SetupGet(x => x.Target).Returns(typeof(Class));   
            mTypeConventionContext.Setup(x => x.RegisterField(It.IsAny<FieldInfo>()))
                .Callback(() =>
                {
                    count++;
                });

            mConvention.Apply(mTypeConventionContext.Object);

            Assert.AreEqual(1, count);
        }

        [Test]
        public void Apply_Registers_Interface_Properties_When_Running_Against_That_Interface()
        {
            mTypeConventionContext.SetupGet(x => x.Target).Returns(typeof(ITestInterface));
            mConvention.Apply(mTypeConventionContext.Object);
            mTypeConventionContext.Verify(x => x.RegisterProperty(It.IsAny<PropertyInfo>()), Times.Once());

        }

        public class BaseClass : IBaseTestInteface
        {
            public string BaseProperty
            {
                get;
                set;
            }

            public string BaseField;

            public string BaseInterfaceProperty
            {
                get;
                set;
            }
        }

        public class ClassWithPrivateSetters
        {
            public string Protected { get; protected set; }
            public string Private { get; private set; }
            public string Internal { get; private set; }
        }

        public class Class : BaseClass, ITestInterface
        {
            public string TopProperty
            {
                get;
                set;
            }

            public string InterfaceProperty
            {
                get;
                set;
            }

            public string TopField;
        }

        public interface IBaseTestInteface
        {
            string BaseInterfaceProperty
            {
                get;
                set;
            }
        }


        public interface ITestInterface : IBaseTestInteface
        {
            string InterfaceProperty
            {
                get;
                set;
            }
        }
    }
}
