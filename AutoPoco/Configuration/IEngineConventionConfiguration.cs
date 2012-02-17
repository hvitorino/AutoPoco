using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace AutoPoco.Configuration
{
    public interface IEngineConventionConfiguration
    {
        /// <summary>
        /// Registers a convention with the engine
        /// </summary>
        /// <param name="conventionType"></param>
        void Register(Type conventionType);

        /// <summary>
        /// Registers a convention with the engine
        /// </summary>
        /// <typeparam name="T"></typeparam>
        void Register<T>() where T : IConvention;

        /// <summary>
        /// Scans the assembly containing the type specified for conventions
        /// </summary>
        /// <typeparam name="T"></typeparam>
        void ScanAssemblyWithType<T>();

        /// <summary>
        /// Scans the assembly specified for conventions
        /// </summary>
        /// <param name="assembly"></param>
        void ScanAssembly(Assembly assembly);

        /// <summary>
        /// Uses the conventions that ship with AutoPoco
        /// </summary>
        void UseDefaultConventions();
    }
}
