// <copyright file="DateParserTests.cs" company="GeneGenie.com">
// Copyright (c) GeneGenie.com. All Rights Reserved.
// Licensed under the GNU Affero General Public License v3.0. See LICENSE in the project root for license information.
// </copyright>

namespace GeneGenie.DataQuality.Tests.DateParsing
{
    using DataQuality.Models;
    using ExtensionMethods;

    /// <summary>
    /// Tests for checking that the date parsing code passes and fails as expected.
    /// </summary>
    public class DateParserTests
    {
        /// <summary>
        /// Data for checking that dates can be parsed into date ranges and the formats are detected correctly.
        /// </summary>
        public static IEnumerable<object[]> DateRangeData =>
            new List<object[]>
            {
                new object[] { "1600", new DateTime(1600, 1, 1), new DateTime(1600, 12, 31).EndOfDay(), DateFormat.Yyyy },
                new object[] { "1937 02", new DateTime(1937, 2, 1), new DateTime(1937, 2, 28).EndOfDay(), DateFormat.YyyyMm },
                new object[] { "02 1937", new DateTime(1937, 2, 1), new DateTime(1937, 2, 28).EndOfDay(), DateFormat.MmYyyy },
                new object[] { "1937 2", new DateTime(1937, 2, 1), new DateTime(1937, 2, 28).EndOfDay(), DateFormat.YyyyMm },
                new object[] { "2 1937", new DateTime(1937, 2, 1), new DateTime(1937, 2, 28).EndOfDay(), DateFormat.MmYyyy },
                new object[] { "1939 3 9", new DateTime(1939, 3, 9), new DateTime(1939, 3, 9).EndOfDay(), DateFormat.UnsureEndingWithDateOrMonth },
                new object[] { "1939 9 3", new DateTime(1939, 9, 3), new DateTime(1939, 9, 3).EndOfDay(), DateFormat.UnsureEndingWithDateOrMonth },
                new object[] { "1939 9 27", new DateTime(1939, 9, 27), new DateTime(1939, 9, 27).EndOfDay(), DateFormat.YyyyMmDd },
                new object[] { "1939 27 9", new DateTime(1939, 9, 27), new DateTime(1939, 9, 27).EndOfDay(), DateFormat.YyyyDdMm },

                new object[] { "3 9 1939", new DateTime(1939, 3, 9), new DateTime(1939, 3, 9).EndOfDay(), DateFormat.UnsureStartingWithDateOrMonth },
                new object[] { "9 3 1939", new DateTime(1939, 9, 3), new DateTime(1939, 9, 3).EndOfDay(), DateFormat.UnsureStartingWithDateOrMonth },
                new object[] { "9 27 1939", new DateTime(1939, 9, 27), new DateTime(1939, 9, 27).EndOfDay(), DateFormat.MmDdYyyy },
                new object[] { "27 9 1939", new DateTime(1939, 9, 27), new DateTime(1939, 9, 27).EndOfDay(), DateFormat.DdMmYyyy },
            };

        /// <summary>
        /// Data that tests that junk source values are detected as junk and not parsed as valid data.
        /// </summary>
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

