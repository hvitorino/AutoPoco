using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Engine;

namespace AutoPoco.Configuration
{
    public interface IEngineConfigurationDatasource
    {
        /// <summary>
        /// Builds the data source this configuration item represents
        /// </summary>
        /// <returns></returns>
        IDatasource Build();
    }
}
