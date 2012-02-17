using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPoco.Engine
{
    public interface IObjectBuilder
    {
        /// <summary>
        /// Gets the type of object this is building
        /// </summary>
        Type InnerType
        {
            get;
        }

        /// <summary>
        /// Gets the actions currently applied to this builder
        /// </summary>
        IEnumerable<IObjectAction> Actions
        {
            get;
        }

        /// <summary>
        /// Clears all the actions currently on this builder
        /// </summary>
        void ClearActions();

        /// <summary>
        /// Adds an action to this builder
        /// </summary>
        /// <param name="action"></param>
        void AddAction(IObjectAction action);

        /// <summary>
        /// Removes an action from this builder
        /// </summary>
        /// <param name="action"></param>
        void RemoveAction(IObjectAction action);

        /// <summary>
        /// Creates the object and applies any objects
        /// </summary>
        /// <returns></returns>
        Object CreateObject(IGenerationContext context);
    }
}
