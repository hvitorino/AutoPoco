using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Configuration;

namespace AutoPoco
{
    public static class ConfigurationExtensions
    {
        public static IEngineConfigurationBuilder AddFromAssemblyContainingType<T>(this IEngineConfigurationBuilder builder)
        {
            foreach (var type in typeof(T).Assembly.GetTypes())
            {                
                builder.Include(type);
            }
            return builder;
        }
    }
}
