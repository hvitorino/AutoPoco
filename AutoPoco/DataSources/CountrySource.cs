using System;
using System.Globalization;
using System.Linq;
using AutoPoco.Engine;

namespace AutoPoco.DataSources
{
    public class CountrySource : DatasourceBase<string>
    {
        private readonly Random mRandom;
        private readonly CultureInfo[] mCultures;

        public CountrySource()
        {
            mRandom = new Random(1337);
            mCultures = CultureInfo.GetCultures(CultureTypes.AllCultures & ~CultureTypes.NeutralCultures);
        }

        public override string Next(IGenerationContext context)
        {
            string country = string.Empty;
            
            // skip the invariant culture (not a country)
            do
            {
                var index = mRandom.Next(1, mCultures.Count());
                country = mCultures[index].EnglishName;
            
                // some are combination of countries, let's skip them
            } while (country.Contains(","));

            // find the country
            int startIndex = country.IndexOf("(") + 1;
            country = country.Substring(startIndex).Replace(")", string.Empty);

            return country;
        }
    }
}