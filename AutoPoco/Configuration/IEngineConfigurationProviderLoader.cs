using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPoco.Configuration
{
    public interface IEngineConfigurationProviderLoader
    {
        void Apply(IEngineConfigurationProviderLoaderContext context);
    }
}
