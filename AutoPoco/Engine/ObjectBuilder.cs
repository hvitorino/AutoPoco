using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Configuration;
using AutoPoco.Actions;

namespace AutoPoco.Engine
{
    public class ObjectBuilder : IObjectBuilder
    {
        private List<IObjectAction> mActions = new List<IObjectAction>();
        private IDatasource mFactory = null;

        public Type InnerType
        {
            get;
            private set;
        }

        public IEnumerable<IObjectAction> Actions
        {
            get { return mActions; }
        }

        public void ClearActions()
        {
            mActions.Clear();
        }

        public void AddAction(IObjectAction action)
        {
            mActions.Add(action);
        }

        public void RemoveAction(IObjectAction action)
        {
            mActions.Remove(action);
        }

        /// <summary>
        /// Creates this object builder
        /// </summary>
        /// <param name="type"></param>
        public ObjectBuilder(IEngineConfigurationType type)
        {
            this.InnerType = type.RegisteredType;

            if(type.GetFactory() != null)
            {
                mFactory = type.GetFactory().Build();
            }

            type.GetRegisteredMembers()
           .ToList()
           .ForEach(x =>
           {
               var sources = x.GetDatasources().Select(s => s.Build()).ToList();

               if (x.Member.IsField)
               {
                   if (sources.Count == 0) { return; }

                   this.AddAction(new ObjectFieldSetFromSourceAction(
                      (EngineTypeFieldMember)x.Member,
                      sources.First()));
               }
               else if (x.Member.IsProperty)
               {
                   if (sources.Count == 0) { return; }

                   this.AddAction(new ObjectPropertySetFromSourceAction(
                      (EngineTypePropertyMember)x.Member,
                      sources.First()));
               }
               else if (x.Member.IsMethod)
               {
                   this.AddAction(new ObjectMethodInvokeFromSourceAction(
                      (EngineTypeMethodMember)x.Member,
                      sources
                      ));
               }
           });
        }

        public Object CreateObject(IGenerationContext context)
        {
            Object createdObject = null;
            
            if(mFactory != null)
            {
                createdObject = mFactory.Next(context);
                
            } else
            {
                createdObject = Activator.CreateInstance(this.InnerType);
            }

            // Don't set it up if we've reached recursion limit
            if (context.Depth < context.Builders.RecursionLimit)
            {
                EnactActionsOnObject(context, createdObject);
            }
            return createdObject;
        }

        private void EnactActionsOnObject(IGenerationContext context, object createdObject)
        {
            var typeContext = new GenerationContext(context.Builders, new TypeGenerationContextNode(context.Node, createdObject));
            foreach (var action in this.mActions)
            {
                action.Enact(typeContext, createdObject);
            }
        }
    }
}
