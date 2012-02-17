using System;
using System.Drawing;
using AutoPoco.Engine;

namespace AutoPoco.DataSources
{
    public class ColorSource : DatasourceBase<Color>
    {
        private readonly int mMax;
        private readonly Random mRandom;
        private readonly int mMin;

        public ColorSource()
        {
            mRandom = new Random(1337);
            mMin = 0;
            mMax = 255;
        }

        public override Color Next(IGenerationContext context)
        {
            return Color.FromArgb(
                mRandom.Next(mMin, mMax), 
                mRandom.Next(mMin, mMax), 
                mRandom.Next(mMin, mMax)
                );
        }
    }
}