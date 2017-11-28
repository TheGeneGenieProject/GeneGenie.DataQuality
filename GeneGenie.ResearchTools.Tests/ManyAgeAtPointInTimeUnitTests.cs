// <copyright file="ManyAgeAtPointInTimeUnitTests.cs" company="GeneGenie.com">
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.
//
// You should have received a copy of the GNU Affero General Public License
// along with this program. If not, see http:www.gnu.org/licenses/ .
//
// </copyright>
// <author> Copyright (C) 2017 Ryan O'Neill r@genegenie.com </author>

namespace GeneGenie.ResearchTools.Tests
{
    using System;
    using System.Collections.Generic;
    using GeneGenie.ResearchTools.Data;
    using GeneGenie.ResearchTools.Models;
    using Xunit;

    /// <summary>
    /// Tests to check that multiple <see cref="AgeAtPointInTime"/> instances can be
    /// summarised to create a more accurate date of birth range.
    /// </summary>
    public class ManyAgeAtPointInTimeUnitTests
    {
        private readonly BirthdateRangeFinder birthDateRangeFinder;

        /// <summary>
        /// Initializes a new instance of the <see cref="ManyAgeAtPointInTimeUnitTests"/> class.
        /// </summary>
        public ManyAgeAtPointInTimeUnitTests()
        {
            birthDateRangeFinder = new BirthdateRangeFinder();
        }

        /// <summary>
        /// Real data from Ryan's family tree as great grandmother Evans was the inspiration for these
        /// library functions (she had unexpected ages recorded against some census data).
        /// </summary>
        [Fact]
        public void Great_grandmother_evans_has_reasonable_date_range_based_on_multiple_census()
        {
            var knownAges = new List<AgeAtPointInTime>
            {
                new AgeAtPointInTime(7, UkCensus.DateFromCensusYear(UkCensusYears.Census1871)),
                new AgeAtPointInTime(27, UkCensus.DateFromCensusYear(UkCensusYears.Census1891)),
                new AgeAtPointInTime(39, UkCensus.DateFromCensusYear(UkCensusYears.Census1901)),
                new AgeAtPointInTime(50, UkCensus.DateFromCensusYear(UkCensusYears.Census1911))
            };

            var result = birthDateRangeFinder.CalculateBirthdateRange(knownAges);

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

            var result = birthDateRangeFinder.CalculateBirthdateRange(knownAges);

            Assert.Equal(DateTime.MinValue, result.Earliest);
            Assert.Equal(DateTime.MaxValue, result.Latest);
        }
    }
}
