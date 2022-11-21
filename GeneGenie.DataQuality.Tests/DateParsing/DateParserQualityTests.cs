// <copyright file="DateParserQualityTests.cs" company="GeneGenie.com">
// Copyright (c) GeneGenie.com. All Rights Reserved.
// Licensed under the GNU Affero General Public License v3.0. See LICENSE in the project root for license information.
// </copyright>

namespace GeneGenie.DataQuality.Tests.DateParsing
{
    using System.Collections.Generic;
    using DataQuality.Models;
    using Xunit;

    /// <summary>
    /// Tests to check that the quality rating of the source values for dates can be parsed.
    /// </summary>
    public class DateParserQualityTests
    {
        private readonly DateParser dateParser;

        public DateParserQualityTests()
        {
            dateParser = new DateParser();
        }

        /// <summary>
        /// Gets test date values and what we expect their quality to be.
        /// </summary>
        public static IEnumerable<object[]> ExpectedDateQualityData =>
            new List<object[]>
            {
                new object[] { string.Empty, DateQualityStatus.Empty },
                new object[] { " ", DateQualityStatus.Empty },

                new object[] { "1 2 3 4", DateQualityStatus.TooManyDateParts },
                new object[] { "1/2/3/4", DateQualityStatus.TooManyDateParts },
                new object[] { "Jan 1 2020 11:11", DateQualityStatus.TooManyDateParts },

                new object[] { "1970", DateQualityStatus.OK },
                new object[] { "50", DateQualityStatus.OK },
                new object[] { "2020", DateQualityStatus.OK },
                new object[] { "1 Jan 1990", DateQualityStatus.OK },
                new object[] { "Jan 1 1990", DateQualityStatus.OK },

                new object[] { "Jan", DateQualityStatus.OnlyMonthIsPresent },
                new object[] { "January", DateQualityStatus.OnlyMonthIsPresent },
                new object[] { "10", DateQualityStatus.OnlyMonthIsPresent },

                new object[] { "1 Jan", DateQualityStatus.NotValid },
                new object[] { "Jan xxxx", DateQualityStatus.NotValid },
                new object[] { "xxxx Jan", DateQualityStatus.NotValid },
                new object[] { "13 xxxx", DateQualityStatus.NotValid },
                new object[] { "xxxx 13", DateQualityStatus.NotValid },
                new object[] { "49 12", DateQualityStatus.NotValid },
                new object[] { "2050 12", DateQualityStatus.NotValid },
                new object[] { "unknown", DateQualityStatus.NotValid },

                new object[] { "1 1 1", DateQualityStatus.ThreePartsWithoutYear },
                new object[] { "1 1 49", DateQualityStatus.ThreePartsWithoutYear },
                new object[] { "1 1 2050", DateQualityStatus.ThreePartsWithoutYear },
                new object[] { "2050 12 12", DateQualityStatus.ThreePartsWithoutYear },

                new object[] { "Jan 1990 12", DateQualityStatus.YearInMiddle },
                new object[] { "99 Jan 99", DateQualityStatus.NotValid },

                new object[] { "1990 12 12", DateQualityStatus.MonthIsAmbiguous },
                new object[] { "12 12 1990", DateQualityStatus.MonthIsAmbiguous },
            };

        [Theory]
        [MemberData(nameof(ExpectedDateQualityData))]
        public void Dates_with_years_in_the_middle_cannot_be_parsed(string dateText, DateQualityStatus expectedQuality)
        {
            var dateRange = dateParser.Parse(dateText);

            Assert.Equal(expectedQuality, dateRange.Status);
        }
    }
}
