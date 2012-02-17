using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPoco.Configuration.Providers
{
    public interface IEngineConfigurationProvider
    {
        /// <summary>
        /// Gets the configuration types from the provider
        /// </summary>
        /// <returns></returns>
        IEnumerable<IEngineConfigurationTypeProvider> GetConfigurationTypes();
    }
}
