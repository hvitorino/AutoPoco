using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.DataSources;

namespace AutoPoco.Configuration
{
    public class MethodInvocationContext
    {
        private List<DatasourceFactory> mArguments = new List<DatasourceFactory>();

        public void AddArgumentSource(Type source, params Object[] args)
        {
            DatasourceFactory factory = new DatasourceFactory(source);
            factory.SetParams(args);
            mArguments.Add(factory);
        }

        public void AddArgumentSource(Type source)
        {
            AddArgumentSource(source, null);
        }

        public void AddArgumentValue(Object value)
        {
            AddArgumentSource(typeof(ValueSource), value);
        }

        public IEnumerable<DatasourceFactory> GetArguments()
        {
            return mArguments;
        }
    }
}
