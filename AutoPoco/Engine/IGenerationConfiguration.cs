using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPoco.Engine
{
    public interface IGenerationConfiguration
    {
        /// <summary>
        /// Gets the object builder for a certain type
        /// </summary>
        /// <param name="searchType"></param>
        /// <returns></returns>
        IObjectBuilder GetBuilderForType(Type searchType);

        /// <summary>
        /// Gets the recursion limit for this configuration
        /// </summary>
        int RecursionLimit { get; }
    }
}
