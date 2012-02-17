using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Configuration.Providers;

namespace AutoPoco.Configuration
{
    public interface IEngineConfigurationFactory
    {
        /// <summary>
        /// Creates an engine configuration from a configuration provider and a set of conventions
        /// </summary>
        /// <returns></returns>
        IEngineConfiguration Create(IEngineConfigurationProvider configurationProvider, IEngineConventionProvider conventionProvider);
    }
}
