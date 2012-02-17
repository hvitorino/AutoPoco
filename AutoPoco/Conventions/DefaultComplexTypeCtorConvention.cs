using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AutoPoco.Configuration;
using AutoPoco.DataSources;

namespace AutoPoco.Conventions
{
    public class DefaultComplexTypeCtorConvention : ITypeConvention
    {
        public void Apply(ITypeConventionContext context)
        {
            var type = context.Target;
            if (type.IsPrimitive || type == typeof(Decimal) || type == typeof(string)) { return; }

            var ctor = type.GetConstructors(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                        .OrderBy(x => x.GetParameters().Count())
                        .FirstOrDefault();

            if (ctor == null) { return; }

            var ctorSourceType = typeof (CtorSource<>).MakeGenericType(type);
            
            context.SetFactory(ctorSourceType, ctor);
        }
    }
}
