using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Configuration.Providers;

namespace AutoPoco.Configuration
{
    public class EngineConfiguration : IEngineConfiguration
    {
        private List<EngineConfigurationType> mRegisteredTypes = new List<EngineConfigurationType>();

        public IEnumerable<IEngineConfigurationType> GetRegisteredTypes()
        {
            return mRegisteredTypes.ConvertAll<IEngineConfigurationType>(x => (IEngineConfigurationType)x);
        }

        public void RegisterType(Type t)
        {
            if (mRegisteredTypes.Find(x => x.RegisteredType == t) != null)
            {
                throw new ArgumentException("Type has already been registered", "t");
            }
            mRegisteredTypes.Add(new EngineConfigurationType(t));
        }

        public IEngineConfigurationType GetRegisteredType(Type t)
        {
            return mRegisteredTypes.Find(x => x.RegisteredType == t);
        }
    }
}
