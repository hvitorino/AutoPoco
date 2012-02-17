using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Configuration;
using AutoPoco.Engine;

namespace AutoPoco.Actions
{
    public class ObjectMethodInvokeFromSourceAction : IObjectAction
    {
        private EngineTypeMethodMember mMember;
        private IEnumerable<IDatasource> mSources;

        public ObjectMethodInvokeFromSourceAction(EngineTypeMethodMember member, IEnumerable<IDatasource> sources)
        {
            mMember = member;
            mSources = sources.ToArray();
        }

        public void Enact(IGenerationContext context, object target)
        {
            List<Object> paramList = new List<object>();
            var methodContext = new GenerationContext(context.Builders,
                                                      new TypeMethodGenerationContextNode((TypeGenerationContextNode)context.Node, mMember));

            foreach (var source in mSources)
            {
                paramList.Add(source.Next(methodContext));
            }

            mMember.MethodInfo.Invoke(target, paramList.ToArray());
        }
    }
}
