using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPoco.Testing
{
    public interface IGenericInterface<T>
    {
        T GenericProperty
        {
            get;
            set;
        }
    }
}
