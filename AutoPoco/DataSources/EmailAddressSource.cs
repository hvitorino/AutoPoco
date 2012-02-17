using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Engine;

namespace AutoPoco.DataSources
{
    public class EmailAddressSource : DatasourceBase<String>
    {
        private int mIndex = 0;

        public override string Next(IGenerationContext context)
        {
            return string.Format("{0}@example.com", mIndex++);
        }
    }
}
