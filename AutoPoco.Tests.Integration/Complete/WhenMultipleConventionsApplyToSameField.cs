using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using AutoPoco.Configuration;
using AutoPoco.Conventions;

namespace AutoPoco.Tests.Integration.Complete
{
    [TestFixture]
    public class WhenMultipleConventionsApplyToSameField
    {
        [Test]
        public void TypeOverridesNone()
        {
            var session = AutoPocoContainer.Configure(x =>
            {
                x.Conventions(c =>
                {
                    c.Register<DefaultTypeConvention>();
                    c.Register<SetFieldsOfStringTo1>();
                    c.Register<SetFieldsTo0>();
                });
                x.Include<TestMultipleFieldsConventionClass>();
            })
            .CreateSession();

            TestMultipleFieldsConventionClass testObj
                    = session.Single<TestMultipleFieldsConventionClass>().Get();

            Assert.True(
                    testObj.FirstIntegerField == 0 &&
                    testObj.FirstStringField == "1" &&
                    testObj.SecondIntegerField == 0 &&
                    testObj.SecondStringField == "1", "Conventions were not applied in expected order"
                );

        }

        [Test]
        public void NameOverridesType()
        {
            var session = AutoPocoContainer.Configure(x =>
            {
                x.Conventions(c =>
                {
                    c.Register<DefaultTypeConvention>();
                    c.Register<SetFieldsContainingFirstTo2>();
                    c.Register<SetFieldsOfStringTo1>();
                    c.Register<SetFieldsTo0>();
                });
                x.Include<TestMultipleFieldsConventionClass>();
            })
            .CreateSession();

            TestMultipleFieldsConventionClass testObj
                    = session.Single<TestMultipleFieldsConventionClass>().Get();

            Assert.True(
                    testObj.FirstIntegerField == 2 &&
                    testObj.FirstStringField == "2" &&
                    testObj.SecondIntegerField == 0 &&
                    testObj.SecondStringField == "1", "Conventions were not applied in expected order"
                );
        }

        [Test]
        public void NameAndTypeOverrideName()
        {
            var session = AutoPocoContainer.Configure(x =>
            {
                x.Conventions(c =>
                {
                    c.Register<DefaultTypeConvention>();
                    c.Register<SetFieldsOfStringCalledFirstTo3>();
                    c.Register<SetFieldsContainingFirstTo2>();
                    c.Register<SetFieldsOfStringTo1>();
                    c.Register<SetFieldsTo0>();
                });
                x.Include<TestMultipleFieldsConventionClass>();
            })
            .CreateSession();

            TestMultipleFieldsConventionClass testObj
                    = session.Single<TestMultipleFieldsConventionClass>().Get();

            Assert.True(
                    testObj.FirstIntegerField == 2 &&
                    testObj.FirstStringField == "3" &&
                    testObj.SecondIntegerField == 0 &&
                    testObj.SecondStringField == "1", "Conventions were not applied in expected order"
                );
        }

        public class SetFieldsTo0 : ITypeFieldConvention
        {
            public void SpecifyRequirements(ITypeMemberConventionRequirements requirements)
            {

            }

            public void Apply(ITypeFieldConventionContext context)
            {
                if (context.Member.FieldInfo.FieldType == typeof(string))
                {
                    context.SetValue("0");
                }
                else if (context.Member.FieldInfo.FieldType == typeof(int?))
                {
                    context.SetValue(0);
                }
            }
        }


        public class SetFieldsOfStringTo1 : ITypeFieldConvention
        {
            public void SpecifyRequirements(ITypeMemberConventionRequirements requirements)
            {
                requirements.Type(x => x == typeof(string));
            }

            public void Apply(ITypeFieldConventionContext context)
            {
                context.SetValue("1");
            }
        }

        public class SetFieldsContainingFirstTo2 : ITypeFieldConvention
        {
            public void SpecifyRequirements(ITypeMemberConventionRequirements requirements)
            {
                requirements.Name(x => x.StartsWith("First"));
            }

            public void Apply(ITypeFieldConventionContext context)
            {
                if (context.Member.FieldInfo.FieldType == typeof(string))
                {
                    context.SetValue("2");
                }
                else if (context.Member.FieldInfo.FieldType == typeof(int?))
                {
                    context.SetValue(2);
                }
            }
        }


        public class SetFieldsOfStringCalledFirstTo3 : ITypeFieldConvention
        {
            public void SpecifyRequirements(ITypeMemberConventionRequirements requirements)
            {
                requirements.Name(x => x.StartsWith("First"));
                requirements.Type(x => x == typeof(string));
            }

            public void Apply(ITypeFieldConventionContext context)
            {
                context.SetValue("3");
            }
        }

        public class TestMultipleFieldsConventionClass
        {
            public int? FirstIntegerField;
            public int? SecondIntegerField;
            public String FirstStringField;
            public String SecondStringField;
        }
    }
}
