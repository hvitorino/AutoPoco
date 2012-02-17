using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Configuration;

namespace AutoPoco.Conventions
{
    public class DefaultIntegerMemberConvention : ITypeFieldConvention, ITypePropertyConvention
    {
        public void Apply(ITypePropertyConventionContext context)
        {
            if (context.Member.PropertyInfo.PropertyType == typeof(int))
            {
                context.SetValue(0);
            }
        }

        public void Apply(ITypeFieldConventionContext context)
        {
            if (context.Member.FieldInfo.FieldType == typeof(int))
            {
                context.SetValue(0);
            }
        }

        public void SpecifyRequirements(ITypeMemberConventionRequirements requirements)
        {
            requirements.Type(x => x == typeof(int));
        }
    }
}
