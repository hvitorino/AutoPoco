using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Engine;

namespace AutoPoco.DataSources
{
    public class RandomStringSource : DatasourceBase<String>
    {
        private int mMin;
        private int mMax;
        private Random mRandom = new Random(1337);

        public RandomStringSource(int min, int max)
        {
            mMin = min;
            mMax = max;
        }

        public override string Next(IGenerationContext context)
        {            
            StringBuilder builder = new StringBuilder();
            int length = mRandom.Next(mMin, mMax + 1);
            for (int x = 0; x < length; x++)
            {
                int value = mRandom.Next(65, 123);
                builder.Append((char)value);
            }
            return builder.ToString();
        }
    }
}
