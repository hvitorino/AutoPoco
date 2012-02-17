using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPoco.Configuration
{
    public interface ITypeRegistrationAction
    {
        void Apply(IEngineConfigurationType type);
    }
}
