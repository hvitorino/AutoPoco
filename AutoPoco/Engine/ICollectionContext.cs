using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace AutoPoco.Engine
{
    public interface ICollectionContext<TPoco, TCollection> where TCollection : ICollection<TPoco>
    {
        /// <summary>
        /// Imposes a property value on all the items in the current selection
        /// </summary>
        ICollectionContext<TPoco, TCollection> Impose<TMember>(Expression<Func<TPoco, TMember>> propertyExpr, TMember value);

        /// <summary>
        /// Overrides the data source for this particular generation scope
        /// </summary>
        /// <returns></returns>
        ICollectionContext<TPoco, TCollection> Source<TMember>(Expression<Func<TPoco, TMember>> propertyExpr, IDatasource dataSource);

        /// <summary>
        /// Invokes a method on all the items in the current selection
        /// </summary>
        /// <param name="methodExpr"></param>
        /// <returns></returns>
        ICollectionContext<TPoco, TCollection> Invoke(Expression<Action<TPoco>> methodExpr);

        /// <summary>
        /// Invokes a method on all the items in the current selection
        /// </summary>
        /// <param name="methodExpr"></param>
        /// <returns></returns>
        ICollectionContext<TPoco, TCollection> Invoke<TMember>(Expression<Func<TPoco, TMember>> methodExpr);

        /// <summary>
        /// Gets the first items in this collection for modification
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        ICollectionSequenceSelectionContext<TPoco, TCollection> First(int count);

        /// <summary>
        /// Gets a random selection from this collection for modification
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        ICollectionSequenceSelectionContext<TPoco, TCollection> Random(int count);

        /// <summary>
        /// Gets the current generated collection of items
        /// </summary>
        /// <returns></returns>
        TCollection Get();
    }
}
