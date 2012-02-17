using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPoco.Engine
{
    public abstract class DatasourceBase<T> : IDatasource<T>
    {
        /// <summary>
        /// Gets the next object from this data source
        /// </summary>
        /// <returns></returns>
        public abstract T Next(IGenerationContext context);

        object IDatasource.Next(IGenerationContext context)
        {
            return Next(context);
        }
    }
}
