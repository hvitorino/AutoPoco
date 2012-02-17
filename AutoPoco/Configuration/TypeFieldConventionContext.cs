using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPoco.Configuration
{
    public class TypeFieldConventionContext : TypeMemberConventionContext, ITypeFieldConventionContext
    {
        public new EngineTypeFieldMember Member
        {
            get
            {
                return base.Member as EngineTypeFieldMember;
            }
        }

        public TypeFieldConventionContext(IEngineConfiguration config, IEngineConfigurationTypeMember member)
            : base(config, member)
        {

        }

    }
}
