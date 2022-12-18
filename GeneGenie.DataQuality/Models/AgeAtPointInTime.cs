// <copyright file="AgeAtPointInTime.cs" company="GeneGenie.com">
// Copyright (c) GeneGenie.com. All Rights Reserved.
// Licensed under the GNU Affero General Public License v3.0. See LICENSE in the project root for license information.
// </copyright>

namespace GeneGenie.DataQuality.Models
{
    /// <summary>
    /// Holds the age of a person at a specific point in time.
    /// </summary>
    /// <param name="Age">Gets or sets the persons' age as normally reported in a census or official document.</param>
    /// <param name="Date">Gets or sets the date that the person was known to be a specific age.</param>
    public record AgeAtPointInTime(int Age, DateTime Date)
    {
    }
}
