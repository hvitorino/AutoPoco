using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Engine;

namespace AutoPoco.Configuration
{
    public class DatasourceFactory : IEngineConfigurationDatasource
    {
        private Type mDatasourceType;
        private Object[] mParams;
        private IDatasource sourceInstance;

        public DatasourceFactory(Type t)
        {
            mDatasourceType = t;
        }

        public DatasourceFactory(IDatasource sourceInstance)
        {
            this.sourceInstance = sourceInstance;
        }

        public void SetParams(params Object[] args)
        {
            mParams = args;
        }

        public IDatasource Build()
        {
            if(sourceInstance == null)
                return Activator.CreateInstance(mDatasourceType, mParams) as IDatasource;
            return sourceInstance;
        }
    }
}
