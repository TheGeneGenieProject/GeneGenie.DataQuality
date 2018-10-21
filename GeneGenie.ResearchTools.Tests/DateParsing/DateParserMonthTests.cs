// <copyright file="DateParserMonthTests.cs" company="GeneGenie.com">
// Copyright (c) GeneGenie.com. All Rights Reserved.
// Licensed under the GNU Affero General Public License v3.0. See LICENSE in the project root for license information.
// </copyright>

namespace GeneGenie.DataQuality.Tests.DateParsing
{
    using System;
    using System.Collections.Generic;
    using GeneGenie.DataQuality.ExtensionMethods;
    using GeneGenie.DataQuality.Models;
    using Xunit;

    public class DateParserMonthTests
    {
        private readonly DateParser dateParser;

        public DateParserMonthTests()
        {
            dateParser = new DateParser();
        }

        public static IEnumerable<object[]> MonthRangeData =>
            new List<object[]>
            {
                new object[] { "1937 Feb", new DateTime(1937, 2, 1), new DateTime(1937, 2, 28).EndOfDay(), DateFormat.Yyyy_mmm },
                new object[] { "Feb 1937", new DateTime(1937, 2, 1), new DateTime(1937, 2, 28).EndOfDay(), DateFormat.Mmm_yyyy },
                new object[] { "1939 Mar 9", new DateTime(1939, 3, 9), new DateTime(1939, 3, 9).EndOfDay(), DateFormat.Yyyy_mmm_dd },
                new object[] { "1939 9 Mar", new DateTime(1939, 3, 9), new DateTime(1939, 3, 9).EndOfDay(), DateFormat.Yyyy_dd_mmm },
                new object[] { "1939 Sep 27", new DateTime(1939, 9, 27), new DateTime(1939, 9, 27).EndOfDay(), DateFormat.Yyyy_mmm_dd },
                new object[] { "1939 27 Sep", new DateTime(1939, 9, 27), new DateTime(1939, 9, 27).EndOfDay(), DateFormat.Yyyy_dd_mmm },

                new object[] { "1937 February", new DateTime(1937, 2, 1), new DateTime(1937, 2, 28).EndOfDay(), DateFormat.Yyyy_mmm },
                new object[] { "February 1937", new DateTime(1937, 2, 1), new DateTime(1937, 2, 28).EndOfDay(), DateFormat.Mmm_yyyy },
                new object[] { "1939 March 9", new DateTime(1939, 3, 9), new DateTime(1939, 3, 9).EndOfDay(), DateFormat.Yyyy_mmm_dd },
                new object[] { "1939 9 March", new DateTime(1939, 3, 9), new DateTime(1939, 3, 9).EndOfDay(), DateFormat.Yyyy_dd_mmm },
                new object[] { "1939 September 27", new DateTime(1939, 9, 27), new DateTime(1939, 9, 27).EndOfDay(), DateFormat.Yyyy_mmm_dd },
                new object[] { "1939 27 September", new DateTime(1939, 9, 27), new DateTime(1939, 9, 27).EndOfDay(), DateFormat.Yyyy_dd_mmm },

                new object[] { "March 9 1939", new DateTime(1939, 3, 9), new DateTime(1939, 3, 9).EndOfDay(), DateFormat.Mmm_dd_yyyy },
                new object[] { "9 March 1939", new DateTime(1939, 3, 9), new DateTime(1939, 3, 9).EndOfDay(), DateFormat.Dd_mmm_yyyy },
                new object[] { "September 27 1939", new DateTime(1939, 9, 27), new DateTime(1939, 9, 27).EndOfDay(), DateFormat.Mmm_dd_yyyy },
                new object[] { "27 September 1939", new DateTime(1939, 9, 27), new DateTime(1939, 9, 27).EndOfDay(), DateFormat.Dd_mmm_yyyy },
            };

        public static IEnumerable<object[]> MonthMissingOtherData =>
            new List<object[]>
            {
                new object[] { "January", DateFormat.Mmm },
                new object[] { "Jan", DateFormat.Mmm },
                new object[] { "Jan 1", DateFormat.Mmm_dd },
                new object[] { "1 Jan", DateFormat.Dd_mmm },
                new object[] { "January 1", DateFormat.Mmm_dd },
                new object[] { "1 January", DateFormat.Dd_mmm },
                new object[] { "1 1", DateFormat.Dd_mm },
                new object[] { "1 13", DateFormat.Mm_dd },
                new object[] { "13 1", DateFormat.Dd_mm },
            };

        [Theory]
        [MemberData(nameof(MonthRangeData))]
        private void Dates_with_english_month_names_can_be_parsed_and_expanded_into_date_ranges(string dateText, DateTime expectedDateFrom, DateTime expectedDateTo, DateFormat expectedFormatGuess)
        {
            var dateRange = dateParser.Parse(dateText);

            Assert.Equal(expectedDateFrom, dateRange.DateFrom);
            Assert.Equal(expectedDateTo, dateRange.DateTo);
            Assert.Equal(expectedFormatGuess, dateRange.SourceFormat);
        }

        [Theory]
        [MemberData(nameof(MonthMissingOtherData))]
        private void Dates_with_partial_data_are_still_detected_but_not_valid(string dateText, DateFormat expectedFormatGuess)
        {
            var dateRange = dateParser.Parse(dateText);

            Assert.Equal(expectedFormatGuess, dateRange.SourceFormat);
        }
    }
}
