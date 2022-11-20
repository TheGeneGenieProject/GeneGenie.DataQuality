// <copyright file="BirthdateRange.cs" company="GeneGenie.com">
// Copyright (c) GeneGenie.com. All Rights Reserved.
// Licensed under the GNU Affero General Public License v3.0. See LICENSE in the project root for license information.
// </copyright>

namespace GeneGenie.DataQuality.Models
{
    using System;

    /// <summary>
    /// Stores the earliest and latest possible dates for a birthdate based on a persons' age.
    /// </summary>
    public class BirthdateRange
    {
        /// <summary>
        /// Gets or sets the earliest possible date that the person could have been born calculated from their age.
        /// </summary>
        public DateTime Earliest { get; set; }

        /// <summary>
        /// Gets or sets the latest possible date that the person could have been born calculated from their age.
        /// </summary>
        public DateTime Latest { get; set; }
    }
}
