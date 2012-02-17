using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AutoPoco.Configuration;
using AutoPoco.Engine;
using AutoPoco.Configuration.Providers;

namespace AutoPoco.Tests.Integration.Engine
{
    [TestFixture]
    public class GenerationSessionFactoryTestBase
    {
        public EngineConfiguration Configuration
        {
            get;
            private set;
        }

        public IGenerationSession GenerationSession
        {
            get;
            private set;
        }

        [SetUp]
        public void SetupConfiguration()
        {
            Configuration = new EngineConfiguration();
            IEngineConventionProvider conventionProvider = new EngineConventionConfiguration();
            PopulateConfiguration();
            GenerationSessionFactory factory = new GenerationSessionFactory(
                this.Configuration, conventionProvider);
            this.GenerationSession = factory.CreateSession();
        }

        protected virtual void PopulateConfiguration()
        {

        }
    }
}
