using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Configuration;
using AutoPoco.Engine;

namespace AutoPoco.Actions
{
    public class ObjectFieldSetFromValueAction : IObjectAction
    {
        private EngineTypeFieldMember mMember;
        private Object mValue;

        public ObjectFieldSetFromValueAction(EngineTypeFieldMember member, Object value) 
        {
            mMember = member;
            mValue = value;
        }

        public void Enact(IGenerationContext context, object target)
        {
            mMember.FieldInfo.SetValue(target, mValue);
        }
    }
}
