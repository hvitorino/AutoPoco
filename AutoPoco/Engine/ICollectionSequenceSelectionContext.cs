using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace AutoPoco.Engine
{
    public interface ICollectionSequenceSelectionContext<TPoco, TCollection> where TCollection : ICollection<TPoco>             
    {
        /// <summary>
        /// Gets the number of items yet remaining in the sequence for processing
        /// </summary>
        int Remaining
        {
            get;
        }

        /// <summary>
        /// Imposes a property value on all the items in the current selection
        /// </summary>
        ICollectionSequenceSelectionContext<TPoco, TCollection> Impose<TMember>(Expression<Func<TPoco, TMember>> propertyExpr, TMember value);

        /// <summary>
        /// Invokes a method on all the items in the current selection
        /// </summary>
        /// <param name="methodExpr"></param>
        /// <returns></returns>
        ICollectionSequenceSelectionContext<TPoco, TCollection> Invoke(Expression<Action<TPoco>> methodExpr);

        /// <summary>
        /// Invokes a method on all the items in the current selection
        /// </summary>
        /// <param name="methodExpr"></param>
        /// <returns></returns>
        ICollectionSequenceSelectionContext<TPoco, TCollection> Invoke<TMember>(Expression<Func<TPoco, TMember>> methodExpr);

        /// <summary>
        /// Gets the next group of items from this sequence
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        ICollectionSequenceSelectionContext<TPoco, TCollection> Next(int count);

        /// <summary>
        /// Returns to the parent collection of item builders
        /// </summary>
        /// <returns></returns>
        ICollectionContext<TPoco, TCollection> All();
    }
}
