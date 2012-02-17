using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPoco.Configuration
{
    /// <summary>
    /// Core interface for AutoPoco configuration
    /// </summary>
    public interface IEngineConfiguration
    {
        /// <summary>
        /// Registers a type with the configuration
        /// </summary>
        /// <param name="t"></param>
        void RegisterType(Type t);

        /// <summary>
        /// Gets all of the thus-far registered types
        /// </summary>
        /// <returns></returns>
        IEnumerable<IEngineConfigurationType> GetRegisteredTypes();

        /// <summary>
        /// Gets a registered type from the configuration
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        IEngineConfigurationType GetRegisteredType(Type t);
    }
}
