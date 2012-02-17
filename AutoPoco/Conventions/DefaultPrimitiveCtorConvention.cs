using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Configuration;
using AutoPoco.DataSources;

namespace AutoPoco.Conventions
{
    public class DefaultPrimitiveCtorConvention : ITypeConvention
    {
        public void Apply(ITypeConventionContext context)
        {
            var type = context.Target;
            if(type.IsPrimitive || type == typeof(Decimal))
            {
                context.SetFactory(typeof(DefaultSource<>).MakeGenericType(type));
            }
            else if(type == typeof(string))
            {
                context.SetFactory(typeof (DefaultStringSource));
            }
        }
    }
}
