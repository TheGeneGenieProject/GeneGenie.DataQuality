﻿// <copyright file="DateParserPartialDateTests.cs" company="GeneGenie.com">
// Copyright (c) GeneGenie.com. All Rights Reserved.
// Licensed under the GNU Affero General Public License v3.0. See LICENSE in the project root for license information.
// </copyright>

namespace GeneGenie.DataQuality.Tests.DateParsing
{
    /// <summary>
    /// Tests for checking that the date parser can extract partial dates.
    /// </summary>
    public class DateParserPartialDateTests
    {
        /// <summary>
        /// Test partial date data.
        /// </summary>
        public static IEnumerable<object[]> MonthMissingOtherData =>
            new List<object[]>
            {
                new object[] { "January", DateFormat.Mmm },
                new object[] { "Jan", DateFormat.Mmm },
                new object[] { "Jan 1", DateFormat.MmmDd },
                new object[] { "1 Jan", DateFormat.DdMmm },
                new object[] { "January 1", DateFormat.MmmDd },
                new object[] { "1 January", DateFormat.DdMmm },
                new object[] { "1 1", DateFormat.DdMm },
                new object[] { "1 13", DateFormat.MmDd },
                new object[] { "13 1", DateFormat.DdMm },
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
            var dateRange = DateParser.Parse(dateText);

            Assert.Equal(expectedFormatGuess, dateRange.SourceFormat);
        }
    }
}
