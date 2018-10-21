// <copyright file="PointInTimeEarliestUnitTests.cs" company="GeneGenie.com">
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

namespace GeneGenie.DataQuality.Tests
{
    using System;
    using System.Collections.Generic;
    using GeneGenie.DataQuality;
    using GeneGenie.DataQuality.Data;
    using GeneGenie.DataQuality.Models;
    using GeneGenie.DataQuality.Tests.Models;
    using Xunit;

    /// <summary>
    /// Tests for ensuring that given a single date and age, the earliest birth date
    /// can be calculated.
    /// </summary>
    public class PointInTimeEarliestUnitTests
    {
        /// <summary>
        /// Returns a set of test data for checking an <see cref="BirthdateRange.Earliest"/> instance returned from <see cref="BirthdateRangeFinder"/>.
        /// </summary>
        /// <returns>A list of objects wrapping <see cref="ExpectedResultForAgeAtPointInTime"/> instances.</returns>
        public static IEnumerable<object[]> Earliest_date_test_data()
        {
            yield return new object[] { TestDataFactoryHelpers.CreateExpectedAge(-1, new DateTime(2000, 1, 1), new DateTime(2000, 1, 2)) };
            yield return new object[] { TestDataFactoryHelpers.CreateExpectedAge(0, new DateTime(2000, 1, 1), new DateTime(1999, 1, 2)) };
            yield return new object[] { TestDataFactoryHelpers.CreateExpectedAge(0, new DateTime(2000, 1, 1), new DateTime(1999, 1, 2)) };
            yield return new object[] { TestDataFactoryHelpers.CreateExpectedAge(10, new DateTime(2010, 1, 1), new DateTime(1999, 1, 2)) };
            yield return new object[] { TestDataFactoryHelpers.CreateExpectedAge(7, UkCensus.DateFromCensusYear(UkCensusYears.Census1871), new DateTime(1863, 4, 3)) };
        }

        /// <summary>
        /// Tests that the earliest date makes sense for each of the test cases defined when looking for a birth date range.
        /// </summary>
        /// <param name="data">Data to check that an <see cref="AgeAtPointInTime"/>instance has the correct earliest date.</param>
        [Theory]
        [MemberData(nameof(Earliest_date_test_data))]
        public void Earliest_date_can_be_calculated_correctly(ExpectedResultForAgeAtPointInTime data)
        {
            var birthDateRangeFinder = new BirthdateRangeFinder();

            var result = birthDateRangeFinder.CalculateBirthdateRange(data.KnownAge);

            Assert.Equal(data.Expected, result.Earliest);
        }
    }
}
