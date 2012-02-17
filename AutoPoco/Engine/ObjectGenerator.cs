using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Configuration;
using AutoPoco.Util;
using AutoPoco.Actions;
using System.Linq.Expressions;

namespace AutoPoco.Engine
{
    public class ObjectGenerator<T> : IObjectGenerator<T>
    {
        private IObjectBuilder mType;
        private List<IObjectAction> mOverrides = new List<IObjectAction>();
        private IGenerationContext mContext;

        public ObjectGenerator(IGenerationContext session, IObjectBuilder type)
        {
            mContext = session;
            mType = type;
        }

        public T Get()
        {
            // Create the object     
            var createdObject = mType.CreateObject(mContext);
            
            // And overrides
             var typeContext = new GenerationContext(mContext.Builders, new TypeGenerationContextNode(mContext.Node, createdObject));
            foreach (var action in mOverrides)
            {
                action.Enact(typeContext, createdObject);
            }

            // And return the created object
            return (T)createdObject;
        }

        public void AddAction(IObjectAction action)
        {
            mOverrides.Add(action);
        }

        public IObjectGenerator<T> Impose<TMember>(System.Linq.Expressions.Expression<Func<T, TMember>> propertyExpr, TMember value)
        {
            var member = ReflectionHelper.GetMember(propertyExpr);
            if (member.IsField)
            {
                this.AddAction(new ObjectFieldSetFromValueAction((EngineTypeFieldMember)member, value));
            }
            else if (member.IsProperty)
            {
                this.AddAction(new ObjectPropertySetFromValueAction((EngineTypePropertyMember)member, value));
            }
                        
            return this;
        }
        
        public IObjectGenerator<T> Source<TMember>(Expression<Func<T, TMember>> propertyExpr, IDatasource dataSource)
        {
            var member = ReflectionHelper.GetMember(propertyExpr);
            if (member.IsField)
            {
                this.AddAction(new ObjectFieldSetFromSourceAction((EngineTypeFieldMember)member, dataSource));
            }
            else if (member.IsProperty)
            {
                this.AddAction(new ObjectPropertySetFromSourceAction((EngineTypePropertyMember)member, dataSource));
            }

            return this;
        }

        public IObjectGenerator<T> Invoke(System.Linq.Expressions.Expression<Action<T>> methodExpr)
        {
            ObjectMethodInvokeActionAction<T> invoker = new ObjectMethodInvokeActionAction<T>(methodExpr.Compile());
            mOverrides.Add(invoker);
            return this;
        }

        public IObjectGenerator<T> Invoke<TMember>(System.Linq.Expressions.Expression<Func<T, TMember>> methodExpr)
        {
            ObjectMethodInvokeFuncAction<T, TMember> invoker = new ObjectMethodInvokeFuncAction<T, TMember>(methodExpr.Compile());
            mOverrides.Add(invoker);
            return this;
        }

    }
}
