using System;
using AutoPoco.Engine;

namespace AutoPoco.DataSources
{
    public class DateOfBirthSource : DatasourceBase<DateTime>
    {
        private readonly Random mRandom;
        private readonly int mYearsMax;
        private readonly int mYearsMin;

        public DateOfBirthSource()
            :this(16, 59)
        {}

        public DateOfBirthSource(int min, int max)
        {
            mRandom = new Random(1337);
            mYearsMax = max;
            mYearsMin = min;
        }

        public override DateTime Next(IGenerationContext context)
        {
            return DateTime.Now.AddYears(-mRandom.Next(mYearsMin, mYearsMax));
        }
    }
}