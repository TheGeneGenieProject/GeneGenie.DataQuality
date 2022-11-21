// <copyright file="DateParserPartialDateTests.cs" company="GeneGenie.com">
// Copyright (c) GeneGenie.com. All Rights Reserved.
// Licensed under the GNU Affero General Public License v3.0. See LICENSE in the project root for license information.
// </copyright>

namespace GeneGenie.DataQuality.Tests.DateParsing
{
    using System.Collections.Generic;
    using DataQuality.Models;
    using Xunit;

    /// <summary>
    /// Tests for checking that the date parser can extract partial dates.
    /// </summary>
    public class DateParserPartialDateTests
    {
        private readonly DateParser dateParser;

        /// <summary>
        /// Instantiates a new test instance. Only used by Xunit.
        /// </summary>
        public DateParserPartialDateTests()
        {
            dateParser = new DateParser();
        }

        /// <summary>
        /// Test partial date data.
        /// </summary>
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
                new object[] { "1", DateFormat.Mm },
                new object[] { "01", DateFormat.Mm },
            };

        /// <summary>
        /// Tests that the partial date passed is in the format we expect.
        /// </summary>
        /// <param name="dateText">The source date text to examine.</param>
        /// <param name="expectedFormatGuess">The format we expect the date to be in after parsing.</param>
        [Theory]
        [MemberData(nameof(MonthMissingOtherData))]
        public void Dates_with_partial_data_are_still_detected_but_not_valid(string dateText, DateFormat expectedFormatGuess)
        {
            var dateRange = dateParser.Parse(dateText);

            Assert.Equal(expectedFormatGuess, dateRange.SourceFormat);
        }
    }
}