        /// <summary>
        /// Test data for checking if delimiters cause issues with parsing.
        /// </summary>
        public static IEnumerable<object[]> DelimiterData =>
            new List<object[]>
            {
                new object[] { " " },
                new object[] { "  " },
                new object[] { "       " },

                new object[] { "\u00A0" }, // No-Break Space.
                new object[] { "\u2000" }, // En Quad.
                new object[] { "\u2001" }, // Em Quad.
                new object[] { "\u2002" }, // En Space.
                new object[] { "\u2003" }, // Em Space.
                new object[] { "\u2004" }, // Three-Per-Em Space.
                new object[] { "\u2005" }, // Four-Per-Em Space.
                new object[] { "\u2006" }, // Six-Per-Em Space.
                new object[] { "\u2007" }, // Figure Space.
                new object[] { "\u2008" }, // Punctuation Space.
                new object[] { "\u2009" }, // Thin Space.
                new object[] { "\u200A" }, // Hair Space.
                new object[] { "\u2028" }, // Line Separator.
                new object[] { "\u205F" }, // Medium Mathematical Space.
                new object[] { "\u3000" }, // Ideographic Space.

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

        /// <summary>
        /// Tests that a blank string does not cause the parsing to fail.
        /// </summary>
        [Fact]
        public void Empty_does_not_throw_exception_when_parsing_date()
        {
            var dateRange = DateParser.Parse(string.Empty);

            Assert.NotNull(dateRange);
        }

        /// <summary>
        /// Tests that a blank string yields empty date results.
        /// </summary>
        [Fact]
        public void Empty_yields_invalid_dates()
        {
            var dateRange = DateParser.Parse(string.Empty);

            Assert.Null(dateRange.DateFrom);
            Assert.Null(dateRange.DateTo);
        }

        /// <summary>
        /// Checks that the <see cref="DateRange.Source"/> property is populated
        /// even when passed totally junk data.
        /// </summary>
        [Fact]
        public void Source_value_is_populated_for_corrupt_data()
        {
            var dateRange = DateParser.Parse("!'�$%^&*()_+");

            Assert.Equal("!'�$%^&*()_+", dateRange.Source);
        }

        /// <summary>
        /// Checks that the <see cref="DateRange.Source"/> property is populated
        /// when passed alphabetical data.
        /// </summary>
        [Fact]
        public void Source_value_is_populated_for_non_numeric_data()
        {
            var dateRange = DateParser.Parse("abcdef");

            Assert.Equal("abcdef", dateRange.Source);
        }

        /// <summary>
        /// Checks that the <see cref="DateRange.Source"/> property is populated
        /// when passed valid data.
        /// </summary>
        [Fact]
        public void Source_value_is_populated_for_correct_data()
        {
            var dateRange = DateParser.Parse("1997");

            Assert.Equal("1997", dateRange.Source);
        }

        /// <summary>
        /// Tests that text made up only of delimiters is not parsed as valid data.
        /// </summary>
        /// <param name="dateText"></param>
        [Theory]
        [MemberData(nameof(DelimiterData))]
        public void Text_with_only_delimiters_indicates_invalid_dates(string dateText)
        {
            var dateRange = DateParser.Parse(dateText);

            Assert.Null(dateRange.DateFrom);
            Assert.Null(dateRange.DateTo);
        }

        /// <summary>
        /// Tests that a date can be parsed and expanded into a date range.
        /// </summary>
        /// <param name="dateText">The text to parse.</param>
        /// <param name="expectedDateFrom">The expected start of the resulting date range.</param>
        /// <param name="expectedDateTo">The expected end of the resulting date range.</param>
        /// <param name="expectedFormatGuess">The format we expect to be detected from the source text.</param>
        [Theory]
        [MemberData(nameof(DateRangeData))]
        public void Dates_can_be_parsed_and_expanded_into_date_ranges(string dateText, DateTime expectedDateFrom, DateTime expectedDateTo, DateFormat expectedFormatGuess)
        {
            var dateRange = DateParser.Parse(dateText);

            Assert.Equal(expectedDateFrom, dateRange.DateFrom);
            Assert.Equal(expectedDateTo, dateRange.DateTo);
            Assert.Equal(expectedFormatGuess, dateRange.SourceFormat);
        }

        /// <summary>
        /// Tests that dates that have years in the middle are not parsed as this does not seem to be a format anyone would use.
        /// </summary>
        /// <param name="dateText"></param>
        /// <param name="expectedFormatGuess"></param>
        [Theory]
        [MemberData(nameof(UnableToParseData))]
        public void Dates_with_years_in_the_middle_cannot_be_parsed(string dateText, DateFormat expectedFormatGuess)
        {
            var dateRange = DateParser.Parse(dateText);

            Assert.Equal(expectedFormatGuess, dateRange.SourceFormat);
        }
    }
}
