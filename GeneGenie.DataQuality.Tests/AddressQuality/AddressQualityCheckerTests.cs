﻿// <copyright file="AddressQualityCheckerTests.cs" company="GeneGenie.com">
// Copyright (c) GeneGenie.com. All Rights Reserved.
// Licensed under the GNU Affero General Public License v3.0. See LICENSE in the project root for license information.
// </copyright>

namespace GeneGenie.DataQuality.Tests.AddressQuality
{
    using DataQuality.Models;

    /// <summary>
    /// Tests to ensure that we don't do an expensive lookup on known junk addresses.
    /// The address is parsed to see if it is any of this type of data and a
    /// quality status is returned.
    /// </summary>
    public class AddressQualityCheckerTests
    {
        /// <summary>
        /// Gets test data for checking the output of <see cref="AddressQualityChecker"/>.
        /// </summary>
        public static IEnumerable<object[]> AddressQualityData =>
            new List<object[]>
            {
                new object[] { string.Empty, AddressQualityStatus.Empty },
                new object[] { " ", AddressQualityStatus.Empty },
                new object[] { "  ", AddressQualityStatus.Empty },
                new object[] { " \t ", AddressQualityStatus.Empty },
                new object[] { " \n\r \r\n \t ", AddressQualityStatus.Empty },

                new object[] { "Jan 1 2000", AddressQualityStatus.SeemsToBeADate },
                new object[] { "1 Jan 2000", AddressQualityStatus.SeemsToBeADate },
                new object[] { "1/1/2000", AddressQualityStatus.SeemsToBeADate },
                new object[] { "1\\1\\2000", AddressQualityStatus.SeemsToBeADate },

                new object[] { "1", AddressQualityStatus.AllNumeric },
                new object[] { "UnKnown", AddressQualityStatus.KnownErroneous },
                new object[] { "?", AddressQualityStatus.KnownErroneous },
                new object[] { "? ?", AddressQualityStatus.KnownErroneous },
                new object[] { " ??? ", AddressQualityStatus.KnownErroneous },
                new object[] { ",.", AddressQualityStatus.KnownErroneous },

                new object[] { "aplacename", AddressQualityStatus.OK },
                new object[] { "a-placename", AddressQualityStatus.OK },
                new object[] { "a-place-name", AddressQualityStatus.OK },
                new object[] { "a-four-part-name", AddressQualityStatus.OK },
                new object[] { "another four part name", AddressQualityStatus.OK },
            };

        /// <summary>
        /// Asserts that <see cref="AddressQualityChecker"/> handles known bad / good data.
        /// </summary>
        /// <param name="source">The text to validate.</param>
        /// <param name="expected">The expected quality status of the text after validating.</param>
        [Theory]
        [MemberData(nameof(AddressQualityData))]
        public void Address_quality_can_be_calculated_correctly(string source, AddressQualityStatus expected)
        {
            var status = AddressQualityChecker.StatusGuessFromSourceQuality(source);

            Assert.Equal(expected, status);
        }
    }
}
