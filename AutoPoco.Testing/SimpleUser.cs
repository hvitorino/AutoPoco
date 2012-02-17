using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPoco.Testing
{
    public class SimpleUser
    {
        public string FirstName
        {
            get;
            set;
        }

        public string LastName
        {
            get;
            set;
        }

        public string EmailAddress
        {
            get;
            set;
        }

        public SimpleUserRole Role
        {
            get;
            set;
        }

        public IList<SimpleUserProperty> Properties
        {
            get;
            protected set;
        }
    }
}
