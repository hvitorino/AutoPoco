using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Configuration;
using AutoPoco.Conventions;
using AutoPoco.DataSources;
using Moq;
using NUnit.Framework;

namespace AutoPoco.Tests.Unit.Conventions
{
    [TestFixture]
    public class DefaultTypeCtorConventionTests
    {
        [Test]
        public void DefaultTypeCtor_Seeks_Least_Greedy_Ctor()
        {
            var convention = new DefaultComplexTypeCtorConvention();
            var context = new Mock<ITypeConventionContext>();
            context.SetupGet(x => x.Target).Returns(typeof (SampleCtorType));
            convention.Apply(context.Object);
            
            context.Verify(x => x.SetFactory(
                It.Is<Type>(type=> type == typeof(CtorSource<SampleCtorType>)),
                typeof (SampleCtorType).GetConstructor(new[] { typeof(int)})), Times.Once());

        }

        [Test]
        public void DefaultTypeCtor_Uses_Default_Ctor_If_Available()
        {
            var convention = new DefaultComplexTypeCtorConvention();
            var context = new Mock<ITypeConventionContext>();
            context.SetupGet(x => x.Target).Returns(typeof(SampleDefaultCtorType));
            convention.Apply(context.Object);

            context.Verify(x => x.SetFactory(
                It.Is<Type>(type => type == typeof (CtorSource<SampleDefaultCtorType>)),
                typeof(SampleDefaultCtorType).GetConstructor(Type.EmptyTypes)), Times.Once());

        }
    }

    public class SampleDefaultCtorType
    {
        public SampleDefaultCtorType()
        {
            
        }
        public SampleDefaultCtorType(int x)
        {
        }

        public SampleDefaultCtorType(int x, int y)
        {
        }
        public SampleDefaultCtorType(int x, int y, int z)
        {

        }
    }

    public class SampleCtorType
    {
        public SampleCtorType(int x)
        {
        }

        public SampleCtorType(int x, int y)
        {
        }
        public SampleCtorType(int x, int y, int z)
        {
            
        }
    }
}
