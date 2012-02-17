using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutoPoco.Engine
{
    public interface IGenerationSessionFactory
    {
        /// <summary>
        /// Creates a session from this configured factory
        /// </summary>
        /// <returns></returns>
        IGenerationSession CreateSession();

        /// <summary>
        /// Creates a session, overriding the default recursion limit
        /// Note: This method signature will probably change at some point
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        IGenerationSession CreateSession(int recursionLimit);
    }
}
