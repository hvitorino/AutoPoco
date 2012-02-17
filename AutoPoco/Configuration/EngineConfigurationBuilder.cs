using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Engine;
using AutoPoco.Configuration.Providers;
using AutoPoco.Conventions;

namespace AutoPoco.Configuration
{
    public class EngineConfigurationBuilder : IEngineConfigurationBuilder, IEngineConfigurationProvider
    {
        private EngineConventionConfiguration mConventions = new EngineConventionConfiguration();
        private List<IEngineConfigurationTypeProvider> mTypes = new List<IEngineConfigurationTypeProvider>();

        public IEngineConventionProvider ConventionProvider
        {
            get
            {
                return mConventions;
            }
        }

        public IEngineConfigurationTypeBuilder<T> Include<T>()
        {
            // Create the configuration
            var configuration = new EngineConfigurationTypeBuilder<T>();

            // Store it locally
            mTypes.Add(configuration);

            //And return the public interface
            return (IEngineConfigurationTypeBuilder<T>)configuration;
        }

        public IEngineConfigurationTypeBuilder Include(Type t)
        {
            // Create the configuration
            var configuration = new EngineConfigurationTypeBuilder(t);

            // Store it locally
            mTypes.Add(configuration);

            //And return the public interface
            return configuration;
        }        
       
        public void Conventions(Action<IEngineConventionConfiguration> config)
        {
            config.Invoke(mConventions);
        }

        public IEnumerable<IEngineConfigurationTypeProvider> GetConfigurationTypes()
        {
            return mTypes;
        }

        public void RegisterTypeProvider(IEngineConfigurationTypeProvider provider)
        {
            mTypes.Add(provider);
        }
    }
}
