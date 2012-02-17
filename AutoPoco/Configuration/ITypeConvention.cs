using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPoco.Configuration
{
    public interface ITypeConvention : IConvention
    {
        /// <summary>
        /// Apply the convention to the registered type
        /// </summary>
        void Apply(ITypeConventionContext context);
    }
}
