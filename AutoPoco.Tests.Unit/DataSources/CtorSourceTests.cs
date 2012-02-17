using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.DataSources;
using AutoPoco.Engine;
using Moq;
using NUnit.Framework;

namespace AutoPoco.Tests.Unit.DataSources
{
    [TestFixture]
    public class CtorSourceTests
    {
        [Test]
        public void CtorSource_CallsCtor_RequestsArguments_From_Session()
        {
           var source = new CtorSource<TestCtorObject>(
                    typeof(TestCtorObject).GetConstructor(new[] { typeof(TestDependency) })
                );

            var context = new Mock<IGenerationContext>();
            context.Setup(x => x.Next<TestDependency>()).Returns(new TestDependency());
            
            var result = source.Next(context.Object);
            Assert.NotNull(result.Dependency);
        }
    }

    public class TestCtorObject
    {
        public TestDependency Dependency { get; private set; }
        public TestCtorObject(TestDependency dependency)
        {
            this.Dependency = dependency;
        }
    }
    

    public class TestDependency
    {
        
    }
}
