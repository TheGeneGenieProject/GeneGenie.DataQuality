// <copyright file="ExpectedResultForAgeAtPointInTime.cs" company="GeneGenie.com">
// Copyright (c) GeneGenie.com. All Rights Reserved.
// Licensed under the GNU Affero General Public License v3.0. See LICENSE in the project root for license information.
// </copyright>

namespace GeneGenie.DataQuality.Tests.Models
{
    using System;
    using DataQuality.Models;

    /// <summary>
    /// Used to create objects to hold test data for the expected outcome
    /// of a test against <see cref="BirthdateRangeFinder"/>.
    /// </summary>
    public class ExpectedResultForAgeAtPointInTime
    {
        /// <summary>
        /// Instantiates an <see cref="ExpectedResultForAgeAtPointInTime"/> for testing the output of <see cref="BirthdateRangeFinder.CalculateBirthdateRange(AgeAtPointInTime)"/>.
        /// </summary>
        /// <param name="expected">Expected earliest date for the person to be the age passed.</param>
        /// <param name="knownAge">The age for a person at a point in time.</param>
        public ExpectedResultForAgeAtPointInTime(DateTime expected, AgeAtPointInTime knownAge)
        {
            Expected = expected;
            KnownAge = knownAge;
        }

        /// <summary>
        /// Gets the expected result (up to the caller to compare against earliest or latest date in range).
        /// </summary>
        public DateTime Expected { get; init; }

        /// <summary>
        /// Gets the age and point in time at which the person was that age for the range calculation.
        /// </summary>
        public AgeAtPointInTime KnownAge { get; init; }
    }
}
