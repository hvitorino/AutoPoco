using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Engine;

namespace AutoPoco.Configuration
{
    public interface IEngineConfigurationTypeMemberBuilder<TPoco, TMember>
    {
        /// <summary>
        /// Uses the specified data source for member values
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <returns></returns>
        IEngineConfigurationTypeBuilder<TPoco> Use<TSource>() where TSource : IDatasource<TMember>;

        /// <summary>
        /// Uses the specified data source for member values (with args)
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="args"></param>
        /// <returns></returns>
        IEngineConfigurationTypeBuilder<TPoco> Use<TSource>(params Object[] args) where TSource : IDatasource<TMember>;

        /// <summary>
        /// Allows this property to be set by convention
        /// </summary>
        /// <returns></returns>
        IEngineConfigurationTypeBuilder<TPoco> Default();
    }
}
