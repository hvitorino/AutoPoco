using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Configuration;
using System.Reflection;

namespace AutoPoco.Conventions
{
    public class DefaultTypeConvention : ITypeConvention
    {
        public void Apply(ITypeConventionContext context)
        {
            // Register every public property on this type
            foreach(var property in context.Target
                .GetProperties( System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                .Where(x => !x.PropertyType.ContainsGenericParameters && IsDefinedOnType(x, context.Target)))
            {
                if(PropertyHasPublicSetter(property))
                {
                    context.RegisterProperty(property);
                }
            }

            // Register every public field on this type
            foreach (var field in context.Target
                .GetFields(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance)
                .Where(x => !x.FieldType.ContainsGenericParameters && IsDefinedOnType(x, context.Target)))
            {
                context.RegisterField(field);
            }                
        }

        private bool PropertyHasPublicSetter(PropertyInfo property)
        {
            var setter = property.GetSetMethod();
            return setter != null && setter.IsPublic;
        }
        
        private bool IsDefinedOnType(MemberInfo member, Type type)
        {
            if(member.DeclaringType != type) { return false;}

            if (member.MemberType == MemberTypes.Property && !type.IsInterface)
            {
                PropertyInfo property = (PropertyInfo)member;

                var interfaceMethods =
                    (from i in type.GetInterfaces()
                     from method in type.GetInterfaceMap(i).TargetMethods
                     select method);

                var exists = (from method in interfaceMethods
                             where property.GetAccessors().Contains(method) == true
                             select 1).Count() > 0;
                    

                if(exists) return false;
            }

            return true;           
        }
    }
}
