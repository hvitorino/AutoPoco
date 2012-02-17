using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using AutoPoco.Util;
using AutoPoco.Configuration.Providers;
using AutoPoco.Engine;
using System.Reflection;
using AutoPoco.DataSources;

namespace AutoPoco.Configuration
{
    public class EngineConfigurationTypeBuilder : IEngineConfigurationTypeProvider, IEngineConfigurationTypeBuilder
    {
        private List<IEngineConfigurationTypeMemberProvider> mMembers = new List<IEngineConfigurationTypeMemberProvider>();
        private Type mType;
        private DatasourceFactory mFactory;

        public EngineConfigurationTypeBuilder(Type type)
        {
            mType = type;
        }

        Type IEngineConfigurationTypeProvider.GetConfigurationType()
        {
            return mType;
        }

        IEnumerable<IEngineConfigurationTypeMemberProvider> IEngineConfigurationTypeProvider.GetConfigurationMembers()
        {
            return mMembers;
        }

        IEngineConfigurationDatasource IEngineConfigurationTypeProvider.GetFactory()
        {
            return mFactory;
        }

        IEngineConfigurationTypeMemberBuilder IEngineConfigurationTypeBuilder.SetupProperty(string propertyName)
        {
            MemberInfo info = mType.GetProperty(propertyName);
            if (info == null) { throw new ArgumentException("Property does not exist", propertyName); }
            var memberBuilder = new EngineConfigurationTypeMemberBuilder(ReflectionHelper.GetMember(info), this);
            mMembers.Add(memberBuilder);
            return memberBuilder;
        }

        IEngineConfigurationTypeMemberBuilder IEngineConfigurationTypeBuilder.SetupField(string fieldName)
        {
            FieldInfo info = mType.GetField(fieldName);
            if (info == null) { throw new ArgumentException("Field does not exist", fieldName); }
            var memberBuilder = new EngineConfigurationTypeMemberBuilder(ReflectionHelper.GetMember(info), this);
            mMembers.Add(memberBuilder);
            return memberBuilder;
        }

        public IEngineConfigurationTypeBuilder SetupMethod(string methodName)
        {
            return SetupMethod(methodName, new MethodInvocationContext());
        }

        public IEngineConfigurationTypeBuilder SetupMethod(string methodName, MethodInvocationContext context)
        {
            DatasourceFactory[] factories = context.GetArguments().ToArray();
            MethodInfo info = mType.GetMethods(BindingFlags.Public | BindingFlags.Instance).Where(x => 
                x.Name == methodName
                && x.GetParameters().Length == factories.Length)
                .FirstOrDefault();

            if (info == null) { throw new ArgumentException("Method does not exist", methodName); }

            var memberBuilder = new EngineConfigurationTypeMemberBuilder(ReflectionHelper.GetMember(info), this);
            mMembers.Add(memberBuilder);
            memberBuilder.SetDatasources(factories);

            return this;
        }

        public void RegisterTypeMemberProvider(IEngineConfigurationTypeMemberProvider memberProvider)
        {
            this.mMembers.Add(memberProvider);
        }

        public IEngineConfigurationTypeBuilder ConstructWith(Type type)
        {
            mFactory = new DatasourceFactory(type);
            return this;
        }

        public IEngineConfigurationTypeBuilder ConstructWith(Type type, params object[] args)
        {
            mFactory = new DatasourceFactory(type);
            mFactory.SetParams(args);
            return this;
        }
    }
}
