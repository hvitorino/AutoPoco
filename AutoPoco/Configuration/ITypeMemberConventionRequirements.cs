using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace AutoPoco.Configuration
{
    public interface ITypeMemberConventionRequirements
    {
        /// <summary>
        /// Defines a rule for this convention's application that depends on the name of this member
        /// </summary>
        /// <param name="rule"></param>
        void Name(Expression<Func<String, bool>> rule);

        /// <summary>
        ///  Defines a rule for this convention's application that depends on the type of this member
        /// </summary>
        /// <param name="rule"></param>
        void Type(Expression<Func<Type, bool>> rule);
    }
}
