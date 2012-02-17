using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPoco.Configuration.Providers
{
    public interface IEngineConfigurationTypeMemberProvider
    {
        EngineTypeMember GetConfigurationMember();

        IEnumerable<IEngineConfigurationDatasource> GetDatasources();
    }
}
