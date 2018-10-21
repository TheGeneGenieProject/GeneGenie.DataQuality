// <copyright file="CensusDayAndMonthTests.cs" company="GeneGenie.com">
// Copyright (c) GeneGenie.com. All Rights Reserved.
// Licensed under the GNU Affero General Public License v3.0. See LICENSE in the project root for license information.
// </copyright>
// <author> Copyright (C) 2017 Ryan O'Neill r@genegenie.com </author>

namespace GeneGenie.DataQuality.Tests
{
    using System;
    using GeneGenie.DataQuality.Data;
    using Xunit;

    /// <summary>
    /// Sanity checks to ensure the days of the census' are correct.
    /// </summary>
    public class CensusDayAndMonthTests
    {
        /// <summary>
        /// Checks the census dates were on specific weekdays (data sanity check).
        /// </summary>
        /// <param name="censusYear">The census year.</param>
        /// <param name="expectedDay">The expected day of the week.</param>
        /// <param name="expectedMonth">The expected month.</param>
        [Theory]
        [InlineData(UkCensusYears.Census1801, DayOfWeek.Tuesday, MonthNames.Mar)]
        [InlineData(UkCensusYears.Census1811, DayOfWeek.Monday, MonthNames.May)]
        [InlineData(UkCensusYears.Census1821, DayOfWeek.Monday, MonthNames.May)]
        [InlineData(UkCensusYears.Census1831, DayOfWeek.Monday, MonthNames.May)]
        [InlineData(UkCensusYears.Census1841, DayOfWeek.Sunday, MonthNames.Jun)]
        [InlineData(UkCensusYears.Census1851, DayOfWeek.Sunday, MonthNames.Mar)]
        [InlineData(UkCensusYears.Census1861, DayOfWeek.Sunday, MonthNames.Apr)]
        [InlineData(UkCensusYears.Census1871, DayOfWeek.Sunday, MonthNames.Apr)]
        [InlineData(UkCensusYears.Census1881, DayOfWeek.Sunday, MonthNames.Apr)]
        [InlineData(UkCensusYears.Census1891, DayOfWeek.Sunday, MonthNames.Apr)]
        [InlineData(UkCensusYears.Census1901, DayOfWeek.Sunday, MonthNames.Mar)]
        [InlineData(UkCensusYears.Census1911, DayOfWeek.Sunday, MonthNames.Apr)]
        internal void Check_census_dates_are_on_correct_days(UkCensusYears censusYear, DayOfWeek expectedDay, MonthNames expectedMonth)
        {
            AssertDayAndMonth(censusYear, expectedDay, expectedMonth);
        }

        private void AssertDayAndMonth(UkCensusYears censusYear, DayOfWeek day, MonthNames monthOfYear)
        {
            var date = UkCensus.DateFromCensusYear(censusYear);

            Assert.Equal(date.DayOfWeek, day);
            Assert.Equal(date.Month, (int)monthOfYear);
        }
    }
}
