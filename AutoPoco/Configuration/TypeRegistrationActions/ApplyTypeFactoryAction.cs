using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Configuration.Providers;
using AutoPoco.DataSources;

namespace AutoPoco.Configuration.TypeRegistrationActions
{
    public class ApplyTypeFactoryAction : TypeRegistrationAction
    {
        private readonly IEngineConfigurationProvider mConfigurationProvider;

        public ApplyTypeFactoryAction(IEngineConfigurationProvider configurationProvider)
        {
            mConfigurationProvider = configurationProvider;
        }
        public override void Apply(IEngineConfigurationType type)
        {
            var typeProvider =
                mConfigurationProvider.GetConfigurationTypes().Where(x => x.GetConfigurationType() == type.RegisteredType)
                    .FirstOrDefault();
            
            if(typeProvider != null && typeProvider.GetFactory() != null)
            {
                type.SetFactory(typeProvider.GetFactory());
            }
            else if (type.GetFactory() == null)
            {
                // Activator.CreateInstance as a last resort
                Type fallbackType = typeof (FallbackObjectFactory<>).MakeGenericType(type.RegisteredType);
                type.SetFactory(new DatasourceFactory(fallbackType));
            }
        }
    }
}
