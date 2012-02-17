using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace AutoPoco.Engine
{
    public class CollectionSequenceSelectionContext<TPoco, TCollection> 
        : ICollectionSequenceSelectionContext<TPoco, TCollection> where TCollection : ICollection<TPoco>
    {

        IEnumerable<IObjectGenerator<TPoco>> mAllGenerators;
        IEnumerable<IObjectGenerator<TPoco>> mSelected;
        int mCurrentSkip;
        int mCurrentCount;
        ICollectionContext<TPoco, TCollection> mParentContext;

        public int Remaining
        {
            get { return mAllGenerators.Count() - (mCurrentSkip + mCurrentCount); }
        }

        public CollectionSequenceSelectionContext(
            ICollectionContext<TPoco, TCollection> parentContext,
            IEnumerable<IObjectGenerator<TPoco>> generators,
            int initialPull) 
        {
            mAllGenerators = generators;
            mCurrentCount = 0;
            mCurrentSkip = 0;
            mParentContext = parentContext;
            Next(initialPull);
        }

        public ICollectionSequenceSelectionContext<TPoco, TCollection> Impose<TMember>(Expression<Func<TPoco, TMember>> propertyExpr, TMember value)
        {
            foreach (var item in mSelected)
            {
                item.Impose(propertyExpr, value);
            }
            return this;             
        }

        public ICollectionSequenceSelectionContext<TPoco, TCollection> Invoke(Expression<Action<TPoco>> methodExpr)
        {
            foreach (var item in mSelected)
            {
                item.Invoke(methodExpr);
            }
            return this;    
        }

        public ICollectionSequenceSelectionContext<TPoco, TCollection> Invoke<TMember>(Expression<Func<TPoco, TMember>> methodExpr)
        {
            foreach (var item in mSelected)
            {
                item.Invoke(methodExpr);
            }
            return this;    
        }

        public ICollectionSequenceSelectionContext<TPoco, TCollection> Next(int count)
        {
            // Skip ahead + return this
            mCurrentSkip += mCurrentCount;
            mCurrentCount = count;
            mSelected = mAllGenerators
                .Skip(mCurrentSkip)
                .Take(mCurrentCount);

            return this;
        }

        public ICollectionContext<TPoco, TCollection> All()
        {
            return mParentContext;
        }
    }
}
