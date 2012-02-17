using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPoco.Configuration
{
    /// <summary>
    /// A registered type within engine configuration
    /// </summary>
    public interface IEngineConfigurationType
    {
        /// <summary>
        /// Gets the type this configuration item represents
        /// </summary>
        Type RegisteredType
        {
            get;
        }

        /// <summary>
        /// Gets all the registered members thus far
        /// </summary>
        /// <returns></returns>
        IEnumerable<IEngineConfigurationTypeMember> GetRegisteredMembers();

        /// <summary>
        /// Registers a member with configuration for actioning
        /// </summary>
        /// <param name="member"></param>
        void RegisterMember(EngineTypeMember member);

        /// <summary>
        /// Gets a registered member from configuration
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        IEngineConfigurationTypeMember GetRegisteredMember(EngineTypeMember member);

        /// <summary>
        /// Sets the factory used to instantiate instances of this type
        /// </summary>
        /// <param name="factory"></param>
        void SetFactory(IEngineConfigurationDatasource factory);

        /// <summary>
        /// Gets the factory used to instantiate instances of this type
        /// </summary>
        /// <returns></returns>
        IEngineConfigurationDatasource GetFactory();
         
    }
}
