using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using AutoPoco.Configuration;

namespace AutoPoco.Engine
{
    public class ObjectPropertySetFromSourceAction : IObjectAction
    {
        private EngineTypePropertyMember mMember;
        private IDatasource mDatasource;

        public ObjectPropertySetFromSourceAction(EngineTypePropertyMember member, IDatasource source)
        {
            mMember = member;
            mDatasource = source;
        }

        public void Enact(IGenerationContext context, object target)
        {
            var propertyContext = new GenerationContext(context.Builders, new TypePropertyGenerationContextNode(
                                                                           (TypeGenerationContextNode)context.Node,
                                                                           mMember));
            mMember.PropertyInfo.SetValue(target, mDatasource.Next(propertyContext), null);
        }
    }
}
