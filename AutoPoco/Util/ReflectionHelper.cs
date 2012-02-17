using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Linq.Expressions;
using AutoPoco.Configuration;

namespace AutoPoco.Util
{
    public static class ReflectionHelper
    {
        public static bool LooseCompare(this MemberInfo info, MemberInfo other)
        {
            return info.MetadataToken == other.MetadataToken && info.Module == other.Module;
        }

        public static bool ArgumentsAreEqualTo(this MethodInfo one, MethodInfo two)
        {
            var paramOne = one.GetParameters();
            var paramTwo = two.GetParameters();

            if (paramTwo.Length != paramOne.Length) return false;

            for (int x = 0; x < paramOne.Length; x++)
            {
                if (paramOne[x] != paramTwo[x]) { return false; }
            }

            return true;
        }


        public static EngineTypeMember GetMember<TPoco, TReturn>(Expression<Func<TPoco, TReturn>> expression)
        {
            var member = GetMemberInfo(typeof(TPoco), expression.Body);
            return GetMember(member);
        }

        public static EngineTypeMember GetMember<TPoco>(Expression<Func<TPoco, object>> expression)
        {
            var member = GetMemberInfo(typeof(TPoco), expression.Body);
            return GetMember(member);
        }

        public static PropertyInfo GetProperty<TPoco>(Expression<Func<TPoco, object>> expression)
        {
            var member = GetMemberInfo(typeof(TPoco), expression.Body);
            return member.ReflectedType.GetProperty(member.Name);
        }

        public static FieldInfo GetField<TPoco>(Expression<Func<TPoco, object>> expression)
        {
            var member = GetMemberInfo(typeof(TPoco), expression.Body);
            return member.ReflectedType.GetField(member.Name);
        }

        private static MemberInfo GetMemberInfo(Type declaringType, Expression expression)
        {
            MemberExpression memberExpression = expression as MemberExpression;
            if (memberExpression == null)
            {
                throw new ArgumentException("Expression not supported", "expression");
            }

            return memberExpression.Member;
        }
        
        public static string GetMethodName<TPoco>(Expression<Action<TPoco>> action)
        {
            MethodCallExpression methodExpression = action.Body as MethodCallExpression;
            if (methodExpression == null) { throw new ArgumentException("Method expression expected, and not passed in", "action"); }
            return methodExpression.Method.Name;
        }

        public static string GetMethodName<TPoco, TReturn>(Expression<Func<TPoco, TReturn>> function)
        {
            MethodCallExpression methodExpression = function.Body as MethodCallExpression;
            if (methodExpression == null) { throw new ArgumentException("Method expression expected, and not passed in", "action"); }
            return methodExpression.Method.Name;
        }

        public static EngineTypeMember GetMember(MemberInfo info)
        {
            if (info is PropertyInfo)
            {
                return new EngineTypePropertyMember(info as PropertyInfo);
            }
            if (info is MethodInfo)
            {
                return new EngineTypeMethodMember(info as MethodInfo);
            }
            if (info is FieldInfo)
            {
                return new EngineTypeFieldMember(info as FieldInfo);
            }
            throw new ArgumentException("Unsupported member type", "info");
        }
        
    }
}
