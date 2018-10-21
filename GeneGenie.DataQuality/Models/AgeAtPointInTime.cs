// <copyright file="AgeAtPointInTime.cs" company="GeneGenie.com">
// Copyright (c) GeneGenie.com. All Rights Reserved.
// Licensed under the GNU Affero General Public License v3.0. See LICENSE in the project root for license information.
// </copyright>
// <author> Copyright (C) 2017 Ryan O'Neill r@genegenie.com </author>

namespace GeneGenie.DataQuality.Models
{
    using System;

    /// <summary>
    /// Holds the age of a person at a specific point in time.
    /// </summary>
    public class AgeAtPointInTime
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AgeAtPointInTime"/> class.
        /// </summary>
        public AgeAtPointInTime()
        {
        }

        /// <summary>
        /// Gets or sets the persons' age as normally reported in a census or official document.
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Gets or sets the date that the person was known to be a specific age.
        /// </summary>
        public DateTime Date { get; set; }
    }
}
