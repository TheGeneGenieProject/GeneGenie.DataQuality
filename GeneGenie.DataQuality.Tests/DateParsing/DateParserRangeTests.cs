// <copyright file="DateParserRangeTests.cs" company="GeneGenie.com">
// Copyright (c) GeneGenie.com. All Rights Reserved.
// Licensed under the GNU Affero General Public License v3.0. See LICENSE in the project root for license information.
// </copyright>

namespace GeneGenie.DataQuality.Tests.DateParsing
{
    using System;
    using System.Collections.Generic;
    using DataQuality.Models;
    using ExtensionMethods;
    using Xunit;

    /// <summary>
    /// Tests for ensuring a date or partial date can be parsed into a date range.
    /// </summary>
    public class DateParserRangeTests
    {
        /// <summary>
        /// Test data for verifying that single dates can be parsed into date ranges.
        /// </summary>
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

        /// <summary>
        /// Tests for checking that a textual date can be parsed into a date range and format.
        /// </summary>
        /// <param name="dateText">The source date text to parse.</param>
        /// <param name="expectedDateFrom">The start of the date range we expect to find after parsing.</param>
        /// <param name="expectedDateTo">The end of the date range we expect to find after parsing.</param>
        /// <param name="expectedFormatGuess">The format we expect that the source date text is in.</param>
        [Theory]
        [MemberData(nameof(MonthRangeData))]
        public void Dates_with_english_month_names_can_be_parsed_and_expanded_into_date_ranges(string dateText, DateTime expectedDateFrom, DateTime expectedDateTo, DateFormat expectedFormatGuess)
        {
            var dateRange = DateParser.Parse(dateText);

            Assert.Equal(expectedDateFrom, dateRange.DateFrom);
            Assert.Equal(expectedDateTo, dateRange.DateTo);
            Assert.Equal(expectedFormatGuess, dateRange.SourceFormat);
        }
    }
}
