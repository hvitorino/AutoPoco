using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace AutoPoco.Configuration
{
    public abstract class EngineTypeMember
    {
        public abstract string Name
        {
            get;
        }
       
        public abstract bool IsMethod
        {
            get;
        }

        public abstract bool IsField
        {
            get;
        }

        public abstract bool IsProperty
        {
            get;
        }    

        public static bool operator ==(EngineTypeMember memberOne, EngineTypeMember memberTwo)
        {
            return Equals(memberOne, memberTwo);
        }

        public static bool operator !=(EngineTypeMember memberOne, EngineTypeMember memberTwo)
        {
            return !Equals(memberOne, memberTwo);
        }
    }
}
