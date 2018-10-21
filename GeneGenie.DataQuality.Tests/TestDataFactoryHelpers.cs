// <copyright file="TestDataFactoryHelpers.cs" company="GeneGenie.com">
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

namespace GeneGenie.DataQuality.Tests
{
    using System;
    using GeneGenie.DataQuality.Models;
    using GeneGenie.DataQuality.Tests.Models;

    /// <summary>
    /// Helper functions to create test data objects.
    /// </summary>
    internal static class TestDataFactoryHelpers
    {
        /// <summary>
        /// Create an instance of <see cref="ExpectedResultForAgeAtPointInTime"/> for use in testing birth date calculations.
        /// </summary>
        /// <param name="age">The age for the <see cref="AgeAtPointInTime"/> to be tested.</param>
        /// <param name="pointInTime">The date for the <see cref="AgeAtPointInTime"/> to be tested.</param>
        /// <param name="expected">The expected result to be compared against earliest or latest dare in range.</param>
        /// <returns>An instance of <see cref="ExpectedResultForAgeAtPointInTime"/> to be fed into a test method.</returns>
        internal static ExpectedResultForAgeAtPointInTime CreateExpectedAge(int age, DateTime pointInTime, DateTime expected)
        {
            return new ExpectedResultForAgeAtPointInTime
            {
                Expected = expected,
                KnownAge = new AgeAtPointInTime { Age = age, Date = pointInTime },
            };
        }
    }
}
