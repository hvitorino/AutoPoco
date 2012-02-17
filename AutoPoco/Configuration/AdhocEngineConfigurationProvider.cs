using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Configuration.Providers;

namespace AutoPoco.Configuration
{
    public class AdhocEngineConfigurationProvider : IEngineConfigurationProvider
    {
        private IEnumerable<IEngineConfigurationTypeProvider> mTypes;

        public AdhocEngineConfigurationProvider(IEnumerable<Type> types)
        {
            mTypes = types.Select(x => new AdhocEngineTypeProvider(x)).ToArray();
        }

        public IEnumerable<IEngineConfigurationTypeProvider> GetConfigurationTypes()
        {
            return mTypes;
        }
    }
}
