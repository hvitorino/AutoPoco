using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Engine;

namespace AutoPoco.DataSources
{
    public class ParentSource<T> : DatasourceBase<T>
    {
        public override T Next(IGenerationContext context)
        {
            // Search upwards for parent
            return FindParent(context.Node, false);
        }

        private T FindParent(IGenerationContextNode current, bool foundOne)
        {
            if (current == null) { return default(T); }
            if (current.ContextType == GenerationTargetTypes.Object)
            {
                var type = (TypeGenerationContextNode)current;
                if (type.Target is T)
                {
                    if (foundOne)
                    {
                        return (T)type.Target;
                    }
                    else return FindParent(current.Parent, true);
                }
            }
            return FindParent(current.Parent, foundOne);
        }
    }
}
