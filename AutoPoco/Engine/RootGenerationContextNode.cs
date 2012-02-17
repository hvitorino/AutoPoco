using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPoco.Engine
{
    public class RootGenerationContextNode : IGenerationContextNode
    {
        public IGenerationContextNode Parent
        {
            get { return null; }
        }
        public GenerationTargetTypes ContextType
        {
            get { return GenerationTargetTypes.Root; }
        }
    }
}
