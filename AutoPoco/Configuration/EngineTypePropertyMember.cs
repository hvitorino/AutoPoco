using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

using AutoPoco.Util;

namespace AutoPoco.Configuration
{
    public class EngineTypePropertyMember : EngineTypeMember
    {
        private PropertyInfo mPropertyInfo;

        public override string Name
        {
            get { return mPropertyInfo.Name; }
        }

        public override bool IsMethod
        {
            get { return false; }
        }

        public override bool IsField
        {
            get { return false; }
        }

        public override bool IsProperty
        {
            get { return true; }
        }

        public PropertyInfo PropertyInfo
        {
            get { return mPropertyInfo; }
        }

        public EngineTypePropertyMember(PropertyInfo propertyInfo)
        {
            if (propertyInfo == null) { throw new ArgumentNullException("propertyInfo"); }
            mPropertyInfo = propertyInfo;
        }
        
        public override bool Equals(object obj)
        {
            var otherMember = obj as EngineTypePropertyMember;
            if (otherMember != null)
            {
                var propertyOne = this.PropertyInfo;
                var propertyTwo = otherMember.PropertyInfo;

                if (String.Compare(propertyOne.Name, propertyTwo.Name, false) != 0) { return false; }
                if (propertyOne.PropertyType != propertyTwo.PropertyType) { return false; }

                return true;                   
            }
            return false;
        }
        
        public override int GetHashCode()
        {
            return PropertyInfo.GetHashCode();
        }
    }
}
