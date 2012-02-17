using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Engine;
using AutoPoco.Testing;
using NUnit.Framework;
using AutoPoco.Configuration;
using AutoPoco.DataSources;
using AutoPoco.Tests.Unit;

namespace AutoPoco.Tests.Integration.Complete
{
    [TestFixture]
    public class WhenSettingUpCtor
    {
        IGenerationSession mSession;
        
        [Test]
        public void With_Source_Source_Is_Used_To_Create_Object()
        {
            mSession = AutoPocoContainer.Configure(x => x.Include<SimpleCtorClass>()
                                                            .ConstructWith<TestFactory>()).CreateSession();

            var result = mSession.Next<SimpleCtorClass>();
            Assert.AreEqual("one", result.ReadOnlyProperty);
            Assert.AreEqual("two", result.SecondaryProperty); 
        }

  
     [Test]
     public void With_No_Config_Least_Greedy_Constructor_With_Default_Conventions_Is_Used_By_Default()
     {
         mSession = AutoPocoContainer.CreateDefaultSession();

         var result = mSession.Next<SimpleCtorClass>();

         Assert.NotNull(result.ReadOnlyProperty);
         Assert.Null(result.SecondaryProperty);
     }

        /*
 [Test]
 public void With_Literal_Literal_Is_Used()
 {
     mSession = AutoPocoContainer.Configure(x => x.Include<SimpleCtorClass>()
                                                     .Ctor(() => new SimpleCtorClass("hello"))).CreateSession();

     var result = mSession.Next<SimpleCtorClass>();
     Assert.AreEqual("hello", result.ReadOnlyProperty); 
 }

 [Test]
 public void With_Ctor_Sources_Sources_Are_Used_To_Populate_Ctor()
 {
     mSession = AutoPocoContainer.Configure(x => x.Include<SimpleCtorClass>()
                                                     .Ctor(() => new SimpleCtorClass(
                                                                     Use.Source<String, FirstNameSource>())))
     .CreateSession();

     var result = mSession.Next<SimpleCtorClass>();
     Assert.True(result.ReadOnlyProperty.Length > 0);
 }
  * 
  * */
    }

    public class TestFactory  : IDatasource<SimpleCtorClass>
    {
        public object Next(IGenerationContext context)
        {
            return new SimpleCtorClass("one", "two");
        }
    }
}
