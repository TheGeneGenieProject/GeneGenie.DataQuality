// <copyright file="DateParserTests.cs" company="GeneGenie.com">
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

    public class DateParserTests
    {
        private readonly DateParser dateParser;

        public DateParserTests()
        {
            dateParser = new DateParser();
        }

        public static IEnumerable<object[]> DateRangeData =>
            new List<object[]>
            {
                new object[] { "1600", new DateTime(1600, 1, 1), new DateTime(1600, 12, 31).EndOfDay(), DateFormat.Yyyy },
                new object[] { "1937 02", new DateTime(1937, 2, 1), new DateTime(1937, 2, 28).EndOfDay(), DateFormat.Yyyy_mm },
                new object[] { "02 1937", new DateTime(1937, 2, 1), new DateTime(1937, 2, 28).EndOfDay(), DateFormat.Mm_yyyy },
                new object[] { "1937 2", new DateTime(1937, 2, 1), new DateTime(1937, 2, 28).EndOfDay(), DateFormat.Yyyy_mm },
                new object[] { "2 1937", new DateTime(1937, 2, 1), new DateTime(1937, 2, 28).EndOfDay(), DateFormat.Mm_yyyy },
                new object[] { "1939 3 9", new DateTime(1939, 3, 9), new DateTime(1939, 3, 9).EndOfDay(), DateFormat.UnsureEndingWithDateOrMonth },
                new object[] { "1939 9 3", new DateTime(1939, 9, 3), new DateTime(1939, 9, 3).EndOfDay(), DateFormat.UnsureEndingWithDateOrMonth },
                new object[] { "1939 9 27", new DateTime(1939, 9, 27), new DateTime(1939, 9, 27).EndOfDay(), DateFormat.Yyyy_mm_dd },
                new object[] { "1939 27 9", new DateTime(1939, 9, 27), new DateTime(1939, 9, 27).EndOfDay(), DateFormat.Yyyy_dd_mm },

                new object[] { "3 9 1939", new DateTime(1939, 3, 9), new DateTime(1939, 3, 9).EndOfDay(), DateFormat.UnsureStartingWithDateOrMonth },
                new object[] { "9 3 1939", new DateTime(1939, 9, 3), new DateTime(1939, 9, 3).EndOfDay(), DateFormat.UnsureStartingWithDateOrMonth },
                new object[] { "9 27 1939", new DateTime(1939, 9, 27), new DateTime(1939, 9, 27).EndOfDay(), DateFormat.Mm_dd_yyyy },
                new object[] { "27 9 1939", new DateTime(1939, 9, 27), new DateTime(1939, 9, 27).EndOfDay(), DateFormat.Dd_mm_yyyy },
            };

        public static IEnumerable<object[]> UnableToParseData =>
            new List<object[]>
            {
                new object[] { "27 1939 9", DateFormat.UnableToParseAsYearInMiddle },
                new object[] { "27 1939 Sep", DateFormat.UnableToParseAsYearInMiddle },
                new object[] { "9 1939 27", DateFormat.UnableToParseAsYearInMiddle },
                new object[] { "Sep 1939 27", DateFormat.UnableToParseAsYearInMiddle },

                new object[] { "Sep 27 xxx", DateFormat.UnableToParse },
                new object[] { "Sep xx 1939", DateFormat.UnableToParse },
                new object[] { "xxx 27 1939", DateFormat.UnableToParse },
                new object[] { "xxx 27 xxx", DateFormat.UnableToParse },
                new object[] { "xxx xx 1939", DateFormat.UnableToParse },
                new object[] { "xxx xx xxxx", DateFormat.UnableToParse },

                new object[] { "yyyy", DateFormat.UnableToParse },
                new object[] { "yyyy mm", DateFormat.UnableToParse },
                new object[] { "yyyy mm dd", DateFormat.UnableToParse },
                new object[] { "2000 mm dd", DateFormat.UnableToParse },
                new object[] { "mm dd 2000", DateFormat.UnableToParse },
            };

        public static IEnumerable<object[]> DelimiterData =>
            new List<object[]>
            {
                new object[] { " " },
                new object[] { "  " },
                new object[] { "       " },
                new object[] { "-" },
                new object[] { "--" },
                new object[] { "-------" },
                new object[] { "\\" },
                new object[] { "\\\\" },
                new object[] { "\\\\\\\\" },
                new object[] { "/" },
                new object[] { "//" },
                new object[] { "////////" },
            };

        [Fact]
        public void Empty_does_not_throw_exception_when_parsing_date()
        {
            var dateRange = dateParser.Parse(string.Empty);

            Assert.NotNull(dateRange);
        }

        [Fact]
        public void Empty_result_indicates_invalid_dates()
        {
            var dateRange = dateParser.Parse(string.Empty);

            Assert.Null(dateRange.DateFrom);
            Assert.Null(dateRange.DateTo);
        }

        [Fact]
        public void Source_value_is_populated_for_corrupt_data()
        {
            var dateRange = dateParser.Parse("abcdef");

            Assert.Equal("abcdef", dateRange.Source);
        }

        [Fact]
        public void Source_value_is_populated_for_correct_data()
        {
            var dateRange = dateParser.Parse("1997");

            Assert.Equal("1997", dateRange.Source);
        }

        [Theory]
        [MemberData(nameof(DelimiterData))]
        public void Text_with_only_delimiters_indicates_invalid_dates(string dateText)
        {
            var dateRange = dateParser.Parse(dateText);

            Assert.Null(dateRange.DateFrom);
            Assert.Null(dateRange.DateTo);
        }

        [Theory]
        [MemberData(nameof(DateRangeData))]
        private void Dates_can_be_parsed_and_expanded_into_date_ranges(string dateText, DateTime expectedDateFrom, DateTime expectedDateTo, DateFormat expectedFormatGuess)
        {
            var dateRange = dateParser.Parse(dateText);

            Assert.Equal(expectedDateFrom, dateRange.DateFrom);
            Assert.Equal(expectedDateTo, dateRange.DateTo);
            Assert.Equal(expectedFormatGuess, dateRange.SourceFormat);
        }

        [Theory]
        [MemberData(nameof(UnableToParseData))]
        private void Dates_with_years_in_the_middle_cannot_be_parsed(string dateText, DateFormat expectedFormatGuess)
        {
            var dateRange = dateParser.Parse(dateText);

            Assert.Equal(expectedFormatGuess, dateRange.SourceFormat);
        }
    }
}
