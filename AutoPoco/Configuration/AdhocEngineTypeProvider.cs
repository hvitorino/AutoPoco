using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Configuration.Providers;

namespace AutoPoco.Configuration
{
    public class AdhocEngineTypeProvider : IEngineConfigurationTypeProvider
    {
        private Type mType;

        public AdhocEngineTypeProvider(Type type) { mType = type; }

        public Type GetConfigurationType()
        {
            return mType;
        }

        public IEnumerable<IEngineConfigurationTypeMemberProvider> GetConfigurationMembers()
        {
            return new IEngineConfigurationTypeMemberProvider[] { };
        }

        public IEngineConfigurationDatasource GetFactory()
        {
            return null;
        }
    }
}
