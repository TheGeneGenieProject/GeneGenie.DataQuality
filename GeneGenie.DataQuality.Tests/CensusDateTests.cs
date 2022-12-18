// <copyright file="CensusDateTests.cs" company="GeneGenie.com">
// Copyright (c) GeneGenie.com. All Rights Reserved.
// Licensed under the GNU Affero General Public License v3.0. See LICENSE in the project root for license information.
// </copyright>

namespace GeneGenie.DataQuality.Tests
{
    /// <summary>
    /// Tests to ensure year census enum parses OK.
    /// </summary>
    public class CensusDateTests
    {
        /// <summary>
        /// Ensures every year in the census enum has a date.
        /// </summary>
        [Fact]
        public void Every_year_in_census_enum_except_for_none_has_a_date()
        {
            var censusYears = Enum
                .GetValues(typeof(UkCensusYears))
                .Cast<UkCensusYears>()
                .Where(cy => cy != UkCensusYears.None)
                .Select(cy => cy)
                .ToList();

            foreach (UkCensusYears censusYear in censusYears)
            {
                var censusDate = UkCensus.DateFromCensusYear(censusYear);

                Assert.Equal((int)censusYear, censusDate.Year);
            }
        }

        /// <summary>
        /// Ensures that if someone passes in and casts an invalid int, they don't get a date
        /// but get an exception instead.
        /// </summary>
        [Fact]
        public void Year_not_in_census_throws_exception()
        {
            Assert.Throws<InvalidOperationException>(() => UkCensus.DateFromCensusYear((UkCensusYears)2099));
        }
    }
}
