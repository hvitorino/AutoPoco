using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Configuration;
using AutoPoco.DataSources;
using System.Collections;

namespace AutoPoco.Conventions
{
    public class ReferenceMemberConvention : ITypeFieldConvention, ITypePropertyConvention
    {
        public void Apply(ITypeFieldConventionContext context)
        {
            context.SetSource(typeof(AutoSource<>).MakeGenericType(context.Member.FieldInfo.FieldType));
        }

        public void Apply(ITypePropertyConventionContext context)
        {
            context.SetSource(typeof(AutoSource<>).MakeGenericType(context.Member.PropertyInfo.PropertyType));           
        }

        public void SpecifyRequirements(ITypeMemberConventionRequirements requirements)
        {
            requirements.Type(x => 
                x.IsClass 
                && x.GetInterface(typeof(IEnumerable).FullName) == null);
        }
    }
}
