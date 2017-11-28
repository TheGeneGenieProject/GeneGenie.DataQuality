// <copyright file="BirthdateRangeFinder.cs" company="GeneGenie.com">
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

namespace GeneGenie.ResearchTools
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using GeneGenie.ResearchTools.Models;

    /// <summary>
    /// Used to find a range of possible birth dates given a known age and date.
    /// </summary>
    public class BirthdateRangeFinder
    {
        /// <summary>
        /// Calculates a range of possible birthdates given a list of known ages and dates.
        /// </summary>
        /// <param name="knownAges">A list of ages at specific points in time.</param>
        /// <returns>A <see cref="BirthdateRange"/> instance representing a possible birth
        /// date range.
        /// </returns>
        public BirthdateRange CalculateBirthdateRange(List<AgeAtPointInTime> knownAges)
        {
            var birthDateRanges = new List<BirthdateRange>();
            foreach (var knownAge in knownAges)
            {
                birthDateRanges.Add(CalculateBirthdateRange(knownAge));
            }

            if (!birthDateRanges.Any())
            {
                birthDateRanges.Add(new BirthdateRange
                {
                    Earliest = DateTime.MinValue,
                    Latest = DateTime.MaxValue
                });
            }

            return new BirthdateRange()
            {
                Earliest = birthDateRanges.Min(b => b.Earliest),
                Latest = birthDateRanges.Max(b => b.Latest)
            };
        }

        /// <summary>
        /// Given a persons' age at a specific date returns the earliest and latest
        /// possible birthdates for them.
        /// </summary>
        /// <param name="knownAge">A reported date and age for a person.</param>
        /// <returns>A range of dates that the person could have been born on.</returns>
        public BirthdateRange CalculateBirthdateRange(AgeAtPointInTime knownAge)
        {
            return new BirthdateRange
            {
                Earliest = knownAge.Date.AddYears(-(knownAge.Age + 1)).AddDays(1),
                Latest = knownAge.Date.AddYears(-knownAge.Age)
            };
        }
    }
}
