// <copyright file="UkCensus.cs" company="GeneGenie.com">
// Copyright (c) GeneGenie.com. All Rights Reserved.
// Licensed under the GNU Affero General Public License v3.0. See LICENSE in the project root for license information.
// </copyright>
// <author> Copyright (C) 2017 Ryan O'Neill r@genegenie.com </author>

namespace GeneGenie.DataQuality.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Data and helper function to find specific dates based on UK census years.
    /// </summary>
    public static class UkCensus
    {
        /// <summary>
        /// Dates for publicly accessible UK census records.
        /// </summary>
        private static List<DateTime> censusDates = new List<DateTime>
        {
            new DateTime((int)UkCensusYears.Census1801, (int)MonthNames.Mar, 10),
            new DateTime((int)UkCensusYears.Census1811, (int)MonthNames.May, 27),
            new DateTime((int)UkCensusYears.Census1821, (int)MonthNames.May, 28),
            new DateTime((int)UkCensusYears.Census1831, (int)MonthNames.May, 30),
            new DateTime((int)UkCensusYears.Census1841, (int)MonthNames.Jun,  6),
            new DateTime((int)UkCensusYears.Census1851, (int)MonthNames.Mar, 30),
            new DateTime((int)UkCensusYears.Census1861, (int)MonthNames.Apr,  7),
            new DateTime((int)UkCensusYears.Census1871, (int)MonthNames.Apr,  2),
            new DateTime((int)UkCensusYears.Census1881, (int)MonthNames.Apr,  3),
            new DateTime((int)UkCensusYears.Census1891, (int)MonthNames.Apr,  5),
            new DateTime((int)UkCensusYears.Census1901, (int)MonthNames.Mar, 31),
            new DateTime((int)UkCensusYears.Census1911, (int)MonthNames.Apr,  2),
            new DateTime((int)UkCensusYears.Census1921, (int)MonthNames.Jun, 19),
            new DateTime((int)UkCensusYears.Census1931, (int)MonthNames.Apr, 26),
            new DateTime((int)UkCensusYears.Census1939, (int)MonthNames.Sep, 29),
            new DateTime((int)UkCensusYears.Census1951, (int)MonthNames.Apr,  8),
            new DateTime((int)UkCensusYears.Census1961, (int)MonthNames.Apr, 23),
            new DateTime((int)UkCensusYears.Census1971, (int)MonthNames.Apr, 25),
            new DateTime((int)UkCensusYears.Census1981, (int)MonthNames.Apr,  5),
            new DateTime((int)UkCensusYears.Census1991, (int)MonthNames.Apr, 21),
            new DateTime((int)UkCensusYears.Census2001, (int)MonthNames.Apr, 29),
            new DateTime((int)UkCensusYears.Census2011, (int)MonthNames.Mar, 27),
        };

        /// <summary>
        /// Returns the date on which the passed UK census was recorded.
        /// </summary>
        /// <param name="year">The census year that you want the date for.</param>
        /// <returns>The date of the census for the passed year.</returns>
        public static DateTime DateFromCensusYear(UkCensusYears year)
        {
            return UkCensus.censusDates.Single(d => d.Year == (int)year);
        }
    }
}
