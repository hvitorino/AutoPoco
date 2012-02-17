using System;
using AutoPoco.Engine;

namespace AutoPoco.DataSources
{
    public class GuidSource : DatasourceBase<Guid>
    {
        private Random mRandom;

        public GuidSource()
        {
            mRandom = new Random(1337);
        }

        public override Guid Next(IGenerationContext context)
        {
            Byte[] buffer = new Byte[16];
            mRandom.NextBytes(buffer);
            return new Guid(buffer);
        }
    }
}