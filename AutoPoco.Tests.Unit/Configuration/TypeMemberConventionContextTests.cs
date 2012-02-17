using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AutoPoco.Configuration;
using Moq;
using AutoPoco.DataSources;
using AutoPoco.Engine;
using AutoPoco.Util;

namespace AutoPoco.Tests.Unit.Configuration
{
    [TestFixture]
    public class TypeMemberConventionContextTests
    {
        TypeMemberConventionContext mContext;
        Mock<IEngineConfigurationTypeMember> mMemberMock;

        [SetUp]
        public void Setup()
        {
            mMemberMock = new Mock<IEngineConfigurationTypeMember>();
            mContext = new TypeMemberConventionContext(null, mMemberMock.Object);
        }

        [Test]
        public void SetValue_SetsDatasource()
        {
            mContext.SetValue(10);
            mMemberMock.Verify(x => x.SetDatasource(It.IsAny<IEngineConfigurationDatasource>()), Times.Once());
        }

        [Test]
        public void SetValue_SetsValueDataSource()
        {
            IEngineConfigurationDatasource source = null;
            mMemberMock.Setup(x => x.SetDatasource(It.IsAny<IEngineConfigurationDatasource>())).Callback((IEngineConfigurationDatasource configSource) =>
            {
                source = configSource;
            });
            mContext.SetValue(10);
            IDatasource datasource = source.Build();
            Assert.AreEqual(10, datasource.Next(null));
        }

        [Test]
        public void SetSouce_SetsDatasource()
        {
            mContext.SetSource<TestSource>();
            mMemberMock.Verify(x => x.SetDatasource(It.IsAny<IEngineConfigurationDatasource>()), Times.Once());
        }

        [Test]
        public void SetSource_SetsCorrectDatasource()
        {
            IEngineConfigurationDatasource source = null;
            mMemberMock.Setup(x => x.SetDatasource(It.IsAny<IEngineConfigurationDatasource>())).Callback((IEngineConfigurationDatasource configSource) =>
            {
                source = configSource;
            });
            mContext.SetSource<TestSource>();
            IDatasource datasource = source.Build();
            Assert.AreEqual(typeof(TestSource), datasource.GetType());
        }

        [Test]
        public void Member_ReturnsConfigurationMember()
        {
            var field = ReflectionHelper.GetMember<TestClass>(x => x.Field);
            mMemberMock.SetupGet(x => x.Member).Returns(field);
            Assert.AreEqual(field, mContext.Member);

        }

        public class TestClass
        {
            public string Field;
        }

        public class TestSource : IDatasource
        {
            public object Next(IGenerationContext context)
            {
                throw new NotImplementedException();
            }
        }
    }
}
