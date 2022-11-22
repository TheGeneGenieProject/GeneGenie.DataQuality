// <copyright file="TestDataFactoryHelpers.cs" company="GeneGenie.com">
// Copyright (c) GeneGenie.com. All Rights Reserved.
// Licensed under the GNU Affero General Public License v3.0. See LICENSE in the project root for license information.
// </copyright>

namespace GeneGenie.DataQuality.Tests
{
    using DataQuality.Models;
    using Models;

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
            return new ExpectedResultForAgeAtPointInTime(
                expected,
                new AgeAtPointInTime { Age = age, Date = pointInTime });
        }
    }
}
