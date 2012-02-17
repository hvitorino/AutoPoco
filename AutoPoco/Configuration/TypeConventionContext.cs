using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Util;
using System.Reflection;

namespace AutoPoco.Configuration
{
    public class TypeConventionContext : ITypeConventionContext
    {
        private IEngineConfigurationType mType;

        public Type Target
        {
            get
            {
                return mType.RegisteredType;
            }
        }

        public void SetFactory(Type factory)
        {
            mType.SetFactory(new DatasourceFactory(factory));
        }

        public void SetFactory(Type factory, params object[] ctorArgs)
        {
            var sourceFactory = new DatasourceFactory(factory);
            sourceFactory.SetParams(ctorArgs);
            mType.SetFactory(sourceFactory);
        }

        public void RegisterField(FieldInfo field)
        {
            var member = ReflectionHelper.GetMember(field);
            if (mType.GetRegisteredMember(member) == null)
            {
                mType.RegisterMember(member);
            }
        }

        public void RegisterProperty(PropertyInfo property)
        {
            var member = ReflectionHelper.GetMember(property);
            if (mType.GetRegisteredMember(member) == null)
            {
                mType.RegisterMember(ReflectionHelper.GetMember(property));
            }
        }

        public void RegisterMethod(MethodInfo method, MethodInvocationContext context)
        {
            var member = ReflectionHelper.GetMember(method);
            if (mType.GetRegisteredMember(member) == null)
            {
                mType.RegisterMember(member);
            }
            var registeredMember = mType.GetRegisteredMember(member);
            registeredMember.SetDatasources(context.GetArguments().Cast<IEngineConfigurationDatasource>());
        }

        public TypeConventionContext(IEngineConfigurationType type)
        {
            mType = type;
        }
    }
}
