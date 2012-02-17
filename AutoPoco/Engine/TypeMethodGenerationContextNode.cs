using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Configuration;

namespace AutoPoco.Engine
{
    public class TypeMethodGenerationContextNode : IGenerationContextNode
    {
        private TypeGenerationContextNode mParent;
        private EngineTypeMethodMember mMethod  ;

        public TypeMethodGenerationContextNode(TypeGenerationContextNode parent, EngineTypeMethodMember method)
        {
            mParent = parent;
            mMethod = method;
        }

        public virtual EngineTypeMethodMember Method
        {
            get { return mMethod; }
        }

        public virtual IGenerationContextNode Parent
        {
            get { return mParent; }
        }

        public virtual object Target
        {
            get { return mParent.Target; }
        }

        public GenerationTargetTypes ContextType
        {
            get { return GenerationTargetTypes.Method; }
        }
    }
}
