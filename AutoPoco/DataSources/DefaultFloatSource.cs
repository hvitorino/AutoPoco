using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Engine;

namespace AutoPoco.DataSources
{
    public class DefaultFloatSource : DatasourceBase<float>
    {
        public override float Next(IGenerationContext context)
        {
            return 0;
        }
    }
}
