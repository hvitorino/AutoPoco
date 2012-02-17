using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AutoPoco.Configuration;
using System.Reflection;

namespace AutoPoco.Tests.Unit.Configuration
{
    [TestFixture]
    public class EngineTypeMethodTests
    {
        [Test]
        public void Same_Method_Requested_From_Base_And_Derived_Types_Is_Considered_Equal()
        {
            EngineTypeMethodMember baseMethod = new EngineTypeMethodMember(typeof(BaseClass).GetMethod("SealedMethod", System.Type.EmptyTypes));
            EngineTypeMethodMember derivedMethod = new EngineTypeMethodMember(typeof(DerivedClass).GetMethod("SealedMethod", System.Type.EmptyTypes));

            Assert.True(baseMethod == derivedMethod);
        }

        [Test]
        public void Same_Method_Requested_From_Interface_And_Implementing_Type_Is_Considered_Equal()
        {
            EngineTypeMethodMember interfaceMethod = new EngineTypeMethodMember(typeof(IFoo).GetMethod("InterfaceMethod", System.Type.EmptyTypes));
            EngineTypeMethodMember implementedMethod = new EngineTypeMethodMember(typeof(BaseClass).GetMethod("InterfaceMethod", System.Type.EmptyTypes));

            Assert.True(interfaceMethod == implementedMethod);
        }

        
        [Test]
        public void Same_Method_Requested_From_Interface_And_DerivedType_From_Implementing_Type_Is_Considered_Equal()
        {
            EngineTypeMethodMember interfaceMethod = new EngineTypeMethodMember(typeof(IFoo).GetMethod("InterfaceMethod", System.Type.EmptyTypes));
            EngineTypeMethodMember deriveddMethod = new EngineTypeMethodMember(typeof(DerivedClass).GetMethod("InterfaceMethod", System.Type.EmptyTypes));

            Assert.True(interfaceMethod == deriveddMethod);
        }

        [Test]
        public void Overridden_Method_Requested_From_Base_And_Derived_Type_Is_ConsideredEqual()
        {
            EngineTypeMethodMember baseMethod = new EngineTypeMethodMember(typeof(BaseClass).GetMethod("VirtualMethod", System.Type.EmptyTypes));
            EngineTypeMethodMember overriddenMethod = new EngineTypeMethodMember(typeof(DerivedClass).GetMethod("VirtualMethod", System.Type.EmptyTypes));

            Assert.True(baseMethod == overriddenMethod);
        }

        [Test]
        [Ignore("Not important yet")]
        public void New_Method_Requested_From_Derived_Type_Is_Not_Considered_To_Hidden_Method()
        {
            var flags = BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Instance |
                           BindingFlags.Public | BindingFlags.Static;

            EngineTypeMethodMember baseMethod = new EngineTypeMethodMember(typeof(BaseClass).GetMethod("AnotherSealedMethod", flags, null, System.Type.EmptyTypes, null));
            EngineTypeMethodMember overriddenMethod = new EngineTypeMethodMember(typeof(DerivedClass)
                .GetMethod("AnotherSealedMethod", flags, null, System.Type.EmptyTypes, null));

            Assert.False(baseMethod == overriddenMethod);
        }


        [Test]
        public void Different_Method_Are_Not_Considered_Equal()
        {
            EngineTypeMethodMember MethodOne = new EngineTypeMethodMember(typeof(BaseClass).GetMethod("SealedMethod", Type.EmptyTypes));
            EngineTypeMethodMember MethodTwo = new EngineTypeMethodMember(typeof(BaseClass).GetMethod("SealedMethod", new[] { typeof(int)}));

            Assert.False(MethodOne == MethodTwo);
        }

        public interface IFoo
        {
            void InterfaceMethod();
        }

        public class BaseClass : IFoo

        {
            public void SealedMethod() { }
            public void AnotherSealedMethod() { }
            public virtual void VirtualMethod() { }
            public void InterfaceMethod() { }

            public void SealedMethod(int param) { }            
        }

        public class DerivedClass : BaseClass
        {
            public override void VirtualMethod() { }
            public new void AnotherSealedMethod() { }
        }
    }
}
