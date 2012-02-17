using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPoco.Configuration
{
    public class EngineConfigurationTypeMember : IEngineConfigurationTypeMember
    {
        private List<IEngineConfigurationDatasource> mDataSources = new List<IEngineConfigurationDatasource>();

        private EngineTypeMember mMember;

        public EngineConfigurationTypeMember(EngineTypeMember member)
        {
            mMember = member;
        }

        public EngineTypeMember Member
        {
            get { return mMember; }
        }

        public void SetDatasource(IEngineConfigurationDatasource action)
        {
            mDataSources.Clear();
            mDataSources.Add(action);
        }

        public void SetDatasources(IEnumerable<IEngineConfigurationDatasource> sources)
        {
            mDataSources.Clear();
            mDataSources.AddRange(sources);
        }

        public IEnumerable<IEngineConfigurationDatasource> GetDatasources()
        {
            return mDataSources.ToArray();
        }
    }
}
