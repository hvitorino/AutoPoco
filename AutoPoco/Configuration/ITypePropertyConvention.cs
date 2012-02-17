using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPoco.Configuration
{
    public interface ITypePropertyConvention : IConvention
    {
        /// <summary>
        /// Specify the conditions under which this convention will be invoked
        /// </summary>
        /// <param name="requirements"></param>
        void SpecifyRequirements(ITypeMemberConventionRequirements requirements);

        /// <summary>
        /// Apply the convention to the registered property
        /// </summary>
        void Apply(ITypePropertyConventionContext context);
    }
}
