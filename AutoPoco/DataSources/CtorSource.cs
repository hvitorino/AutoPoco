using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using AutoPoco.Engine;

namespace AutoPoco.DataSources
{
    public class CtorSource<T> : DatasourceBase<T>
    {
        private ConstructorInfo mConstructorInfo;

        public CtorSource(ConstructorInfo ctor)
        {
            mConstructorInfo = ctor;    
        }

        public override T Next(IGenerationContext context)
        {
            // TODO: May actually create a parallel set of interfaces for doing non-generic requests to session
            // This would negate the need for that awful reflection
            var args = mConstructorInfo
                .GetParameters()
                .Select(x =>
                            {
                                var method = context.GetType().GetMethod("Next", Type.EmptyTypes);
                                var target = method.MakeGenericMethod(x.ParameterType);
                                return target.Invoke(context, null);
                            })
                .ToArray();

            return (T)mConstructorInfo.Invoke(args);
        }
    }
}
