using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using AutoPoco.Configuration.Providers;

namespace AutoPoco.Configuration
{
    public class EngineConventionConfiguration : IEngineConventionConfiguration, IEngineConventionProvider
    {
        private HashSet<Type> mConventions = new HashSet<Type>();

        public void Register(Type conventionType)
        {
            mConventions.Add(conventionType);
        }

        public void Register<T>() where T : IConvention
        {
            Register(typeof(T));
        }

        public void UseDefaultConventions()
        {
            ScanAssemblyWithType<EngineConventionConfiguration>();
        }

        public void ScanAssemblyWithType<T>()
        {
            ScanAssembly(typeof(T).Assembly);
        }

        public void ScanAssembly(Assembly assembly)
        {
            foreach (var type in assembly.GetTypes()
               .Where(x => typeof(IConvention).IsAssignableFrom(x)))
            {
                Register(type);
            }
        }

        public IEnumerable<Type> Find<T>() where T : IConvention
        {
            return mConventions.Where(x => 
                x.IsClass 
                && typeof(T).IsAssignableFrom(x)
                && !x.IsAbstract);
        }
    }
}
