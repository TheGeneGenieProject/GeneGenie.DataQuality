﻿// <copyright file="DateRange.cs" company="GeneGenie.com">
// Copyright (c) GeneGenie.com. All Rights Reserved.
// Licensed under the GNU Affero General Public License v3.0. See LICENSE in the project root for license information.
// </copyright>

namespace GeneGenie.DataQuality.Models
{
    using System;

    public record DateRange
    {
        public DateRange(string source)
        {
            Source = source;
        }

        // Accuracy:
        // Type: NotSet, Guess, needs attention.

        // TODO: Is DateTime the correct type here? DateTimeOffset? These are historical dates, we won't always know the timezone or location.
        // TODO: When going to the client in a view model, these should not be nullable.
        public DateTime? DateFrom { get; set; }

        public DateTime? DateTo { get; set; }

        // TODO: Ensure guessed source formats cannot be used until set to specific format.
        public DateFormat SourceFormat { get; set; }

        /// <summary>
        /// Holds a copy of the data that the user entered for parsing.
        /// </summary>
        public string Source { get; init; }

        /// <summary>
        /// Gets or sets an indicator for the quality of the source data.
        /// </summary>
        public DateQualityStatus Status { get; set; }
    }
}
