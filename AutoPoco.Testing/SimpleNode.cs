using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPoco.Testing
{
    public class SimpleNode
    {
        public SimpleNode Parent { get; set; }
        public List<SimpleNode> Children { get; set; }
    }
}
