// <copyright file="ManyAgeAtPointInTimeUnitTests.cs" company="GeneGenie.com">
// Copyright (c) GeneGenie.com. All Rights Reserved.
// Licensed under the GNU Affero General Public License v3.0. See LICENSE in the project root for license information.
// </copyright>

namespace GeneGenie.DataQuality.Tests
{
    using System;
    using System.Collections.Generic;
    using GeneGenie.DataQuality.Data;
    using GeneGenie.DataQuality.Models;
    using Xunit;

    /// <summary>
    /// Tests to check that multiple <see cref="AgeAtPointInTime"/> instances can be
    /// summarised to create a more accurate date of birth range.
    /// </summary>
    public class ManyAgeAtPointInTimeUnitTests
    {
        /// <summary>
        /// Real data from Ryan's family tree as great grandmother Evans was the inspiration for these
        /// library functions (she had unexpected ages recorded against some census data).
        /// </summary>
        [Fact]
        public void Great_grandmother_evans_has_reasonable_date_range_based_on_multiple_census()
        {
            var knownAges = new List<AgeAtPointInTime>
            {
                new AgeAtPointInTime { Age = 7, Date = UkCensus.DateFromCensusYear(UkCensusYears.Census1871) },
                new AgeAtPointInTime { Age = 27, Date = UkCensus.DateFromCensusYear(UkCensusYears.Census1891) },
                new AgeAtPointInTime { Age = 39, Date = UkCensus.DateFromCensusYear(UkCensusYears.Census1901) },
                new AgeAtPointInTime { Age = 50, Date = UkCensus.DateFromCensusYear(UkCensusYears.Census1911) },
            };

            var result = BirthdateRangeFinder.CalculateBirthdateRange(knownAges);

            Assert.Equal(new DateTime(1860, 4, 3), result.Earliest);
            Assert.Equal(new DateTime(1864, 4, 5), result.Latest);
        }

        /// <summary>
        /// Checks that an empty set of dates does not throw an exception.
        /// </summary>
        [Fact]
        public void Empty_list_returns_minimum_and_maximum_dates_instead_of_blowing_up()
        {
            var knownAges = new List<AgeAtPointInTime>();

            var result = BirthdateRangeFinder.CalculateBirthdateRange(knownAges);

            Assert.Equal(DateTime.MinValue, result.Earliest);
            Assert.Equal(DateTime.MaxValue, result.Latest);
        }
    }
}
