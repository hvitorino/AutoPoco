using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace AutoPoco.Engine
{
    public class CollectionContext<TPoco, TCollection>
        : ICollectionContext<TPoco, TCollection> where TCollection : ICollection<TPoco>
    {
        private IEnumerable<IObjectGenerator<TPoco>> mGenerators;
        private Random mRandom = new Random(1337);

        public CollectionContext(IEnumerable<IObjectGenerator<TPoco>> generators)
        {
            mGenerators = generators;
        }

        public ICollectionContext<TPoco, TCollection> Impose<TMember>(Expression<Func<TPoco, TMember>> propertyExpr, TMember value)
        {
            foreach (var item in mGenerators)
            {
                item.Impose(propertyExpr, value);
            }
            return this;
        }


        public ICollectionContext<TPoco, TCollection> Source<TMember>(Expression<Func<TPoco, TMember>> propertyExpr, IDatasource dataSource)
        {
            foreach (var item in mGenerators)
            {
                item.Source(propertyExpr, dataSource);
            }
            return this;
        }

        public ICollectionSequenceSelectionContext<TPoco, TCollection> First(int count)
        {
            return new CollectionSequenceSelectionContext<TPoco, TCollection>(
                this,
                mGenerators,
                count);
        }

        public ICollectionSequenceSelectionContext<TPoco, TCollection> Random(int count)
        {
            // Randomise and return
            return new CollectionSequenceSelectionContext<TPoco, TCollection>(
                this,
                 mGenerators.OrderBy(r => mRandom.Next()).ToArray(),
                count);           
        }

        public TCollection Get()
        {
            // Create an array if it's an array
            if (typeof(TPoco[]).IsAssignableFrom(typeof(TCollection)))
            {
                return (TCollection)(Object)mGenerators.Select(x => x.Get()).ToArray();
            }
            // Return a list if it's a list
            if (typeof(IList<>).MakeGenericType(typeof(TPoco)).IsAssignableFrom(typeof(TCollection)))
            {
                return (TCollection)(Object)mGenerators.Select(x => x.Get()).ToList();
            }         
            throw new InvalidOperationException();
        }

        public ICollectionContext<TPoco, TCollection> Invoke(Expression<Action<TPoco>> methodExpr)
        {
            foreach (var item in mGenerators)
            {
                item.Invoke(methodExpr);
            }
            return this;   
        }

        public ICollectionContext<TPoco, TCollection> Invoke<TMember>(Expression<Func<TPoco, TMember>> methodExpr)
        {
            foreach (var item in mGenerators)
            {
                item.Invoke(methodExpr);
            }
            return this;   
        }
    }
}
