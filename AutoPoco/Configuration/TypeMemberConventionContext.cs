using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.DataSources;
using AutoPoco.Engine;

namespace AutoPoco.Configuration
{
    public class TypeMemberConventionContext
    {
        private IEngineConfigurationTypeMember mTypeMember;
        private IEngineConfiguration mConfiguration;

        public IEngineConfiguration Configuration
        {
            get
            {
                return mConfiguration;
            }
        }

        public EngineTypeMember Member
        {
            get { return mTypeMember.Member; }
        }

        public void SetValue(object value)
        {
            var factory = new DatasourceFactory(typeof(ValueSource));
            factory.SetParams(value);
            mTypeMember.SetDatasource(factory);
        }

        public void SetSource<T>() where T : AutoPoco.Engine.IDatasource
        {
            SetSource(typeof(T));
        }

        public void SetSource(Type t)
        {
            mTypeMember.SetDatasource(new DatasourceFactory(t));
        }

        public void SetSource(IDatasource sourceInstance)
        {
            mTypeMember.SetDatasource(new DatasourceFactory(sourceInstance));
        }

        public TypeMemberConventionContext(IEngineConfiguration configuration, IEngineConfigurationTypeMember member)
        {
            mConfiguration = configuration;
            mTypeMember = member;
        }
        
    }
}
