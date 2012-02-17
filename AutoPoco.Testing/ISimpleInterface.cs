using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPoco.Testing
{
    public interface ISimpleInterface
    {
        string InterfaceValue
        {
            get;
            set;
        }

        string OtherInterfaceValue
        {
            get;
            set;
        }

        void DoSomething();
    }
}
