using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPoco.Configuration
{
    public class TypePropertyConventionContext : TypeMemberConventionContext, ITypePropertyConventionContext
    {
        public new EngineTypePropertyMember Member
        {
            get
            {
                return base.Member as EngineTypePropertyMember;
            }
        }

        public TypePropertyConventionContext(IEngineConfiguration config, IEngineConfigurationTypeMember member)
            : base(config, member)
        {

        }

    }
}
