using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Engine;

namespace AutoPoco.Configuration
{
    public class EngineConfigurationTypeMemberBuilder<TPoco, TMember> : EngineConfigurationTypeMemberBuilder, IEngineConfigurationTypeMemberBuilder<TPoco, TMember>
    {
        private IEngineConfigurationTypeBuilder<TPoco> mParentConfiguration;

        public EngineConfigurationTypeMemberBuilder(EngineTypeMember member, EngineConfigurationTypeBuilder<TPoco> parentConfiguration)
            : base(member, parentConfiguration)
        {
            mParentConfiguration = parentConfiguration;
        }

        public IEngineConfigurationTypeBuilder<TPoco> Use<TSource>() where TSource : IDatasource<TMember>
        {
            return Use<TSource>(new Object[] { });
        }

        public IEngineConfigurationTypeBuilder<TPoco> Use<TSource>(params Object[] args) where TSource : IDatasource<TMember>
        {
            DatasourceFactory factory = new DatasourceFactory(typeof(TSource));
            factory.SetParams(args);
            SetDatasources(factory);
            return mParentConfiguration;
        }

        public new IEngineConfigurationTypeBuilder<TPoco> Default()
        {
            base.Default();
            return mParentConfiguration;
        }       
    }
}
