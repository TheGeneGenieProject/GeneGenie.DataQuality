// <copyright file="BirthdateRangeFinder.cs" company="GeneGenie.com">
// Copyright (c) GeneGenie.com. All Rights Reserved.
// Licensed under the GNU Affero General Public License v3.0. See LICENSE in the project root for license information.
// </copyright>

namespace GeneGenie.DataQuality
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Models;

    /// <summary>
    /// Used to find a range of possible birth dates given a known age and date.
    /// </summary>
    public static class BirthdateRangeFinder
    {
        /// <summary>
        /// Calculates a range of possible birthdates given a list of known ages and dates.
        /// </summary>
        /// <param name="knownAges">A list of ages at specific points in time.</param>
        /// <returns>A <see cref="BirthdateRange"/> instance representing a possible birth
        /// date range.
        /// </returns>
        public static BirthdateRange CalculateBirthdateRange(List<AgeAtPointInTime> knownAges)
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
                    Latest = DateTime.MaxValue,
                });
            }

            return new BirthdateRange
            {
                Earliest = birthDateRanges.Min(b => b.Earliest),
                Latest = birthDateRanges.Max(b => b.Latest),
            };
        }

        /// <summary>
        /// Given a persons' age at a specific date returns the earliest and latest
        /// possible birthdates for them.
        /// </summary>
        /// <param name="knownAge">A reported date and age for a person.</param>
        /// <returns>A range of dates that the person could have been born on.</returns>
        public static BirthdateRange CalculateBirthdateRange(AgeAtPointInTime knownAge)
        {
            return new BirthdateRange
            {
                Earliest = knownAge.Date.AddYears(-(knownAge.Age + 1)).AddDays(1),
                Latest = knownAge.Date.AddYears(-knownAge.Age),
            };
        }
    }
}
