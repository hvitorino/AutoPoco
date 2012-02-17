using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Engine;
using AutoPoco.Testing;
using NUnit.Framework;
using Moq;
using AutoPoco.Configuration;
using System.Reflection;

namespace AutoPoco.Tests.Unit.Configuration
{
    [TestFixture]
    public class TypeConventionContextTests
    {
        private Mock<IEngineConfigurationType> mTypeMock;
        private TypeConventionContext mContext;

        [SetUp]
        public void Setup()
        {
            mTypeMock = new Mock<IEngineConfigurationType>();
            mContext = new TypeConventionContext(mTypeMock.Object);
        }

        [Test]
        public void RegisterField_TypeMemberRegistered()
        {
            FieldInfo field = typeof(TestClass).GetField("Field");
            mContext.RegisterField(field);
            mTypeMock.Verify(x => x.RegisterMember(It.Is<EngineTypeMember>(y => y.Name == field.Name)), Times.Once());
        }

        [Test]
        public void RegisterProperty_TypePropertyRegistered()
        {
            PropertyInfo property = typeof(TestClass).GetProperty("Property");
            mContext.RegisterProperty(property);
            mTypeMock.Verify(x => x.RegisterMember(It.Is<EngineTypeMember>(y => y.Name == property.Name)), Times.Once());
        }

        public void SetFactory_FactoryIsSet()
        {
            var type = new EngineConfigurationType(typeof (SimpleUser));
            var context = new TypeConventionContext(type);
            context.SetFactory(typeof (SimpleUserFactory));
            var factory = type.GetFactory().Build();

            Assert.True(factory is SimpleUserFactory);
        }

        [Test]
        public void RegisterMethod_TypeMethodRegistered()
        {
            MethodInfo info = typeof(TestClass).GetMethod("Method");
            Mock<IEngineConfigurationTypeMember> typeMemberMock =new Mock<IEngineConfigurationTypeMember>();
            
            MethodInvocationContext context = new MethodInvocationContext();
            context.AddArgumentValue(5);

            int count = 0;
            mTypeMock.Setup(x => x.GetRegisteredMember(It.IsAny<EngineTypeMember>()))
                .Returns(()=>
                {
                    if (count == 0) { count++; return null; }
                    else
                    {
                        return typeMemberMock.Object;
                    }
                });
            mContext.RegisterMethod(info, context);
            mTypeMock.Verify(x => x.RegisterMember(It.Is<EngineTypeMember>(y => y.Name == info.Name)), Times.Once());
        }

        [Test]
        public void Target_ReturnsConfigurationType()
        {
            mTypeMock.SetupGet(x => x.RegisteredType).Returns(typeof(TestClass));
            Assert.AreEqual(typeof(TestClass), mContext.Target);
        }

        public class TestClass
        {
            public string Field;
            public string Property
            {
                get;
                set;
            }

            public void Method(int value)
            {

            }
        }
    }

    public class SimpleUserFactory : IDatasource<SimpleUser>
    {
        public object Next(IGenerationContext context)
        {
            throw new NotImplementedException();
        }
    }
}
