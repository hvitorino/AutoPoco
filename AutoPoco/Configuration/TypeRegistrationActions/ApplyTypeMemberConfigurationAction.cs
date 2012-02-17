using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Configuration.Providers;

namespace AutoPoco.Configuration.TypeRegistrationActions
{
    public class ApplyTypeMemberConfigurationAction : TypeRegistrationAction
    {
        private IEngineConfigurationProvider mConfigurationProvider;

        public ApplyTypeMemberConfigurationAction(IEngineConfigurationProvider configurationProvider)
        {
            mConfigurationProvider = configurationProvider;
        }

        public override void Apply(IEngineConfigurationType type)
        {
           ApplyToType(type);  
        }

        private void ApplyToType(IEngineConfigurationType type)
        {
            var typeProviders = mConfigurationProvider.GetConfigurationTypes()
                .Where(x => x.GetConfigurationType() == type.RegisteredType);

            foreach (var typeProvider in typeProviders)
            {
                foreach (var memberProvider in typeProvider.GetConfigurationMembers())
                {
                    EngineTypeMember typeMember = memberProvider.GetConfigurationMember();

                    // Get the member
                    var configuredMember = type.GetRegisteredMember(typeMember);

                    // Set the action on that member if a datasource has been set explicitly for this member
                    var datasources = memberProvider.GetDatasources();
                    if (datasources.Count() > 0)
                    {
                        configuredMember.SetDatasources(datasources);
                    }
                }
            }
        }
    }
}
