using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Configuration.Providers;

namespace AutoPoco.Configuration.TypeRegistrationActions
{
    public class RegisterTypeMembersFromConfigurationAction : TypeRegistrationAction
    {
        private IEngineConfigurationProvider mConfigurationProvider;

        public RegisterTypeMembersFromConfigurationAction(IEngineConfigurationProvider configurationProvider)
        {
            mConfigurationProvider = configurationProvider;
        }

        public override void Apply(IEngineConfigurationType type)
        {
            ApplyToType(type);  
        }

        private void ApplyToType(IEngineConfigurationType type)
        {
            var typeProviders = mConfigurationProvider.GetConfigurationTypes().Where(x => x.GetConfigurationType() == type.RegisteredType);
            foreach (var typeProvider in typeProviders)
            {
                foreach (var member in typeProvider.GetConfigurationMembers())
                {
                    EngineTypeMember typeMember = member.GetConfigurationMember();

                    if (type.GetRegisteredMember(typeMember) == null)
                    {
                        type.RegisterMember(typeMember);
                    }
                }
            }
        }
    }
}
