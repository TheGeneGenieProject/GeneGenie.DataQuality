// <copyright file="PointInTimeLatestUnitTests.cs" company="GeneGenie.com">
// Copyright (c) GeneGenie.com. All Rights Reserved.
// Licensed under the GNU Affero General Public License v3.0. See LICENSE in the project root for license information.
// </copyright>

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
    /// Tests for ensuring that given a single date and age, the latest birth date
    /// can be calculated.
    /// </summary>
    public class PointInTimeLatestUnitTests
    {
        /// <summary>
        /// Returns a set of test data for checking an <see cref="BirthdateRange.Earliest"/> instance returned from <see cref="BirthdateRangeFinder"/>.
        /// </summary>
        /// <returns>A list of objects wrapping <see cref="ExpectedResultForAgeAtPointInTime"/> instances.</returns>
        public static IEnumerable<object[]> Latest_date_test_data()
        {
            yield return new object[] { TestDataFactoryHelpers.CreateExpectedAge(10, new DateTime(2010, 1, 1), new DateTime(2000, 1, 1)) };
            yield return new object[] { TestDataFactoryHelpers.CreateExpectedAge(7, UkCensus.DateFromCensusYear(UkCensusYears.Census1871), new DateTime(1864, 4, 2)) };
        }

        /// <summary>
        /// Tests that the latest date makes sense for each of the test cases defined when looking for a birth date range.
        /// </summary>
        /// <param name="data">Data to check that an <see cref="AgeAtPointInTime"/>instance has the correct earliest date.</param>
        [Theory]
        [MemberData(nameof(Latest_date_test_data))]
        internal void Latest_date_can_be_calculated_correctly(ExpectedResultForAgeAtPointInTime data)
        {
            var result = BirthdateRangeFinder.CalculateBirthdateRange(data.KnownAge);

            Assert.Equal(data.Expected, result.Latest);
        }
    }
}
