using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Configuration.Providers;

namespace AutoPoco.Configuration.TypeRegistrationActions
{
    public class ApplyTypeMemberConventionsAction : TypeRegistrationAction
    {
        private IEngineConfiguration mConfiguration;
        private IEngineConventionProvider mConventionProvider;

        public ApplyTypeMemberConventionsAction(IEngineConfiguration configuration, IEngineConventionProvider conventions)
        {
            mConfiguration = configuration;
            mConventionProvider = conventions;
        }

        public override void Apply(IEngineConfigurationType type)
        {
            ApplyToType(type);

        }

        private void ApplyToType(IEngineConfigurationType type)
        {
            foreach (var member in type.GetRegisteredMembers())
            {
                ApplyToTypeMember(member);
            }
        }

        private void ApplyToTypeMember(IEngineConfigurationTypeMember member)
        {
            if (member.Member.IsField)
            {
                ApplyFieldConventions(member);
            }
            if (member.Member.IsProperty)
            {
                ApplyPropertyConventions(member);
            }
        }

        private void ApplyPropertyConventions(IEngineConfigurationTypeMember member)
        {
            var convention = mConventionProvider.Find<ITypePropertyConvention>()
                 .Select(t =>
                 {
                     var details = new
                     {
                         Convention = (ITypePropertyConvention)Activator.CreateInstance(t),
                         Requirements = new TypePropertyConventionRequirements()
                     };
                     details.Convention.SpecifyRequirements(details.Requirements);
                     return details;
                 })
                 .Where(x => x.Requirements.IsValid((EngineTypePropertyMember)member.Member))
                 .OrderByDescending(x => ScoreRequirements(x.Requirements))
                 .FirstOrDefault();

            if (convention != null)
            {
                convention.Convention.Apply(new TypePropertyConventionContext(mConfiguration, member));
            }
        }

        private void ApplyFieldConventions(IEngineConfigurationTypeMember member)
        {
            var convention = mConventionProvider.Find<ITypeFieldConvention>()
            .Select(t =>
            {
                var details = new
                {
                    Convention = (ITypeFieldConvention)Activator.CreateInstance(t),
                    Requirements = new TypeFieldConventionRequirements()
                };
                details.Convention.SpecifyRequirements(details.Requirements);
                return details;
            })
            .Where(x => x.Requirements.IsValid((EngineTypeFieldMember)member.Member))
            .OrderByDescending(x => ScoreRequirements(x.Requirements))
            .FirstOrDefault();

            if (convention != null)
            {
                convention.Convention.Apply(new TypeFieldConventionContext(mConfiguration, member));
            }
        }

        private int ScoreRequirements(TypeMemberConventionRequirements requirements)
        {
            int score = 0;
            if (requirements.HasNameRule()) { score += 2; }
            if (requirements.HasTypeRule()) { score++; }
            return score;
        }
    }
}
