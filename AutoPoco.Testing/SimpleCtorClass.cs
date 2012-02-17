using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPoco.Testing
{
    public class SimpleCtorClass
    {
        public string ReadOnlyProperty { get; private set; }
        public string SecondaryProperty { get; private set; }
         
        public SimpleCtorClass(string arg)
        {
            this.ReadOnlyProperty = arg;
        }

        public SimpleCtorClass(string argOne, string argTwo)
        {
            this.ReadOnlyProperty = argOne;
            this.SecondaryProperty = argTwo;
        }
    }
}
