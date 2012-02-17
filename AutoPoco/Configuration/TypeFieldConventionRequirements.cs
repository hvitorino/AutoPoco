using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPoco.Configuration
{
    public class TypeFieldConventionRequirements : TypeMemberConventionRequirements
    {
        public bool IsValid(EngineTypeFieldMember member)
        {
            return IsValidName(member.Name) && IsValidType(member.FieldInfo.FieldType);
        }
    }
}
