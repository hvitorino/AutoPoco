using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Engine;

namespace AutoPoco.Testing
{
    public class SimpleDataSource : DatasourceBase<String>
    {
        private String mValue;
        public SimpleDataSource(String value)
        {
            mValue = value;
        }

        public override string Next(IGenerationContext context)
        {
            return mValue;
        }
    }
}
