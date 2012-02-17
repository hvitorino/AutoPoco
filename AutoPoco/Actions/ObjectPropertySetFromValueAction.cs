using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Configuration;
using AutoPoco.Engine;

namespace AutoPoco.Actions
{
    public class ObjectPropertySetFromValueAction : IObjectAction
    {
        private EngineTypePropertyMember mMember;
        private Object mValue;

        public ObjectPropertySetFromValueAction(EngineTypePropertyMember member, Object value) 
        {
            mMember = member;
            mValue = value;
        }

        public void Enact(IGenerationContext context, object target)
        {
            mMember.PropertyInfo.SetValue(target, mValue, null);
        }
    }
}
