// <copyright file="BirthdateRange.cs" company="GeneGenie.com">
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU Affero General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Affero General Public License for more details.
//
// You should have received a copy of the GNU Affero General Public License
// along with this program. If not, see http:www.gnu.org/licenses/ .
//
// </copyright>
// <author> Copyright (C) 2017 Ryan O'Neill r@genegenie.com </author>

namespace GeneGenie.ResearchTools.Models
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
