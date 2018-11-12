// <copyright file="AddressQualityChecker.cs" company="GeneGenie.com">
// Copyright (c) GeneGenie.com. All Rights Reserved.
// Licensed under the GNU Affero General Public License v3.0. See LICENSE in the project root for license information.
// </copyright>

namespace GeneGenie.DataQuality
{
    using System.Collections.Generic;
    using System.Linq;
    using GeneGenie.DataQuality.Models;

    public class AddressQualityChecker
    {
        private static readonly List<string> KnownJunkLocations = new List<string>
        {
            "unknown",
            "?",
        };

        private readonly DateParser dateParser;

        public AddressQualityChecker(DateParser dateParser)
        {
            this.dateParser = dateParser;
        }

        public AddressQualityStatus StatusGuessFromSourceQuality(string source)
        {
            if (string.IsNullOrWhiteSpace(source))
            {
                return AddressQualityStatus.Empty;
            }

            var cleaned = source.Replace(" ", string.Empty).ToLower();

            if (KnownJunkLocations.Contains(cleaned))
            {
                return AddressQualityStatus.KnownErroneous;
            }

            if (cleaned.All(char.IsDigit))
            {
                return AddressQualityStatus.AllNumeric;
            }

            if (cleaned.All(char.IsPunctuation))
            {
                return AddressQualityStatus.KnownErroneous;
            }

            var date = dateParser.Parse(source);
            if (date.DateFrom != null || date.DateTo != null)
            {
                return AddressQualityStatus.SeemsToBeADate;
            }

            return AddressQualityStatus.OK;
        }
    }
}
