using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPoco.Testing
{
    public class SimpleBaseClass : ISimpleInterface
    {
        public string BaseProperty
        {
            get;
            set;
        }

        public virtual string BaseVirtualProperty
        {
            get;
            set;
        }
    
        public string InterfaceValue
        {
	      set;
          get;
        }
        
        public string OtherInterfaceValue
        {
            get;
            set;
        }


        public void DoSomething()
        {
            
        }
    }
}
