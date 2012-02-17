using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Util;
using System.Linq.Expressions;

namespace AutoPoco.Configuration
{
    public class EngineConfigurationTypeBuilder<TPoco> : EngineConfigurationTypeBuilder, IEngineConfigurationTypeBuilder<TPoco>
    {
        public IEngineConfigurationTypeMemberBuilder<TPoco, TMember> Setup<TMember>(Expression<Func<TPoco, TMember>> expression)
        {
            // Get the member this set up is for
            EngineTypeMember member = ReflectionHelper.GetMember(expression);

            // Create the configuration builder
            var configuration = new EngineConfigurationTypeMemberBuilder<TPoco, TMember>(member, this);

            // Store it in the local list
            this.RegisterTypeMemberProvider(configuration);

            // And return it
            return (IEngineConfigurationTypeMemberBuilder<TPoco, TMember>)configuration;
        }

        public EngineConfigurationTypeBuilder() : base(typeof(TPoco)) { }

        public IEngineConfigurationTypeBuilder<TPoco> ConstructWith<TSource>() where TSource : Engine.IDatasource<TPoco>
        {
            base.ConstructWith(typeof (TSource));
            return this;
        }

        public  IEngineConfigurationTypeBuilder<TPoco> ConstructWith<TSource>(params object[] args) where TSource : Engine.IDatasource<TPoco>
        {
            base.ConstructWith(typeof (TSource), args);
            return this;
        }

        public IEngineConfigurationTypeBuilder<TPoco> Ctor(Expression<Func<TPoco>> creationExpr)
        {
            var ctor = creationExpr.Body as NewExpression;
           // this.fa
            return this;
        }

        public IEngineConfigurationTypeBuilder<TPoco> Invoke(Expression<Action<TPoco>> action)
        {
            MethodInvocationContext context = GetMethodArgs(action);
            String name = ReflectionHelper.GetMethodName(action);
            this.SetupMethod(name, context);
            return this;
        }

        public IEngineConfigurationTypeBuilder<TPoco> Invoke<TReturn>(Expression<Func<TPoco, TReturn>> func)
        {
            MethodInvocationContext context = GetMethodArgs(func);
            String name = ReflectionHelper.GetMethodName(func);
            this.SetupMethod(name, context);
            return this;
        }


        private MethodInvocationContext GetMethodArgs(Expression<Action<TPoco>> action)
        {
            MethodCallExpression methodExpression = action.Body as MethodCallExpression;
            if (methodExpression == null) { throw new ArgumentException("Method expression expected, and not passed in", "action"); }
            return GetMethodArgs(methodExpression);
        }

        private MethodInvocationContext GetMethodArgs<TReturn>(Expression<Func<TPoco, TReturn>> function)
        {
            MethodCallExpression methodExpression = function.Body as MethodCallExpression;
            if (methodExpression == null) { throw new ArgumentException("Method expression expected, and not passed in", "function"); }
            return GetMethodArgs(methodExpression);
        }

        private MethodInvocationContext GetMethodArgs(MethodCallExpression methodExpression)
        {
            MethodInvocationContext context = new MethodInvocationContext();
            foreach (var arg in methodExpression.Arguments)
            {
                switch (arg.NodeType)
                {
                    case ExpressionType.Call:

                        MethodCallExpression paramCall = arg as MethodCallExpression;

                        // Extract the data source type
                        Type sourceType = ExtractDatasourceType(paramCall);
                        Object[] factoryArgs = ExtractDatasourceParameters(paramCall);

                        context.AddArgumentSource(sourceType, factoryArgs);

                        break;
                    case ExpressionType.Constant:

                        // Simply pop the constant into the list
                        ConstantExpression paramConstant = arg as ConstantExpression;
                        context.AddArgumentValue(paramConstant);
                        break;
                    default:
                        throw new ArgumentException("Unsupported argument used in method invocation list", "methodExpression");
                }
            }

            return context;
        }

        private Type ExtractDatasourceType(MethodCallExpression paramCall)
        {
            if (!paramCall.Method.IsGenericMethod)
            {
                throw new ArgumentException("Method expression is not generic and types cannot be resolved", "paramCall");
            }
            Type sourceType = paramCall.Method.GetGenericArguments().Skip(1).FirstOrDefault();

            if (sourceType == null)
            {
                throw new ArgumentException("Method expression uses un-recognised generic method and types cannot be resolved");
            }
            return sourceType;
        }

        private object[] ExtractDatasourceParameters(MethodCallExpression paramCall)
        {
            if (paramCall.Arguments.Count == 0) { return new Object[] { }; }

            List<Object> args = new List<object>();
           
            if(paramCall.Arguments.Count > 1) { throw new ArgumentException("Method expression uses unrecognised method and types cannot be resolved");}
            if (paramCall.Arguments[0].NodeType != ExpressionType.NewArrayInit ) { throw new ArgumentException("Method expression uses unrecognised method and types cannot be resolved"); }

            // Each item in this array is an argument, but wrapped as a unary expression cos that's how it works
            NewArrayExpression expr = paramCall.Arguments[0] as NewArrayExpression;
            foreach(UnaryExpression argumentExpression in expr.Expressions)
            {
                ConstantExpression constantValue = argumentExpression.Operand as ConstantExpression;
                if(constantValue == null) { throw new ArgumentException("Method expression uses unrecognised method and types cannot be resolved"); }

                args.Add(constantValue.Value);               
            }            
      
            return args.ToArray();
        }
    }
}
