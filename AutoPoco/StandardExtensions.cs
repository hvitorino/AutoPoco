using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Configuration;
using AutoPoco.DataSources;
using System.Collections;
using AutoPoco.Engine;

namespace AutoPoco
{
    public static class StandardExtensions
    {
        /// <summary>
        /// Sets the value of a member directly
        /// </summary>
        public static IEngineConfigurationTypeBuilder<TPoco> Value<TPoco, TMember>(this IEngineConfigurationTypeMemberBuilder<TPoco, TMember> memberConfig,TMember value)
        {
            return memberConfig.Use<ValueSource<TMember>>(value);
        }

        public static IEngineConfigurationTypeBuilder<TPoco> FromParent<TPoco, TMember>(this IEngineConfigurationTypeMemberBuilder<TPoco, TMember> memberConfig)
        {
            return memberConfig.Use<ParentSource<TMember>>();
        }

        public static IEngineConfigurationTypeBuilder<TPoco> Collection<TPoco, TCollection>(
            this IEngineConfigurationTypeMemberBuilder<TPoco, TCollection> memberConfig, int min, int max) where TCollection : IEnumerable
        {
            var collectionType = typeof (TCollection);
            Type collectionContentType = null;

            // We need to find the base collection type (we only support generic collections of X for the moment)
            collectionContentType = GetCollectionContentType(collectionType);

            if(collectionContentType == null)
            {
                throw new ArgumentException("Unable to find collection type, only collections of type IEnumerable<T> are supported", "TCollection");
            }

            // So this will give us AutoSource<Y>
            var autoSourceType = typeof(AutoSource<>).MakeGenericType(collectionContentType);
            
            //// And this will give us FlexibleEnumerableSource<AutoSource<Y>, X<Y>, Y>
            var enumerableSourceType = typeof (FlexibleEnumerableSource<,,>).MakeGenericType(
                autoSourceType, collectionType, collectionContentType
                );

            // I can't believe I'm doing this, if anybody can give us a good method definition that just_works and 
            // negates the need for this tomfoolery I'm all ears
            var method = memberConfig.GetType().GetMethod("Use",
                                                          System.Reflection.BindingFlags.Public |
                                                          System.Reflection.BindingFlags.Instance, null, new[] { typeof(Object[]) }, null);
            
           var genericMethod = method.MakeGenericMethod(new[] {enumerableSourceType});
           return (IEngineConfigurationTypeBuilder<TPoco>)genericMethod.Invoke(memberConfig, new object[] { new object[] { min, max}});
        }

        private static Type GetCollectionContentType(Type collectionType)
        {
            Type collectionContentType = null;
            while(collectionType != null)
            {
                if( collectionType.GetGenericArguments().Count() == 1)
                {
                    // Nefarious, I know - shocking, this will get is Y from X<Y> where X = IEnumerable/List/Etc and Y = Type
                    // So List<Y> etc
                    collectionContentType = collectionType.GetGenericArguments()[0];
                }
                collectionType = collectionType.BaseType;
            }
            return collectionContentType;
        }
    }
}
