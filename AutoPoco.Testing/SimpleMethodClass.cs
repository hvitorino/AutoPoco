using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPoco.Testing
{
    public class SimpleMethodClass
    {
        public string Value
        {
            get;
            set;
        }

        public string OtherValue
        {
            get;
            set;
        }

        public bool ReturnSomethingCalled
        {
            get;
            set;
        }

        public void SetSomething(String value)
        {
            this.Value = value;
        }

        public void SetSomething(String value, string otherValue)
        {
            this.Value = value;
            this.OtherValue = otherValue;
        }

        public string ReturnSomething()
        {
            ReturnSomethingCalled = true;
            return string.Empty;
        }
    }
}
