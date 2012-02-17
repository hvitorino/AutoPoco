using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPoco.Testing
{
    public class SimpleDerivedClass : SimpleBaseClass, ISimpleInterface
    {
        public string Name
        {
            get;
            set;
        }

        public override string BaseVirtualProperty
        {
            get;
            set;
        }
    }
}
