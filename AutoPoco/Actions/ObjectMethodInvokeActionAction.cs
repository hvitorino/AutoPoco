using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Engine;
using AutoPoco.Configuration;

namespace AutoPoco.Actions
{
    public class ObjectMethodInvokeActionAction<T> : IObjectAction
    {
        private Action<T> mAction;

        public ObjectMethodInvokeActionAction(Action<T> action)
        {
            mAction = action;
        }

        public void Enact(IGenerationContext context, object target)
        {
            mAction.Invoke((T)target);
        }
    }
}
