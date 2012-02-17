using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Engine;

namespace AutoPoco.Configuration
{
    public interface IEngineConfigurationTypeMemberBuilder
    {
        /// <summary>
        /// Uses the specified data source for member values
        /// </summary>
        /// <returns></returns>
        IEngineConfigurationTypeBuilder Use(Type dataSource);

        /// <summary>
        /// Uses the specified data source for member values (with args)
        /// </summary>
        /// <returns></returns>
        IEngineConfigurationTypeBuilder Use(Type dataSource, params Object[] args);

        /// <summary>
        /// Allows this property to be set by convention
        /// </summary>
        /// <returns></returns>
        IEngineConfigurationTypeBuilder Default();
    }
}
