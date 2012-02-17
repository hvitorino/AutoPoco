using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Engine;

namespace AutoPoco.DataSources
{
    public class IntegerIdSource : DatasourceBase<int>
    {
        int mCurrentId = 0;

        public override int Next(IGenerationContext context)
        {
            return mCurrentId++;
        }
    }
}
