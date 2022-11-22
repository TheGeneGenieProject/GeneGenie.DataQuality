// <copyright file="AddressQualityChecker.cs" company="GeneGenie.com">
// Copyright (c) GeneGenie.com. All Rights Reserved.
// Licensed under the GNU Affero General Public License v3.0. See LICENSE in the project root for license information.
// </copyright>

namespace GeneGenie.DataQuality
{
    using Models;

    /// <summary>
    /// Class for checking the quality of input data to avoid expensive lookups via
    /// external services. Used primarily for address fields but can be extended
    /// to look for other data.
    /// </summary>
    public static class AddressQualityChecker
    {
        private static readonly List<string> KnownJunkLocations = new List<string>
        {
            "unknown",
            "?",
        };

        /// <summary>
        /// Makes a guess as to the quality of the passed data.
        /// </summary>
        /// <param name="source">The string data to check for quality.</param>
        /// <returns>An enumeration from <see cref="AddressQualityStatus"/> indicating
        /// how useful or junky the passed text was.
        /// </returns>
        public static AddressQualityStatus StatusGuessFromSourceQuality(string source)
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

            var date = DateParser.Parse(source);
            if (date.DateFrom != null || date.DateTo != null)
            {
                return AddressQualityStatus.SeemsToBeADate;
            }

            return AddressQualityStatus.OK;
        }
    }
}
