// <copyright file="AddressQualityCheckerTests.cs" company="GeneGenie.com">
// Copyright (c) GeneGenie.com. All Rights Reserved.
// Licensed under the GNU Affero General Public License v3.0. See LICENSE in the project root for license information.
// </copyright>

namespace GeneGenie.DataQuality.Tests.AddressQuality
{
    using System.Collections.Generic;
    using GeneGenie.DataQuality.Models;
    using Xunit;

    /// <summary>
    /// Tests to ensure that we don't do an expensive lookup on known junk addresses.
    /// The address is parsed to see if it is any of this type of data and a
    /// quality status is returned.
    /// </summary>
    public class AddressQualityCheckerTests
    {
        private readonly AddressQualityChecker addressQualityChecker;

        public AddressQualityCheckerTests()
        {
            var dateParser = new DateParser();
            addressQualityChecker = new AddressQualityChecker(dateParser);
        }

        public static IEnumerable<object[]> WhitespaceKeyValueData =>
            new List<object[]>
            {
                new object[] { null, AddressQualityStatus.Empty },
                new object[] { string.Empty, AddressQualityStatus.Empty },
                new object[] { " ", AddressQualityStatus.Empty },
                new object[] { "  ", AddressQualityStatus.Empty },
                new object[] { " \t ", AddressQualityStatus.Empty },
                new object[] { " \n\r \r\n \t ", AddressQualityStatus.Empty },
            };

        public static IEnumerable<object[]> DateKeyValueData =>
            new List<object[]>
            {
                new object[] { "Jan 1 2000", AddressQualityStatus.SeemsToBeADate },
                new object[] { "1 Jan 2000", AddressQualityStatus.SeemsToBeADate },
                new object[] { "1/1/2000", AddressQualityStatus.SeemsToBeADate },
                new object[] { "1\\1\\2000", AddressQualityStatus.SeemsToBeADate },
            };

        public static IEnumerable<object[]> OtherJunkKeyValueData =>
            new List<object[]>
            {
                new object[] { "1", AddressQualityStatus.AllNumeric },
                new object[] { "UnKnown", AddressQualityStatus.KnownErroneous },
                new object[] { "?", AddressQualityStatus.KnownErroneous },
                new object[] { "? ?", AddressQualityStatus.KnownErroneous },
                new object[] { " ??? ", AddressQualityStatus.KnownErroneous },
                new object[] { ",.", AddressQualityStatus.KnownErroneous },
            };

        public static IEnumerable<object[]> GoodLocationsKeyValueData =>
            new List<object[]>
            {
                new object[] { "aplacename" },
                new object[] { "a-placename" },
                new object[] { "a-place-name" },
                new object[] { "a-four-part-name" },
                new object[] { "another four part name" },
            };

        [Theory]
        [MemberData(nameof(WhitespaceKeyValueData))]
        public void Whitespace_is_detected_in_locations(string source, AddressQualityStatus expected)
        {
            var status = addressQualityChecker.StatusGuessFromSourceQuality(source);

            Assert.Equal(expected, status);
        }

        [Theory]
        [MemberData(nameof(DateKeyValueData))]
        public void Dates_are_detected_in_locations(string source, AddressQualityStatus expected)
        {
            var status = addressQualityChecker.StatusGuessFromSourceQuality(source);

            Assert.Equal(expected, status);
        }

        [Theory]
        [MemberData(nameof(GoodLocationsKeyValueData))]
        public void Place_names_are_parsed_as_ok(string source)
        {
            var status = addressQualityChecker.StatusGuessFromSourceQuality(source);

            Assert.Equal(AddressQualityStatus.OK, status);
        }

        [Theory]
        [MemberData(nameof(OtherJunkKeyValueData))]
        public void Locations_are_filtered_of_known_junk_formats(string source, AddressQualityStatus expected)
        {
            var status = addressQualityChecker.StatusGuessFromSourceQuality(source);

            Assert.Equal(expected, status);
        }
    }
}
