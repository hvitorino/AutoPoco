using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Configuration;

namespace AutoPoco.Engine
{
    public interface IGenerationContextNode
    {
        /// <summary>
        /// Gets the next level up in the call graph
        /// </summary>
        IGenerationContextNode Parent { get; }

        GenerationTargetTypes ContextType { get; }
    }
}
