// <copyright file="CensusDateTests.cs" company="GeneGenie.com">
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
    using GeneGenie.DataQuality.Data;
    using Xunit;

    /// <summary>
    /// Tests to ensure year census enum parses OK.
    /// </summary>
    public class CensusDateTests
    {
        /// <summary>
        /// Ensures every year in the census enum has a date.
        /// </summary>
        [Fact]
        public void Every_year_in_census_enum_has_a_date()
        {
            foreach (UkCensusYears censusYear in Enum.GetValues(typeof(UkCensusYears)))
            {
                UkCensus.DateFromCensusYear(censusYear);
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
