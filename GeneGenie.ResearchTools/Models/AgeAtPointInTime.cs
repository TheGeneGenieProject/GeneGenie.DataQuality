// <copyright file="AgeAtPointInTime.cs" company="GeneGenie.com">
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
