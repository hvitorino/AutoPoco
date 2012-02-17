using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace AutoPoco.Configuration
{
    public class EngineTypeFieldMember : EngineTypeMember
    {
        private FieldInfo mFieldInfo;

        public override string Name
        {
            get { return mFieldInfo.Name; }
        }

        public override bool IsMethod
        {
            get { return false; }
        }

        public override bool IsField
        {
            get { return true; }
        }

        public override bool IsProperty
        {
            get { return false; }
        }

        public FieldInfo FieldInfo
        {
            get
            {
                return mFieldInfo;
            }
        }

        public EngineTypeFieldMember(FieldInfo fieldInfo)
        {
            mFieldInfo = fieldInfo;
        }

        public override bool Equals(object obj)
        {
            var otherMember = obj as EngineTypeFieldMember;
            if (otherMember != null)
            {
                return otherMember.FieldInfo == this.FieldInfo;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return FieldInfo.GetHashCode();
        }
    }
}
