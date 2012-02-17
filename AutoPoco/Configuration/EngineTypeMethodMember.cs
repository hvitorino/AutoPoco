using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using AutoPoco.Util;

namespace AutoPoco.Configuration
{
    public class EngineTypeMethodMember : EngineTypeMember
    {
        private MethodInfo mMethodInfo;

        public override string Name
        {
            get { return mMethodInfo.Name; }
        }

        public override bool IsMethod
        {
            get { return true; }
        }

        public override bool IsField
        {
            get { return false; }
        }

        public override bool IsProperty
        {
            get { return false; }
        }

        public MethodInfo MethodInfo
        {
            get { return mMethodInfo; }
        }

        public EngineTypeMethodMember(MethodInfo methodInfo)
        {
            mMethodInfo = methodInfo;
        }

        public override bool Equals(object obj)
        {
            var otherMember = obj as EngineTypeMethodMember;
            if (otherMember != null)
            {
                return (otherMember.MethodInfo.Name == this.MethodInfo.Name) && otherMember.MethodInfo.ArgumentsAreEqualTo(this.MethodInfo);
                    
            }
            return false;
        }

        public override int GetHashCode()
        {
            return mMethodInfo.GetHashCode();
        }
    }
}
