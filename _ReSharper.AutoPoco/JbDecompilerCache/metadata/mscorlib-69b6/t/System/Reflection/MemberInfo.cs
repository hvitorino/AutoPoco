// Type: System.Reflection.MemberInfo
// Assembly: mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// Assembly location: C:\Windows\Microsoft.NET\Framework\v2.0.50727\mscorlib.dll

using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Reflection
{
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [ComDefaultInterface(typeof (_MemberInfo))]
    [Serializable]
    [PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
    public abstract class MemberInfo : ICustomAttributeProvider, _MemberInfo
    {
        public virtual int MetadataToken { get; }
        public virtual Module Module { get; }

        #region _MemberInfo Members

        Type _MemberInfo.GetType();
        void _MemberInfo.GetTypeInfoCount(out uint pcTInfo);
        void _MemberInfo.GetTypeInfo(uint iTInfo, uint lcid, IntPtr ppTInfo);
        void _MemberInfo.GetIDsOfNames([In] ref Guid riid, IntPtr rgszNames, uint cNames, uint lcid, IntPtr rgDispId);
        void _MemberInfo.Invoke(uint dispIdMember, [In] ref Guid riid, uint lcid, short wFlags, IntPtr pDispParams, IntPtr pVarResult, IntPtr pExcepInfo, IntPtr puArgErr);
        public abstract MemberTypes MemberType { get; }
        public abstract string Name { get; }
        public abstract Type DeclaringType { get; }
        public abstract Type ReflectedType { get; }

        #endregion

        #region ICustomAttributeProvider Members

        public abstract object[] GetCustomAttributes(bool inherit);
        public abstract object[] GetCustomAttributes(Type attributeType, bool inherit);
        public abstract bool IsDefined(Type attributeType, bool inherit);

        #endregion
    }
}
