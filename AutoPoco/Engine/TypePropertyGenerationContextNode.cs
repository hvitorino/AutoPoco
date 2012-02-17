using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Configuration;

namespace AutoPoco.Engine
{
    public class TypePropertyGenerationContextNode : IGenerationContextNode
    {
        private TypeGenerationContextNode mParent;
        private EngineTypePropertyMember mProperty;

        public TypePropertyGenerationContextNode(TypeGenerationContextNode parent, EngineTypePropertyMember property)
        {
            mParent = parent;
            mProperty = property;
        }

        public virtual EngineTypePropertyMember Property
        {
            get { return mProperty; }
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
            get { return GenerationTargetTypes.Property; }
        }
    }
}
