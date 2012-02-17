using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPoco.Configuration.TypeRegistrationActions
{
    public class CascadeBaseTypeConfigurationAction : TypeRegistrationAction
    {
        private IEngineConfiguration mConfiguration;

        public CascadeBaseTypeConfigurationAction(IEngineConfiguration configuration)
        {
            mConfiguration = configuration;
        }

        public override void Apply(IEngineConfigurationType type)
        {
            ApplyToType(type);
        }

        private void ApplyToType(IEngineConfigurationType type)
        {
            // Create the dependency stack
            IEnumerable<IEngineConfigurationTypeMember> membersToApply = GetAllTypeHierarchyMembers(mConfiguration, type);

            foreach (var existingMemberConfig in membersToApply)
            {
                IEngineConfigurationTypeMember currentMemberConfig = type.GetRegisteredMember(existingMemberConfig.Member);
                if (currentMemberConfig == null)
                {
                    type.RegisterMember(existingMemberConfig.Member);
                    currentMemberConfig = type.GetRegisteredMember(existingMemberConfig.Member);
                    currentMemberConfig.SetDatasources(existingMemberConfig.GetDatasources());
                }
            }
        }

        protected virtual IEnumerable<IEngineConfigurationTypeMember> GetAllTypeHierarchyMembers(IEngineConfiguration baseConfiguration, IEngineConfigurationType sourceType)
        {
            Stack<IEngineConfigurationType> configurationStack = new Stack<IEngineConfigurationType>();
            Type currentType = sourceType.RegisteredType;
            IEngineConfigurationType currentTypeConfiguration = null;

            // Get all the base types into a stack, where the base-most type is at the top
            while (currentType != null)
            {
                currentTypeConfiguration = baseConfiguration.GetRegisteredType(currentType);
                if (currentTypeConfiguration != null) { configurationStack.Push(currentTypeConfiguration); }
                currentType = currentType.BaseType;
            }

            // Put all the implemented interfaces on top of that
            foreach (var interfaceType in sourceType.RegisteredType.GetInterfaces())
            {
                currentTypeConfiguration = baseConfiguration.GetRegisteredType(interfaceType);
                if (currentTypeConfiguration != null)
                {
                    configurationStack.Push(currentTypeConfiguration);
                }
            }

            var membersToApply = (from typeConfig in configurationStack
                                  from memberConfig in typeConfig.GetRegisteredMembers()
                                  select memberConfig).ToArray();

            return membersToApply;
        }
    }
}
