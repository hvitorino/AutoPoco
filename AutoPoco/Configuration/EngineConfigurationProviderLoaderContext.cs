using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Configuration.Providers;

namespace AutoPoco.Configuration
{
    public class EngineConfigurationProviderLoaderContext : IEngineConfigurationProviderLoaderContext
    {
        public IEngineConfiguration Configuration
        {
            get;
            private set;
        }

        public IEngineConfigurationProvider ConfigurationProvider
        {
            get;
            private set;
        }

        public IEngineConventionProvider ConventionProvider
        {
            get;
            private set;
        }

        public EngineConfigurationProviderLoaderContext(
            IEngineConfiguration configuration, 
            IEngineConfigurationProvider configurationProvider, 
            IEngineConventionProvider conventionProvider)
        {
            this.Configuration = configuration;
            this.ConfigurationProvider = configurationProvider;
            this.ConventionProvider = conventionProvider;
        }
    }
}
