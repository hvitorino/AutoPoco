using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoPoco.Configuration;
using AutoPoco.DataSources;

namespace AutoPoco
{
    public static class StringExtensions
    {
        /// <summary>
        /// Declares that this string member should have a random length between min and max
        /// </summary>
        public static IEngineConfigurationTypeBuilder<TPoco> Random<TPoco>(this IEngineConfigurationTypeMemberBuilder<TPoco, string> memberConfig, int minLength, int maxLength)
        {
           return memberConfig.Use<RandomStringSource>(minLength, maxLength);
        }

    }
}
