// <copyright file="DateQualityStatus.cs" company="GeneGenie.com">
// Copyright (c) GeneGenie.com. All Rights Reserved.
// Licensed under the GNU Affero General Public License v3.0. See LICENSE in the project root for license information.
// </copyright>

namespace GeneGenie.DataQuality.Models
{
    /// <summary>
    /// Indicates the quality of the data in a date field which is used to figure out
    /// if we should ask the user to correct it.
    /// </summary>
    public enum DateQualityStatus
    {
        /// <summary>
        /// An invalid state, the date quality should never be equal to this.
        /// Used to ensure that a default instance is explicitly set before use.
        /// </summary>
        NotSet = 0,

        /// <summary>
        /// Data exists in the field but cannot be parsed as a valid date.
        /// </summary>
        NotValid = 1,

        /// <summary>
        /// The date field is empty or just consists of whitespace.
        /// </summary>
        Empty = 2,

        /// <summary>
        /// The text was split into parts using standard date delimiters and there
        /// were more than 3 parts found. Only up to 3 parts are allowed for day, month and year.
        /// </summary>
        TooManyDateParts = 3,

        /// <summary>
        /// Date was successfully parsed into a date range.
        /// </summary>
        OK = 4,

        /// <summary>
        /// A single month was parsed which cannot be used as a date.
        /// </summary>
        OnlyMonthIsPresent = 5,

        /// <summary>
        /// Although 3 parts of the date have been found, none of them look like a valid year.
        /// </summary>
        ThreePartsWithoutYear = 5,

        /// <summary>
        /// Although 3 parts of the date have been found, the year was in the middle which is not a sensible format and we can't use it.
        /// </summary>
        YearInMiddle = 6,

        /// <summary>
        /// Although 3 parts of the date have been found, the month and the day could not be determined because they are ambiguous.
        /// </summary>
        MonthIsAmbiguous = 7,
    }
}
