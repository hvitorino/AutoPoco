using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AutoPoco.Engine;
using AutoPoco.Configuration.Providers;

namespace AutoPoco.Configuration
{
    public class EngineConfigurationTypeMemberBuilder : IEngineConfigurationTypeMemberBuilder, IEngineConfigurationTypeMemberProvider
    {
        private EngineConfigurationTypeBuilder mParentConfiguration;
        private EngineTypeMember mMember;
        private List<DatasourceFactory> mDatasources = new List<DatasourceFactory>();

        public EngineConfigurationTypeMemberBuilder(EngineTypeMember member, EngineConfigurationTypeBuilder parentConfiguration)
        {
            mMember = member;
            mParentConfiguration = parentConfiguration;
        }

        public IEngineConfigurationTypeBuilder Use(Type dataSource)
        {
            return Use(dataSource, new Object[] { });
        }

        public IEngineConfigurationTypeBuilder Use(Type dataSource, params object[] args)
        {
            if (dataSource.GetInterface(typeof(IDatasource).FullName) == null) { throw new ArgumentException("dataSource does not implement IDatasource", "dataSource"); }
            mDatasources.Clear();
            
            DatasourceFactory newFactory = new DatasourceFactory(dataSource);
            newFactory.SetParams(args);
            mDatasources.Add(newFactory);          
            return mParentConfiguration;
        }

        public void SetDatasources(params DatasourceFactory[] dataSources)
        {
            mDatasources.Clear();
            if (dataSources.Length > 0)
            {
                mDatasources.AddRange(dataSources);
            }
        }

        public IEngineConfigurationTypeBuilder Default()
        {
            mDatasources.Clear();
            return mParentConfiguration;
        }

        public EngineTypeMember GetConfigurationMember()
        {
            return mMember;
        }

        public IEnumerable<IEngineConfigurationDatasource> GetDatasources()
        {
            return mDatasources.Cast<IEngineConfigurationDatasource>();
        }
    }
}
