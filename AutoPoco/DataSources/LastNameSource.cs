using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Engine;

namespace AutoPoco.DataSources
{
    public class LastNameSource : DatasourceBase<String>
    {
        private Random mRandom = new Random(1337);

        public override string Next(IGenerationContext context)
        {
            return SecondNames[mRandom.Next(0, SecondNames.Length)];
        }

        private static string[] SecondNames = new String[]
        {
            "White",
            "Black",
            "Red",
            "Gray",
            "Magenta",
            "Myrtle",
            "Gold",
            "Silver",
            "Green",
            "Puce",
            "Carmine",
            "Purple",
            "Yellow",
            "Indigo"
        };
    }
}
