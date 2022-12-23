// <copyright file="DateRange.cs" company="GeneGenie.com">
// Copyright (c) GeneGenie.com. All Rights Reserved.
// Licensed under the GNU Affero General Public License v3.0. See LICENSE in the project root for license information.
// </copyright>

namespace GeneGenie.DataQuality.Models
{
    /// <summary>
    /// Holds a from and to date derived from a parsed text date string.
    /// </summary>
    /// <param name="Source"></param>
    public record DateRange(string Source)
    {
        /// <summary>
        /// The start date parsed from the source text. The time is left as 00:00 which
        /// is the start of the day so the whole day will be covered.
        /// </summary>
        public DateTime? DateFrom { get; set; }

        /// <summary>
        /// The end date of parsed out of the date range. Includes a time set to just
        /// before midnight on the close of that day so that the whole day is covered.
        /// </summary>
        public DateTime? DateTo { get; set; }

        /// <summary>
        /// After parsing the data in <see cref="Source"/> this holds the
        /// format that the code detected the data was in.
        /// </summary>
        public DateFormat SourceFormat { get; set; }

        /// <summary>
        /// Holds a copy of the data that the user entered for parsing.
        /// </summary>
        public string Source { get; init; } = Source;

        /// <summary>
        /// Gets or sets an indicator for the quality of the source data.
        /// </summary>
        public DateQualityStatus Status { get; set; }
    }
}
