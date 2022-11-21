// <copyright file="DateFormat.cs" company="GeneGenie.com">
// Copyright (c) GeneGenie.com. All Rights Reserved.
// Licensed under the GNU Affero General Public License v3.0. See LICENSE in the project root for license information.
// </copyright>

namespace GeneGenie.DataQuality.Models
{
    // TODO: Can probably merge with GeneGenie.Gedcom formats.
    /// <summary>
    /// When parsed a date will be in one of these formats.
    /// </summary>
    public enum DateFormat
    {
        /// <summary>This date has not been parsed yet.</summary>
        NotSet = 0,

        /// <summary>
        /// Date was found to be of the format;
        ///     day month year
        /// where day is a maximum of 2 digits, month is a maximum of 2 digits and year is always 4 digits.
        ///
        /// For example, 25 12 1939 for 25th of Dec 1939.
        /// </summary>
        Dd_mm_yyyy = 1,

        /// <summary>
        /// Date was found to be of the format;
        ///     month day year
        /// where month is a maximum of 2 digits, day is a maximum of 2 digits and year is always 4 digits.
        ///
        /// For example, 12 25 1939 for 25th of Dec 1939.
        /// </summary>
        Mm_dd_yyyy = 2,

        /// <summary>
        /// Date was found to be of the format;
        ///     year month day
        /// where year is always 4 digits, month is a maximum of 2 digits and day is a maximum of 2 digits.
        ///
        /// For example, 1939 12 25 for 25th of Dec 1939.
        /// </summary>
        Yyyy_mm_dd = 3,

        /// <summary>
        /// Date was found to be of the format;
        ///     year day month
        /// where year is always 4 digits, day is a maximum of 2 digits and month is a maximum of 2 digits.
        ///
        /// For example, 1939 25 12 for 25th of Dec 1939.
        /// </summary>
        Yyyy_dd_mm = 4,

        /// <summary>
        /// Could be either <see cref="Dd_mm_yyyy"/> or <see cref="Mm_dd_yyyy"/> depending on the
        /// surrounding records.
        /// </summary>
        UnsureStartingWithDateOrMonth = 5,

        /// <summary>
        /// Could be either <see cref="Yyyy_mm_dd"/> or <see cref="Yyyy_dd_mm"/> depending on the
        /// surrounding records.
        /// </summary>
        UnsureEndingWithDateOrMonth = 5,

        /// <summary>
        /// The date input was parsed as a 4 digit year.
        /// </summary>
        Yyyy = 6,
        
        /// <summary>
        /// The date input was parsed as a 4 digit year followed by 1 or 2 digits for the month.
        /// </summary>
        Yyyy_mm = 7,
        
        /// <summary>
        /// The date input was parsed as a one or two digit month followed by a 4 digit year.
        /// </summary>
        Mm_yyyy = 8,

        /// <summary>
        /// The input could not be parsed into one of the date formats. It's probably junk
        /// as I've been a bit excessive about trying to catch all variations.
        /// </summary>
        UnableToParse = 9,
        
        Mmm = 10,
        Mmm_dd = 11,
        Dd_mmm = 12,
        Yyyy_mmm = 13,
        Mmm_yyyy = 14,
        Yyyy_mmm_dd = 15,
        Yyyy_dd_mmm = 16,
        Mmm_dd_yyyy = 17,
        Dd_mmm_yyyy = 18,
        Mm = 19,
        Dd_mm = 20,
        Mm_dd = 21,

        UnableToParseAsYearInMiddle = 22,
    }
}
