// <copyright file="ExpectedResultForAgeAtPointInTime.cs" company="GeneGenie.com">
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

namespace GeneGenie.DataQuality.Tests.Models
{
    using System;
    using GeneGenie.DataQuality.Models;

    /// <summary>
    /// Used to create objects to hold test data for the expected outcome
    /// of a test against <see cref="BirthdateRangeFinder"/>.
    /// </summary>
    public class ExpectedResultForAgeAtPointInTime
    {
        /// <summary>
        /// Gets or sets the expected result (up to the caller to compare against earliest or latest date in range).
        /// </summary>
        public DateTime Expected { get; set; }

        /// <summary>
        /// Gets or sets the age and point in time at which the person was that age for the range calculation.
        /// </summary>
        public AgeAtPointInTime KnownAge { get; set; }
    }
}
