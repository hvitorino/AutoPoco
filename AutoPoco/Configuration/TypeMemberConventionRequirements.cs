using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace AutoPoco.Configuration
{
    public class TypeMemberConventionRequirements : ITypeMemberConventionRequirements
    {
        private Func<string, bool> mNameRule;
        private Func<Type, bool> mTypeRule;

        public void Name(System.Linq.Expressions.Expression<Func<string, bool>> rule)
        {
            mNameRule = rule.Compile();
        }

        public void Type(System.Linq.Expressions.Expression<Func<Type, bool>> rule)
        {
            mTypeRule = rule.Compile();
        }

        public bool IsValidType(Type type)
        {
            if (mTypeRule == null) { return true; }
            return mTypeRule.Invoke(type);
        }

        public bool IsValidName(String name)
        {
            if (mNameRule == null) { return true; }
            return mNameRule.Invoke(name);
        }

        internal bool HasNameRule()
        {
            return mNameRule != null;
        }

        internal bool HasTypeRule()
        {
            return mTypeRule != null;
        }
    }
}
