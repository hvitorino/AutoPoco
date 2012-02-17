using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPoco.Engine
{
    /// <summary>
    /// The base interface for any action to be enacted on an object post-creation
    /// </summary>
    public interface IObjectAction
    {
        /// <summary>
        /// Enacts this action on the target object
        /// </summary>
        /// <param name="target"></param>
        void Enact(IGenerationContext context, Object target);
    }
}
