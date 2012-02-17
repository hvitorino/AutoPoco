using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Configuration;

namespace AutoPoco.Engine
{
    public class TypeFieldGenerationContextNode : IGenerationContextNode
    {
        private TypeGenerationContextNode mParent;
        private EngineTypeFieldMember mField;

        public TypeFieldGenerationContextNode(TypeGenerationContextNode parent, EngineTypeFieldMember field)
        {
            mParent = parent;
            mField = field;
        }

        public virtual EngineTypeFieldMember Field
        {
            get { return mField; }
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
            get { return GenerationTargetTypes.Field; }
        }
    }
}
