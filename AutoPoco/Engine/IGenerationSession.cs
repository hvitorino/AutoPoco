using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPoco.Engine
{
    public interface IGenerationSession
    {
        /// <summary>
        /// Sets up the generation of a single item of the specified type
        /// </summary>
        /// <typeparam name="TPoco"></typeparam>
        /// <returns></returns>
        IObjectGenerator<TPoco> Single<TPoco>();

        /// <summary>
        /// Sets up the generation of a list of items of the specified type
        /// </summary>
        /// <param name="count">The number of items to be generated</param>
        /// <returns></returns>
        ICollectionContext<TPoco, IList<TPoco>> List<TPoco>(int count);

        /// <summary>
        /// Generates the next poco and returns it
        /// </summary>
        /// <typeparam name="TPoco"></typeparam>
        /// <returns></returns>
        TPoco Next<TPoco>();
       
        /// <summary>
        /// Generates the next poco and returns it, setting it up if necessary
        /// </summary>
        /// <typeparam name="TPoco"></typeparam>
        /// <returns></returns>
        TPoco Next<TPoco>(Action<IObjectGenerator<TPoco>> cfg);

        /// <summary>
        /// Generates the next N pocos and returns it, setting it up if necessary
        /// </summary>
        /// <typeparam name="TPoco"></typeparam>
        /// <returns></returns>
        IEnumerable<TPoco> Collection<TPoco>(int count);

        /// <summary>
        /// Generates the next N pocos and returns it, setting it up if necessary
        /// </summary>
        /// <typeparam name="TPoco"></typeparam>
        /// <returns></returns>
        IEnumerable<TPoco> Collection<TPoco>(int count,  Action<ICollectionContext<TPoco, IList<TPoco>>> cfg);
    }
}
