using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AutoPoco.Engine;
using AutoPoco.Testing;
using Moq;
using AutoPoco.Configuration;
using AutoPoco.Configuration.Providers;

namespace AutoPoco.Tests.Unit.Engine
{
    [TestFixture]
    public class GenerationContextTests
    {
        private GenerationContext mGenerationSession;

        [SetUp]
        public void TestSetup()
        {
            IEngineConfiguration configuration = new EngineConfiguration();
            IEngineConventionProvider conventionProvider = new Mock<IEngineConventionProvider>().Object;
            GenerationConfiguration repository = new GenerationConfiguration(configuration, conventionProvider, 10);
            configuration.RegisterType(typeof(SimpleUser));
            mGenerationSession = new GenerationContext(repository);
        }

        [Test]
        public void Single_ValidType_ReturnsObject()
        {
           IObjectGenerator<SimpleUser> userGenerator = mGenerationSession.Single<SimpleUser>();
           Assert.NotNull(userGenerator);
        }

        [Test]
        public void Single_UnknownType_ReturnsObject()
        {
            IObjectGenerator<SimpleUser> userGenerator = mGenerationSession.Single<SimpleUser>();
            Assert.NotNull(userGenerator);
        }

        [Test]
        public void List_ValidType_ReturnsCollectionContext()
        {
            ICollectionContext<SimpleUser, IList<SimpleUser>> userGenerator = mGenerationSession.List<SimpleUser>(10);

            Assert.NotNull(userGenerator);
        }

        [Test]
        public void List_UnknownType_ReturnsObjectGenerator()
        {
            ICollectionContext<SimpleUser, IList<SimpleUser>> userGenerator = mGenerationSession.List<SimpleUser>(10);
            Assert.NotNull(userGenerator);
        }

        [Test]
        public void Single_Passes_Context_Through_To_Session()
        {
            Mock<IObjectBuilder> builder = new Mock<IObjectBuilder>();
            Mock<IGenerationConfiguration> builderRepository = new Mock<IGenerationConfiguration>();
            builderRepository.Setup(x => x.GetBuilderForType(typeof (Object))).Returns(builder.Object);
            IGenerationContext context = new GenerationContext(builderRepository.Object);

            context.Single<Object>().Get();

            builder.Verify(x => x.CreateObject(context), Times.Once());
        }
    }
}
