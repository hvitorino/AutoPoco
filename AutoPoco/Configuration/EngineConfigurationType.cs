using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPoco.Configuration
{
    public class EngineConfigurationType : IEngineConfigurationType
    {
        private List<EngineConfigurationTypeMember> mRegisteredMembers = new List<EngineConfigurationTypeMember>();
        private Type mRegisteredType;
        private IEngineConfigurationDatasource mFactory;
        
        public Type RegisteredType
        {
            get
            {
                return mRegisteredType;
            }
        }

        public EngineConfigurationType(Type t)
        {
            mRegisteredType = t;
        }

        public void RegisterMember(EngineTypeMember member)
        {
            if (mRegisteredMembers.Find(x => x.Member == member) != null)
            {
                throw new ArgumentException("Member has already been registered", "member");
            }
            mRegisteredMembers.Add(new EngineConfigurationTypeMember(member));
        }

        public IEngineConfigurationTypeMember GetRegisteredMember(EngineTypeMember member)
        {
            return mRegisteredMembers.Find(x => x.Member == member);
        }

        public IEnumerable<IEngineConfigurationTypeMember> GetRegisteredMembers()
        {
            return mRegisteredMembers.ConvertAll(x => (IEngineConfigurationTypeMember)x);
        }

        public void SetFactory(IEngineConfigurationDatasource factory)
        {
            this.mFactory = factory;
        }

        public IEngineConfigurationDatasource GetFactory()
        {
            return mFactory;
        }
    }
}
