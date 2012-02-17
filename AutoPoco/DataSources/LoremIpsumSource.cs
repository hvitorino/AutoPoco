using System.Text;
using AutoPoco.Engine;
using AutoPoco.Properties;

namespace AutoPoco.DataSources
{
    public class LoremIpsumSource : DatasourceBase<string>
    {
        private readonly int mTimes;

        public LoremIpsumSource()
            : this(1)
        {}

        public LoremIpsumSource(int times)
        {
            mTimes = times;
        }

        public override string Next(IGenerationContext context)
        {
            var builder = new StringBuilder(Resources.LoremIpsum);

            for (int i = 1; i < mTimes; i++)
            {
                builder.AppendLine();
                builder.AppendLine();
                builder.AppendLine(Resources.LoremIpsum);
            }

            return builder.ToString();
        }
    }
}