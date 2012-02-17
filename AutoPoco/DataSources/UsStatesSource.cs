using System;
using System.Collections.Generic;
using System.Linq;
using AutoPoco.Engine;

namespace AutoPoco.DataSources
{
    public class UsStatesSource : DatasourceBase<string>
    {
        public static readonly Dictionary<string, string> STATES = new Dictionary<string, string>
                                                                       {
                                                                           {"AL", "Alabama"},
                                                                           {"AK", "Alaska"},
                                                                           {"AZ", "Arizona"},
                                                                           {"AR", "Arkansas"},
                                                                           {"CA", "California"},
                                                                           {"CO", "Colorado"},
                                                                           {"CT", "Connecticut"},
                                                                           {"DE", "Delaware"},
                                                                           {"FL", "Florida"},
                                                                           {"GA", "Georgia"},
                                                                           {"HI", "Hawaii"},
                                                                           {"ID", "Idaho"},
                                                                           {"IL", "Illinois"},
                                                                           {"IN", "Indiana"},
                                                                           {"IA", "Iowa"},
                                                                           {"KS", "Kansas"},
                                                                           {"KY", "Kentucky"},
                                                                           {"LA", "Louisiana"},
                                                                           {"ME", "Maine"},
                                                                           {"MD", "Maryland"},
                                                                           {"MA", "Massachusetts"},
                                                                           {"MI", "Michigan"},
                                                                           {"MN", "Minnesota"},
                                                                           {"MS", "Mississippi"},
                                                                           {"MO", "Missouri"},
                                                                           {"MT", "Montana"},
                                                                           {"NE", "Nebraska"},
                                                                           {"NV", "Nevada"},
                                                                           {"NH", "New Hampshire"},
                                                                           {"NJ", "New Jersey"},
                                                                           {"NM", "New Mexico"},
                                                                           {"NY", "New York"},
                                                                           {"NC", "North Carolina"},
                                                                           {"ND", "North Dakota"},
                                                                           {"OH", "Ohio"},
                                                                           {"OK", "Oklahoma"},
                                                                           {"OR", "Oregon"},
                                                                           {"PA", "Pennsylvania"},
                                                                           {"RI", "Rhode Island"},
                                                                           {"SC", "South Carolina"},
                                                                           {"SD", "South Dakota"},
                                                                           {"TN", "Tennessee"},
                                                                           {"TX", "Texas"},
                                                                           {"UT", "Utah"},
                                                                           {"VT", "Vermont"},
                                                                           {"VA", "Virginia"},
                                                                           {"WA", "Washington"},
                                                                           {"WV", "West Virginia"},
                                                                           {"WI", "Wisconsin"},
                                                                           {"WY", "Wyoming"},
                                                                           {"AS", "American Samoa"},
                                                                           {"DC", "District of Columbia"},
                                                                           {"FM", "Federated States of Micronesia"},
                                                                           {"MH", "Marshall Islands"},
                                                                           {"MP", "Northern Mariana Islands"},
                                                                           {"PW", "Palau"},
                                                                           {"PR", "Puerto Rico"},
                                                                           {"VI", "Virgin Islands"},
                                                                           {"GU", "Guam"}
                                                                       };

        private readonly Random mRandom;
        private readonly bool mUseAbbreviations;

        public UsStatesSource()
            : this(false)
        {
        }

        public UsStatesSource(bool useAbbreviations)
        {
            mUseAbbreviations = useAbbreviations;
            mRandom = new Random(1337);
        }

        public override string Next(IGenerationContext context)
        {
            int num = mRandom.Next(0, STATES.Count);
            return mUseAbbreviations ? STATES.Keys.ToList()[num] : STATES.Values.ToList()[num];
        }
    }
}