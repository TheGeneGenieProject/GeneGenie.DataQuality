// <copyright file="DateFormat.cs" company="GeneGenie.com">
// Copyright (c) GeneGenie.com. All Rights Reserved.
// Licensed under the GNU Affero General Public License v3.0. See LICENSE in the project root for license information.
// </copyright>

namespace GeneGenie.DataQuality.Models
{
    /// <summary>
    /// When parsed a date will be in one of these formats.
    /// Possibly this and GeneGenie.Gedcom formats could be merged.
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
        UnsureEndingWithDateOrMonth = 6,

        /// <summary>
        /// The date input was parsed as a 4 digit year.
        /// </summary>
        Yyyy = 7,
        
        /// <summary>
        /// The date input was parsed as a 4 digit year followed by 1 or 2 digits for the month.
        /// </summary>
        Yyyy_mm = 8,
        
        /// <summary>
        /// The date input was parsed as a one or two digit month followed by a 4 digit year.
        /// </summary>
        Mm_yyyy = 9,

        /// <summary>
        /// The input could not be parsed into one of the date formats. It's probably junk
        /// as I've been a bit excessive about trying to catch all variations.
        /// </summary>
        UnableToParse = 10,
        
        /// <summary>
        /// The input was parsed as an English month name. Either in full or the first 3 characters.
        ///
        /// <example>
        ///     Jan or January
        /// </example>
        /// </summary>
        Mmm = 11,
        
        /// <summary>
        /// The input was parsed as an English month name followed by the day of the month as 1 or 2 digits.
        ///
        /// The month was either in full or the first 3 characters.
        ///
        /// <example>
        ///     Jan 12
        /// or
        ///     January 12
        /// </example>
        /// </summary>
        Mmm_dd = 12,

        /// <summary>
        /// The input was parsed as the day of the month (1 or 2 digits) followed by an
        /// English month (3 characters or in full).
        ///
        /// <example>
        ///     12 Jan
        /// or
        ///     12 January
        /// </example>
        /// </summary>
        Dd_mmm = 13,

        /// <summary>
        /// The input was parsed as a 4 digit year followed by an English month name (3 characters or in full).
        ///
        /// <example>
        ///     1939 Jan
        /// or
        ///     1939 January
        /// </example>
        /// </summary>
        Yyyy_mmm = 14,

        /// <summary>
        /// The input was parsed as an English month name (3 characters or in full)
        /// followed by a 4 digit year.
        ///
        /// <example>
        ///     1939 Jan
        /// or
        ///     1939 January
        /// </example>
        /// </summary>
        Mmm_yyyy = 15,

        /// <summary>
        /// The input was parsed as a 4 digit year followed by an English month name (3 characters or in full)
        /// and finally a 1 or 2 digit day of the month.
        ///
        /// <example>
        ///     1939 Jan 1
        /// or
        ///     1939 January 1
        /// </example>
        /// </summary>
        Yyyy_mmm_dd = 16,

        /// <summary>
        /// The input was parsed as a 4 digit year followed by a 1 or 2 digit day of the month
        /// and finally an English month name (3 characters or in full).
        ///
        /// <example>
        ///     1939 1 Jan
        /// or
        ///     1939 1 January
        /// </example>
        /// </summary>
        Yyyy_dd_mmm = 17,

        /// <summary>
        /// The input was parsed as an English month name (3 characters or in full)
        /// followed by a 1 or 2 digit day of the month and finally a 4 digit year.
        ///
        /// <example>
        ///     Jan 1 1939
        /// or
        ///     January 2 1939
        /// </example>
        /// </summary>
        Mmm_dd_yyyy = 18,

        /// <summary>
        /// The input was parsed as a 1 or 2 digit day of the month
        /// followed by an English month name (3 characters or in full)
        /// and finally a 4 digit year.
        ///
        /// <example>
        ///     1 Jan 1939
        /// or
        ///     1 January 1939
        /// </example>
        /// </summary>
        Dd_mmm_yyyy = 19,

        /// <summary>
        /// The input was parsed as a 1 or 2 digit day of the month.
        ///
        /// <example>
        ///     1
        /// or
        ///     01
        /// </example>
        /// </summary>
        Mm = 20,

        /// <summary>
        /// Date was found to be of the format;
        ///     day month
        /// where day is a maximum of 2 digits and month is a maximum of 2 digits.
        ///
        /// For example, 25 12 for 25th of Dec.
        /// </summary>
        Dd_mm = 21,

        /// <summary>
        /// Date was found to be of the format;
        ///     month day
        /// where month is a maximum of 2 digits and day is a maximum of 2 digits.
        ///
        /// For example, 12 25 for 25th of Dec.
        /// </summary>
        Mm_dd = 22,

        /// <summary>
        /// The input was parsed and we found a year in the middle which is not
        /// a format that I've seen in the wild. For now, we give up and the user
        /// must fix their data. If this format turns up more than a little then
        /// I'll consider handling it.
        /// <example>
        ///     27 1939 9
        /// or
        ///     27 1939 Sep
        /// </example>
        /// </summary>
        UnableToParseAsYearInMiddle = 23,
    }
}
