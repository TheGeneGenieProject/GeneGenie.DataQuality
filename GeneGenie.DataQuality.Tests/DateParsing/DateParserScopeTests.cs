// <copyright file="DateParserScopeTests.cs" company="GeneGenie.com">
// Copyright (c) GeneGenie.com. All Rights Reserved.
// Licensed under the GNU Affero General Public License v3.0. See LICENSE in the project root for license information.
// </copyright>

namespace GeneGenie.DataQuality.Tests.DateParsing
{
    using System.Collections.Generic;
    using GeneGenie.DataQuality.Models;
    using Xunit;

    public class DateParserScopeTests
    {
        private readonly DateParser dateParser;

        public DateParserScopeTests()
        {
            dateParser = new DateParser();
        }

        public static IEnumerable<object[]> DateRangeScopeData =>
            new List<object[]>
            {
                new object[] { "1600", DateRangeScope.DateRangeWithTimeRange },
                new object[] { "1937 02", DateRangeScope.DateRangeWithTimeRange },
                new object[] { "02 1937", DateRangeScope.DateRangeWithTimeRange },
                new object[] { "1937 2", DateRangeScope.DateRangeWithTimeRange },
                new object[] { "2 1937", DateRangeScope.DateRangeWithTimeRange },

                new object[] { "1939 3 9", DateRangeScope.ExactDateWithTimeRange },
                new object[] { "1939 9 3", DateRangeScope.ExactDateWithTimeRange },
                new object[] { "1939 9 27", DateRangeScope.ExactDateWithTimeRange },
                new object[] { "1939 27 9", DateRangeScope.ExactDateWithTimeRange },
                new object[] { "3 9 1939", DateRangeScope.ExactDateWithTimeRange },
                new object[] { "9 3 1939", DateRangeScope.ExactDateWithTimeRange },
                new object[] { "9 27 1939", DateRangeScope.ExactDateWithTimeRange },
                new object[] { "27 9 1939", DateRangeScope.ExactDateWithTimeRange },
            };

        [Theory]
        [MemberData(nameof(DateRangeScopeData))]
        public void Dates_can_be_parsed_and_expanded_into_date_ranges(string dateText, DateRangeScope expectedScope)
        {
            var dateRange = dateParser.Parse(dateText);

            Assert.Equal(expectedScope, dateRange.Scope);
        }
    }
}
